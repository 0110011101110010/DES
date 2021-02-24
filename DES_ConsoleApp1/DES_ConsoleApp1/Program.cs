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
                //Console.WriteLine("Hello world!");
                FakeMain();
                // 3. punkto realizavimas: raktas yra sudaromas iš 8 ASCII simbolių; prieš išsiunčiant jį, reikia konvertuoti i byte'ų array'ų

                /* Žingsniai:
                 * 
                 *  1.-2. Atskirti šifravimą ir dešifravimą
                 *  
                 * 3. Vartotojas įveda raktą
                 * 3.1. šifravimas
                 * 3.2. dešifravimas
                 * 
                 * 4. Pasirinktina šifravimo moda
                 *  4.1. ECB
                 *  4.2. CBC
                 * 
                 * 5. Kelti tarpinius komentarus į githubą
                 * 
                 * Klausimai:
                 * 0. Naudoti biblioteką ar kurti algoritmą? - biblioteką
                 * 1. Galimybė užšifruoti/dešifruoti failo tekstą? - vartotojas apie tai nežino; visada šifruojam tekstą į failą
                 */
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
                // Create a new DES object to generate a key
                // and initialization vector (IV).
                DES DESalg = DES.Create();

                // Create a string to encrypt.
                string sData = "Here is some data to encrypt.";
                string FileName = "CText.txt";
                string key_temp = "12345678";

                // Vartotojo raktas
                Console.WriteLine(key_temp.Length);
                byte[] key = GenerateKey(key_temp);

                // Encrypt text to a file using the file name, key, and IV.
                EncryptTextToFile(sData, FileName, DESalg.Key, DESalg.IV);

                // Decrypt the text from a file using the file name, key, and IV.
                string Final = DecryptTextFromFile(FileName, DESalg.Key, DESalg.IV);

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
                byte[] keyInBytes = null;
                if (key.Length == 8)
                {
                    byte[] array = Encoding.ASCII.GetBytes(key);
                    for (int i = 0; i < 8; i++)
                    {
                        keyInBytes[i] = array[i];
                    }

                    //Console.Write("Raktas ASCII koduote: ");
                    foreach (byte _byte in keyInBytes)
                    {
                        //Console.Write(_byte + " ");
                    }
                    //Console.WriteLine("");
                    return keyInBytes;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static void EncryptTextToFile(String Data, String FileName, byte[] Key, byte[] IV)
        {
            try
            {
                // Create or open the specified file.
                FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate);

                // Create a new DES object.
                DES DESalg = DES.Create();

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

        public static string DecryptTextFromFile(String FileName, byte[] Key, byte[] IV)
        {
            try
            {
                // Create or open the specified file.
                FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate);

                // Create a new DES object.
                DES DESalg = DES.Create();

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
