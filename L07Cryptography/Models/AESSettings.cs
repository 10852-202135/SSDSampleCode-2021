using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L07Cryptography.Models
{
    public class AESSettings
    {
        public byte[] Key { get; set; }
        public byte[] IV{ get; set; }
    }
}
