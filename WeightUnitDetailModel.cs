using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{
    [Table("WeightUnitDetail")]
    public class WeightUnitDetailModel
    {
        [Key]
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public int WeightUnitId { get; set; }
        public string Title { get; set; }

    }
}
