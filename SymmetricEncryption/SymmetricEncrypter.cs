using System;
using System.IO;
using System.Security.Cryptography;

namespace SymmetricEncryption
{
    class SymmetricEncrypter
    {
        #region Private Fields

        private SymmetricAlgorithm _algorithm;

        #endregion

        #region Constructors

        public SymmetricEncrypter(string algorithm)
        {
            if (algorithm == "DES")
                _algorithm = DES.Create();
            if (algorithm == "TRIPLEDES")
                _algorithm = TripleDES.Create();
            if (algorithm == "AES")
                _algorithm = Aes.Create();
        }

        #endregion

        #region Public Methods

        public string Encrypt(byte[] input, byte[] key, byte[] iv)
        {
            _algorithm.Key = key;
            _algorithm.IV = iv;

            ICryptoTransform cryptoTransform = _algorithm.CreateEncryptor(_algorithm.Key, _algorithm.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(input);
                    }
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public string Decrypt(byte[] input, byte[] key, byte[] iv)
        {
            _algorithm.Key = key;
            _algorithm.IV = iv;

            ICryptoTransform cryptoTransform = _algorithm.CreateDecryptor(_algorithm.Key, _algorithm.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(input);
                    }
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public byte[] GenerateRandom(int length)
        {
            using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
            {
                byte[] randomNumber = new byte[length];

                generator.GetBytes(randomNumber);

                return randomNumber;
            }
        }

        #endregion
    }
}
