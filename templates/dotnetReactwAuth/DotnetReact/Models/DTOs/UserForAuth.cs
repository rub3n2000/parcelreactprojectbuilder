using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using DotnetReact.Models;

namespace DotnetReact.Models.Dto
{
    public class UserForAuth
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}