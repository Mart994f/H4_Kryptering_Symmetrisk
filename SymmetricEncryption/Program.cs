using System;
using System.Text;

namespace SymmetricEncryption
{
    class Program
    {
        static SymmetricEncrypter encrypter;
        static void Main(string[] args)
        {
            ConsoleKeyInfo selectInput;
            bool appIsRunning = true;

            while (appIsRunning)
            {
                Console.Clear();
                Console.WriteLine("Symmectric Encryption & Decryption\n");

                Console.WriteLine("[1] DES");
                Console.WriteLine("[2] TripleDES");
                Console.WriteLine("[3] AES");
                Console.WriteLine("[0] EXIT");

                Console.Write("\nSELECT: ");
                selectInput = Console.ReadKey();

                switch (selectInput.Key)
                {
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        appIsRunning = false;
                        break;
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        encrypter = new SymmetricEncrypter("DES");
                        GetInput("DES");
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        encrypter = new SymmetricEncrypter("TRIPLEDES");
                        GetInput("TrippleDES");
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        encrypter = new SymmetricEncrypter("AES");
                        GetInput("AES");
                        break;
                }
            }
        }

        private static void GetInput(string algorithm)
        {
            ConsoleKeyInfo selectInput;
            string message;
            byte[] key;
            byte[] iv;

            Console.Clear();
            Console.WriteLine($"{algorithm} Encryption & Decryption\n");
            Console.WriteLine("[1] Encrypt");
            Console.WriteLine("[2] Decrypt");

            Console.Write("\nSELECT: ");
            selectInput = Console.ReadKey();

            switch (selectInput.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    Console.Write("\nMesssage: ");
                    message = Console.ReadLine();

                    key = encrypter.GenerateRandom(32);
                    iv = encrypter.GenerateRandom(16);

                    Console.WriteLine($"\r\nEncrypted message: {encrypter.Encrypt(Encoding.UTF8.GetBytes(message), key, iv)}");
                    Console.WriteLine($"\nKey: {Convert.ToBase64String(key)}");
                    Console.WriteLine($"IV: {Convert.ToBase64String(iv)}");
                    Console.WriteLine("Press [ENTER] to continue");
                    Console.ReadLine();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    Console.Write("\nEncrypted messsage: ");
                    message = Console.ReadLine();

                    Console.Write("\nKey: ");
                    key = Convert.FromBase64String(Console.ReadLine());

                    Console.Write("\nIV: ");
                    iv = Convert.FromBase64String(Console.ReadLine());

                    Console.WriteLine($"\r\nDecrypted message: {encrypter.Decrypt(Convert.FromBase64String(message), key, iv)}");
                    Console.WriteLine("Press [ENTER] to continue");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
