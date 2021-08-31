using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Business
{
    public class Role
    {
        private readonly Model.ECommerceDB _db;
        public Role(Model.ECommerceDB db)
        {
            _db = db;
        }

        public string Insert(ViewModel.vm_Role role)
        {
            Model.RoleModel roleModel = new Model.RoleModel();
            roleModel.IsDeleted = role.IsDeleted;
            roleModel.Title = role.Title;
            try
            {
                _db.Roles.Add(roleModel);
                Business.Log log = new Business.Log(_db);
                role.Log.Comment = CreateComment(role.Id, role.Title);
                role.Log.Type = Control.Enumuration.Type.Role;
                role.Log.Operation = Control.Enumuration.Operation.Insert;
                role.Log.RefId = role.Id.ToString();

                log.Insert(role.Log);

                _db.SaveChanges();
                return Control.Constant.SuccessResult;

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string Update(ViewModel.vm_Role role)
        {
            Model.RoleModel roleModel = new Model.RoleModel();
            roleModel.IsDeleted = role.IsDeleted;
            roleModel.Title = role.Title;
            roleModel.Id = role.Id;
            try
            {
                _db.Entry(roleModel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                Business.Log log = new Business.Log(_db);
                role.Log.Comment = CreateComment(role.Id, role.Title);
                role.Log.Type = Control.Enumuration.Type.Role;
                role.Log.Operation = Control.Enumuration.Operation.Update;
                role.Log.RefId = role.Id.ToString();
                log.Insert(role.Log);

                _db.SaveChanges();
                return Control.Constant.SuccessResult;

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string Delete(ViewModel.vm_Role role)
        {
            Model.RoleModel roleModel = new Model.RoleModel();
            roleModel.IsDeleted = role.IsDeleted;
            roleModel.Title = role.Title;
            roleModel.Id = role.Id;
            try
            {
                _db.Entry(roleModel).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

                Business.Log log = new Business.Log(_db);
                role.Log.Comment = CreateComment(role.Id, role.Title);
                role.Log.Type = Control.Enumuration.Type.Role;
                role.Log.Operation = Control.Enumuration.Operation.Delete;
                role.Log.RefId = role.Id.ToString();
                log.Insert(role.Log);

                _db.SaveChanges();
                return Control.Constant.SuccessResult;

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }



        public ViewModel.vm_Role GetById(int Id)
        {
            ViewModel.vm_Role result = (from role in _db.Roles
                                        where
                                        role.Id.Equals(Id)
                                        select new ViewModel.vm_Role
                                        {
                                            Id = role.Id,
                                            IsDeleted = role.IsDeleted,
                                            Title = role.Title
                                        }).SingleOrDefault();
            return result;
        }

        public List<ViewModel.vm_Role> GetAll(int IsDeleted)
        {
            List<ViewModel.vm_Role> result = (from role in _db.Roles
                                              where
                                              role.IsDeleted.Equals(IsDeleted)
                                              select new ViewModel.vm_Role
                                              {
                                                  IsDeleted = role.IsDeleted,
                                                  Id = role.Id,
                                                  Title = role.Title
                                              }
                                             ).ToList();
            return result;
        }

        public string CreateComment(int Id, string Title)
        {
            string Comment = "";
            if (Id == 0)
            {
                Comment = "Title : " + Title;
            }
            else
            {
                ViewModel.vm_Role Role = GetById(Id);
                Comment = "Title : " + Role.Title;
            }
            return Comment;
        }

        public List<ViewModel.vm_RolePermission> GetPermissionByRoleId(int RoleId)
        {
            List<ViewModel.vm_RolePermission> result = (from permission in _db.RolePermissions
                                                        where
                                                        permission.RoleId.Equals(RoleId)
                                                        select new ViewModel.vm_RolePermission
                                                        {
                                                            RoleId = permission.RoleId,
                                                            Id = permission.Id,
                                                            Operation = permission.Operation,
                                                            Page = permission.Page
                                                        }).ToList();
            return result;
        }

        public int InsertPermission(ViewModel.vm_RolePermission role)
        {
            Model.RolePermissionModel perModel = new Model.RolePermissionModel();
            ViewModel.vm_RolePermission Permissions = GetPermission(role.RoleId, role.Page, role.Operation);
            if (Permissions==null)
            {
                perModel.IsDeleted = 0;
                perModel.Operation = (int)role.Operation;
                perModel.Page = (int)role.Page;
                perModel.RoleId = role.RoleId;
                _db.RolePermissions.Add(perModel);
                try
                {
                    _db.SaveChanges();
                    return perModel.Id;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                perModel.Id = Permissions.Id;
                _db.Entry(perModel).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                try
                {
                    _db.SaveChanges();
                    return -4;
                }
                catch (Exception ex)
                {

                    return -2;
                }

            }
        }

        public ViewModel.vm_RolePermission GetPermission(int RoleId, int Page, int Operation)
        {
            ViewModel.vm_RolePermission result = (from role in _db.RolePermissions
                                                  where
                                                  role.RoleId.Equals(RoleId) &&
                                                  role.Page.Equals(Page) &&
                                                  role.Operation.Equals(Operation)
                                                  select new ViewModel.vm_RolePermission
                                                  {
                                                      Id = role.Id,
                                                      Operation = role.Operation,
                                                      Page = role.Page,
                                                      RoleId = role.RoleId
                                                  }).SingleOrDefault();
            return result;
        }

        public int InsertUserInRole(ViewModel.vm_UserInRole userInRole)
        {
            Model.UserInRoleModel RoleModel = new Model.UserInRoleModel();
            RoleModel.RoleId = userInRole.RoleId;
            RoleModel.UserId = userInRole.UserId;
            try
            {
                _db.UserInRoles.Add(RoleModel);
                return RoleModel.Id;
            }
            catch 
            {

                throw;
            }
        }

        public string DeleteUserInRoleByUserId(Guid UserId)
        {
            List<Model.UserInRoleModel> Result = _db.UserInRoles.Where(x => x.UserId.Equals(UserId)).ToList();
            foreach (var item in Result)
            {
                _db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }
            return Control.Constant.SuccessResult;
        }

        public List<int> GetByUserId(Guid UserId)
        {
            return  _db.UserInRoles.Where(x => x.UserId.Equals(UserId)).Select(x => x.RoleId).ToList();
        }
    }
}
