using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{[Table("CategoryPropDetail")]
    public class CategoryPropDetailModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CategoryPropId { get; set; }
        public string Title { get; set; }
        public int IsForFilter { get; set; }
        public int Order { get; set; }
        public int  LanguageId { get; set; }
    }
}
