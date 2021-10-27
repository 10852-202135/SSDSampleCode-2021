using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace L07Cryptography.Models
{
    public class Password
    {
        [Key]
        public int Id { get; set; }
        [ReadOnly(true)]
        public string  UserId { get; set; }
        [Required]
        public string PlainTextPassword { get; set; }
        [ReadOnly(true)]
        public string HashedPassword { get; set; }
        [ReadOnly(true)]
        public string Salt { get; set; }
        [ReadOnly(true)]
        public string HashedSaltedPassword { get; set; }
        [ReadOnly(true)]
        public string BcryptPassword { get; set; }
    }
}
