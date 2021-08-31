using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{
    [Table("Log")]
    public class LogModel
    {
        [Key]
        public Guid Id { get; set; }
        public int Type { get; set; }
        public string Comment { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }
        public string UserFullName { get; set; }
        public int Operation { get; set; }
        public string IP { get; set; }
        public string RefId { get; set; }
        public Guid UserId { get; set; }
        public int UserType { get; set; }
    }
}
