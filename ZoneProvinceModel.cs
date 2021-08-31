using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{
    [Table("ZoneProvince")]
    public class ZoneProvinceModel
    {
        [Key]
        public int Id { get; set; }
        public int ZoneId { get; set; }
        public int ProvinceId { get; set; }      
    }
}
