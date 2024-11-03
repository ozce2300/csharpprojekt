
using System.Text.Json;

namespace ATMMIUN
{
    public class Bankcs
    {
        // deklarar filnamnet samt deklarar lista med objekt.
        private string filename = @"customer.json";
        private List<Customer> customers = new List<Customer>();

        //Konstruktor
        public Bankcs()
        {
            if (File.Exists(filename))
            {
                string jsonString = File.ReadAllText(filename);
                customers = JsonSerializer.Deserialize<List<Customer>>(jsonString)!;
            }

            else
            {
                Console.WriteLine("FInns ingen fil");
            }
        }

        // Validera kortnummer och lösenord
        public Customer ValidateCustomer(int cardNr, int password)
        {
            return customers?.FirstOrDefault(c => c.CardNr == cardNr && c.Password == password)!;
        }
        public void ApplyForLoan()
        {
            try
            {
                // Läs in användarens input för lånebelopp
                float loanAmount;
                while (true)
                {   Console.CursorVisible = true;
                    Console.Clear();
                    Console.WriteLine("Skriv in det lånebelopp du vill låna: ");
                    if (float.TryParse(Console.ReadLine(), out loanAmount) && loanAmount > 0)
                    {
                        break; // Avsluta loopen om inmatningen är giltig
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ogiltig inmatning. Vänligen ange ett positivt tal.");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Tryck enter för att skriva in lånebeloppet du vill låna...");
                    Console.ReadLine();
                }

                // Läs in användarens input för inkomst
                float income;
                while (true)
                {
                    Console.CursorVisible = true;
                    Console.Clear();
                    Console.WriteLine("Skriv in din årsinkomst: ");
                    if (float.TryParse(Console.ReadLine(), out income) && income > 0)
                    {
                        break; // Avsluta loopen om inmatningen är giltig
                    }
                    Console.WriteLine();
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Ogiltig inmatning. Vänligen ange en positiv årsinkomst.");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Tryck enter för att skriva in lånebeloppet du vill låna...");
                    Console.ReadLine();
                }


                // Läs in användarens input för ålder
                float age;
                while (true)
                {
                    Console.CursorVisible = true;
                    Console.Clear();
                    Console.WriteLine("Skriv in din ålder: ");
                    if (float.TryParse(Console.ReadLine(), out age) && age >= 18 && age <= 65)
                    {
                        break; // Avsluta loopen om inmatningen är giltig
                    }
                    Console.WriteLine();
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Ogiltig inmatning. Vänligen ange en giltig ålder (18-65).");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Tryck enter för att skriva in lånebeloppet du vill låna...");
                    Console.ReadLine();
                }

                // Läs in användarens anställningsstatus
                string isEmployedInput;
                float isEmployedNumeric;
                while (true)
                {
                    Console.CursorVisible = true;
                    Console.Clear();
                    Console.WriteLine("Är du anställd? Ja/Nej");
                    isEmployedInput = Console.ReadLine()!.ToLower();
                    if (isEmployedInput == "ja")
                    {
                        isEmployedNumeric = 1F;
                        break;
                    }
                    else if (isEmployedInput == "nej")
                    {
                        isEmployedNumeric = 0F;
                        break;
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ogiltig inmatning. Vänligen svara med 'Ja' eller 'Nej'.");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Tryck enter för att skriva in lånebeloppet du vill låna...");
                    Console.ReadLine();
                }


                // Läs in användarens skulder
                float debt;
                while (true)
                {
                    Console.CursorVisible = true;
                    Console.Clear();
                    Console.WriteLine("Hur mycket har du i skulder?");
                    if (float.TryParse(Console.ReadLine(), out debt) && debt >= 0)
                    {
                        break; // Avsluta loopen om inmatningen är giltig
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("Ogiltig inmatning. Testa igen");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Tryck enter för att skriva in lånebeloppet du vill låna...");
                    Console.ReadLine();
                }


                // Ladda in sample data med användarens inmatningar
                var sampleData = new MLModel2.ModelInput()
                {
                    LoanAmount = loanAmount,
                    Income = income,
                    Age = age,
                    IsEmployed = isEmployedNumeric,
                    ExistingDebt = debt
                };

                // Ladda modellen och förutsäg utfallet
                var result = MLModel2.Predict(sampleData);
                Console.Clear();
                // Skriv ut resultatet beroende på om lånet godkändes eller inte
                if (result.PredictedLabel == 1F)

                {
                    Console.CursorVisible = true;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Du kan få ett lån");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Tryck enter för att gå till startsidan...");
                    Console.ReadLine();
                }
                else

                {
                    Console.CursorVisible = true;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du kan inte få ett lån");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Tryck enter för att gå till startsidan...");
                    Console.ReadLine();
                }

                Console.WriteLine();

            }
            catch (FormatException)
            {
                Console.WriteLine("Ogiltig inmatning. Var god mata in ett giltigt heltal.");
            }
        }


        // Insättning
        public void Deposit(int cardNr, int password, int amount)
        {
            // Insättning får ej vara negativ
            if (amount < 0 )
            {
                Console.Clear();
                throw new ArgumentException("Insättningsbeloppet kan inte vara negativt.");
            }
            // Insättning får ej vara 0

            if (amount == 0)
            {
                Console.Clear();
                throw new ArgumentException("Du kan inte sätta in 0 kr");
            }


            var customer = customers?.FirstOrDefault(c => c.CardNr == cardNr && c.Password == password);
            if (customer != null)
            {
                customer.Saldo += amount;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Insättning av {amount} kr genomförd.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.Write($"Nytt saldo: {customer.Saldo} kr");
                Console.WriteLine();
                SaveChanges();
            }
            else
            {   
                throw new ArgumentException("Något gick fel.");

            }
        }

        // Uttag
        public void Withdraw(int cardNr, int password, int amount)
        {
            var customer = customers?.FirstOrDefault(c => c.CardNr == cardNr && c.Password == password);
            //Utsättningsbeloppet kan inte vara negativt
            if (amount < 0)
            {
                Console.Clear();
                throw new ArgumentException("Utsättningsbeloppet kan inte vara negativt.");
            }
            //Utsättningsbeloppet kan inte vara 0
            if (amount == 0)
            {
                Console.Clear();
                throw new ArgumentException("Utsättningsbeloppet kan inte vara 0.");
            }
            if (customer != null && customer.Saldo >= amount)
            {
                customer.Saldo -= amount;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Uttag av {amount} kr genomfört.");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Nytt saldo: {customer.Saldo} kr");                
                Console.WriteLine();
                Console.WriteLine("Tryck enter för att gå tillbaka...");
                SaveChanges();
            }
            else
            {
                Console.Clear();
                throw new ArgumentException("Otillräckligt med saldo");

            }
        }

        // Hämta saldo
        public int GetBalance(int cardNr, int password)
        {
            var customer = customers?.FirstOrDefault(c => c.CardNr == cardNr && c.Password == password);
            // Returnera saldo eller 0 om kunden inte finns
            return customer?.Saldo ?? 0; 
        }

        // Spara ändringar till Json-fil
        private void SaveChanges()
        {
            string jsonString = JsonSerializer.Serialize(customers);
            File.WriteAllText(filename, jsonString);
        }
    }
}