using eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Repository
{
    public class BrandRepository : Repository<Brand>
    {
        public BrandRepository(MobileMineContext context) : base(context)
        {

        }
    }
}