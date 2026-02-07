using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginWeb_CSharp.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NombreCompleto { get; set; }
    }
}