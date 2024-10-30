using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ATMMIUN
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Title = "MIUN ATM";
            Bankcs bankcs = new Bankcs();
            Welcome(bankcs);
        }

        // Metod för inloggning med välkomsttext
        public static void Welcome(Bankcs bankcs)
        {
            bool cardNumberTrue = true;

            // Kör så länge cardNumberTrue är sant

            while (cardNumberTrue)
            {
            Console.WriteLine("*********************************");
            Console.WriteLine("**** Välkomna till MIUN Bank ****");
            Console.WriteLine("*********************************");
            Console.WriteLine();

                // Variabel för att spara den inloggade kunden
                Customer loggedInCustomer = null!; 

        
                // Felhantering
                try
                {
                    // Skriv in kortnummer och lösenord
                    Console.WriteLine("Skriv in ditt kortnr:");
                    int inputCard = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Skriv in ditt lösenord:");
                    int inputCardPassword = Convert.ToInt32(Console.ReadLine());

                    // Kontrollera rätt kortnummer och lösenord
                    loggedInCustomer = bankcs.ValidateCustomer(inputCard, inputCardPassword);
                    if (loggedInCustomer != null)
                    {
                        // Om rätt, avsluta loopen och rensa konsolen
                        cardNumberTrue = false;
                        Console.Clear();
                        toDo(bankcs, loggedInCustomer); // Skicka in den inloggade kunden
                    }
                    else
                    {
                        // Annars skriv in felmeddelande
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Fel kortnummer/lösenord!");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Tryck enter för att gå tillbaka...");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                // Catch om det är ogiltig inmatning
                catch (FormatException)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ogiltig inmatning. Var god mata in ett giltigt heltal.");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Tryck enter för att gå tillbaka...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        //ToDO metod
        public static void toDo(Bankcs bankcs, Customer loggedInCustomer)
        {
            Console.CursorVisible = false;
            Console.WriteLine($"Hej {loggedInCustomer.Name}, vad vill du göra?");
            Console.WriteLine();
            Console.WriteLine("[1] Sätta in pengar");
            Console.WriteLine("[2] Ta ut pengar");
            Console.WriteLine("[3] Se saldo");
            Console.WriteLine("[4] Ansöka om bolån");
            Console.WriteLine("[5] Avsluta");


            // Läs användarens val
            int inp = (int)Console.ReadKey(true).Key;
            // Switch-sats
            switch (inp)
            {
                case '1':
                    while (true)
                    {
                        Console.Clear();

                        try
                        {
                            Console.CursorVisible = true;
                            Console.WriteLine("Hur mycket vill du sätta in?");

                            // Försök att läsa och konvertera inmatningen
                            int depositAmount = Convert.ToInt32(Console.ReadLine());

                            // Om konverteringen är lyckad, sätt in pengar och avsluta loopen
                            bankcs.Deposit(loggedInCustomer.CardNr, loggedInCustomer.Password, depositAmount);
                            Console.WriteLine();

                            Console.Write("Tryck enter för att komma till startsidan...");
                            break;
                        }
                        catch (ArgumentException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message); // Skriv ut felmeddelandet för negativt eller 0-belopp
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Tryck enter för att gå tillbaka...");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        catch (FormatException)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ogiltig inmatning. Var god mata in ett giltigt heltal.");
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Tryck enter för att gå tillbaka...");
                            Console.ReadLine(); 
                            Console.Clear();
                        }
                    }
                    Console.ReadLine(); // Vänta på input så att konsolen inte stängs direkt
                    Console.Clear();
                    break;

                case '2':
                    while (true)
                    {
                        try
                        {
                            Console.CursorVisible = true;
                            Console.Clear();
                            Console.WriteLine("Hur mycket vill du ta ut?");
                            int withdrawalAmount = Convert.ToInt32(Console.ReadLine());

                            bankcs.Withdraw(loggedInCustomer.CardNr, loggedInCustomer.Password, withdrawalAmount);
                            Console.WriteLine();
                            break;  // Avsluta loopen när inmatningen är korrekt

                        }
                        catch (ArgumentException ex)

                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Tryck enter för att gå tillbaka...");
                            Console.ReadLine();
                            Console.Clear();

                        }
                        catch (FormatException)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ogiltig inmatning. Var god mata in ett giltigt heltal.");
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Tryck enter för att gå tillbaka...");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                        Console.ReadLine(); // Vänta på input så att konsolen inte stängs direkt
                        Console.Clear();
                        break;


                case '3':
                    try
                    {
                        Console.Clear();
                        Console.WriteLine($"Ditt saldo är: {bankcs.GetBalance(loggedInCustomer.CardNr, loggedInCustomer.Password)} kr");
                        Console.WriteLine();
                        Console.WriteLine("Tryck enter för att gå tillbaka...");
                        Console.ReadLine();
                        Console.Clear();

                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ogiltig inmatning. Var god mata in ett giltigt heltal.");
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    break;

                case '4':
                    Console.Clear();
                    bankcs.ApplyForLoan();
        
                    break;

                case '5':
                    Console.Clear();
                    Console.WriteLine("Tack för att du använde MIUN Bank!");
                    Environment.Exit(0);
                    break;

                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            // Återgå till meny efter ett val
            Console.Clear();
            toDo(bankcs, loggedInCustomer);
        }
    }
}
