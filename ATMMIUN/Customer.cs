using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMMIUN
{
    //Getters och Setter
    public class Customer
    {
        public string Name { get; }
        public int CardNr { get; }
        public int Password { get; }
        public int Saldo { get; set; }

        // Konstruktör för att skapa kundobjekt med alla värden
        public Customer(string name, int cardNr, int password, int saldo)
        {
            Name = name;
            CardNr = cardNr;
            Password = password;
            Saldo = saldo;
        }
    }
}
