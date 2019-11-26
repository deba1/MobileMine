using eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Repository
{
    public class PurchaseHistoryRepository : Repository<PurchaseHistory>
    {
        public PurchaseHistoryRepository(MobileMineContext context)
            : base(context)
        {

        }
    }
}