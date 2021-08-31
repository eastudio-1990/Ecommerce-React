using E_Commerce_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Business
{
    public class ProductVariety
    {
        private readonly ECommerceDB _db;
        public ProductVariety(ECommerceDB db)
        {
            _db = db;
        }
        public string Insert(ViewModel.vm_ProductVariety productvariety)
        {
            using var trans = _db.Database.BeginTransaction();

            ProductVarietyModel model = new ProductVarietyModel();
            model.Id = Guid.NewGuid();
            model.ProductId = productvariety.ProductId;
            model.VarietyId = productvariety.VarietyId;
            model.Order = productvariety.Order;
            try
            {
                _db.ProductVarieties.Add(model);


                productvariety.Log.Comment = "تعیین تنوع محصول :" + CreateComment(Guid.Empty, productvariety);
                productvariety.Log.RefId = model.Id.ToString();
                productvariety.Log.Type = Control.Enumuration.Type.ProductVariety;
                productvariety.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(productvariety.Log);
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

        public string Update(ViewModel.vm_ProductVariety productvariety)
        {
            using var trans = _db.Database.BeginTransaction();

            ProductVarietyModel model = _db.ProductVarieties.Find(productvariety.Id);

            model.ProductId = productvariety.ProductId;
            model.VarietyId = productvariety.VarietyId;
            model.Order = productvariety.Order;
            try
            {
                _db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;


                productvariety.Log.Comment = "ویرایش تنوع یک محصول :" + CreateComment(model.Id, productvariety);
                productvariety.Log.RefId = model.Id.ToString();
                productvariety.Log.Type = Control.Enumuration.Type.ProductVariety;
                productvariety.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(_db);
                log.Insert(productvariety.Log);
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

        public string Delete(ViewModel.vm_ProductVariety productvariety)
        {
            using var trans = _db.Database.BeginTransaction();

            ProductVarietyModel model = _db.ProductVarieties.Find(productvariety.Id);

            model.ProductId = productvariety.ProductId;
            model.VarietyId = productvariety.VarietyId;
            try
            {
                _db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;


                productvariety.Log.Comment = "حذف تنوع یک محصول :" + CreateComment(model.Id, productvariety);
                productvariety.Log.RefId = model.Id.ToString();
                productvariety.Log.Type = Control.Enumuration.Type.ProductVariety;
                productvariety.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(productvariety.Log);
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

        public ViewModel.vm_ProductVariety GetById(Guid Id)
        {
            ViewModel.vm_ProductVariety result = (from productvariety in _db.ProductVarieties
                                                  join
                                                  variety in _db.varieties
                                                  on
                                                  productvariety.VarietyId equals variety.Id
                                                  join
                                                  product in _db.products
                                                  on
                                                  productvariety.ProductId equals product.Id
                                                  where
                                                  productvariety.Id.Equals(Id)
                                                  select new ViewModel.vm_ProductVariety
                                                  {
                                                      Id = productvariety.Id,
                                                      ProductId = productvariety.ProductId,
                                                      VarietyId = productvariety.VarietyId,
                                                      ProductTitle = product.Title,
                                                      VarietyTitle = variety.Title,
                                                      Order = productvariety.Order

                                                  }).SingleOrDefault();
            return result;
        }

        public List<ViewModel.vm_ProductVariety> GetByProductId(Guid ProductId)
        {
            List<ViewModel.vm_ProductVariety> result = (from productvariety in
                                                            _db.ProductVarieties
                                                        join
                                                        variety in _db.varieties
                                                        on
                                                        productvariety.VarietyId equals variety.Id
                                                        where
                                                        productvariety.ProductId.Equals(ProductId)
                                                        select new ViewModel.vm_ProductVariety
                                                        {
                                                            Id = productvariety.Id,
                                                            ProductId = productvariety.ProductId,
                                                            VarietyId = productvariety.VarietyId,
                                                            VarietyTitle = variety.Title,
                                                            Order=productvariety.Order
                                                        }).OrderBy(x => x.Order).ToList();
            return result;
        }
        public string CreateComment(Guid Id, ViewModel.vm_ProductVariety variety)
        {
            Business.Product pr = new Product(_db);
            Business.Variety va = new Variety(_db);

            string Comment = "";
            if (Id == Guid.Empty)
            {
                Comment = "تعیین دسته بندی محصول" + pr.GetById(variety.ProductId).Title + " - " + va.GetById(variety.VarietyId).Title;
            }
            else
            {
                ViewModel.vm_ProductVariety myVar = GetById(Id);
                Comment = "تعیین دسته بندی محصول" + myVar.ProductTitle + " - " + myVar.VarietyTitle;
            }
            return Comment;
        }
    }
}
