using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace E_Commerce_API.Control
{
    public static class StringCipher
    {
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText)
        {
            string passPhrase = "D98955322A4B49A6869239924D920CB8";
            var key = Encoding.UTF8.GetBytes(passPhrase);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            string passPhrase = "D98955322A4B49A6869239924D920CB8";
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(passPhrase);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            //var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            byte[] randomBytes = new byte[] { 10, 46, 58, 189, 125, 247, 140, 178, 214, 230, 123, 125, 14, 78, 96, 95, 84, 82, 255, 20, 5, 13, 26, 249, 26, 53, 25, 14, 58, 96, 32, 25 };
            //using (var rngCsp = new RNGCryptoServiceProvider())
            //{
            //    // Fill the array with cryptographically secure random bytes.
            //    rngCsp.GetBytes(randomBytes);
            //}
            return randomBytes;
        }

    }
}