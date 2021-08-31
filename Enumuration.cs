using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Control
{
    public class Enumuration
    {
        public enum Operation:int
        {
            Insert = 0,
            Update = 1,
            Delete = 2,
            Select = 3,// انتخاب
            Search = 4,//جستجو
            Show = 5,//نمایش صفحه
            RolePermission = 6,// تعیین سطوح دسترسی
            Print = 7,//پرینت
            Download = 12,// دانلود
            Confirm = 13,//تایید با رد
        }

        public enum Type:int
        {
            Role = 0,
            User = 1,
            Language=2,
            WeightUnit=3,
            Country=4,
            Province=5,
            City=6,
            Shipping=7,
            Material=8,
            Variety=9,
            Category=10,
            CategoryProp = 11,
            Product=12,
            ProductProperty=13,
            ProductPhoto=14,
            ProductVariety=15,
            Zone=16,
            CurrencyConvertor=17
        }

        public enum LogUserType:int
        {
            Users = 0,//کاربران
            Profile=1,//پروفایل
        }
    }
}
