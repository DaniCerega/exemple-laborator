using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Exemple.Domain.Models.Basket;

namespace Exemple.Domain.Models
{
    public record PayBasketCommand
    {
        public PayBasketCommand(IReadOnlyCollection<UnvalidatedProduct> inputBasket)
        {
            InputBasket = inputBasket;
        }

        public IReadOnlyCollection<UnvalidatedProduct> InputBasket { get; }
    }
}
