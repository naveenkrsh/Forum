using Forum.Core.Models;
using Forum.Core.Models.Bases;
using System;
using System.Text;
namespace Forum.Core.Models.User
{
    public class User : BaseEntity
    {
        
        public User(): this("")
        {
           
        }
        public User(string email)
        {
            ProfileDetails = new Profile();
            Email = email;
        }
        public string Password{get;set;}
        public string Email {get;set;}
        public Profile ProfileDetails {get;set;}

        public string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}