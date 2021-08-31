using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{
    [Table("ProvinceDetail")]
    public class ProvinceDetailModel
    {[Key]
       public int Id { get; set; }
       public string Title { get; set; }
       public int Provinceid { get; set; }
        public int CountryId { get; set; }
    }
}
