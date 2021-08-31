using E_Commerce_API.Model;
using E_Commerce_API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Business
{
    public class City
    {
        private readonly ECommerceDB _db;
        public City(ECommerceDB db)
        {
            _db = db;
        }
        public string Insert(vm_City vm_city) 
        {
            using var trans = _db.Database.BeginTransaction();
            CityModel city = new CityModel();
            city.Id = vm_city.Id;
            city.Title = vm_city.Title;

            try
            {
                _db.cities.Add(city);
                _db.SaveChanges();

                CityDetailModel detailModel = new CityDetailModel();
                ProvinceModel province = new ProvinceModel();
                detailModel.ProvinceId = vm_city.ProvinceId;
                detailModel.CityId = city.Id;
                detailModel.Title = city.Title;

                _db.cityDetails.Add(detailModel);
                //#region Log              
                vm_city.Log.Comment = "ثبت شهر جدید :: " + CreateComment(0, vm_city);
                vm_city.Log.RefId = city.Id.ToString();
                vm_city.Log.Type = Control.Enumuration.Type.City;
                vm_city.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(vm_city.Log);
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
        public string Update(vm_City vm_city)
        {
            using var trans = _db.Database.BeginTransaction();

            CityModel cityModel = _db.cities.Find(vm_city.Id);            
            cityModel.Title = vm_city.Title;
            
            try
            {
                _db.Entry(cityModel).State = EntityState.Modified;
                CityDetailModel detailModel = _db.cityDetails.
                    Where(x => x.CityId.Equals(cityModel.Id)).SingleOrDefault();

                detailModel.Title = cityModel.Title;
                detailModel.ProvinceId = vm_city.ProvinceId;
                _db.Entry(detailModel).State = EntityState.Modified;
                //#region Log              
                vm_city.Log.Comment = "ویرایش شهر  :: " + CreateComment(vm_city.Id, vm_city);
                vm_city.Log.RefId = vm_city.Id.ToString();
                vm_city.Log.Type = Control.Enumuration.Type.City;
                vm_city.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(_db);
                log.Insert(vm_city.Log);
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
        public string Delete(vm_City city)
        {
            CityModel cityModel = new CityModel();
            cityModel.Id = city.Id;
            _db.Entry(cityModel).State = EntityState.Deleted;
            try
            {
                //#region Log              
                city.Log.Comment = "حذف شهر  :: " + CreateComment(city.Id, city);
                city.Log.RefId = city.Id.ToString();
                city.Log.Type = Control.Enumuration.Type.City;
                city.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(city.Log);
                //#endregion

                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public vm_City GetById(int Id)
        {
            vm_City result = (from City in _db.cities
                                 where
                                 City.Id.Equals(Id)
                                 select new vm_City
                                 {
                                     Id = City.Id,
                                     Title = City.Title,
                                 }).SingleOrDefault();
            return result;
        }
        public List<vm_City> GetAll()
        {
            List<vm_City> result = (from City in _db.cities
                                       select new vm_City
                                       {
                                           Id = City.Id,
                                           Title = City.Title,                                           
                                       }

                                     ).OrderBy(x => x.Title).ToList();
            return result;
        }
        public string CreateComment(int Id, vm_City vm_city)
        {
            string Comment = "";
            if (Id == 0)
            {
                Comment = "شهر:" + vm_city.Title;
            }
            else
            {
                vm_city = GetById(Id);
                Comment = "شهر :" + vm_city.Title;
            }
            return Comment;
        }   
        ///////////////////////////////
        public string InsertDetail(vm_CityDetail vm_detail)
        {
            CityDetailModel Detail = new CityDetailModel();
            Detail.Title = vm_detail.Title;
            Detail.CityId = vm_detail.CityId;

            try
            {
                _db.Add(Detail);

                //#region Log              
                vm_detail.Log.Comment = "ثبت شهر جدید :: " + CreateCommentDetail(0, vm_detail);
                vm_detail.Log.RefId = vm_detail.id.ToString();
                vm_detail.Log.Type = Control.Enumuration.Type.City;
                vm_detail.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(vm_detail.Log);
                //#endregion

                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdateDetail(vm_CityDetail vm_detail)
        {
            CityDetailModel Detail = _db.cityDetails.Find(vm_detail.id);
            Detail.Title = vm_detail.Title;
            Detail.CityId = vm_detail.CityId;
            try
            {
                _db.Entry(Detail).State = EntityState.Modified;
                //#region Log              
                vm_detail.Log.Comment = "ویرایش شهر  :: " + CreateCommentDetail(vm_detail.id, vm_detail);
                vm_detail.Log.RefId = vm_detail.id.ToString();
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
        public string DeleteDetail(vm_CityDetail city)
        {
            CityModel cityModel = new CityModel();
            cityModel.Id = city.id;
            _db.Entry(city).State = EntityState.Deleted;
            try
            {
                //#region Log              
                city.Log.Comment = "حذف شهر  :: " + CreateCommentDetail(city.id, city);
                city.Log.RefId = city.id.ToString();
                city.Log.Type = Control.Enumuration.Type.City;
                city.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(city.Log);
                //#endregion

                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public vm_CityDetail GetByIdDetail(int Id)
        {
            vm_CityDetail result = (from CityDetail in _db.cityDetails
                              where
                              CityDetail.Id.Equals(Id)
                              select new vm_CityDetail
                              {
                                  id=CityDetail.Id,
                                  Title=CityDetail.Title,
                                  CityId=CityDetail.CityId
                              }).SingleOrDefault();
            return result;
        }
        public List<vm_CityDetail> GetAllDetail()
        {
            List<vm_CityDetail> result = (from CityDetail in _db.cityDetails
                                          select new vm_CityDetail
                                    {
                                              id = CityDetail.Id,
                                              Title = CityDetail.Title,
                                              CityId = CityDetail.CityId
                                          }

                                     ).ToList();
            return result;
        }
        public string CreateCommentDetail(int Id, vm_CityDetail vm_detail)
        {
            string Comment = "";
            if (Id == 0)
            {
                Comment = "شهر:" + vm_detail.Title;
            }
            else
            {
                vm_detail = GetByIdDetail(Id);
                Comment = "شهر :" + vm_detail.Title;
            }
            return Comment;
        }
    }
}

            

