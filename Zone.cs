using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Business
{
    public class Zone
    {
        private readonly Model.ECommerceDB _db;
        public Zone(Model.ECommerceDB db)
        {
            _db = db;
        }

        public string Insert(ViewModel.vm_Zone vm_zone)
        {
            using var trans = _db.Database.BeginTransaction();

            Model.ZoneModel zoneModel = new Model.ZoneModel();

            zoneModel.Id = vm_zone.Id;
            zoneModel.Title = vm_zone.Title;            
            try
            {
                _db.Add(zoneModel);
                _db.SaveChanges();

                vm_zone.Log.Comment = "ثبت منطقه جدید :" + CreateComment(0, vm_zone);
                vm_zone.Log.RefId = zoneModel.Id.ToString();
                vm_zone.Log.Type = Control.Enumuration.Type.Zone;
                vm_zone.Log.Operation = Control.Enumuration.Operation.Insert;

                Business.Log log = new Log(_db);
                log.Insert(vm_zone.Log);
                _db.SaveChanges();
                trans.Commit();
                return Control.Constant.SuccessResult;                
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return ex.InnerException.Message;
            }

        }
        public string CreateComment(int Id,ViewModel.vm_Zone vm_zone)
        {
            string Comment = "";
            if (Id == 0)
            {
                Comment = "دسته بندی" + vm_zone.Title;
            }
            else
            {
                vm_zone = GetById(Id);
                Comment = "دسته بندی :" + vm_zone.Title;
            }
            return Comment;
        }
        public string Update(ViewModel.vm_Zone vm_zone)
        {
            using var trans = _db.Database.BeginTransaction();
            Model.ZoneModel modelZone = _db.Zones.Find(vm_zone.Id);
            modelZone.Title = vm_zone.Title;
            try
            {
            _db.Entry(modelZone).State = EntityState.Modified;
            vm_zone.Log.Comment = "ویرایش منطقه :" + CreateComment(vm_zone.Id, vm_zone);
            vm_zone.Log.RefId = modelZone.Id.ToString();
            vm_zone.Log.Type = Control.Enumuration.Type.Zone;
            vm_zone.Log.Operation = Control.Enumuration.Operation.Update;
            Business.Log log = new Log(_db);
            log.Insert(vm_zone.Log);
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
        public string Delete(ViewModel.vm_Zone vm_zone)
        {
             using var trans= _db.Database.BeginTransaction();
            Model.ZoneModel modelzone = _db.Zones.Find(vm_zone.Id);
            try
            {
            _db.Entry(modelzone).State = EntityState.Deleted;
                vm_zone.Log.Comment = "حذف منطقه :" + CreateComment(vm_zone.Id, vm_zone);
                vm_zone.Log.RefId = modelzone.Id.ToString();
                vm_zone.Log.Type = Control.Enumuration.Type.Zone;
                vm_zone.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(vm_zone.Log);
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
        public ViewModel.vm_Zone GetById(int id)
        {
            ViewModel.vm_Zone result = (from Zone in _db.Zones
                                        where
                                        Zone.Id.Equals(id)
                                        select new ViewModel.vm_Zone
                                        {
                                            Id=Zone.Id,
                                            Title=Zone.Title
                                        }).SingleOrDefault();
            return result;
        }
        public List<ViewModel.vm_Zone> GetAll()
        {
            List<ViewModel.vm_Zone> result = (from Zone in _db.Zones
                                              select new ViewModel.vm_Zone
                                              {
                                                  Id = Zone.Id,
                                                  Title = Zone.Title
                                              }).ToList();
            return result;
        }
        public string InsertZoneProvince(ViewModel.vm_ZoneProvince vm_ZoneProvince)
        {
            using var trans = _db.Database.BeginTransaction();
            Model.ZoneProvinceModel zoneProvinceModel = new Model.ZoneProvinceModel();
            zoneProvinceModel.Id = vm_ZoneProvince.Id;
            zoneProvinceModel.ProvinceId = vm_ZoneProvince.ProvinceId;
            zoneProvinceModel.ZoneId = vm_ZoneProvince.ZoneId;            
            try
            {
              
                _db.zoneProvinces.Add(zoneProvinceModel);
                _db.SaveChanges();
                vm_ZoneProvince.Log.Comment = "اضافه کردن شهر به منطقه :" + CreateCommentZoneProvince(0, vm_ZoneProvince);
                vm_ZoneProvince.Log.RefId = zoneProvinceModel.Id.ToString();
                vm_ZoneProvince.Log.Type = Control.Enumuration.Type.Zone;
                vm_ZoneProvince.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(vm_ZoneProvince.Log);
                _db.SaveChanges();
                trans.Commit();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return ex.InnerException.Message;
            }
        }
        public string DeleteZoneProvince(ViewModel.vm_ZoneProvince vm_ZoneProvince)
        {
            using var trans = _db.Database.BeginTransaction();
            Model.ZoneProvinceModel zoneProvince = _db.zoneProvinces.Find(vm_ZoneProvince.Id);
            try
            {
                _db.Entry(zoneProvince).State = EntityState.Deleted;
                vm_ZoneProvince.Log.Comment = "حذف شهر از منطقه :" + CreateCommentZoneProvince(vm_ZoneProvince.Id, vm_ZoneProvince);
                vm_ZoneProvince.Log.RefId = zoneProvince.Id.ToString();
                vm_ZoneProvince.Log.Type = Control.Enumuration.Type.Zone;
                vm_ZoneProvince.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(vm_ZoneProvince.Log);
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
        public string UpdateZoneProvince(ViewModel.vm_ZoneProvince vm_ZoneProvince)
        {
            using var trans = _db.Database.BeginTransaction();
            Model.ZoneProvinceModel ZonecityModel = _db.zoneProvinces.Find(vm_ZoneProvince.Id);
            ZonecityModel.ProvinceId = vm_ZoneProvince.ProvinceId;            
            ZonecityModel.ZoneId = vm_ZoneProvince.ZoneId;
            try
            {
                _db.Entry(ZonecityModel).State = EntityState.Modified;
                _db.SaveChanges();
                vm_ZoneProvince.Log.Comment = "ویرایش شهر در منطقه :" + CreateCommentZoneProvince(vm_ZoneProvince.Id, vm_ZoneProvince);
                vm_ZoneProvince.Log.RefId = ZonecityModel.Id.ToString();
                vm_ZoneProvince.Log.Type = Control.Enumuration.Type.Zone;
                vm_ZoneProvince.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(_db);
                log.Insert(vm_ZoneProvince.Log);
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
        public string CreateCommentZoneProvince(int Id,ViewModel.vm_ZoneProvince vm)
        {
            string Comment = "";
            if (Id==0)
            {
                Comment = "منطقه" + vm.ZoneId;
            }
            else
            {
                vm = GetZoneProvinceById(Id);
                Comment = "منطقه :" + vm.ZoneId;
            }
            return Comment;
        }
        public ViewModel.vm_ZoneProvince GetZoneProvinceById(int id)
        {
            ViewModel.vm_ZoneProvince result = (from ZoneProvince in _db.zoneProvinces
                                            where ZoneProvince.Id.Equals(id)
                                            select new ViewModel.vm_ZoneProvince
                                            {
                                                Id= ZoneProvince.Id,
                                                ProvinceId= ZoneProvince.ProvinceId,                                               
                                                ZoneId= ZoneProvince.ZoneId
                                            }).SingleOrDefault();
            return result;
        }
        public List<ViewModel.vm_ZoneProvince> GetAllProvinceByZoneId(int zoneid)
        {
            List<ViewModel.vm_ZoneProvince> result = (from ZoneProvince in _db.zoneProvinces
                                                      join  
                                                      Province in _db.provinces
                                                      on
                                                      ZoneProvince.ProvinceId equals Province.Id
                                                      where
                                                      ZoneProvince.ZoneId.Equals(zoneid)
                                                  select new ViewModel.vm_ZoneProvince
                                                  {
                                                      Id=ZoneProvince.Id,
                                                      ProvinceId=ZoneProvince.ProvinceId, 
                                                      ProvinceTitle=Province.Title,
                                                      ZoneId =ZoneProvince.ZoneId
                                                  }).ToList();
            return result;
        }
        public int CheckDuplicate(int provinceId,int zoneId)
        {
            return _db.zoneProvinces.Where(x => x.ZoneId.Equals(zoneId) && x.ProvinceId.Equals(provinceId)).Count();
        }
    }
}
