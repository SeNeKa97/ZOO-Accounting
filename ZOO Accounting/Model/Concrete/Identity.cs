using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Security.Cryptography;

namespace ZOO_Accounting.Model.Concrete
{
    public static class Identity
    {
        private static bool isAuthorized = false;

        public static bool Login(string UserName, string Password)
        {
            MD5 crypt = MD5.Create();
            string passHash = GetMd5Hash(crypt, Password);
            bool result = false;

            using (EFDbContext context = new EFDbContext()) {
                if (context.Users.Where(x => (x.Login == UserName) && (x.PasswordHash == passHash)).Count() > 0) {
                    isAuthorized = true;
                    result = true;
                }
            }
            
            return result;
        }
        public static void Logout() {
            isAuthorized = false;
        }

        static string GetMd5Hash(MD5 md5Hash, string input) 
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }

        public static bool IsAuthorized() {
            return isAuthorized;
        }
    }
}
