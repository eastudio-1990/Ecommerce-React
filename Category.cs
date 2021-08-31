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
    public class Category
    {
        private readonly ECommerceDB _db;
        public Category(ECommerceDB db)
        {
            _db = db;
        }
        public string Insert(vm_Category vm_category)
        {
            using var trans = _db.Database.BeginTransaction();

            CategoryModel category = new CategoryModel();
            category.Id = vm_category.Id;
            category.Title = vm_category.Title;
            category.ParentId = vm_category.ParentId;
            category.Comment = vm_category.Comment;
            try
            {
                _db.Add(category);
                _db.SaveChanges();


                CategoryDetailModel detailModel = new CategoryDetailModel();

                Business.Language lan = new Language(_db);

                detailModel.LanguageId = lan.GetDefault().Id;
                detailModel.CatId = category.Id;
                detailModel.Title = category.Title;
                detailModel.HomeShow = 0;

                _db.categoryDetails.Add(detailModel);

                vm_category.Log.Comment = "ثبت دسته بندی جدید :" + CreateComment(0, vm_category);
                vm_category.Log.RefId = category.Id.ToString();
                vm_category.Log.Type = Control.Enumuration.Type.Category;
                vm_category.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(vm_category.Log);
                _db.SaveChanges();
                trans.Commit();

                if (vm_category.file != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "Content\\Category", category.Id + ".png");
                    using (Stream stream = new FileStream(path, FileMode.Create))
                    {
                        vm_category.file.CopyTo(stream);
                    }
                    return ("Ok");
                }
              

                return Control.Constant.SuccessResult;

            }
            catch (Exception ex)
            {

                trans.Rollback();
                return ex.Message;
            }
        }
        public string Update(vm_Category vm_category)
        {
            using var trans = _db.Database.BeginTransaction();

            CategoryModel category = _db.categories.Find(vm_category.Id);
            category.Title = vm_category.Title;
            category.Comment = vm_category.Comment;
            try
            {
                _db.Entry(category).State = EntityState.Modified;

                Business.Language lan = new Language(_db);

                vm_Language language = lan.GetDefault();

                CategoryDetailModel detailModel = _db.categoryDetails
                    .Where(x => x.CatId.Equals(category.Id)
                    && x.LanguageId.Equals(language.Id)).SingleOrDefault();


                detailModel.Title = category.Title;
                _db.Entry(detailModel).State = EntityState.Modified;

                vm_category.Log.Comment = "ویرایش دسته بندی :" + CreateComment(vm_category.Id, vm_category);
                vm_category.Log.RefId = category.Id.ToString();
                vm_category.Log.Type = Control.Enumuration.Type.Category;
                vm_category.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(_db);
                log.Insert(vm_category.Log);
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
        public string Delete(vm_Category vm_Category)
        {
            CategoryModel category = new CategoryModel();
            category.Id = vm_Category.Id;
            _db.Entry(category).State = EntityState.Deleted;
            try
            {
                vm_Category.Log.Comment = "حذف دسته بندی " + CreateComment(vm_Category.Id, vm_Category);
                vm_Category.Log.RefId = category.Id.ToString();
                vm_Category.Log.Type = Control.Enumuration.Type.Category;
                vm_Category.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log lo = new Log(_db);
                lo.Insert(vm_Category.Log);
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public vm_Category GetById(int id)
        {
            vm_Category result = (from Category in _db.categories
                                  where
                                  Category.Id.Equals(id)
                                  select new vm_Category
                                  {
                                      Id = Category.Id,
                                      Title = Category.Title,
                                      Comment = Category.Comment
                                  }).SingleOrDefault();
            return result;
        }
        public List<vm_Category> GetAll()
        {
            List<vm_Category> res = (from Category in _db.categories
                                     select new vm_Category
                                     {
                                         Id = Category.Id,
                                         Title = Category.Title,
                                         ParentId = Category.ParentId,
                                         Comment = Category.Comment
                                     }).ToList();
            return res;
        }
        public string CreateComment(int Id, vm_Category vm_Category)
        {
            string Comment = "";
            if (Id == 0)
            {
                Comment = "دسته بندی" + vm_Category.Title;
            }
            else
            {
                vm_Category = GetById(Id);
                Comment = "دسته بندی :" + vm_Category.Title;
            }
            return Comment;
        }
        public List<ViewModel.vm_Category> GetByParentId(int ParentId)
        {
            List<ViewModel.vm_Category> result = (from category in _db.categories
                                                  where
                                                  category.ParentId.Equals(ParentId)
                                                  select new ViewModel.vm_Category
                                                  {
                                                      Id = category.Id,
                                                      ParentId = category.ParentId,
                                                      Title = category.Title
                                                  }).ToList();
            foreach (var item in result)
            {
                if (GetByParentId(item.Id).Count > 0)
                    item.HasChild = true;
                else
                    item.HasChild = false;

            }
            return result;
        }



        ////////////////////////
        public string InsertDetail(vm_CategoryDetail vm_Detail)
        {
            CategoryDetailModel Detail = new CategoryDetailModel();
            Detail.Title = vm_Detail.Title;
            Detail.CatId = vm_Detail.CatId;
            Detail.Comment = vm_Detail.Comment;
            Detail.Order = vm_Detail.Order;
            Detail.LanguageId = vm_Detail.LanguageId;
            Detail.HomeShow = vm_Detail.HomeShow;
            try
            {
                _db.Add(Detail);
                vm_Detail.Log.Comment = "ثبت دسته بندی جدید :" + CreateCommentDetail(0, vm_Detail);
                vm_Detail.Log.RefId = vm_Detail.Id.ToString();
                vm_Detail.Log.Type = Control.Enumuration.Type.Category;
                vm_Detail.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(vm_Detail.Log);
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdateDetail(vm_CategoryDetail vm_Detail)
        {
            CategoryDetailModel Detail = _db.categoryDetails.Find(vm_Detail.Id);
            Detail.Title = vm_Detail.Title;
            Detail.CatId = vm_Detail.CatId;
            Detail.Comment = vm_Detail.Comment;
            Detail.Order = vm_Detail.Order;
            Detail.LanguageId = vm_Detail.LanguageId;
            Detail.HomeShow = vm_Detail.HomeShow;

            try
            {
                _db.Entry(Detail).State = EntityState.Modified;
                vm_Detail.Log.Comment = "ویرایش دسته بندی جدید :" + CreateCommentDetail(vm_Detail.Id, vm_Detail);
                vm_Detail.Log.RefId = vm_Detail.Id.ToString();
                vm_Detail.Log.Type = Control.Enumuration.Type.Category;
                vm_Detail.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(vm_Detail.Log);
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string DeleteDetail(vm_CategoryDetail categoryDetail)
        {
            CategoryDetailModel category = new CategoryDetailModel();
            category.Id = categoryDetail.Id;
            _db.Entry(category).State = EntityState.Deleted;
            try
            {
                categoryDetail.Log.Comment = "حذف دسته بندی ::" + CreateCommentDetail(categoryDetail.Id, categoryDetail);
                categoryDetail.Log.RefId = categoryDetail.Id.ToString();
                categoryDetail.Log.Type = Control.Enumuration.Type.Country;
                categoryDetail.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(categoryDetail.Log);
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public vm_CategoryDetail GetDetailById(int Id)
        {
            vm_CategoryDetail result = (from CategoryDetailModel in _db.categoryDetails
                                        where CategoryDetailModel.Id.Equals(Id)
                                        select new vm_CategoryDetail
                                        {
                                            Id = CategoryDetailModel.Id,
                                            CatId = CategoryDetailModel.CatId,
                                            Comment = CategoryDetailModel.Comment,
                                            LanguageId = CategoryDetailModel.LanguageId,
                                            Order = CategoryDetailModel.Order,
                                            Title = CategoryDetailModel.Title,
                                            HomeShow = CategoryDetailModel.HomeShow

                                        }).SingleOrDefault();
            return result;
        }
        public List<vm_CategoryDetail> GetDetailAll(int CategoryId)
        {
            List<vm_CategoryDetail> result = (from CategoryDetailModel in _db.categoryDetails
                                              join
                                              language in _db.Languages
                                              on
                                              CategoryDetailModel.LanguageId equals language.Id
                                              where
                                              CategoryDetailModel.CatId.Equals(CategoryId)
                                              select new vm_CategoryDetail
                                              {
                                                  Id = CategoryDetailModel.Id,
                                                  CatId = CategoryDetailModel.CatId,
                                                  Comment = CategoryDetailModel.Comment,
                                                  LanguageId = CategoryDetailModel.LanguageId,
                                                  Order = CategoryDetailModel.Order,
                                                  Title = CategoryDetailModel.Title,
                                                  HomeShow = CategoryDetailModel.HomeShow
                                              })
                                            .ToList();
            return result;
        }
        public string CreateCommentDetail(int Id, vm_CategoryDetail vm_Categorydetail)
        {
            string Comment = "";
            if (Id == 0)
            {
                Comment = "دسته بندی:" + vm_Categorydetail.Title;
            }
            else
            {
                vm_Categorydetail = GetDetailById(Id);
                Comment = "دسته بندی:" + vm_Categorydetail.Title;
            }
            return Comment;

        }



    }
}
