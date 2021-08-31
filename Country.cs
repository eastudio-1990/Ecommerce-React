using E_Commerce_API.ViewModel;
using E_Commerce_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API.Business
{
    public class Country
    {
        private readonly ECommerceDB _db;
        public Country(ECommerceDB db)
        {
            _db = db;
        }
        public string Insert(vm_Country vm_country)
        {
            using var trans = _db.Database.BeginTransaction();
            CountryModel country = new CountryModel();

            country.Id = vm_country.Id;
            country.Title = vm_country.Title;
            try
            {
                _db.Add(country);
                _db.SaveChanges();

                CountryDetailModel detailModel = new CountryDetailModel();
                Business.Language lang = new Language(_db);

                detailModel.CountryId = country.Id;
                detailModel.LanguageId = lang.GetDefault().Id;
                detailModel.Title = vm_country.Title;

                _db.countryDetails.Add(detailModel);
                //#region Log              
                vm_country.Log.Comment = "ثبت کشور جدید :: " + CreateComment(0, vm_country);
                vm_country.Log.RefId = country.Id.ToString();
                vm_country.Log.Type = Control.Enumuration.Type.Country;
                vm_country.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(vm_country.Log);
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
        public string Update(vm_Country country)
        {
            using var trans = _db.Database.BeginTransaction();

            CountryModel countrymodel = _db.countries.Find(country.Id);
            countrymodel.Id = country.Id;
            countrymodel.Title = country.Title;


            try
            {
                _db.Entry(countrymodel).State = EntityState.Modified;
                Business.Language lang = new Language(_db);
                ViewModel.vm_Language language = lang.GetDefault();

                CountryDetailModel detailModel = _db.countryDetails
                    .Where(x => x.CountryId.Equals(country.Id)
                    && x.LanguageId.Equals(language.Id)).SingleOrDefault();


                detailModel.Title = country.Title;
                _db.Entry(detailModel).State = EntityState.Modified;
                //#region Log              
                country.Log.Comment = "ویرایش کشور  :: " + CreateComment(country.Id, country);
                country.Log.RefId = country.Id.ToString();
                country.Log.Type = Control.Enumuration.Type.Country;
                country.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(_db);
                log.Insert(country.Log);
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
        public string Delete(vm_Country vm_country)
        {
            using var trans = _db.Database.BeginTransaction();

            CountryModel countrymodel = new CountryModel();
            countrymodel.Id = vm_country.Id;
            _db.Entry(countrymodel).State = EntityState.Deleted;
            try
            {
                Business.Language lang = new Language(_db);
                ViewModel.vm_Language language = lang.GetDefault();

                CountryDetailModel detailModel = _db.countryDetails
                    .Where(x => x.CountryId.Equals(vm_country.Id)
                    && x.LanguageId.Equals(language.Id)).SingleOrDefault();


                _db.Entry(detailModel).State = EntityState.Deleted;
                //#region Log              
                vm_country.Log.Comment = "حذف کشور  :: " + CreateComment(vm_country.Id, vm_country);
                vm_country.Log.RefId = vm_country.Id.ToString();
                vm_country.Log.Type = Control.Enumuration.Type.Country;
                vm_country.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(vm_country.Log);
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
        public vm_Country GetById(int Id)
        {
            vm_Country result = (from Country in _db.countries
                                 where
                                 Country.Id.Equals(Id)
                                 select new vm_Country
                                 {

                                     Id = Country.Id,
                                     Title = Country.Title,
                                 }).SingleOrDefault();
            return result;
        }
        public List<vm_Country> GetAll()
        {
            List<vm_Country> result = (from Country in _db.countries
                                       select new vm_Country
                                       {
                                           Id = Country.Id,
                                           Title = Country.Title
                                       }

                                     ).OrderBy(x => x.Title).ToList();
            return result;
        }
        public string CreateComment(int Id, vm_Country vm_Country)
        {
            string Comment = "";
            if (Id == 0)
            {
                Comment = "کشور:" + vm_Country.Title;
            }
            else
            {
                vm_Country = GetById(Id);
                Comment = "کشور :" + vm_Country.Title;
            }
            return Comment;
        }
        public string InsertDetail(vm_CountryDetail vm_detail)
        {
            using var trans = _db.Database.BeginTransaction();

            CountryDetailModel Detail = new CountryDetailModel();
            Detail.Title = vm_detail.Title;
            Detail.LanguageId = vm_detail.LanguageId;
            Detail.CountryId = vm_detail.CountryId;

            try
            {
                _db.Add(Detail);

                //#region Log              
                vm_detail.Log.Comment = "ثبت کشور جدید :: " + CreateDetailComment(0, vm_detail);
                vm_detail.Log.RefId = vm_detail.Id.ToString();
                vm_detail.Log.Type = Control.Enumuration.Type.Country;
                vm_detail.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(vm_detail.Log);
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
        public string UpdateDetail(vm_CountryDetail vm_detail)
        {
            using var trans = _db.Database.BeginTransaction();

            CountryDetailModel Detail = _db.countryDetails.Find(vm_detail.Id);
            Detail.Title = vm_detail.Title;
            Detail.LanguageId = vm_detail.LanguageId;
            try
            {
                _db.Entry(Detail).State = EntityState.Modified;
                //#region Log              
                vm_detail.Log.Comment = "ویرایش کشور  :: " + CreateDetailComment(vm_detail.Id, vm_detail);
                vm_detail.Log.RefId = vm_detail.Id.ToString();
                vm_detail.Log.Type = Control.Enumuration.Type.Country;
                vm_detail.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(_db);
                log.Insert(vm_detail.Log);
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
        public string DeleteDetail(vm_CountryDetail vm_detail)
        {
            using var trans = _db.Database.BeginTransaction();

            CountryDetailModel countryDetail = new CountryDetailModel();
            countryDetail.Id = vm_detail.Id;
            _db.Entry(countryDetail).State = EntityState.Deleted;
            try
            {
                //#region Log              
                vm_detail.Log.Comment = "حذف کشور  :: " + CreateDetailComment(vm_detail.Id, vm_detail);
                vm_detail.Log.RefId = vm_detail.Id.ToString();
                vm_detail.Log.Type = Control.Enumuration.Type.Country;
                vm_detail.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(vm_detail.Log);
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
        public vm_CountryDetail GetDetailById(int Id)
        {
            vm_CountryDetail result = (from CountryDetail in _db.countryDetails
                                       where
                                       CountryDetail.Id.Equals(Id)
                                       select new vm_CountryDetail
                                       {

                                           Id = CountryDetail.Id,
                                           CountryId = CountryDetail.CountryId,
                                           LanguageId = CountryDetail.LanguageId,
                                           Title = CountryDetail.Title
                                       }).SingleOrDefault();
            return result;
        }
        public List<vm_CountryDetail> GetDetailAll(int CountryId)
        {
            List<vm_CountryDetail> result = (from CountryDetailModel in _db.countryDetails
                                             join
                                             language in _db.Languages
                                             on
                                             CountryDetailModel.LanguageId equals language.Id
                                             where
                                             CountryDetailModel.CountryId.Equals(CountryId)
                                             select new vm_CountryDetail
                                             {
                                                 Id = CountryDetailModel.Id,
                                                 Title = CountryDetailModel.Title,
                                                 CountryId = CountryDetailModel.CountryId,
                                                 LanguageId = CountryDetailModel.LanguageId,
                                                 LanguageTitle = language.Title
                                             }

                                     ).ToList();
            return result;
        }
        public string CreateDetailComment(int Id, vm_CountryDetail vm_detail)
        {
            string Comment = "";
            if (Id == 0)
            {
                Comment = "کشور:" + vm_detail.Title;
            }
            else
            {
                vm_detail = GetDetailById(Id);
                Comment = "کشور :" + vm_detail.Title;
            }
            return Comment;
        }
        public int CheckDuplicate(int id)
        {
            return _db.countries.Where(x => x.Id.Equals(id)).Count();
        }





    }
}
