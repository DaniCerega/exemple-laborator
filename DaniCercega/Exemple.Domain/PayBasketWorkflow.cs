using Exemple.Domain.Models;
using static Exemple.Domain.Models.BasketPaidEvent;
using static Exemple.Domain.BasketOperation;
using System;
using static Exemple.Domain.Models.Basket;
using LanguageExt;
using System.Threading.Tasks;

namespace Exemple.Domain
{
    public class PayBasketWorkflow
    {
        public async Task<IBasketPaidEvent> ExecuteAsync(PayBasketCommand command, Func<OrderID, TryAsync<bool>> checkProductExists)
        {
            UnvalidatedBasket unvalidatedBasket = new UnvalidatedBasket(command.InputBasket);
            IBasket products = await ValidateBasket(checkProductExists, unvalidatedBasket);
            products = PayBasket(products);

            return products.Match(
                    whenInvalidBasket: invalidBasket => new BasketPaidFailedEvent(invalidBasket.Reason) as IBasketPaidEvent,
                    whenUnvalidatedBasket: unvalidatedBasket => new BasketPaidFailedEvent("Unexpected unvalidated state") as IBasketPaidEvent,
                    whenValidatedBasket: validatedBasket => new BasketPaidFailedEvent("Unexpected validated state") as IBasketPaidEvent,
                    whenPaidBasket: PaidBasket => new BasketPaidSucceededEvent(PaidBasket.Price, PaidBasket.PaymentDate) as IBasketPaidEvent
                );
        }
    }
}
