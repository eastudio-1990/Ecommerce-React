using E_Commerce_API.Model;
using E_Commerce_API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    

     


namespace E_Commerce_API.Business
{
    public class Shipping
    {
        private readonly ECommerceDB _db;
        public Shipping(ECommerceDB db)
        {
            _db = db;
        }
        public string Insert(vm_Shipping vm)
        {
            using var trans = _db.Database.BeginTransaction();
                 
            ShippingModel shipping = new ShippingModel();
            shipping.Id = vm.Id;            
            shipping.MaxPrice = vm.MaxPrice;
            shipping.MinPrice = vm.MinPrice;
            shipping.MinWeight = vm.MinWeight;
            shipping.MaxWeight = vm.MaxWeight;
            shipping.ZoneId = vm.ZoneId;
            shipping.Price = vm.Price;
            shipping.Date = vm.Date;
            try
            {
                _db.Add(shipping); ;
                _db.SaveChanges();
                vm.Log.Comment = "ثبت ارسال جدید :" + CreateComment(0, vm);
                vm.Log.RefId = vm.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.Shipping;
                vm.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(vm.Log);
                _db.SaveChanges();
                trans.Commit();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return ex.Message;
            }
        }
        public string Update(vm_Shipping vm)
        {
            ShippingModel shipping = _db.shippings.Find(vm.Id);
            shipping.Id = vm.Id;
            shipping.MaxPrice = vm.MaxPrice;            
            shipping.MinWeight =vm.MinWeight;            
            shipping.Price = vm.Price;
            shipping.MinPrice = vm.MinPrice;
            shipping.MinWeight = vm.MinWeight;
            shipping.ZoneId = vm.ZoneId;
            shipping.Date = vm.Date;
            try
            {
                _db.Add(shipping); ;
                vm.Log.Comment = "ویرایش ارسال :" + CreateComment(vm.Id, vm);
                vm.Log.RefId = vm.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.Shipping;
                vm.Log.Operation = Control.Enumuration.Operation.Update;
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Delete(vm_Shipping vm)
        {
            ShippingModel shipping = new ShippingModel();
            shipping.Id = vm.Id;
            _db.Entry(shipping).State = EntityState.Deleted;
            try
            {
                vm.Log.Comment = "حذف ارسال :" + CreateComment(vm.Id, vm);
                vm.Log.RefId = vm.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.Shipping;
                vm.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(vm.Log);
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public vm_Shipping GetById(int Id)
        {
            vm_Shipping result = (from Shipping in _db.shippings
                              where
                              Shipping.Id.Equals(Id)
                              select new vm_Shipping
                              {
                                  Id = Shipping.Id,
                                  ZoneId=Shipping.ZoneId,
                                  Price=Shipping.Price,
                                  MinPrice=Shipping.MinPrice,
                                  MinWeight=Shipping.MinWeight,
                                  MaxPrice=Shipping.MaxPrice,
                                  MaxWeight=Shipping.MaxWeight,
                                  Date=Shipping.Date
                                  
                              }).SingleOrDefault();
            return result;
        }
        public List<vm_Shipping> GetAll() 
        {
            List<vm_Shipping> result = (from Shipping in _db.shippings
                                    select new vm_Shipping
                                    {
                                        Id = Shipping.Id,
                                        ZoneId = Shipping.ZoneId,
                                        Date=Shipping.Date,
                                        Price = Shipping.Price,
                                        MinPrice = Shipping.MinPrice,
                                        MinWeight = Shipping.MinWeight,
                                        MaxPrice = Shipping.MaxPrice,
                                        MaxWeight = Shipping.MaxWeight
                                    }).ToList();
            return result;
        }
        public string CreateComment(int Id, vm_Shipping vm_Shipping)
        {
            string Comment = "";
            if (Id == 0)
            {
                Comment = "حمل:" + vm_Shipping.Id;
            }
            else
            {
                vm_Shipping = GetById(Id);
                Comment = "حمل :" + vm_Shipping.Id;
            }
            return Comment;
        }
        public vm_Shipping GetDefault()
        {
            vm_Shipping result = (from Shipping in _db.shippings
                              where
                              Shipping.Id.Equals(1)
                              select new vm_Shipping
                              {
                                  Id = Shipping.Id,
                                  ZoneId = Shipping.ZoneId,
                                  Date=Shipping.Date,
                                  Price = Shipping.Price,
                                  MinPrice = Shipping.MinPrice,
                                  MinWeight = Shipping.MinWeight,
                                  MaxPrice = Shipping.MaxPrice,
                                  MaxWeight = Shipping.MaxWeight
                                  
                              }).SingleOrDefault();
            return result;
        }
    }
}
