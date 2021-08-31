using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{
    [Table("RolePermission")]
    public class RolePermissionModel
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int Page { get; set; }
        public int Operation { get; set; }
        public int IsDeleted { get; set; }
    }
}
