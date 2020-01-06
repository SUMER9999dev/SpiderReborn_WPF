using System;
using System.Security.Cryptography;
using System.Text;
class AES
{

        private AesCryptoServiceProvider Provider;
        private string IV = "JrSTIOpmoHSdFc4x";
        private string Key = "azrPnKZXUXXQAs7bs53afXvrL6S7PWJj";

        public AES()
        {
            Provider = new AesCryptoServiceProvider();
            Provider.BlockSize = 128;
            Provider.KeySize = 256;
            Provider.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            Provider.Key = ASCIIEncoding.ASCII.GetBytes(Key);
            Provider.Mode = CipherMode.CBC;
            Provider.Padding = PaddingMode.PKCS7;
        }

        public string Encrypt(string Text)
        {
            using (var Transform = Provider.CreateEncryptor())
            {
                byte[] Encrypted_Bytes = Transform.TransformFinalBlock(ASCIIEncoding.ASCII.GetBytes(Text), 0, Text.Length);
                Transform.Dispose();
                var _String = Convert.ToBase64String(Encrypted_Bytes);
                return _String;
            }
        }

        public string Decrypt(string Text)
        {
            using (var Transform = Provider.CreateDecryptor())
            {
                byte[] Encrypted_Bytes = Convert.FromBase64String(Text);
                byte[] Decrypted_Bytes = Transform.TransformFinalBlock(Encrypted_Bytes, 0, Encrypted_Bytes.Length);
                var _String = ASCIIEncoding.ASCII.GetString(Decrypted_Bytes);
                return _String;
            }
        }
}
