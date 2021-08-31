using E_Commerce_API.Model;
using E_Commerce_API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Business
{
    public class Province
    {
        private readonly ECommerceDB _db;
        public Province(ECommerceDB db)
        {
            _db = db;
        }
        public string Insert(vm_Province vm_province)
        {

            using var trans = _db.Database.BeginTransaction();
            ProvinceModel province = new ProvinceModel();
            province.Id = vm_province.Id;
            province.Title = vm_province.Title;
            province.CountryId = vm_province.CountryId;

            try
            {
                _db.provinces.Add(province);
                _db.SaveChanges();


                ProvinceDetailModel detailModel = new ProvinceDetailModel();
            
                detailModel.CountryId = vm_province.CountryId;
                detailModel.Provinceid = province.Id;
                detailModel.Title = province.Title;

                _db.provinceDetails.Add(detailModel);
                //#region Log              
                vm_province.Log.Comment = "ثبت استان جدید :: " + Createcomment(0, vm_province);
                vm_province.Log.RefId = province.Id.ToString();
                vm_province.Log.Type = Control.Enumuration.Type.Province;
                vm_province.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(vm_province.Log);
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
        public string Update(vm_Province province)
        {
            using var trans = _db.Database.BeginTransaction();
            ProvinceModel provinceModel = _db.provinces.Find(province.Id);            
            provinceModel.Title = province.Title;
            provinceModel.CountryId = province.CountryId;
            try
            {
                _db.Entry(provinceModel).State = EntityState.Modified;
                ProvinceDetailModel detailModel = _db.provinceDetails.Where(
                    x => x.Provinceid.Equals(province.Id)).SingleOrDefault();
                detailModel.Title = province.Title;
                _db.Entry(detailModel).State = EntityState.Modified;
                //#region Log
                province.Log.Comment = "ویرایش استان :" + Createcomment(province.Id, province);
                province.Log.RefId = province.Id.ToString();
                province.Log.Type = Control.Enumuration.Type.Province;
                province.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(_db);
                //#end Region
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
        public string Delete(vm_Province province)
        {
            ProvinceModel provinceModel = new ProvinceModel();
            provinceModel.Id = province.Id;
            _db.Entry(provinceModel).State = EntityState.Deleted;
            try
            {
                //@region Log
                province.Log.Comment = "حذف استان : " + Createcomment(province.Id, province);
                province.Log.RefId = province.Id.ToString();
                province.Log.Type = Control.Enumuration.Type.Province;
                province.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                //#end Region
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public vm_Province GetById(int Id)
        {
            vm_Province result = (from Province in _db.provinces
                                  where
                                  Province.Id.Equals(Id)
                                  select new vm_Province
                                  {
                                      Id = Province.Id,
                                      Title = Province.Title,
                                   
                                  }
                                ).SingleOrDefault();
            return result;
        }
        public List<vm_Province> GetAll()
        {
            List<vm_Province> result = (from Province in _db.provinces
                                        select new vm_Province
                                        {
                                            Id = Province.Id,
                                            Title = Province.Title,

                                        }).OrderBy(x => x.Title).ToList();
            return result;
        }
        public string Createcomment(int Id, vm_Province vm_province)
        {
            string Comment = "";
            if (Id == 0)
            {
                Comment = "استان :" + vm_province.Title;
            }
            else
            {
                vm_province = GetById(Id);
                Comment = "استان :" + vm_province.Title;
            }
            return Comment;
        }

        //
        
        public string InsertDetail(vm_ProvinceDetail vm_detail)
        {
            ProvinceDetailModel detail = new ProvinceDetailModel();

            detail.Title = vm_detail.Title;

            try
            {
                _db.Add(detail);


                //#region Log              
                vm_detail.Log.Comment = "ثبت استان جدید :: " + CreatecommentDetail(0, vm_detail);
                vm_detail.Log.RefId = vm_detail.Id.ToString();
                vm_detail.Log.Type = Control.Enumuration.Type.Province;
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
        public string UpdateDetail(vm_ProvinceDetail vm_detail)
        {

            ProvinceDetailModel detail = new ProvinceDetailModel();
            detail.Title = vm_detail.Title;

            try
            {


                _db.Entry(detail).State = EntityState.Modified;
                //regiod log
                vm_detail.Log.Comment = "ویرایش استان : " + CreatecommentDetail(vm_detail.Id, vm_detail);
                vm_detail.Log.RefId = vm_detail.Id.ToString();
                vm_detail.Log.Type = Control.Enumuration.Type.Province;
                vm_detail.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(_db);
                log.Insert(vm_detail.Log);
                //endregion
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }
        public string DeleteDetail(vm_ProvinceDetail vm_detail)
            {
                ProvinceDetailModel detail = new ProvinceDetailModel();
                detail.Id = vm_detail.Id;
                _db.Entry(detail).State = EntityState.Deleted;
                try
                {
                    //@region Log
                    vm_detail.Log.Comment = "حذف استان : " + CreatecommentDetail(vm_detail.Id, vm_detail);
                    vm_detail.Log.RefId = vm_detail.Id.ToString();
                    vm_detail.Log.Type = Control.Enumuration.Type.Province;
                    vm_detail.Log.Operation = Control.Enumuration.Operation.Delete;
                    Business.Log log = new Log(_db);
                    //#end Region
                    _db.SaveChanges();
                    return Control.Constant.SuccessResult;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        public vm_ProvinceDetail GetByIdDetail(int Id)
            {
                vm_ProvinceDetail result = (from ProvinceDetail in _db.provinceDetails
                                      where
                                      ProvinceDetail.Id.Equals(Id)
                                      select new vm_ProvinceDetail
                                      {
                                          Id = ProvinceDetail.Id,
                                          Title = ProvinceDetail.Title,
                                      }
                                    ).SingleOrDefault();
                return result;
            }
        public List<vm_ProvinceDetail> GetByAllDetail()
            {
                List<vm_ProvinceDetail> result = (from ProvinceDetail in _db.provinceDetails
                                            select new vm_ProvinceDetail
                                            {
                                                Id = ProvinceDetail.Id,
                                                Title = ProvinceDetail.Title,

                                            }).ToList();
                return result;
            }
        public string CreatecommentDetail(int Id, vm_ProvinceDetail vm_detail)
            {
                string Comment = "";
                if (Id == 0)
                {
                    Comment = "استان :" + vm_detail.Title;
                }
                else
                {
                    vm_detail = GetByIdDetail(Id);
                    Comment = "استان :" + vm_detail.Title;
                }
                return Comment;
            }

        
    }
}