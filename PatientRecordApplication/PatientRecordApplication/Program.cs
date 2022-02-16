using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientRecordApplication
{
    /// <summary>
	/// MAIN - this program allows the user to add patient records, display patients, search by patient ID, and show patient with specified min balance
	/// </summary>
	/// <Student>Aaron Salo</Student>
	/// <Class>CIS297</Class>
	/// <Semester>Winter 2022</Semester>
    class Program
    {
        static void Main(string[] args)
        {
            PatientInput input = new PatientInput();
            PatientOutput output = new PatientOutput();
            PatientSearch search = new PatientSearch();
            MinBalance balance = new MinBalance();

            int userInput = 0;

            while (userInput != 5)
            {
                Console.Clear();
                Console.WriteLine("PATIENT RECORD APPLICATION\n\n1-Add Record\n2-Display Records\n3-Search Patient ID\n4-Min Balance Due\n5-Exit");
                Console.Write("\nEnter choice: ");
                try
                {
                    userInput = Convert.ToInt32(Console.ReadLine());

                    if (userInput < 1 || userInput > 5)
                        throw new Exception("Input must be between 1 and 5. Try again.");
                }
                catch(FormatException fe)
                {
                    Console.WriteLine($"\n{fe.Message} Try again.");
                    Console.WriteLine("\nPress enter to continue...");
                    Console.ReadLine();
                }
                catch(Exception e)
                {
                    Console.WriteLine($"\n{e.Message}");
                    Console.WriteLine("\nPress enter to continue...");
                    Console.ReadLine();
                }

                Console.Clear();
                Console.WriteLine("PATIENT RECORD APPLICATION\n");
                
                switch (userInput)
                {
                    case 1:
                        input.RecordEnter();
                        break;
                    case 2:
                        output.RecordDisplay();
                        Console.WriteLine("\nPress enter to continue...");
                        Console.ReadLine();
                        break;
                    case 3:
                        search.RecordSearch();
                        Console.WriteLine("\nPress enter to continue...");
                        Console.ReadLine();
                        break;
                    case 4:
                        balance.BalanceSearch();
                        Console.WriteLine("\nPress enter to continue...");
                        Console.ReadLine();
                        break;
                }

            }
        }
    }
}
