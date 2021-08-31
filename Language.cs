using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Business
{
    public class Language
    {
        private readonly Model.ECommerceDB _db;

        public Language(Model.ECommerceDB db)
        {
            _db = db;
        }
        public string Insert(ViewModel.vm_Language lang)
        {
            Model.LanguageModel langModel = new Model.LanguageModel();
            langModel.Currency = lang.Currency;
            langModel.IsDefault = lang.IsDefault;
            langModel.RTL = lang.RTL;
            langModel.Title = lang.Title;

            try
            {

                _db.Languages.Add(langModel);
                //#region Log              
                lang.Log.Comment = "ثبت زبان جدید :: " + CreateComment(0, lang);
                lang.Log.RefId = langModel.Id.ToString();
                lang.Log.Type = Control.Enumuration.Type.Language;
                lang.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(_db);
                log.Insert(lang.Log);
                //#endregion
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string Update(ViewModel.vm_Language lang)
        {
            Model.LanguageModel langModel = new Model.LanguageModel();
            langModel.Currency = lang.Currency;
            langModel.IsDefault = lang.IsDefault;
            langModel.RTL = lang.RTL;
            langModel.Title = lang.Title;
            langModel.Id = lang.Id;
            try
            {

                _db.Entry(langModel).State = EntityState.Modified;
                //#region Log              
                lang.Log.Comment = "ویرایش زبان  :: " + CreateComment(langModel.Id, lang);
                lang.Log.RefId = langModel.Id.ToString();
                lang.Log.Type = Control.Enumuration.Type.Language;
                lang.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(_db);
                log.Insert(lang.Log);
                //#endregion
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string Delete(ViewModel.vm_Language lang)
        {
            Model.LanguageModel langModel = new Model.LanguageModel();
            langModel.Id = lang.Id;
            _db.Entry(langModel).State = EntityState.Deleted;
            try
            {
                //#region Log              
                lang.Log.Comment = "حذف زبان  :: " + CreateComment(langModel.Id, lang);
                lang.Log.RefId = langModel.Id.ToString();
                lang.Log.Type = Control.Enumuration.Type.Language;
                lang.Log.Operation = Control.Enumuration.Operation.Delete;
                Business.Log log = new Log(_db);
                log.Insert(lang.Log);
                //#endregion
                _db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public ViewModel.vm_Language GetById(int Id)
        {
            ViewModel.vm_Language result = (from language in _db.Languages
                                            where
                                            language.Id.Equals(Id)
                                            select new ViewModel.vm_Language
                                            {
                                                Currency = language.Currency,
                                                Id = language.Id,
                                                IsDefault = language.IsDefault,
                                                RTL = language.RTL,
                                                Title = language.Title
                                            }).SingleOrDefault();
            return result;
        }

        public List< ViewModel.vm_Language> GetAll()
        {
           List< ViewModel.vm_Language> result = (from language in _db.Languages
                                          
                                            select new ViewModel.vm_Language
                                            {
                                                Currency = language.Currency,
                                                Id = language.Id,
                                                IsDefault = language.IsDefault,
                                                RTL = language.RTL,
                                                Title = language.Title
                                            }).ToList();
            return result;
        }
        public string CreateComment(int Id, ViewModel.vm_Language language)
        {
            string Comment = "";
            if (Id == 0)
            {
                Comment = "زبان : " + language.Title;
            }
            else
            {
                language = GetById(Id);
                Comment = "زبان : " + language.Title;
            }
            return Comment;
        }

        public ViewModel.vm_Language GetDefault()
         {
            ViewModel.vm_Language result = (from language in _db.Languages
                                            where 
                                            language.IsDefault.Equals(1)
                                            select new ViewModel.vm_Language
                                            {
                                                Currency = language.Currency,
                                                Id = language.Id,
                                                IsDefault = language.IsDefault,
                                                RTL = language.RTL,
                                                Title = language.Title
                                            }).SingleOrDefault();
            return result;
        }


    }
}
