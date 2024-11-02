using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;

public static class EncryptionHelper
{
    private static readonly string part1 = "St3vEsB";
    private static readonly string part2 = "4Mb0oL";
    private static readonly string part3 = "0V3";

    private static string GetKey()
    {
        return part1 + part2 + part3;
    }

    public static string Encrypt(string text)
    {
        
        string key = GetKey();

        using (var aes = System.Security.Cryptography.Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.GenerateIV();

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(text);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                byte[] result = new byte[aes.IV.Length + encryptedBytes.Length];

                Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
                Buffer.BlockCopy(encryptedBytes, 0, result, aes.IV.Length, encryptedBytes.Length);

                return Convert.ToBase64String(result);
            }
        }
    }

    public static string Decrypt(string cipherText)
    {
        string key = GetKey();

        byte[] fullChipher = Convert.FromBase64String(cipherText);

        using (var aes = System.Security.Cryptography.Aes.Create())
        {
            aes.Key=Encoding.UTF8.GetBytes(key);

            byte[] iv = new byte[aes.BlockSize / 8];
            byte[] cipher = new byte[ fullChipher.Length -iv.Length];

            Buffer.BlockCopy(fullChipher, 0 , iv, 0,iv.Length);
            Buffer.BlockCopy(fullChipher,iv.Length, cipher, 0, cipher.Length);

            aes.IV = iv;

            using (var decryptor = aes.CreateDecryptor(aes.Key , aes.IV))
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(cipher,0,cipher.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }


}
