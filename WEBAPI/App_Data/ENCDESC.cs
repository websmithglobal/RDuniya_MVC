using com.pakhee.common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

public static class ENCDESC
{
    public static string Encrypt(string data, string key)
    {
        CryptLib _crypt = new CryptLib();
        String iv = CryptLib.GenerateRandomIV(16); //16 bytes = 128 bits
        key = CryptLib.getHashSha256(key, 31); //32 bytes = 256 bits
        String cypherText = _crypt.encrypt(data, key, iv);
        return cypherText;
    }

    public static string Decrypt(string data, string key,string iv)
    {
        CryptLib _crypt = new CryptLib();
        //16 bytes = 128 bits
        key = CryptLib.getHashSha256(key, 32); //32 bytes = 256 bits
        return _crypt.decrypt(data, key, iv);
    }
}