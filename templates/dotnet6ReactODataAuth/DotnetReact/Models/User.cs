using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DotnetReact.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public static implicit operator User(Task<User> v)
        {
            throw new NotImplementedException();
        }
    }
}