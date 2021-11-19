using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace L07Cryptography.Models
{
    public class BankAccountDp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string  Number { get; set; }
        [ReadOnly(true)]
        public string EncryptedNumber { get; set; }
        [ReadOnly(true)]
        public string DecryptedNumber { get; set; }

    }
}
