using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{
    [Table("UserInRole")]
    public class UserInRoleModel
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public Guid UserId { get; set; }

    }
}
