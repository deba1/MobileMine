using eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Repository
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(MobileMineContext context)
            : base(context)
        {

        }
    }
}