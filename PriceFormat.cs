using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_API.Control
{
    public class PriceFormat
    {

        public static string FormatPrice(string Price)
        {
            string FinalPrice = string.Empty;
            byte j = 0;
            for (int i = Price.Length;i>0 ; i--)
            {
                FinalPrice = Price[i-1] + FinalPrice;
                j++;
                if (j == 3 && i != 1)
                {
                    FinalPrice = ',' + FinalPrice;
                    j = 0;
                }
            }
            return FinalPrice;
        }

        

        private static string ToText(long n)
        {
            string output = string.Empty; ;

            if (n == 0 || n<0)
                output = "";
            else if (n >= 1 && n <= 19)
            {
                string[] arr ={ "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه", "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
                output = arr[n - 1].ToString() + " ";
            }
            else if (n >= 20 && n <= 99)
            {
                string[] arr ={ "بیست و", "سی و", "چهل و", "پنجاه و", "شصت و", "هفتاد و", "هشتاد و", "نود و" };
                string[] arr1 ={ "بیست ", "سی ", "چهل ", "پنجاه ", "شصت ", "هفتاد ", "هشتاد ", "نود " };
                if (n - ((n / 10) * 10) != 0)
                    output = arr[n / 10 - 2].ToString() + " " + ToText(n - ((n / 10) * 10));
                else
                    output = arr1[n / 10 - 2].ToString() + " " + ToText(n - ((n / 10) * 10));

            }
            else if (n >= 100 && n <= 999)
            {
                string[] arr ={ "یکصد و", "دویست و", "سیصد و", "چهارصد و", "پانصد و", "ششصد و", "هفتصد و", "هشتصد و", "نهصد و" };
                string[] arr1 ={ "یکصد ", "دویست ", "سیصد ", "چهارصد ", "پانصد ", "ششصد ", "هفتصد ", "هشتصد ", "نهصد " };
                if (n - ((n / 100) * 100) != 0)
                    output = arr[n / 100 - 1].ToString() + " " + ToText(n - ((n / 100) * 100));
                else
                    output = arr1[n / 100 - 1].ToString() + " " + ToText(n - ((n / 100) * 100));
            }
            else if (n >= 1000 && n <= 1999)
                if (n - ((n / 1000) * 1000) != 0)
                    output = "یک هزار و" + " " + ToText(n - ((n / 1000) * 1000));
                else
                    output = "یک هزار " + " " + ToText(n - ((n / 1000) * 1000));

            else if (n >= 2000 && n <= 999999)
                if (n - ((n / 1000) * 1000) != 0)
                    output = ToText(n / 1000) + "هزار و" + " " + ToText(n - ((n / 1000) * 1000));
                else
                    output = ToText(n / 1000) + "هزار " + " " + ToText(n - ((n / 1000) * 1000));

            else if (n >= 1000000 && n <= 1999999)
                if (n - ((n / 1000000) * 1000000) != 0)
                    output = "یک میلیون و" + " " + ToText(n - ((n / 1000000) * 1000000));
                else
                    output = "یک میلیون " + " " + ToText(n - ((n / 1000000) * 1000000));

            else if (n >= 2000000 && n <= 999999999)
                if (n - ((n / 1000000) * 1000000) != 0)
                    output = ToText(n / 1000000) + "میلیون و" + " " + ToText(n - ((n / 1000000) * 1000000));
                else
                    output = ToText(n / 1000000) + "میلیون " + " " + ToText(n - ((n / 1000000) * 1000000));

            else if (n >= 1000000000 && n <= 1999999999)
                if (n - ((n / 1000000000) * 1000000000) != 0)
                    output = "یک میلیارد و" + " " + ToText(n - ((n / 1000000000) * 1000000000));
                else
                    output = "یک میلیارد " + " " + ToText(n - ((n / 1000000000) * 1000000000));

            else
                if (n - ((n / 1000000000) * 1000000000) != 0)
                    output = ToText(n / 1000000000) + "میلیارد و" + " " + ToText(n - ((n / 1000000000) * 1000000000));
                else
                    output = ToText(n / 1000000000) + "میلیارد " + " " + ToText(n - ((n / 1000000000) * 1000000000));
            return output;

        }

        public static string NumberConvertor(string Number)
        {
            string temp = string.Empty;

            try
            {
                temp = ToText(Convert.ToInt64(Number));
                if (Convert.ToInt64(Number) - ((Convert.ToInt64(Number) / 10) * 10) == 0)
                    temp = temp.Substring(0, temp.Length - 2);
                //temp += " ریال ";
                temp += "  ";

            }
            catch { }
            return temp;
        }

    }
}
