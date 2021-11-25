using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Domain.Models
{
    [AsChoice]
    public static partial class BasketPaidEvent
    {
        public interface IBasketPaidEvent { }

        public record BasketPaidSucceededEvent : IBasketPaidEvent 
        {
            public int Price {
                get;
            }
            public DateTime PaidDate {
                get;
            }

            internal BasketPaidSucceededEvent(int price, DateTime paidDate) {
                Price = price;
                PaidDate = paidDate;
            }
        }

        public record BasketPaidFailedEvent : IBasketPaidEvent
        {
            public string Reason { get; }

            internal BasketPaidFailedEvent(string reason)
            {
                Reason = reason;
            }
        }

        
    }
}
