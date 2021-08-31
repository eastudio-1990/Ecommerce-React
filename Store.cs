
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Business
{
    public class Store
    {
        private readonly Model.ECommerceDB _db;
        public Store(Model.ECommerceDB db)
        {
            _db = db;
        }

        public ViewModel.vm_Store Get()
        {
            ViewModel.vm_Store result = (from store in _db.Stores
                                         select new ViewModel.vm_Store
                                         {
                                             Id = store.Id,
                                             Title = store.Title
                                         }).SingleOrDefault();
            return result;
        }



    }
}
