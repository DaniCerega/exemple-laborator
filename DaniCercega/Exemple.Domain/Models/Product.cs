using LanguageExt;
using System;
using static LanguageExt.Prelude;

namespace Exemple.Domain.Models
{
    public record Product
    {
        public int Quantity{get;}

        public int Code {get;}

        public Product(int code,int quantity)
        {
            if (IsValid(code,quantity))
            {
                Quantity = quantity;
            }
            else
            {
                throw new InvalidProduct($"{quantity} is an invalid quantity value.");
            }
        }

        
        public static Option<Product> TryParseProduct(string codeString, string quantityString) {
            if (int.TryParse(codeString, out int numericCode) && int.TryParse(quantityString, out int numericQuantity)) 
            {
                if (IsValid(numericCode, numericQuantity)) {
                    return Some<Product>(new(numericCode, numericQuantity));
                }
            }
            return None;
        }

        private static bool IsValid(int code, int quantity) {
            // product must exist in the database
            // quantity must be at least one and maximum 99
            return ProductsDb.productExists(code).Match(
                Succ: exists => exists && quantity > 0,
                Fail: exception => false
            );
        }
   
        public override string ToString() {
            return $"{Quantity} products with code {Code}";
        }
    }
}
