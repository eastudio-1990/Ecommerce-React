using E_Commerce_API.Model;
using E_Commerce_API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Business
{
    public class ProductProperty
    {
        private readonly ECommerceDB _db;
        public ProductProperty(ECommerceDB db)
        {
            _db = db;
        }


        public string Insert(vm_ProductProperty vm)
        {
            using var trans = _db.Database.BeginTransaction();
            ProductPropertyModel model = new ProductPropertyModel();
            model.Id = Guid.NewGuid();
            model.Value = vm.Value;
            model.LanguageId = vm.LanguageId;
            model.CategoryPropId = vm.CategoryPropId;
            model.ProductId = vm.Productid;
            try
            {
                _db.Add(model);
                _db.SaveChanges();

                vm.Log.Comment = "ثبت خصوصیات محصول جدید ::" + CreateComment(Guid.Empty, vm);
                vm.Log.RefId = model.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.ProductProperty;
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
        public string Update(vm_ProductProperty vm)
        {
            using var trans = _db.Database.BeginTransaction();
            ProductPropertyModel model = _db.productProperties.Find(vm.Id);
            model.LanguageId = vm.LanguageId;
            model.Value = vm.Value;


            try
            {

                _db.Entry(model).State = EntityState.Modified;

                vm.Log.Comment = "ویرایش خصوصیات محصول ::" + CreateComment(vm.Id, vm);
                vm.Log.RefId = vm.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.ProductProperty;
                vm.Log.Operation = Control.Enumuration.Operation.Update;


                Business.Log log = new Log(_db);
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
        public string Delete(vm_ProductProperty vm)
        {
            ProductPropertyModel model = new ProductPropertyModel();
            model.Id = vm.Id;
            _db.Entry(model).State = EntityState.Deleted;
            try
            {
                vm.Log.Comment = "حذف خصوصیات محصول ::" + CreateComment(vm.Id, vm);
                vm.Log.RefId = vm.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.ProductProperty;
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
        public string CreateComment(Guid Id, vm_ProductProperty vm)
        {
            string Comment = "";
            if (Id == Guid.Empty)
            {
                Comment = " خصوصیات محصولات :" + vm.Value;
            }
            else
            {
                vm = GetById(Id);
                Comment = "خصوصیات محصولات :" + vm.Value;
            }
            return Comment;
        }
        public vm_ProductProperty GetById(Guid id)
        {
            vm_ProductProperty r = (from ProductProperty in _db.productProperties
                                    where ProductProperty.Id.Equals(id)
                                    select new vm_ProductProperty
                                    {
                                        Id = ProductProperty.Id,
                                        Value = ProductProperty.Value,
                                        LanguageId = ProductProperty.LanguageId,
                                        CategoryPropId = ProductProperty.CategoryPropId,
                                        Productid = ProductProperty.ProductId

                                    }).SingleOrDefault();
            return r;

        }
        public List<vm_ProductProperty> GetByProductId(Guid ProductId)
        {
            List<vm_ProductProperty> result = (from ProductProperty
                                          in _db.productProperties
                                          join
                                          language in _db.Languages
                                          on
                                          ProductProperty.LanguageId equals language.Id
                                          join 
                                          categoryProp in _db.categoryProps 
                                          on 
                                          ProductProperty.CategoryPropId equals categoryProp.Id
                                          where
                                          ProductProperty.ProductId.Equals(ProductId)
                                          select new vm_ProductProperty
                                          {
                                              Id = ProductProperty.Id,
                                              Value = ProductProperty.Value,
                                              CategoryPropId = ProductProperty.CategoryPropId,
                                              Productid = ProductProperty.ProductId,
                                              LanguageTitle = language.Title,
                                              CategoryPropTitle=categoryProp.Title

                                          }).ToList();
            return result;
        }
     
    }
}
