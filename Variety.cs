using E_Commerce_API.Model;
using E_Commerce_API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Business
{
    public class Variety
    {
        private readonly ECommerceDB _db;
        public Variety(ECommerceDB db)
        {
            _db = db;
        }



        public string Insert(vm_Variety vm_variety)
        {
            using var trans = _db.Database.BeginTransaction();

            VarietyModel varietymodel = new VarietyModel();
            varietymodel.Id = vm_variety.Id;
            varietymodel.Title = vm_variety.Title;            

            try
            {
                _db.Add(varietymodel);
                _db.SaveChanges();

                VarietyDetailModel detail = new VarietyDetailModel();

                detail.VarietyId = varietymodel.Id;
                detail.Title = vm_variety.Title;

                _db.varietyDetails.Add(detail);

                //#region Log              
                vm_variety.Log.Comment = "ثبت خصوصیت جدید :: " + CreateComment(0, vm_variety);
                vm_variety.Log.RefId = varietymodel.Id.ToString();
                vm_variety.Log.Type = Control.Enumuration.Type.Variety;
                vm_variety.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(vm_variety.Log);
                //#endregion

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
        public string Update(vm_Variety vm)
        {
            using var trans = _db.Database.BeginTransaction();
            VarietyModel variety = _db.varieties.Find(vm.Id);         
            variety.Title = vm.Title;
            try
            {
                _db.Entry(variety).State =EntityState.Modified;
                VarietyDetailModel varietyDetail = _db.varietyDetails.Where(x => x.VarietyId.Equals(variety.Id)).SingleOrDefault();
                varietyDetail.Title = vm.Title;
                _db.Entry(varietyDetail).State = EntityState.Modified;
                //#region Log
                vm.Log.Comment = "ویرایش خصوصیت ::" + CreateComment(vm.Id, vm);
                vm.Log.RefId = vm.Id.ToString();
                vm.Log.Type = Control.Enumuration.Type.City;
                vm.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(_db);
                log.Insert(vm.Log);
                //#endregion
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
        public string Delete(vm_Variety variety)
        {
            VarietyModel model = new VarietyModel();
            model.Id = variety.Id;
            _db.Entry(model).State = EntityState.Deleted;
            try
            {
                //#region Log              
                variety.Log.Comment = "حذف خصوصیت  :: " + CreateComment(variety.Id, variety);
                variety.Log.RefId = variety.Id.ToString();
                variety.Log.Type = Control.Enumuration.Type.Variety;
                variety.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(variety.Log);
                //#endregion
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }          
        public vm_Variety GetById(int Id)
        {
            vm_Variety result = (from Variety in _db.varieties
                              where
                              Variety.Id.Equals(Id)
                              select new vm_Variety
                              {
                                  Id = Variety.Id,
                                  Title = Variety.Title
                              }).SingleOrDefault();
            return result;
        }
        public List<vm_Variety> GetAll()
        {
            List<vm_Variety> result = (from Variety in _db.varieties
                                    select new vm_Variety
                                    {
                                        Id = Variety.Id,
                                        Title = Variety.Title
                                    }

                                     ).ToList();
            return result;
        }
        public string CreateComment(int Id, vm_Variety vm_Variety)
        {
            string Comment = "";
            if (Id == 0)
            {
                Comment = "خصوصیت:" + vm_Variety.Title;
            }
            else
            {
                vm_Variety = GetById(Id);
                Comment = "خصوصیت :" + vm_Variety.Title;
            }
            return Comment;
        }
        ///////////////////////////////
        public string InsertDetail(vm_VarietyDetail vm_detail)
        {
           
            VarietyDetailModel variety = new VarietyDetailModel();
            variety.Title = vm_detail.Title;
            variety.VarietyId = vm_detail.VarietyId;
            try
            {
                _db.Add(variety);
                
                //#region Log              
                vm_detail.Log.Comment = "ثبت خصوصیت جدید :: " + CreateCommentDetail(0, vm_detail);
                vm_detail.Log.RefId = variety.Id.ToString();
                vm_detail.Log.Type = Control.Enumuration.Type.Variety;
                vm_detail.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(vm_detail.Log);
                //#endregion

                _db.SaveChanges();
             


                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
            
                return ex.Message;
            }
        }
        public string UpdateDetail(vm_VarietyDetail vm_detail)
        {
           
            VarietyDetailModel variety = new VarietyDetailModel();
            variety.Id = vm_detail.Id;
            variety.Title = vm_detail.Title;
            variety.VarietyId = vm_detail.VarietyId;
            try
            {
                _db.Entry(variety).State = EntityState.Modified;

                //#region Log
                vm_detail.Log.Comment = "ویرایش خصوصیت ::" + CreateCommentDetail(vm_detail.Id, vm_detail);
                vm_detail.Log.RefId = vm_detail.Id.ToString();
                vm_detail.Log.Type = Control.Enumuration.Type.City;
                vm_detail.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(_db);
                log.Insert(vm_detail.Log);
                //#endregion
                _db.SaveChanges();
               
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
               
                return ex.Message;
            }
        }
        public string DeleteDetail(vm_VarietyDetail vm_detail)
         {
            VarietyDetailModel variety = new VarietyDetailModel();
            variety.Id = vm_detail.Id;
            _db.Entry(variety).State = EntityState.Deleted;
            try
            {
                //region Log
                vm_detail.Log.Comment = "حذف خصوصیت ::" + CreateCommentDetail(vm_detail.Id, vm_detail);
                vm_detail.Log.RefId = vm_detail.Id.ToString();
                vm_detail.Log.Type = Control.Enumuration.Type.Variety;
                vm_detail.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(vm_detail.Log);
                //#end region
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        public string CreateCommentDetail(int Id,vm_VarietyDetail vm_detail)
        {
            string Comment = "";
            if (Id==0)
            {
                Comment = "خصوصیت:" + vm_detail.Title;
            }
            else
            {
                vm_detail = GetByIdDetail(Id);
                Comment = "خصوصیت:" + vm_detail.Title;
            }
            return Comment;
        }
        public vm_VarietyDetail GetByIdDetail(int Id)
        {
            vm_VarietyDetail result = (from VarietyDetailModel in _db.varietyDetails
                                       where
                                       VarietyDetailModel.Id.Equals(Id)
                                       select new vm_VarietyDetail
                                       {
                                           Id = VarietyDetailModel.Id,
                                           Title = VarietyDetailModel.Title,
                                           VarietyId = VarietyDetailModel.VarietyId,

                                       }).SingleOrDefault();
            return result;
        }
        public List<vm_VarietyDetail> GetDetailAll(int VarietyId)
        {
            List<vm_VarietyDetail> result=(from VarietyDetailModel in _db.varietyDetails
                                           where 
                                           VarietyDetailModel.Id.Equals(VarietyId)
             
                                           select new vm_VarietyDetail
                                           {
                                                Id=VarietyDetailModel.Id,
                                                Title=VarietyDetailModel.Title,
                                                VarietyId=VarietyDetailModel.VarietyId
                                           }
            ).ToList();
            return result;
        }
    }
}
