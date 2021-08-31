using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{
    public class CurrencyConvertor
    {
        public int Id { get; set; }
        public int TargetCurrenyId { get; set; }
        public decimal Value { get; set; }
        public string Date { get; set; }

    }
}
