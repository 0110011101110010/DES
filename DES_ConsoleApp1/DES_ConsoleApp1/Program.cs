/*
Sukurti šifravimo/dešifravimo sistemą. Sistemos lange įvedamas tekstas užšifruojamas AES/DES algoritmu. Kitame sistemos laukelyje įvedamas užšifruotas tekstas ir jis dešifruojamas.

5 taškai - realizuoti aplikaciją, kurios funkcionalumas paminėtas pradinėje sąlygoje.  
4 taškai - užšifruoto teksto išsaugojimas faile ir nuskaitymas iš jo. 
2 taškai - užšifravimui/dešifravimui reikalingas raktas, kuris įvedamas vartotojo. 
3 taškai - turi būti galimybė pasirinkti šifravimo modą (ECB, CBC modos). Gebėti paaiškinti šifravimo modas. 
1 taškas - aplikacijos kodo patalpinimas į GitHub/Gitlab su tarpiniais komentarais (commit). 

https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.des?view=net-5.0 - DES Class
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography; // DES klasė
using System.IO;

namespace DES_ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                FakeMain();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        static void FakeMain()
        {
            try
            {
                int x = 0;
                string text = null;
                string key_temp = null;
                string cipherMode_temp = null;
                int cipherMode = 0;
                // Create a new DES object to generate a key
                // and initialization vector (IV).
                DES DESalg = DES.Create();

                // Šifruojamas tekstas
                while (x == 0)
                {
                    Console.Write("Įveskite šifruojamą tekstą: ");
                    text = Console.ReadLine();
                    if (text.Length > 0)
                        x++;
                    else
                    {
                        Console.WriteLine("Reikia bent vieno simbolio.");
                        Console.WriteLine("Teksto ilgis: " + key_temp.Length);
                    }

                }
                x = 0;

                // Raktas
                while (x == 0)
                {
                    Console.Write("Įveskite raktą šifravimui ir dešifravimui (rakto ilgis 8 simboliai): ");
                    key_temp = Console.ReadLine();
                    if (key_temp.Length == 8)
                        x++;
                    else
                    {
                        Console.WriteLine("Raktas turi būti 8 simboliai. Tarpas skaitosi kaip simbolis!");
                        Console.WriteLine("Rakto ilgis: " + key_temp.Length);
                    }
                }
                x = 0;

                while (x == 0)
                {
                    Console.WriteLine("Modos: 0 = CBC, 1 = EBC");
                    Console.Write("Pasirinkite: ");
                    cipherMode_temp = Console.ReadLine();
                    if (cipherMode_temp == "0")
                    {
                        cipherMode = 0;
                        x++;
                    }
                    else if (cipherMode_temp == "1")
                    {
                        cipherMode = 1;
                        x++;
                    }
                    else
                    {
                        Console.WriteLine("Įveskite tik 0 arba 1");
                        Console.WriteLine("Įvesta reikšmė: " + cipherMode_temp);
                    }
                }

                // Create a string to encrypt.
                //string sData = "Here is some data to encrypt.";
                string FileName = "CText.txt";
                //string key_temp = "123 4567";
                //bool cipherMode = false; // false - CBC, true - ECB

                // Vartotojo raktas
                byte[] key = GenerateKey(key_temp);

                // Encrypt text to a file using the file name, key, and IV.
                EncryptTextToFile(text, FileName, key, DESalg.IV, cipherMode);

                // Decrypt the text from a file using the file name, key, and IV.
                string Final = DecryptTextFromFile(FileName, key, DESalg.IV, cipherMode);

                // Display the decrypted string to the console.
                Console.WriteLine(Final);

                // Panaikinam failą - gali prireikt pakartotinai šifruojant
                //File.Delete(FileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static byte[] GenerateKey(string key)
        {
            try
            {
                if (key.Length == 8)
                {
                    return Encoding.ASCII.GetBytes(key);
                }
                Console.WriteLine("The key must be 8 symbols. Space counts as symbol too!");
                // Potenciali vieta pasiūlyti automatiškai sugeneruoti raktą arba įvesti naują
                // *geresnė vieta būtų tai padaryti, kai vartotojas įveda raktą
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static void EncryptTextToFile(String Data, String FileName, byte[] Key, byte[] IV, int cipherMode)
        {
            try
            {
                // Create or open the specified file.
                FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate);

                // Create a new DES object.
                DES DESalg = DES.Create();
                if (cipherMode == 1)
                    DESalg.Mode = CipherMode.ECB;

                // Create a CryptoStream using the FileStream
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(fStream,
                    DESalg.CreateEncryptor(Key, IV),
                    CryptoStreamMode.Write);

                // Create a StreamWriter using the CryptoStream.
                StreamWriter sWriter = new StreamWriter(cStream);

                // Write the data to the stream
                // to encrypt it.
                sWriter.WriteLine(Data);

                // Close the streams and
                // close the file.
                sWriter.Close();
                cStream.Close();
                fStream.Close();
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file error occurred: {0}", e.Message);
            }
        }

        public static string DecryptTextFromFile(String FileName, byte[] Key, byte[] IV, int cipherMode)
        {
            try
            {
                // Create or open the specified file.
                FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate);

                // Create a new DES object.
                DES DESalg = DES.Create();
                if(cipherMode == 1)
                    DESalg.Mode = CipherMode.ECB;

                // Create a CryptoStream using the FileStream
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(fStream,
                    DESalg.CreateDecryptor(Key, IV),
                    CryptoStreamMode.Read);

                // Create a StreamReader using the CryptoStream.
                StreamReader sReader = new StreamReader(cStream);

                // Read the data from the stream
                // to decrypt it.
                string val = sReader.ReadLine();

                // Close the streams and
                // close the file.
                sReader.Close();
                cStream.Close();
                fStream.Close();
                File.Delete(FileName); // Delete the file

                // Return the string.
                return val;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file error occurred: {0}", e.Message);
                return null;
            }
        }
    }
}
