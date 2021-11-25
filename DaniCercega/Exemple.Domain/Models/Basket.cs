using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Domain.Models
{
    [AsChoice]
    public static partial class Basket
    {
        public interface IBasket { }

        public record UnvalidatedBasket: IBasket
        {
            public UnvalidatedBasket(IReadOnlyCollection<UnvalidatedProduct> productList)
            {
                ProductList = productList;
            }

            public IReadOnlyCollection<UnvalidatedProduct> ProductList { get; }
        }

        public record InvalidBasket: IBasket
        {
            internal InvalidBasket(string reason)
            {
                Reason = reason;
            }

            public string Reason { get; }
        }

        public record ValidatedBasket: IBasket
        {
            internal ValidatedBasket(IReadOnlyCollection<ValidatedProduct> productList)
            {
                ProductList = productList;
            }

            public IReadOnlyCollection<ValidatedProduct> ProductList { get; }
        }

       

        public record PaidBasket : IBasket
        {
            internal PaidBasket(IReadOnlyCollection<ValidatedProduct> productList, int price, DateTime paymentDate)
            {
                ProductList = productList;
                PaymentDate = paymentDate;
                Price = price;
            }

            public IReadOnlyCollection<ValidatedProduct> ProductList { get; }
            public DateTime PaymentDate { get; }
            public int Price { get; }
        }
    }
}
