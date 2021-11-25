using LanguageExt;
using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Exemple.Domain.Models
{
    public record OrderID
    {
        private static readonly Regex ValidPattern = new("^PN[0-9]{5}$");

        public string Value { get; }

        private OrderID(string value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidOrderIdException("");
            }
        }

        private static bool IsValid(string stringValue) => ValidPattern.IsMatch(stringValue);

        public override string ToString()
        {
            return Value;
        }

        public static Option<OrderID> TryParse(string stringValue)
        {
            if (IsValid(stringValue))
            {
                return Some<OrderID>(new(stringValue));
            }
            else
            {
                return None;
            }
        }

        public string toString()
        {
            return Value;
        }

    }
}
