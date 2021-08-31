using E_Commerce_API.Model;
using E_Commerce_API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Business
{
    public class ProductPhoto
    {
        private readonly ECommerceDB _db;
        public ProductPhoto(ECommerceDB db)
        {
            _db = db;
        }
        public string Insert(vm_ProductPhoto vm)
        {
            using var trans = _db.Database.BeginTransaction();
            ProductPhotoModel photo = new ProductPhotoModel();
            photo.Id = Guid.NewGuid();
            photo.Order = vm.Order;
            photo.ProductId = vm.ProductId;
            try
            {
                _db.productPhotos.Add(photo);
                _db.SaveChanges();

                vm.Log.Comment = "ثبت عکس جدید :" + CreateComment(Guid.Empty, vm);
                vm.Log.RefId = photo.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.ProductPhoto;
                vm.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                _db.SaveChanges();
                trans.Commit();

                if (vm.file != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "Content\\ProductPhoto", photo.Id + ".png");
                    using (Stream stream = new FileStream(path, FileMode.Create))
                    {
                        vm.file.CopyTo(stream);
                    }
                }
                return (Control.Constant.SuccessResult);
            }

            catch (Exception ex)
            {
                trans.Rollback();
                return ex.Message;
            }
        }
        public string Update(vm_ProductPhoto vm)
        {
            using var trans = _db.Database.BeginTransaction();
            ProductPhotoModel photo = _db.productPhotos.Find(vm.Id);
            photo.Order = vm.Order;
            photo.ProductId = vm.ProductId;
            try
            {
                _db.Entry(photo).State = EntityState.Modified;
                vm.Log.Comment = "ویرایش عکس محصول ::" + CreateComment(vm.Id, vm);
                vm.Log.RefId = photo.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.ProductPhoto;
                vm.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(_db);
                log.Insert(vm.Log);
                _db.SaveChanges();
                trans.Commit();
                if (vm.file != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "Content\\ProductPhoto", photo.Id + ".png");
                    using (Stream stream = new FileStream(path, FileMode.Create))
                    {
                        vm.file.CopyTo(stream);
                    }
                }
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return ex.Message;

            }

        }
        public string Delete(vm_ProductPhoto vm)
        {
            using var trans = _db.Database.BeginTransaction();

            ProductPhotoModel photo = _db.productPhotos.Find(vm.Id);
            _db.Entry(photo).State = EntityState.Deleted;
            try
            {
                vm.Log.Comment = "حذف عکس محصول ::" + CreateComment(vm.Id, vm);
                vm.Log.RefId = photo.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.ProductPhoto;
                vm.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                _db.SaveChanges();
                trans.Commit();
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Content\\ProductPhoto", photo.Id + ".png");
                System.IO.File.Delete(path);
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return ex.Message;
            }
        }
        public string CreateComment(Guid id, vm_ProductPhoto vm)
        {
            string Comment = "";
            if (id == Guid.Empty)
            {
                Comment = "عکس محصول::";
            }
            else
            {
                vm = GetById(id);
                Comment = "عکس محصول:";
            }
            return Comment;
        }
        public vm_ProductPhoto GetById(Guid id)
        {
            vm_ProductPhoto result = (from ProductPhoto in _db.productPhotos
                                      where
                                      ProductPhoto.Id.Equals(id)
                                      select new vm_ProductPhoto
                                      {
                                          Id = ProductPhoto.Id,
                                          Order = ProductPhoto.Order,
                                          ProductId = ProductPhoto.ProductId,
                                      }).SingleOrDefault();
            return result;
        }
        public List<vm_ProductPhoto> GetByProductId(Guid productid)
        {
            List<vm_ProductPhoto> result = (from ProductPhoto in _db.productPhotos
                                            join
                                            Product in _db.products
                                            on
                                            ProductPhoto.ProductId equals Product.Id
                                            where
                                            ProductPhoto.ProductId.Equals(productid)
                                            select new vm_ProductPhoto
                                            {
                                                Id = ProductPhoto.Id,
                                                Order = ProductPhoto.Order,
                                                ProductId = Product.Id,
                                            }
                                         ).OrderBy(x => x.Order).ToList();
            return result;
        }
    }



}
