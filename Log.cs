using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Business
{
    public class Log
    {
        private readonly Model.ECommerceDB db;
        public Log(Model.ECommerceDB _db)
        {
            db = _db;

        }
        public string Insert(ViewModel.vm_Log log)
        {
            if (log.UserType == Control.Enumuration.LogUserType.Users)
            {
                Business.User user = new User(db);
                ViewModel.vm_User userResult = user.GetByUserName(log.UserName);
                if (userResult != null)
                {
                    log.UserId = userResult.Id;
                    log.UserFullName = userResult.FirstName + " " + userResult.LastName;
                }
            }

            Model.LogModel logModel = new Model.LogModel();
            logModel.Id = Guid.NewGuid();
            logModel.Date = Control.Date.GeorgianToPersian();
            logModel.Hour = Control.Date.CurrentTime();
            logModel.UserId = log.UserId;
            logModel.UserFullName = log.UserFullName;
            logModel.Type =(int) log.Type;
            logModel.Operation = (int)log.Operation;
            logModel.RefId = log.RefId;
            logModel.Comment = log.Comment;
            logModel.UserType =(int) log.UserType;
            try
            {
                db.Logs.Add(logModel);
                return Control.Constant.SuccessResult;

            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }
    }
}
