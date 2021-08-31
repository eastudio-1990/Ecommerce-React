using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{
    [Table("VarietyDetail")]
    public class VarietyDetailModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int VarietyId { get; set; }

    }
}
