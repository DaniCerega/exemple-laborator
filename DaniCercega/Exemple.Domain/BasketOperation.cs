using Exemple.Domain.Models;
using static LanguageExt.Prelude;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Exemple.Domain.Models.Basket;
using static Exemple.Domain.Models.Product;
using System.Threading.Tasks;

namespace Exemple.Domain
{
    public static class BasketOperation
    {
        public static Task<IBasket> ValidateBasket(Func<OrderID, TryAsync<bool>> checkProductExists, UnvalidatedBasket basket) =>
            basket.ProductList
                      .Select(ValidateProduct(checkProductExists))
                      .Aggregate(CreateEmptyValidatedBasket().ToAsync(), ReduceValidProducts)
                      .MatchAsync(
                            Right: validatedProducts => new ValidatedBasket(validatedProducts),
                            LeftAsync: errorMessage => Task.FromResult((IBasket)new InvalidBasket(errorMessage))
                      );


        private static Func<UnvalidatedProduct, EitherAsync<string, ValidatedProduct>> ValidateProduct(Func<OrderID, TryAsync<bool>> checkProductExists) =>
            unvalidatedProduct => ValidateProduct(checkProductExists, unvalidatedProduct);

        private static EitherAsync<string, ValidatedProduct> ValidateProduct(Func<OrderID, TryAsync<bool>> checkProductExists, UnvalidatedProduct unvalidatedProduct) =>
            from product in TryParseProduct(unvalidatedProduct.code, unvalidatedProduct.quantity) 
                         .ToEitherAsync(() => $"code: {unvalidatedProduct.code}, quantity: {unvalidatedProduct.quantity})")
            from orderID in OrderID.TryParse(unvalidatedProduct.orderId)
                                   .ToEitherAsync(() => $"Invalid order ID ({unvalidatedProduct.orderId})")
            select new ValidatedProduct(orderID,product);

        private static Either<string, List<ValidatedProduct>> CreateEmptyValidatedBasket() =>
            Right(new List<ValidatedProduct>());

        private static EitherAsync<string, List<ValidatedProduct>> ReduceValidProducts(EitherAsync<string, List<ValidatedProduct>> acc, EitherAsync<string, ValidatedProduct> next) =>
            from list in acc
            from nextProduct in next
            select list.AppendValidProduct(nextProduct);

        private static List<ValidatedProduct> AppendValidProduct(this List<ValidatedProduct> list, ValidatedProduct validProduct)
        {
            list.Add(validProduct);
            return list;
        }

        public static IBasket PayBasket(IBasket basket) => basket.Match(
            whenInvalidBasket: invalidBasket => invalidBasket,
            whenUnvalidatedBasket: unvalidatedBasket => unvalidatedBasket,
            whenPaidBasket: paidBasket => paidBasket,
            whenValidatedBasket: validatedBasket => {
                int totalPrice = 0;
                foreach(ValidatedProduct product in validatedBasket.ProductList) {
                    int price = ProductsDb.getPrice(product.product.Code).Match(
                        Succ: price => price,
                        Fail: exception => 0
                    );
                    totalPrice += (price * product.product.Quantity);
                }
                return new PaidBasket(validatedBasket.ProductList, totalPrice, DateTime.Now);
            }
        );
    }
}
