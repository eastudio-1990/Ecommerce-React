using E_Commerce_API.Model;
using E_Commerce_API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Business
{
    public class CategoryProp
    {
        private readonly ECommerceDB _db;
        public CategoryProp(ECommerceDB db)
        {
            _db = db;
        }
        public string Insert(vm_CategoryProp vm)
        {
            using var trans = _db.Database.BeginTransaction();
            CategoryPropModel categoryModel = new CategoryPropModel();
            categoryModel.Id = Guid.NewGuid();
            categoryModel.Title = vm.Title;
            categoryModel.CategoryId = vm.CategoryId;            
            try
            {
                _db.Add(categoryModel);
                _db.SaveChanges();
                CategoryPropDetailModel detail = new CategoryPropDetailModel();
                detail.CategoryPropId = categoryModel.Id;
                detail.Title = categoryModel.Title;
                Business.Language language= new Language(_db);
                detail.LanguageId = language.GetDefault().Id;
                _db.categoryPropDetails.Add(detail);
                vm.Log.Comment = "ثبت خصوصیات محصولات :" + CreateComment(Guid.Empty, vm);
                vm.Log.RefId = categoryModel.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.CategoryProp;
                vm.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(vm.Log);
                _db.SaveChanges();
                trans.Commit();
                return Control.Constant.SuccessResult;
            }
            catch(Exception ex)
            {
                trans.Rollback();
                return ex.Message;
            }
        }
        public string Update(vm_CategoryProp vm)
        {
            using var trans = _db.Database.BeginTransaction();

            CategoryPropModel Model = _db.categoryProps.Find(vm.Id);
            Model.Id = vm.Id;
            Model.Title = vm.Title;
            Model.CategoryId = vm.CategoryId;


            try
            {
                _db.Entry(Model).State = EntityState.Modified;
                Business.Language lang = new Language(_db);

                vm_Language language = lang.GetDefault();

                
                CategoryPropDetailModel Detail = _db.categoryPropDetails
                    .Where(x => x.CategoryPropId.Equals(vm.Id) && x.LanguageId.Equals(language.Id)).SingleOrDefault();
                Detail.Title = vm.Title;


                _db.Entry(Detail).State = EntityState.Modified;

                vm.Log.Comment = "ویرایش خصوصیات محصولات::" + CreateComment(vm.Id, vm);
                vm.Log.RefId = vm.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.CategoryProp;
                vm.Log.Operation = Control.Enumuration.Operation.Update;

                Business.Log log = new Log(_db);
                log.Insert(vm.Log);

                _db.SaveChanges();

                trans.Commit();

                return Control.Constant.SuccessResult;
            }

            catch(Exception ex)
            {
                trans.Rollback();

                return ex.Message;
            }
        }        
        public string Delete(vm_CategoryProp vm)
        {
            CategoryPropModel model = new CategoryPropModel();
            model.Id = vm.Id;
            _db.Entry(model).State = EntityState.Deleted;
            try
            {
                //#region Log              
                vm.Log.Comment = "حذف خصوصیات محصولات : " + CreateComment(vm.Id, vm);
                vm.Log.RefId = vm.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.CategoryProp;
                vm.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(vm.Log);
                //#endregion

                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public List<vm_CategoryProp> GetByCategoryId(int categoryid)
        {
            List<vm_CategoryProp> result = (from categoryProps in _db.categoryProps
                                            join 
                                            category in _db.categories
                                            on 
                                            categoryProps.CategoryId equals category.Id
                                            where
                                            categoryProps.CategoryId.Equals(categoryid)
                                            select new vm_CategoryProp
                                            {
                                                Id = categoryProps.Id,
                                                Title = categoryProps.Title,
                                                CategoryTitle=category.Title
                                            }
                                            ).ToList();
            return result;
        }
        public List<vm_CategoryProp> GetAll()
        {
            List<vm_CategoryProp> result = (from categoryProps in _db.categoryProps                                           
                                            join
                                            Category in _db.categories
                                            on
                                            categoryProps.CategoryId equals Category.Id                                      
                                            select new vm_CategoryProp
                                            {
                                                Id = categoryProps.Id,
                                                Title = categoryProps.Title,
                                                CategoryId=categoryProps.CategoryId,
                                                CategoryTitle=Category.Title
                                            }).ToList();
            return result;
        }
        public vm_CategoryProp GetById(Guid Id)
        {
            vm_CategoryProp result = (from CategoryProp in _db.categoryProps
                                      where CategoryProp.Id.Equals(Id)
                                      select new vm_CategoryProp
                                      {
                                          Id = CategoryProp.Id,
                                          Title = CategoryProp.Title
                                      }
                                    ).SingleOrDefault();
            return result;
        }
        public string CreateComment(Guid Id,vm_CategoryProp vm)
        {
            string Comment = "";
            if (Id==Guid.Empty)
            {
                Comment = "دسته بندی محصولات :" + vm.Title;
            }
            else
            {
                vm = GetById(Id);
                Comment = "دسته بندی محصولات :" + vm.Title;
            }
            return Comment;
        }
        /////
        public string InsertDetail(vm_CategoryPropDetail vm)
        {           
            CategoryPropDetailModel Model = new CategoryPropDetailModel();
            Model.Id = Guid.NewGuid();
            Model.Title = vm.Title;
            Model.CategoryPropId = vm.CategoryPropId;
            Model.LanguageId = vm.LanguageId;
            try
            {
                _db.Add(Model);
                vm.Log.Comment = "ثبت خصوصیات محصول جدید :" + CreateCommentDetail(Guid.Empty, vm);
                vm.Log.RefId = vm.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.CategoryProp;
                vm.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(vm.Log);
                _db.SaveChanges();

                return Control.Constant.SuccessResult;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }          
        public string UpdateDetail(vm_CategoryPropDetail vm)
        {
            CategoryPropDetailModel Model = _db.categoryPropDetails.Find(vm.Id);
            Model.Title = vm.Title;
            Model.CategoryPropId = vm.CategoryPropId;
            Model.Order = vm.Order;
            Model.IsForFilter = vm.IsForFilter;
            try
            {
                _db.Entry(Model).State = EntityState.Modified;
                vm.Log.Comment = "ویرایش خصوصیات محصول :" + CreateCommentDetail(vm.Id, vm);
                vm.Log.RefId = vm.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.CategoryProp;
                vm.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(_db);
                log.Insert(vm.Log);
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

        }
        public List<vm_CategoryPropDetail> GetAllDetail()
        {
            List<vm_CategoryPropDetail> result = (from categoryPropDetails in _db.categoryPropDetails                                                                                                
                                                  select new vm_CategoryPropDetail
                                                  {
                                                      Id = categoryPropDetails.Id,
                                                      Title = categoryPropDetails.Title,
                                                      IsForFilter = categoryPropDetails.IsForFilter,
                                                      CategoryPropId = categoryPropDetails.CategoryPropId,
                                                      LanguageId = categoryPropDetails.LanguageId,
                                                      Order = categoryPropDetails.Order

                                                  }
                                                ).ToList();
            return result;
        }
        public vm_CategoryPropDetail GetByIdDetail(Guid Id)
        {
            vm_CategoryPropDetail res = (from CategoryPropDetail in _db.categoryPropDetails
                                         where
                                         CategoryPropDetail.Id.Equals(Id)
                                         select new vm_CategoryPropDetail
                                         {
                                             Id = CategoryPropDetail.Id,
                                             Title = CategoryPropDetail.Title,
                                             IsForFilter = CategoryPropDetail.IsForFilter,
                                             Order = CategoryPropDetail.Order,
                                             CategoryPropId = CategoryPropDetail.CategoryPropId
                                         }).SingleOrDefault();
            return res;
        }

        public List<vm_CategoryPropDetail> GetDetailByCategoryProp(Guid CategoryPropId)
        {
            List< vm_CategoryPropDetail> res = (from CategoryPropDetail in _db.categoryPropDetails
                                                join 
                                                Language in _db.Languages 
                                                on 
                                                CategoryPropDetail.LanguageId equals Language.Id
                                         where
                                         CategoryPropDetail.CategoryPropId.Equals(CategoryPropId)
                                         select new vm_CategoryPropDetail
                                         {
                                             Id = CategoryPropDetail.Id,
                                             Title = CategoryPropDetail.Title,
                                             IsForFilter = CategoryPropDetail.IsForFilter,
                                             Order = CategoryPropDetail.Order,
                                             CategoryPropId = CategoryPropDetail.CategoryPropId,
                                             LanguageTitle=Language.Title
                                         }).ToList();
            return res;
        }
        public string CreateCommentDetail(Guid Id,vm_CategoryPropDetail vm)
        {
            string Comment = "";
            if (Id==Guid.Empty)
            {
                Comment = "دسته بندی محصولات :" + vm.Title;
            }
            else
            {
                vm = GetByIdDetail(Id);
                Comment = "دسته بندی محصولات :" + vm.Title;
            }
            return Comment;
        }
        public string DeleteDetail(vm_CategoryPropDetail vm)
        {
            CategoryPropDetailModel Model = new CategoryPropDetailModel();
            Model.Id = vm.Id;
            _db.Entry(Model).State = EntityState.Deleted;
            try
            {
                //#region Log              
                vm.Log.Comment = "حذف خصوصیات محصول : " + CreateCommentDetail(vm.Id, vm);
                vm.Log.RefId = vm.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.CategoryProp;
                vm.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(vm.Log);
                //#endregion

                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}

