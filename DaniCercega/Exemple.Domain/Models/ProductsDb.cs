using System;
using System.Collections.Generic;
using LanguageExt;
using LanguageExt.Common;

namespace Exemple.Domain.Models
{
	class ProductsDb
	{
		static Dictionary<int, int> products = new Dictionary<int, int> {
			{ 1, 9 },
			{ 2, 18 },
			{ 3, 4 }
		};

		public static Try<bool> productExists(int productCode) => () => {
			if (productCode < 0)
			{
				throw new Exception("Negative product code");
			}
			return products.ContainsKey(productCode);
		};

		public static Try<int> getPrice(int productCode) => () =>
		{
			if (!products.ContainsKey(productCode))
			{
				throw new NotImplementedException("Found no product with code: " + productCode);
			}
			int price;
			products.TryGetValue(productCode, out price);
			return price;
		};
	}
}
