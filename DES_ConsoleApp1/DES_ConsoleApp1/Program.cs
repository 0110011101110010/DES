﻿/*
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
                // 1. punkto realizavimas: kreipkimės į metodą kuriame susigeneruos užšifruotas tekstas

                /* Žingsniai:
                 * 
                 * 1. Šifruojamas, dešifruojamas įvestas tekstas
                 *  1.1. šifravimas
                 *  1.2. dešifravimas
                 *  
                 * 2. Užšifruoto teksto išsaugojimas faile
                 *  2.1. išsaugoti tekstą faile
                 *  2.2. nuskaityti failą
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
                 * 0. Naudoti biblioteką ar kurti algoritmą?
                 * 1. Galimybė užšifruoti/dešifruoti failo tekstą? (2.)
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

                // Encrypt the string to an in-memory buffer.
                byte[] Data = EncryptTextToMemory(sData, DESalg.Key, DESalg.IV);

                // Decrypt the buffer back to a string.
                string Final = DecryptTextFromMemory(Data, DESalg.Key, DESalg.IV);

                // Display the decrypted string to the console.
                Console.WriteLine(Final);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        public static byte[] EncryptTextToMemory(string Data, byte[] Key, byte[] IV)
        {
            try
            {
                // Create a MemoryStream.
                MemoryStream mStream = new MemoryStream();

                // Create a new DES object.
                DES DESalg = DES.Create();

                // Create a CryptoStream using the MemoryStream
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream,
                    DESalg.CreateEncryptor(Key, IV),
                    CryptoStreamMode.Write);

                // Convert the passed string to a byte array.
                byte[] toEncrypt = new ASCIIEncoding().GetBytes(Data);

                // Write the byte array to the crypto stream and flush it.
                cStream.Write(toEncrypt, 0, toEncrypt.Length);
                cStream.FlushFinalBlock();

                // Get an array of bytes from the
                // MemoryStream that holds the
                // encrypted data.
                byte[] ret = mStream.ToArray();

                // Close the streams.
                cStream.Close();
                mStream.Close();

                // Return the encrypted buffer.
                return ret;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }

        public static string DecryptTextFromMemory(byte[] Data, byte[] Key, byte[] IV)
        {
            try
            {
                // Create a new MemoryStream using the passed
                // array of encrypted data.
                MemoryStream msDecrypt = new MemoryStream(Data);

                // Create a new DES object.
                DES DESalg = DES.Create();

                // Create a CryptoStream using the MemoryStream
                // and the passed key and initialization vector (IV).
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    DESalg.CreateDecryptor(Key, IV),
                    CryptoStreamMode.Read);

                // Create buffer to hold the decrypted data.
                byte[] fromEncrypt = new byte[Data.Length];

                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

                //Convert the buffer into a string and return it.
                return new ASCIIEncoding().GetString(fromEncrypt);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }
    }
}
