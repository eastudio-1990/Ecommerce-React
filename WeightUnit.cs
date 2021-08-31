using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Business
{
    public class WeightUnit
    {
        private readonly Model.ECommerceDB db;
        public WeightUnit(Model.ECommerceDB _db)
        {
            db = _db;
        }

        #region UnitWeight
        public string Insert(ViewModel.vm_WeightUnit unit)
        {

            using var trans = db.Database.BeginTransaction();
            Business.Language lang = new Language(db);

            Model.WeightUnitModel unitModel = new Model.WeightUnitModel();
            unitModel.Title = unit.Title;
            db.WeightUnits.Add(unitModel);
            db.SaveChanges();
            Model.WeightUnitDetailModel detailModel = new Model.WeightUnitDetailModel();
            detailModel.LanguageId = lang.GetDefault().Id;
            detailModel.Title = unit.Title;
            detailModel.WeightUnitId = unitModel.Id;
            db.WeightUnitDetails.Add(detailModel);
            try
            {
                //#region Log              
                unit.Log.Comment = "ثبت واحد وزن جدید :: " + CreateComment(0, unit);
                unit.Log.RefId = unitModel.Id.ToString();
                unit.Log.Type = Control.Enumuration.Type.WeightUnit;
                unit.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(db);
                log.Insert(unit.Log);
                //#endregion

                db.SaveChanges();
                trans.Commit();
                
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return ex.InnerException.Message;

            }

        }

        public string Update(ViewModel.vm_WeightUnit unit)
        {
            using var trans = db.Database.BeginTransaction();
            Model.WeightUnitModel unitModel = new Model.WeightUnitModel();
            unitModel.Id = unit.Id;
            unitModel.Title = unit.Title;

            db.Entry(unitModel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Business.Language lang = new Language(db);

            ViewModel.vm_Language DefaultLang = lang.GetDefault();

            Model.WeightUnitDetailModel detailModel = db.WeightUnitDetails.Where(x => x.WeightUnitId.Equals(unit.Id) && x.LanguageId.Equals(DefaultLang.Id)).SingleOrDefault();
            detailModel.Title = unit.Title;
            db.Entry(detailModel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            try
            {
                //#region Log              
                unit.Log.Comment = "ویرایش واحد وزن :: " + CreateComment(unit.Id, unit);
                unit.Log.RefId = unitModel.Id.ToString();
                unit.Log.Type = Control.Enumuration.Type.WeightUnit;
                unit.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(db);
                log.Insert(unit.Log);
                //#endregion
                db.SaveChanges();
                trans.Commit();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return ex.Message;
            }
        }

        public string Delete(ViewModel.vm_WeightUnit unit)
        {
            using var trans = db.Database.BeginTransaction();

            Model.WeightUnitModel unitModel = new Model.WeightUnitModel();
            unitModel.Id = unit.Id;

            db.Entry(unitModel).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            Business.Language lang = new Language(db);

            ViewModel.vm_Language DefaultLang = lang.GetDefault();

            Model.WeightUnitDetailModel detailModel = db.WeightUnitDetails.Where(x => x.WeightUnitId.Equals(unit.Id) && x.LanguageId.Equals(DefaultLang.Id)).SingleOrDefault();
            db.Entry(detailModel).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            try
            {
                //#region Log              
                unit.Log.Comment = "حذف واحد وزن :: " + CreateComment(unit.Id, unit);
                unit.Log.RefId = unitModel.Id.ToString();
                unit.Log.Type = Control.Enumuration.Type.WeightUnit;
                unit.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(db);
                log.Insert(unit.Log);
                //#endregion
                db.SaveChanges();
                trans.Commit();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return ex.Message;
            }
        }

        public ViewModel.vm_WeightUnit GetById(int Id)
        {
            ViewModel.vm_WeightUnit result = (from unit in db.WeightUnits
                                              where
                                              unit.Id.Equals(Id)
                                              select new ViewModel.vm_WeightUnit
                                              {
                                                  Id = unit.Id,
                                                  Title = unit.Title
                                              }).SingleOrDefault();
            return result;
        }

        public List<ViewModel.vm_WeightUnit> GetAll()
        {
            List<ViewModel.vm_WeightUnit> result = (from unit in db.WeightUnits
                                                    select new ViewModel.vm_WeightUnit
                                                    {
                                                        Id = unit.Id,
                                                        Title = unit.Title
                                                    }).ToList();
            return result;
        }

        #endregion

        #region Detail
        public string InsertDetail(ViewModel.vm_WeightUnitDetail unit)
        {
            using var trans = db.Database.BeginTransaction();
            Model.WeightUnitDetailModel detailModel = new Model.WeightUnitDetailModel();
            detailModel.LanguageId = unit.LanguageId;
            detailModel.Title = unit.Title;
            detailModel.WeightUnitId = unit.WeightUnitId;
            db.WeightUnitDetails.Add(detailModel);
            try
            {
                //#region Log              
                unit.Log.Comment = "ثبت واحد وزن جدید :: " + CreateCommentDetail(0, unit);
                unit.Log.RefId = detailModel.Id.ToString();
                unit.Log.Type = Control.Enumuration.Type.WeightUnit;
                unit.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(db);
                log.Insert(unit.Log);
                //#endregion

                db.SaveChanges();
                trans.Commit();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }

        }

        public string UpdateDetail(ViewModel.vm_WeightUnitDetail unit)
        {
            using var trans = db.Database.BeginTransaction();
            Model.WeightUnitDetailModel detailModel = db.WeightUnitDetails.Find(unit.Id);
            detailModel.LanguageId = unit.LanguageId;
            
            detailModel.Title = unit.Title;
            db.Entry(detailModel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            try
            {
                //#region Log              
                unit.Log.Comment = "ویرایش واحد وزن :: " + CreateCommentDetail(unit.Id, unit);
                unit.Log.RefId = detailModel.Id.ToString();
                unit.Log.Type = Control.Enumuration.Type.WeightUnit;
                unit.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(db);
                log.Insert(unit.Log);
                //#endregion
                db.SaveChanges();
                trans.Commit();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return ex.Message;
            }
        }

        public string DeleteDetail(ViewModel.vm_WeightUnitDetail unit)
        {

            using var trans = db.Database.BeginTransaction();
            Model.WeightUnitDetailModel detailModel = db.WeightUnitDetails.Find(unit.Id);
            db.Entry(detailModel).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            try
            {
                //#region Log              
                unit.Log.Comment = "حذف واحد وزن :: " + CreateCommentDetail(unit.Id, unit);
                unit.Log.RefId = detailModel.Id.ToString();
                unit.Log.Type = Control.Enumuration.Type.WeightUnit;
                unit.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(db);
                log.Insert(unit.Log);
                //#endregion
                db.SaveChanges();
                trans.Commit();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return ex.Message;
            }
        }

        public ViewModel.vm_WeightUnitDetail GetDetailById(int Id)
        {
            ViewModel.vm_WeightUnitDetail result = (from unit in db.WeightUnitDetails
                                              where
                                              unit.Id.Equals(Id)
                                              select new ViewModel.vm_WeightUnitDetail
                                              {
                                                  Id = unit.Id,
                                                  Title = unit.Title,
                                                  LanguageId=unit.LanguageId
                                              }).SingleOrDefault();
            return result;
        }

        public List<ViewModel.vm_WeightUnitDetail> GetDetailAll(int WeightUnitId)
        {
            List<ViewModel.vm_WeightUnitDetail> result = (from unit in db.WeightUnitDetails
                                                          join 
                                                          language in db.Languages 
                                                          on 
                                                          unit.LanguageId equals language.Id
                                                          where 
                                                          unit.WeightUnitId.Equals(WeightUnitId)
                                                    select new ViewModel.vm_WeightUnitDetail
                                                    {
                                                        Id = unit.Id,
                                                        Title = unit.Title,
                                                        LanguageTitle=language.Title
                                                    }).ToList();
            return result;
        }

        #endregion

        public string CreateComment(int Id, ViewModel.vm_WeightUnit unit)
        {
            string Comment = "";
            if (Id == 0)
            {
                Comment = "شـــرح : " + unit.Title;
            }
            else
            {
                unit = GetById(Id);
                Comment = "شـــرح : " + unit.Title;
            }
            return Comment;
        }
        public string CreateCommentDetail(int Id, ViewModel.vm_WeightUnitDetail unit)
        {
            string Comment = "";
            if (Id == 0)
            {
                Comment = "شـــرح : " + unit.Title;
            }
            else
            {
                unit = GetDetailById(Id);
                Comment = "شـــرح : " + unit.Title;
            }
            return Comment;
        }

    }
}
