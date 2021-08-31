using E_Commerce_API.Model;
using E_Commerce_API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Business
{
    public class Product
    {
        private readonly ECommerceDB _db;
        public Product(ECommerceDB db)
        {
            _db = db;
        }


        public string Insert(vm_Product vm)
        {
            using var trans = _db.Database.BeginTransaction();
            ProductModel product = new ProductModel();
            product.CategoryId = vm.CategoryId;
            product.Code = vm.Code;
            product.Id = Guid.NewGuid();
            product.Title = vm.Title;
            product.Width = vm.Width;
            product.Weigth = vm.Weigth;
            product.Heigth = vm.Heigth;
            product.Length = vm.Length;
            product.HomeShow = vm.HomeShow;
            product.BuyCount = vm.BuyCount;
            product.HomeShow = vm.HomeShow;
            product.PublishState = 0;
            product.PackageWidth = vm.PackageWidth;
            product.PackageWeigth = vm.PackageWeigth;
            product.PackageLength = vm.PackageLength;
            product.PackageHeigth = vm.PackageHeigth;
            product.MaxBasketCount = vm.MaxBasketCount;
            product.IsDownloadable = vm.IsDownloadable;
            product.MaterialId = vm.MaterialId;
            product.WeightUnitId = vm.WeightUnitId;

            try
            {
                _db.products.Add(product);
                _db.SaveChanges();

                Business.Language lan = new Language(_db);
                ProductDetailModel Detail = new ProductDetailModel();
                Detail.Id = Guid.Empty;
                Detail.LanguageId = lan.GetDefault().Id;
                Detail.ProductId = product.Id;
                Detail.Title = vm.Title;
                _db.productDetails.Add(Detail);

                vm.Log.Comment = "ثبت محصول جدید ::" + CreateComment(Guid.Empty, vm);
                vm.Log.RefId = product.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.Product;
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
        public string Update(vm_Product vm)
        {
            using var trans = _db.Database.BeginTransaction();

            ProductModel product = _db.products.Find(vm.Id);
            product.Id = vm.Id;
            product.Code = vm.Code;
            product.Title = vm.Title;
            product.Width = vm.Width;
            product.Weigth = vm.Weigth;
            product.Heigth = vm.Heigth;
            product.Length = vm.Length;
            product.HomeShow = vm.HomeShow;
            product.BuyCount = vm.BuyCount;
            product.PublishDate = vm.PublishDate;
            product.PublishState = vm.PublishState;
            product.PackageWidth = vm.PackageWidth;
            product.PackageWeigth = vm.PackageWeigth;
            product.PackageLength = vm.PackageLength;
            product.PackageHeigth = vm.PackageHeigth;
            product.MaxBasketCount = vm.MaxBasketCount;
            product.IsDownloadable = vm.IsDownloadable;
            product.CategoryId = vm.CategoryId;
            product.WeightUnitId = vm.WeightUnitId;
            product.MaterialId = vm.MaterialId;
            try
            {
                _db.Entry(product).State = EntityState.Modified;

                Business.Language lan = new Language(_db);
                vm_Language language = lan.GetDefault();

                ProductDetailModel Detail = _db.productDetails.Where(x => x.ProductId.Equals(product.Id)
                 && x.LanguageId.Equals(language.Id)).SingleOrDefault();

                Detail.Title = vm.Title;

                _db.Entry(Detail).State = EntityState.Modified;

                vm.Log.Comment = "ویرایش محصول جدید ::" + CreateComment(vm.Id, vm);
                vm.Log.RefId = product.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.Product;
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
        public string Delete(vm_Product vm)
        {
            using var trans = _db.Database.BeginTransaction();

            ProductModel product = new ProductModel();
            product.Id = vm.Id;
            _db.Entry(product).State = EntityState.Deleted;
            try
            {
                //#region Log
                vm.Log.Comment = "حذف محصول " + CreateComment(vm.Id, vm);
                vm.Log.RefId = vm.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.Product;
                vm.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(vm.Log);
                //#end region
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
        public List<vm_Product> GetAll()
        {
            List<vm_Product> result = (from products in _db.products
                                       join
                                       Category in _db.categories
                                       on
                                       products.CategoryId equals Category.Id
                                       join
                                       material in _db.materials
                                       on
                                       products.MaterialId equals material.Id
                                       join
                                       weightUnit in _db.WeightUnits
                                       on
                                       products.WeightUnitId equals weightUnit.Id
                                       select new vm_Product
                                       {
                                           Id = products.Id,
                                           Code = products.Code,
                                           Title = products.Title,
                                           BuyCount = products.BuyCount,
                                           CategoryId = products.CategoryId,
                                           HomeShow = products.HomeShow,
                                           IsDownloadable = products.IsDownloadable,
                                           MaterialId = products.MaterialId,
                                           MaxBasketCount = products.MaxBasketCount,
                                           PublishDate = products.PublishDate,
                                           PublishState = products.PublishState,
                                           Length = products.Length,
                                           Heigth = products.Heigth,
                                           Width = products.Width,
                                           Weigth = products.Weigth,
                                           PackageHeigth = products.PackageHeigth,
                                           PackageLength = products.PackageLength,
                                           PackageWidth = products.PackageWidth,
                                           PackageWeigth = products.PackageWeigth,
                                           CategoryTitle = Category.Title,
                                           MaterialTitle = material.Title,
                                           WeightUnitTitle = weightUnit.Title,
                                           WeightUnitId = weightUnit.Id
                                       }).ToList();
            return result;
        }
        public vm_Product GetById(Guid Id)
        {
            vm_Product res = (from products in _db.products
                              join
                              category in _db.categories

                              on
                              products.CategoryId equals category.Id

                              where products.Id.Equals(Id)
                              select new vm_Product
                              {
                                  Code = products.Code,
                                  Id = products.Id,
                                  Title = products.Title,
                                  BuyCount = products.BuyCount,
                                  CategoryId = products.CategoryId,
                                  HomeShow = products.HomeShow,
                                  IsDownloadable = products.IsDownloadable,
                                  MaterialId = products.MaterialId,
                                  MaxBasketCount = products.MaxBasketCount,
                                  PublishDate = products.PublishDate,
                                  PublishState = products.PublishState,
                                  Heigth = products.Heigth,
                                  Width = products.Width,
                                  PackageHeigth = products.PackageHeigth,
                                  PackageLength = products.PackageLength,
                                  PackageWeigth = products.PackageWeigth,
                                  Length = products.Length,
                                  PackageWidth = products.PackageWidth,
                                  Weigth = products.Weigth,
                                  CategoryTitle = category.Title,
                                  WeightUnitId = products.WeightUnitId
                              }
                                ).SingleOrDefault();
            return res;
        }
        public string CreateComment(Guid Id, vm_Product vm)
        {
            string Comment = "";
            if (Id == Guid.Empty)
            {
                Comment = "جنس:" + vm.Title;
            }
            else
            {
                vm = GetById(Id);
                Comment = "جنس:" + vm.Title;
            }
            return Comment;
        }
        public string SavePublishState(vm_Product vm)
        {

            ProductModel product = _db.products.Find(vm.Id);
            product.PublishState = vm.PublishState;
            try
            {
                _db.Entry(product).State = EntityState.Modified;
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public List<vm_Product> Search(string search)
        {
            if (string.IsNullOrEmpty(search))
                search = "";

            List<vm_Product> result = (from products in _db.products
                                       join
                                       Category in _db.categories
                                       on
                                       products.CategoryId equals Category.Id
                                       join
                                       material in _db.materials
                                       on
                                       products.MaterialId equals material.Id
                                       join
                                       weightUnit in _db.WeightUnits
                                       on
                                       products.WeightUnitId equals weightUnit.Id

                                       where
                                         products.Title.Contains(search)
                                       ||
                                       products.Code.Contains(search)
                                       select new vm_Product
                                       {
                                           Id = products.Id,
                                           Code = products.Code,
                                           Title = products.Title,
                                           BuyCount = products.BuyCount,
                                           CategoryId = products.CategoryId,
                                           HomeShow = products.HomeShow,
                                           IsDownloadable = products.IsDownloadable,
                                           MaterialId = products.MaterialId,
                                           MaxBasketCount = products.MaxBasketCount,
                                           PublishDate = products.PublishDate,
                                           PublishState = products.PublishState,
                                           Length = products.Length,
                                           Heigth = products.Heigth,
                                           Width = products.Width,
                                           Weigth = products.Weigth,
                                           PackageHeigth = products.PackageHeigth,
                                           PackageLength = products.PackageLength,
                                           PackageWidth = products.PackageWidth,
                                           PackageWeigth = products.PackageWeigth,
                                           PublishStateComment = products.PublishState == 0 ? "انتشار نیافته" : "انتشار یافته",
                                           CategoryTitle = Category.Title,
                                           MaterialTitle = material.Title,
                                           WeightUnitTitle = weightUnit.Title,
                                           WeightUnitId = weightUnit.Id

                                       }).ToList();
            return result;
        }
        ///
        public string InsertDetail(vm_ProductDetail vm)
        {
            using var trans = _db.Database.BeginTransaction();
            ProductDetailModel Model = new ProductDetailModel();
            Model.Id = Guid.NewGuid();
            Model.Title = vm.Title;
            Model.Comment = vm.Comment;
            Model.DownloadUrl = vm.DownloadUrl;
            Model.LanguageId = vm.LanguageId;
            Model.ProductId = vm.ProductId;
            Model.MetaTagTitle = vm.MetaTagTitle;
            Model.MetaTagDescription = vm.MetaTagDescription;

            try
            {
                _db.productDetails.Add(Model);

                vm.Log.Comment = "ثبت کشور جدید :: " + CreateDetailComment(Guid.Empty, vm);
                vm.Log.RefId = vm.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.Product;
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
        public string UpdateDetail(vm_ProductDetail vm)
        {
            using var trans = _db.Database.BeginTransaction();

            ProductDetailModel model = _db.productDetails.Find(vm.Id);
            model.Title = vm.Title;
            model.LanguageId = vm.LanguageId;
            model.Comment = vm.Comment;
            model.DownloadUrl = vm.DownloadUrl;
            model.MetaTagTitle = vm.MetaTagTitle;
            model.MetaTagDescription = vm.MetaTagDescription;
            try
            {
                _db.Entry(model).State = EntityState.Modified;
                vm.Log.Comment = "ویرایش محصول  :: " + CreateDetailComment(vm.Id, vm);
                vm.Log.RefId = vm.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.Product;
                vm.Log.Operation = Control.Enumuration.Operation.Update;
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
        public string DeleteDetail(vm_ProductDetail vm)
        {
            using var trans = _db.Database.BeginTransaction();

            ProductDetailModel Model = new ProductDetailModel();
            Model.Id = vm.Id;
            _db.Entry(Model).State = EntityState.Deleted;
            try
            {
                vm.Log.Comment = "حذف محصول::" + CreateDetailComment(vm.Id, vm);
                vm.Log.RefId = vm.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.Product;
                vm.Log.Operation = Control.Enumuration.Operation.Delete;
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
        public string CreateDetailComment(Guid Id, vm_ProductDetail vm_detail)
        {
            string Comment = "";
            if (Id == Guid.Empty)
            {
                Comment = "محصول:" + vm_detail.Title;
            }
            else
            {
                vm_detail = GetDetailById(Id);
                Comment = "محصول :" + vm_detail.Title;
            }
            return Comment;
        }
        public vm_ProductDetail GetDetailById(Guid Id)
        {
            vm_ProductDetail result = (from productDetails in _db.productDetails
                                       where
                                       productDetails.Id.Equals(Id)
                                       select new vm_ProductDetail
                                       {

                                           Id = productDetails.Id,
                                           Title = productDetails.Title,
                                           Comment = productDetails.Comment,
                                           DownloadUrl = productDetails.DownloadUrl,
                                           LanguageId = productDetails.LanguageId,
                                           ProductId = productDetails.ProductId,
                                           MetaTagDescription = productDetails.MetaTagDescription,
                                           MetaTagTitle = productDetails.MetaTagTitle
                                       }).SingleOrDefault();
            return result;
        }
        public List<vm_ProductDetail> GetDetailByProductId(Guid ProductId)
        {
            List<vm_ProductDetail> result = (from ProductDetail in _db.productDetails
                                             join
                                             language in _db.Languages
                                             on
                                             ProductDetail.LanguageId equals language.Id
                                             where
                                             ProductDetail.ProductId.Equals(ProductId)
                                             select new vm_ProductDetail
                                             {
                                                 Id = ProductDetail.Id,
                                                 Title = ProductDetail.Title,
                                                 Comment = ProductDetail.Comment,
                                                 DownloadUrl = ProductDetail.DownloadUrl,
                                                 LanguageId = ProductDetail.LanguageId,
                                                 ProductId = ProductDetail.ProductId,
                                                 LanguageTitle = language.Title,
                                                 MetaTagDescription = ProductDetail.MetaTagDescription,
                                                 MetaTagTitle = ProductDetail.MetaTagTitle

                                             }

                                     ).ToList();
            return result;
        }
    }
}
