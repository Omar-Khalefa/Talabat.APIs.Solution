using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecs
{
	public class ProductWithBrandAndCategorySpecifications :BaseSpecifications<Product>
	{
        // this Constructor Will Be Used For Creating An Object , That Will Be used To get All Products
        public ProductWithBrandAndCategorySpecifications(string sort)
            : base()
		{
			Includes.Add(p => p.brand);
			Includes.Add(p => p.Category);

			if (!string.IsNullOrEmpty(sort))
			{
				switch (sort)
				{
					case "priceAsc":
						//OrderBy = p => p.Price;
						AddOrderBy(p => p.Price);
						break;
					case "priceDesc":
						//OrderByDesc = p => p.Price;
						AddOrderByDesc(p => p.Price);
						break;
					default:
						AddOrderBy(p => p.Name);
						break;
				}
			}
			else
			{
				AddOrderBy(p => p.Name);
			}
		}
		// this Constructor Will Be Used For Creating An Object , That Will Be used To get Product By Id
		public ProductWithBrandAndCategorySpecifications( int id)
            : base(p=>p.Id == id)
        {
			Includes.Add(p => p.brand);
			Includes.Add(p => p.Category);
		}
	


    }
}
