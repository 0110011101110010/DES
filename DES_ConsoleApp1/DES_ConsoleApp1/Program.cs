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

namespace DES_ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
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
    }
}
