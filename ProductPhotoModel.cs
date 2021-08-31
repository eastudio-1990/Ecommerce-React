using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{[Table("ProductPhoto")]
    public class ProductPhotoModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Order { get; set; } 
    }
}
