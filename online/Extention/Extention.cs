using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace online.Extention
{
    public static class Extention
    {
        public static string ToVND(this double DonGia)
        {
            return DonGia.ToString("#,##0") + "d";
        }

        public static string ToTitleCase(string str)
        {
            string result = str;
            if( string.IsNullOrEmpty(str) )
            {
                var words = str.Split(' ');
                for (int index = 0; index <words.Length; index++)
                {
                    var s = words[index];
                    if (s.Length > 0 )
                    {
                        words[index] = s[0].ToString().ToUpper() + s.Substring(1);
                    }
                }
                result = string.Join(" ", words);
            }
            return result;
        }

        public static string ToUrlFriendly(this string url)
        {
            var result = url.ToLower().Trim();
            result = Regex.Replace(result, "áàảạãâấầẩẫậắằẳẵặ", "a");
            result = Regex.Replace(result, "éèẻẽẹếềểệễ", "e");
            result = Regex.Replace(result, "óòỏõọớờởỡợốồổỗộ", "o");
            result = Regex.Replace(result, "ùúủũụứừửữự", "u");
            result = Regex.Replace(result, "íìịĩỉ", "i");
            result = Regex.Replace(result, "ýỳỷỹỵ", "y");
            result = Regex.Replace(result, "đ", "d");
            result = Regex.Replace(result, "[^a-z0-9-]", "");
            result = Regex.Replace(result, "(-)+", "-");

            return result;
        }
    }
}
