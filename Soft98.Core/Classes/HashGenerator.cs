using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Soft98.Core.Classes
{
    public class HashGenerator
    {
        public static string EncodingPassWithMd5(string password)
        {
            Byte[] mainByte;
            Byte[] encodeBytes;
            MD5 md5 = new MD5CryptoServiceProvider();
            mainByte = ASCIIEncoding.Default.GetBytes(password);
            encodeBytes = md5.ComputeHash(mainByte);

            return BitConverter.ToString(encodeBytes);
        } // end method EncodingPassWithMd5

    } // end public class HashGenerator

} // end namespace Soft98.Core.Classes
