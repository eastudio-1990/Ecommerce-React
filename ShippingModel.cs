using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{
    [Table("Shipping")]
    public class ShippingModel
    {
        [Key]
        public int Id { get; set; }        
        public string MinWeight { get; set; }
        public  string MaxWeight { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public int ZoneId { get; set; }
        public string Date { get; set; }
        public Int64 Price { get; set; }
    }
}

