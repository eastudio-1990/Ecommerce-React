using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Business
{
    public class User
    {
        private readonly Model.ECommerceDB db;
        public User(Model.ECommerceDB _db)
        {
            db = _db;
        }

        public ViewModel.vm_User GetById(Guid Id)
        {
            ViewModel.vm_User result = (from user in db.Users
                                        where
                                        user.Id.Equals(Id)
                                        select new ViewModel.vm_User
                                        {
                                            FirstName = user.FirstName,
                                            Id = user.Id,
                                            IsActive = user.IsActive,
                                            IsDeleted = user.IsDeleted,
                                            LastName = user.LastName,
                                            UserName = user.UserName,
                                            Password=user.Password
                                        }).SingleOrDefault();
            result.Password = Control.StringCipher.Decrypt(result.Password);
            Business.Role role = new Role(db);
            result.Roles = role.GetByUserId(Id);
            return result;
        }
        public ViewModel.vm_User GetByUserName(string UserName)
        {
            ViewModel.vm_User result = (from user in db.Users
                                        where
                                        user.UserName.Equals(UserName)
                                        select new ViewModel.vm_User
                                        {
                                            FirstName = user.FirstName,
                                            Id = user.Id,
                                            IsActive = user.IsActive,
                                            IsDeleted = user.IsDeleted,
                                            LastName = user.LastName,
                                            UserName = user.UserName
                                        }).SingleOrDefault();
            return result;
        }
        public string Login(string UserName, string password)
        {
            ViewModel.vm_User result = (from user in db.Users
                                      where
                                      user.UserName.Equals(UserName)
                                      select new ViewModel.vm_User
                                      {
                                          FirstName = user.FirstName,
                                          UserName = user.UserName,
                                          Id = user.Id,
                                          IsActive = user.IsActive,
                                          IsDeleted = user.IsDeleted,
                                          LastName = user.LastName,
                                          Password = user.Password

                                      }).SingleOrDefault();
            if (result == null)
            {
                return "Invalid";
            }
            if (Control.StringCipher.Decrypt(result.Password) == password)
            {
                return Control.Constant.SuccessResult;
            }
            else
            {
                return "Invalid";
            }
        }
        public List<ViewModel.vm_User> Search(string Search)
        {
            if (Search == null)
                Search = "";
            List<ViewModel.vm_User> result = (from user in db.Users
                                              where
                                              user.FirstName.Contains(Search) ||
                                              user.LastName.Contains(Search) ||
                                              user.UserName.Contains(Search)
                                              select new ViewModel.vm_User {
                                                  FirstName = user.FirstName,
                                                  UserName = user.UserName,
                                                  Id = user.Id,
                                                  IsActive = user.IsActive,
                                                  IsDeleted = user.IsDeleted,
                                                  LastName = user.LastName
                                              }).ToList();
            return result;
        }
        public string Insert(ViewModel.vm_User user)
        {
            Model.UserModel userModel = new Model.UserModel();
            userModel.Id = Guid.NewGuid();
            userModel.FirstName = user.FirstName;
            userModel.LastName = user.LastName;
            userModel.UserName = user.UserName;
            userModel.Password =Control.StringCipher.Encrypt( user.Password);
            userModel.IsActive = user.IsActive;

            try
            {
                db.Users.Add(userModel);
                //#region Log              
                user.Log.Comment = "ثبت کاربر جدید :: " + CreateComment(Guid.Empty, user);
                user.Log.RefId = userModel.Id.ToString();
                user.Log.Type = Control.Enumuration.Type.User;
                user.Log.Operation = Control.Enumuration.Operation.Insert;
                Business.Log log = new Log(db);
                log.Insert(user.Log);
                //#endregion

                #region RolePermission
                    Business.Role role = new Role(db);
                foreach (var item in user.Roles)
                {

                    ViewModel.vm_UserInRole userInRole = new ViewModel.vm_UserInRole();
                    userInRole.RoleId = Convert.ToInt32(item);
                    userInRole.UserId = userModel.Id;
                    role.InsertUserInRole(userInRole);
                }
                #endregion

                db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public string Update(ViewModel.vm_User user)
        {
            Model.UserModel userModel = new Model.UserModel();
            userModel.Id = user.Id;
            userModel.FirstName = user.FirstName;
            userModel.LastName = user.LastName;
            userModel.UserName = user.UserName;
            userModel.Password = Control.StringCipher.Encrypt(user.Password);
            userModel.IsActive = user.IsActive;

            try
            {
                db.Entry(userModel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                //#region Log              
                user.Log.Comment = "ویرایش کاربر :: " + CreateComment(Guid.Empty, user);
                user.Log.RefId = userModel.Id.ToString();
                user.Log.Type = Control.Enumuration.Type.User;
                user.Log.Operation = Control.Enumuration.Operation.Update;
                Business.Log log = new Log(db);
                log.Insert(user.Log);
                //#endregion
                    Business.Role role = new Role(db);
                role.DeleteUserInRoleByUserId(user.Id);
                #region RolePermission
                foreach (var item in user.Roles)
                {

                    ViewModel.vm_UserInRole userInRole = new ViewModel.vm_UserInRole();
                    userInRole.RoleId = Convert.ToInt32(item);
                    userInRole.UserId = userModel.Id;

                    role.InsertUserInRole(userInRole);

                }
                #endregion

                db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public string CreateComment(Guid Id, ViewModel.vm_User User)
        {
            string Comment = "";
            if (Id == Guid.Empty)
            {
                Comment = "نام  : " + User.FirstName + " نام خانوادگی : " + User.LastName + " نام کاربری : " + User.UserName;
            }
            else
            {
                User = GetById(Id);
                Comment = "نام : " + User.FirstName + " نام خانوادگی : " + User.LastName + " نام کاربری : " + User.UserName;
            }
            return Comment;
        }
        public string Delete(ViewModel.vm_User user)
        {
            Model.UserModel userModel = new Model.UserModel();
            userModel.Id = user.Id;

            db.Entry(userModel).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            #region Log
            user.Log.Comment = "حذف کاربر :: " + CreateComment(user.Id, user);
            user.Log.RefId = user.Id.ToString();
            user.Log.Type = Control.Enumuration.Type.User;
            user.Log.Operation = Control.Enumuration.Operation.Delete;
            Business.Log log = new  Log(db);
            log.Insert(user.Log);
            #endregion

            try
            {
                db.SaveChanges();
                return Control.Constant.SuccessResult;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public  bool CheckPermission(Guid UserId, int Page, int Operation)
        {

            int result = (from userInRole in db.UserInRoles
                          join
                          rolePermission in db.RolePermissions
                          on
                          userInRole.RoleId equals rolePermission.RoleId
                          where
                          rolePermission.Page.Equals(Page) &&
                          rolePermission.Operation.Equals(Operation) &&
                          rolePermission.IsDeleted.Equals(0)
                          select new ViewModel.vm_UserInRole { 
                             Id=userInRole.Id
                          }

                        ).ToList().Count();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
          
        }
    }
}
