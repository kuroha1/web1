using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;

namespace online.Helper
{
    public static class Utilities
    {
        public static string StripHTML(string input)
        {
            try
            {
                if (!string.IsNullOrEmpty(input))
                {
                    return Regex.Replace(input, "<.*?>", String.Empty);
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
        public static bool IsValidEmail(string email)
        {
            if(email.Trim().EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch { return false; }
        }

        public static int PAGE_SIZE = 20;
        public static void CreataIfMissing(string path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
                Directory.CreateDirectory(path);
        }

        public static string ToTitleCase(string str)
        {
            string result = str;
            if (!string.IsNullOrEmpty(str))
            {
                var words = str.Split(' ');
                for (int index = 0; index < words.Length; index++)
                {
                    var s = words[index];
                    if (s.Length > 0)
                    {
                        words[index] = s[0].ToString().ToUpper() + s.Substring(1);
                    }
                }
                result = string.Join(" ", words);
            }
            return result;
        }

        //public static bool IsInteger(string str)
        //{
        //    Regex regex = new Regex(@"^[0-9]+$");

        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(str))
        //        {
        //            return false;
        //        }
        //        if(!regex.IsMatch(str))
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch
        //    {

        //    }
        //}

        public static string GetRamdonkey(int length = 5)
        {
            string pattern = @"123456789zxcvbnmasdfghjklqwertyuiop[]{}:~!@#$%^&*()+";
            Random rd = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }
            return sb.ToString();
        }

        //public static string SEOUrl (string str)
        //{

        //}

        public static async Task<String> UploadFile(Microsoft.AspNetCore.Http.IFormFile file, string sDiretory, string newname)
        {
            try
            {
                if (newname == null) newname = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDiretory);
                CreataIfMissing(path);
                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDiretory, newname);
                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
                var fileExit = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExit.ToLower()))
                {
                    return null;
                }
                else
                {
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return newname;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string SEOUrl(string url )
        {
            url = url.ToLower();
            url = Regex.Replace(url, @"[áàảạãâấầẩẫậắằẳẵặ]", "a");
            url = Regex.Replace(url, @"[éèẻẽẹếềểệễ]", "e");
            url = Regex.Replace(url, @"[óòỏõọớờởỡợốồổỗộ]", "o");
            url = Regex.Replace(url, @"[ùúủũụứừửữự]", "u");
            url = Regex.Replace(url, @"[íìịĩỉ]", "i");
            url = Regex.Replace(url, @"[ýỳỷỹỵ]", "y");
            url = Regex.Replace(url, @"[đ]", "d");

            url = Regex.Replace(url.Trim(), @"[^0-9a-z-\s]", "").Trim();

            url = Regex.Replace(url.Trim(), @"\s+", "-");

            url = Regex.Replace(url, @"\s", "-");
            while(true)
            {
                if(url.IndexOf("--") != -1)
                {
                    url = url.Replace("--", "-");
                }
                else
                {
                    break;
                }
            }
            return url;
        }

    }
}
