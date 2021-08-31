using E_Commerce_API.Model;
using E_Commerce_API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Business
{
    public class Material
    {
        private readonly ECommerceDB _db;
        public Material(ECommerceDB db)
        {
            _db = db;
        }
       
        public string Insert(vm_Material vm_material)
        {
            using var trans = _db.Database.BeginTransaction();
            MaterialModel material = new MaterialModel();
            material.Id = vm_material.Id;
            material.Title = vm_material.Title;           
            try
            {
                _db.Add(material);
                _db.SaveChanges();

                MaterialDetailModel detailModel = new MaterialDetailModel();
                Business.Language lang = new Business.Language(_db);

                detailModel.Title = material.Title;
                detailModel.MeterialId = material.Id;
                detailModel.LanguageId = lang.GetDefault().Id;



                _db.materialDetails.Add(detailModel);
                //#region Log
                vm_material.Log.Comment = "ثبت جنس جدید ::" + CreateComment(0, vm_material);
                vm_material.Log.RefId = material.Id.ToString();
                vm_material.Log.Type = Control.Enumuration.Type.Material;
                vm_material.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                //#end region

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
        public string Update(vm_Material vm_material)
        {
            using var trans = _db.Database.BeginTransaction();
            MaterialModel material = _db.materials.Find(vm_material.Id);
            material.Id = vm_material.Id;
            material.Title = vm_material.Title;
            try
            {
                _db.Entry(material).State = EntityState.Modified;
                Business.Language lang = new Language(_db);

                vm_Language lagu = lang.GetDefault();

                MaterialDetailModel materialDetail = _db.materialDetails
                    .Where(x => x.MeterialId.Equals(material.Id) &&
                  x.LanguageId.Equals(lagu.Id)).SingleOrDefault();

                materialDetail.Title = material.Title;
                _db.Entry(materialDetail).State = EntityState.Modified;
                //#region Log
                vm_material.Log.Comment = "ویرایش جنس ::" + CreateComment(vm_material.Id, vm_material);
                vm_material.Log.RefId = material.Id.ToString();
                vm_material.Log.Type = Control.Enumuration.Type.Material;
                vm_material.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(_db);
                log.Insert(vm_material.Log);
                //#end region
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

        public string Delete(vm_Material material)
        {
            MaterialModel materialModel = new MaterialModel();
            materialModel.Id = material.Id;
            _db.Entry(materialModel).State = EntityState.Deleted;
            try
            {
                //#region Log
                material.Log.Comment = "حذف جنس " + CreateComment(material.Id, material);
                material.Log.RefId = material.Id.ToString();
                material.Log.Type = Control.Enumuration.Type.Material;
                material.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(material.Log);
                //#end region
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        public List<vm_Material> GetAll()
        {
            List<vm_Material> res = (from Material in _db.materials
                                     select new vm_Material
                                     {
                                         Id = Material.Id,
                                         Title = Material.Title
                                     }).ToList();
            return res;
        }
        public vm_Material GetById(int Id)
        {
            vm_Material res = (from Material in _db.materials
                               where
                              Material.Id.Equals(Id)
                               select new vm_Material
                               {
                                   Id = Material.Id,
                                   Title = Material.Title,
                                   
                               }
                             ).SingleOrDefault();
            return res;
        }
        public string CreateComment(int Id, vm_Material vm_material)
        {
            string Comment = "";
            if (Id == 0)
            {
                Comment = "جنس:" + vm_material.Title;
            }
            else
            {
                vm_material=GetById(Id);
                Comment = "جنس:" + vm_material.Title;
            }
            return Comment;
        }
        /////////////////
        public vm_MaterialDetail GetByIdDetail(int Id)
        {
            vm_MaterialDetail res = (from MaterialDetail in _db.materialDetails
                                     where
                                     MaterialDetail.Id.Equals(Id)
                                     select new vm_MaterialDetail
                                     {
                                         Id = MaterialDetail.Id,
                                         LanguageId = MaterialDetail.LanguageId,
                                         MeterialId = MaterialDetail.MeterialId,
                                         Title = MaterialDetail.Title,

                                     }).SingleOrDefault();
            return res;
        }
        public string CreateDetailComment(int Id,vm_MaterialDetail vm_MaterialDetail)
        {
            string Comment = "";
            if (Id == 0)
            {
                Comment = "جنس:" + vm_MaterialDetail.Title;
            }
            else
            {
                vm_MaterialDetail = GetByIdDetail(Id);
                Comment = "حنس :" + vm_MaterialDetail.Title;
            }
            return Comment;
        }
        public string InsertDetail(vm_MaterialDetail vm_detail)
        {
            MaterialDetailModel detail = new MaterialDetailModel();
            detail.Title = vm_detail.Title;
            detail.LanguageId = vm_detail.LanguageId;
            detail.MeterialId = vm_detail.MeterialId;
            try
            {
                _db.Add(detail);
                //#region Log
                vm_detail.vm_Log.Comment = "ثبت جنس جدید :: " + CreateDetailComment(0, vm_detail);
                vm_detail.vm_Log.RefId = vm_detail.Id.ToString();
                vm_detail.vm_Log.Type = Control.Enumuration.Type.Country;
                vm_detail.vm_Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(vm_detail.vm_Log);
                //#region End
                _db.SaveChanges();



                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdateDetail(vm_MaterialDetail vm_detail)
        {
            MaterialDetailModel detail = _db.materialDetails.Find(vm_detail.Id);
            detail.Title = vm_detail.Title;
            detail.LanguageId = vm_detail.LanguageId;
            try
            {
                _db.Entry(detail).State = EntityState.Modified;
                //#region Log
                vm_detail.vm_Log.Comment = "ویرایش جنس ::" + CreateDetailComment(vm_detail.Id, vm_detail);
                vm_detail.vm_Log.RefId = vm_detail.Id.ToString();
                vm_detail.vm_Log.Type = Control.Enumuration.Type.Material;
                vm_detail.vm_Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(_db);
                log.Insert(vm_detail.vm_Log);
                //#End Region
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }


        }
        public string DeleteDetail(vm_MaterialDetail vm_detail)
        {
            MaterialDetailModel detail = new MaterialDetailModel();
            detail.Id = vm_detail.Id;
            _db.Entry(detail).State = EntityState.Deleted;
            try
            {
                //#region Log
                vm_detail.vm_Log.Comment = "حذف جنس ::" + CreateDetailComment(vm_detail.Id, vm_detail);
                vm_detail.vm_Log.RefId = vm_detail.Id.ToString();
                vm_detail.vm_Log.Type = Control.Enumuration.Type.Material;
                vm_detail.vm_Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(vm_detail.vm_Log);
                //#End region
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

        }
        public List<vm_MaterialDetail> GetAllDetail()
        {
            List<vm_MaterialDetail> res =
                (from MaterialDetail in _db.materialDetails
                 select new vm_MaterialDetail
                 {
                     Id = MaterialDetail.Id,
                     LanguageId = MaterialDetail.LanguageId,
                     MeterialId = MaterialDetail.MeterialId,
                     Title = MaterialDetail.Title
                 }
                 ).ToList();
            return res;
               
        }

    }
}
