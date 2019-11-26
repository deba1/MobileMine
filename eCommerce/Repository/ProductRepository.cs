using eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Repository
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(MobileMineContext context)
            : base(context)
        {

        }
    }
}