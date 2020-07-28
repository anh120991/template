using HDBH.Core.Identity.Hasher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Core.Identity
{
    public class PasswordHashHelper
    {
        private static PasswordHashHelper _instance = new PasswordHashHelper();

        public static PasswordHashHelper Current
        {
            get
            {
                return _instance == null ? _instance = new PasswordHashHelper() : _instance;
            }
        }

        public string HashPassword(string Password)
        {
            IPasswordHasher hashdata = new PasswordHasher();
            return hashdata.HashPassword(Password);
        }

        public bool VerifyPassword(string Password, string PasswordHashed)
        {
            IPasswordHasher hashdata = new PasswordHasher();
            return hashdata.VerifyHashedPassword(PasswordHashed, Password);
        }
    }
}
