using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication1.Helpers
{
    public static class Criptografia
    {
        public static string HashMD5(string val)
        {
            var bytes = Encoding.ASCII.GetBytes(val);
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(bytes);
            var ret = string.Empty;
            for (int indice=0;indice<hash.Length;indice++)
            {
                ret += hash[indice].ToString("x2");
            }
            return ret;
        }




    }
}