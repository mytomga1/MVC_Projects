using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaoOnline.Helpers
{
    public static class Utilities
    {
        public static int GetFileSize(string urlfile) { 
        
            int sizeFile = 0;
            try
            {

                Uri uriPath = new Uri(urlfile);
                var webRequest = HttpWebRequest.Create(uriPath);
                webRequest.Method = "HEAD";
                using (var webResponse = webRequest.GetResponse()) {

                    var fileSize = webResponse.Headers.Get("Content-Lenght");
                    var fileSizeInMegaByte = Math.Round(Convert.ToDouble(fileSize) / 1024.0 / 1024.0, 2);
                    sizeFile = Convert.ToInt32(fileSizeInMegaByte);
                    //return fileSizeInMegaByte + " MB";
                }

            }catch {

                return sizeFile;
            }
            return sizeFile;
        }

        public static string ToTitleCase(string str)
        {

            string result = str;
            if (!string.IsNullOrEmpty(str)) { 
            
                var words = str.Split(' ');
                for (int index = 0; index < words.Length; index++) { 
                
                    var s = words[index];
                    if (s.Length > 0) { 
                    
                        words[index] = s[0].ToString().ToUpper() + s.Substring(1);
                    }                    
                }
                result = string.Join(" ", words);
            }
            return result;
        }

        public static bool KiemTraHoVaTen(string input) { 
        
            bool result = false;
            try {

                Match match = Regex.Match(input, @"(\d+)");
                if (match.Success) { 
                
                    var numer = int.Parse(match.Groups[1].Value);
                    return true;
                }
            } catch {
                result = true;
            }
            return result;
        }

        public static bool IsInteger(string str) {

            Regex regex = new Regex(@"^[0-9]+$");

            try {

                if (string.IsNullOrWhiteSpace(str))
                {

                    return false;
                }
                else if (!regex.IsMatch(str)) { 
                
                    return false;
                }

                return true;

            }catch { 
            
            }

            return false;
        }

        public static bool IsNumber(this string aNumber) {

            BigInteger temp_big_int;
            var is_number = BigInteger.TryParse(aNumber, out temp_big_int);
            return is_number;
        }

        public static string GetDomain(string url) {

            string host = "";
            try {

                if (!string.IsNullOrEmpty(url)) { 
                
                    Uri myUri = new Uri(url.Trim().ToLower());
                    host = myUri.Host.ToLower();
                }
            }catch {

                host = "";
            }
            return host;
        }

        public static string RemoveLinks(string html) {

            try {

                if (!string.IsNullOrEmpty(html)) {

                    Regex r = new Regex(@"\<a href=.*?\>");
                    html = r.Replace(html, "");
                    r = new Regex(@"\</a\>");
                    html = r.Replace(html, "");
                }

                return html;
            } catch {

                return html;
            }
        }

        public static string StripHTML(string input) {

            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static string Right(string value, int lenght) {

            return value.Substring(value.Length - lenght);
        }

        public static int PAGE_SIZE = 20; // mục đích phân trang VD 1trang có 20 bài

        public static string SEOUrl(string url)
        {
            url = url.ToLower();
            url = Regex.Replace(url, @"[áàạảãâấầậẩẫăắằặẳẵ]", "a");
            url = Regex.Replace(url, @"[éèẹẻẽêếềệểễ]", "e");
            url = Regex.Replace(url, @"[óòọỏõôốồộổỗơớờợởỡ]", "o");
            url = Regex.Replace(url, @"[íìịỉĩ]", "i");
            url = Regex.Replace(url, @"[ýỳỵỉỹ]", "y");
            url = Regex.Replace(url, @"[úùụủũưứừựửữ]", "u");
            url = Regex.Replace(url, @"[đ]", "d");

            //2. Chỉ cho phép nhận:[0-9a-z-\s]
            url = Regex.Replace(url.Trim(), @"[^0-9a-z-\s]", "").Trim();
            //xử lý nhiều hơn 1 khoảng trắng --> 1 kt
            url = Regex.Replace(url.Trim(), @"\s+", "-");
            //thay khoảng trắng bằng -
            url = Regex.Replace(url, @"\s", "-");
            while (true)
            {
                if (url.IndexOf("--") != -1)
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

        public static string GetRandomKey(int lenght = 5) { // tự động sinh ra 1 chuổi 5 ký tự random với mục đích mã hoá mk
            // (password + salt) ==> md5 ==> chuyển thành chuổi string =>> lưu vào db
            //chuổi mẫu (pattern)
            string pattern = @"0123456789zxcvbnmasdfghjklqwertyuiop[]{}:~!@#$%^&*()+";
            Random rd = new Random();

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < lenght; i++) {

                sb.Append(pattern[rd.Next(0,pattern.Length)]);
            }

            return sb.ToString();
        }

        public static async Task<string> UploadFile(Microsoft.AspNetCore.Http.IFormFile file, string sDirectory, string newname = null)
        {
            try
            {
                if (newname == null) newname = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory);
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt.ToLower())) // Khác các file định nghĩa
                {
                    return null;
                }
                else
                {
                    string fullPath = path + "//" + newname;
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return newname;
                }
            }
            catch
            {
                return null;
            }
        }

        //public static async Task<string> UploadFile(Microsoft.AspNetCore.Http.IFormFile file, string sDirectory, string newname = null)
        //{
        //    try
        //    {
        //        if (newname == null) newname = file.FileName;
        //        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory);
        //        CreateIfMissing(path);
        //        string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory, newname);
        //        var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
        //        var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
        //        if (!supportedTypes.Contains(fileExt.ToLower())) /// Khác các file định nghĩa
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            using (var stream = new FileStream(pathFile, FileMode.Create))
        //            {
        //                await file.CopyToAsync(stream);
        //            }
        //            return newname;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public static async Task UploadFileFolderImages(Microsoft.AspNetCore.Http.IFormFile file, string newname) {

            try {

                if (string.IsNullOrEmpty(newname)) newname = file.FileName;

                var tetx = Directory.GetCurrentDirectory();
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", newname);
                    string folderImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", newname);

                if (!System.IO.Directory.Exists(folderImage)) {
                
                    System.IO.Directory.CreateDirectory(folderImage);
                }

                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);

                if (supportedTypes.Contains(fileExt.ToLower())) 
                {

                    using (var stream = new FileStream(path, FileMode.Create)) { 
                    
                        await file.CopyToAsync(stream);
                    }
                }
            } catch { }
        }

        public static async Task<string> UploadFileDocument(Microsoft.AspNetCore.Http.IFormFile file, string sDirectory, string newname = null)
        {
            try
            {
                if (newname == null) newname = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "5771fa5c1406cabdc911e584df9ab2d","tai-lieu-m", sDirectory);
                string path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "5771fa5c1406cabdc911e584df9ab2d","tai-lieu-m", sDirectory);
                if (!System.IO.Directory.Exists(path2))
                {
                    System.IO.Directory.CreateDirectory(path2);
                }
                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt.ToLower())) // Khác các file định nghĩa
                {
                    return null;
                }
                else
                {
                    using (var stream = new FileStream(path, FileMode.Create)) {

                        await file.CopyToAsync(stream);
                    }
                    return newname;
                }
            }
            catch
            {
                return null;
            }
        }

        public static void Great_folder(string link) { 
        
            string folder = link;
            if (!Directory.Exists(@folder)) { 
            
                Directory.CreateDirectory(@folder);
            }
        }

        public static string RandTime() { 
        
            Random r = new Random();
            string rand = DateTime.Now.ToString().Replace("/", ":").Replace(":", "-").Replace(" ", "-").ToLower();
            rand = rand + r.Next(100, 999);
            return rand;
        }

        public static bool IvValidEmail(string email) {

            try {

                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;

            } catch {
            
                return false;
            }
        }

        public static List<string> ExtractLink(string html) { 
        
            List<string> list = new List<string>();
            Regex regex = new Regex("(?:href|src)=[\"|']?(.*?)[\"|'|>]+");

            if (regex.IsMatch(html)) {

                foreach (Match match in regex.Matches(html)) { 
                
                    list.Add(match.Value);
                }
            }
            return list;
        }

    }
}

