using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication1.Helpers
{
    public class DataCiper
    {
        private RSACryptoServiceProvider rSA;

        public DataCiper(string key)
        {
            var csp = new CspParameters
            {
                KeyContainerName = key
            };

            this.rSA = new RSACryptoServiceProvider(csp)
            {
                PersistKeyInCsp = true
            };
        }

        public byte[] Encrypt(string value)
        {
            var bytes = Encoding.ASCII.GetBytes(value);
            var encrypted = this.rSA.Encrypt(bytes, false);

            return encrypted;
        }

        public string Decrypt(byte[] bytes)
        {
            var decrypted = this.rSA.Decrypt(bytes, false);
            var value = Encoding.ASCII.GetString(decrypted);

            return value;
        }
    }
}