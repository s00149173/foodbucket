using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

namespace FoodBucket.Models
{   
    public class User
    {
        private readonly foodbucketEntities _db = new foodbucketEntities();

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }

        public static string Encrypt(string originalPassword)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] originalBytes = Encoding.Default.GetBytes(originalPassword);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes);
        }

        public bool IsValid(string userName, string password)
        {
            password = Encrypt(password);

            var user = _db.System_Users.Where(a => a.Username == userName && a.Password == password);
            if (user.Count() != 1)
            {
                return false;
            }
            return true;
            
        }
    }
}