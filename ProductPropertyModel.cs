using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{
    [Table("ProductProperty")]
    public class ProductPropertyModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CategoryPropId { get; set; }
        public string Value { get; set; }
        public int LanguageId { get; set; }

    }
}
