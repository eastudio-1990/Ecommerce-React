using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{[Table("Product")]
    public class ProductModel
    {[Key]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int CategoryId { get; set; }     
        public string Title { get; set; }
        public double Weigth { get; set; }
        public double Width { get; set; }
        public double Heigth { get; set; }
        public double Length { get; set; }
        public double PackageHeigth { get; set; }
        public double PackageWidth { get; set; }
        public double PackageLength { get; set; }
        public double PackageWeigth { get; set; }
        public string PublishDate { get; set; }
        public int PublishState { get; set; }
        public int BuyCount { get; set; }
        public int MaxBasketCount { get; set; }
        public int IsDownloadable { get; set;}
        public int HomeShow { get; set; }
        public int MaterialId { get; set; }

        public int WeightUnitId { get; set; }

    }
}
