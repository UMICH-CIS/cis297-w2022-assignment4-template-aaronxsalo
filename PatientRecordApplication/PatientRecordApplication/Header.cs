using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PatientRecordApplication
{
    /// <summary>
    /// HEADER - this  header file implements all classes used by the main program
    /// </summary>

    /// <summary>
	/// Patient Class - class for the properties of patient, PatientID, Name, BalanceDue
	/// </summary>
    public class Patient
    {
        public string PatientID { get; set; }
        public string Name { get; set; }
        public decimal BalanceDue { get; set; }
    }

    /// <summary>
	/// PatientData Class - abstract class for classes dealing with the patient data file
	/// </summary>
    public abstract class PatientData
    {
        protected const string FILENAME = "PatientData.txt";
        protected Patient patient = new Patient();
    }

    /// <summary>
	/// PatientInput Class - class for adding records to the patient data file
	/// </summary>
    public class PatientInput : PatientData
    {
        /// <summary>
        /// asks for user input on each patient field and then appends the data file with input
        /// </summary>
        public void RecordEnter()
        {
            try
            {
                Console.Write("Enter Patient ID: ");
                patient.PatientID = Console.ReadLine();
                Console.Write("Enter Patient's Name: ");
                patient.Name = Console.ReadLine();
                Console.Write("Enter Balance Due: ");
                patient.BalanceDue = Convert.ToDecimal(Console.ReadLine());
            }
            catch (FormatException fe)
            {
                Console.WriteLine($"\n{fe.Message} Try again.");
                Console.WriteLine("\nPress enter to continue...");
                Console.ReadLine();
            }

            using (StreamWriter sw = File.AppendText(FILENAME))
            {
                sw.WriteLine(patient.PatientID + ";" + patient.Name + ";" + patient.BalanceDue);
            }
        }
    }

    /// <summary>
    /// PatientOutput Class - class for displaying all records in patient data file
    /// </summary>
    public class PatientOutput : PatientData
    {
        /// <summary>
        /// displays each line of data from the patient data file in a user-friendly format
        /// </summary>
        public void RecordDisplay()
        {
            Console.WriteLine("PID\tName\tBalance Due");
            using (StreamReader sr = new StreamReader(FILENAME))
            {
                string record = sr.ReadLine();

                while(record != null)
                {
                    string[] fields = record.Split(';');

                    patient.PatientID = fields[0];
                    patient.Name = fields[1];
                    patient.BalanceDue = Convert.ToDecimal(fields[2]);

                    Console.WriteLine($"{fields[0]}\t{fields[1]}\t{fields[2]}");

                    record = sr.ReadLine();
                }
            }
        }
    }

    /// <summary>
    /// PatientSearch Class - class for searching for records in patient data file based on patient ID
    /// </summary>
    public class PatientSearch : PatientData
    {
        /// <summary>
        /// takes user input of patient ID and then searches patient data file for that patient's information
        /// </summary>
        public void RecordSearch()
        {
            try
            {
                bool found = false;
                Console.Write("Enter Patient ID: ");
                string PID = Console.ReadLine();

                using (StreamReader sr = new StreamReader(FILENAME))
                {
                    string record = sr.ReadLine();

                    while (record != null)
                    {
                        string[] fields = record.Split(';');

                        patient.PatientID = fields[0];
                        patient.Name = fields[1];
                        patient.BalanceDue = Convert.ToDecimal(fields[2]);

                        if (patient.PatientID == PID)
                        {
                            Console.WriteLine($"\n{fields[0]}\t{fields[1]}\t{fields[2]}");
                            found = true;
                        }

                        record = sr.ReadLine();
                    }

                    if (!found)
                        throw new Exception("Patient record not found in data file. Try again.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\n{e.Message}");
            }
        }
    }

    /// <summary>
    /// MinBalance Class - class for displaying all records in patient data file that are greater than or equal to specified balance
    /// </summary>
    public class MinBalance : PatientData
    {
        /// <summary>
        /// takes user input of min balance and searches the patient data file for each patient with the proper balance
        /// </summary>
        public void BalanceSearch()
        {
            try
            {
                bool found = false;
                Console.Write("Enter Minimum Balance to be Found: ");
                decimal min = Convert.ToDecimal(Console.ReadLine());

                using (StreamReader sr = new StreamReader(FILENAME))
                {
                    string record = sr.ReadLine();

                    while (record != null)
                    {
                        string[] fields = record.Split(';');

                        patient.PatientID = fields[0];
                        patient.Name = fields[1];
                        patient.BalanceDue = Convert.ToDecimal(fields[2]);

                        if (patient.BalanceDue >= min)
                        {
                            Console.WriteLine($"\n{fields[0]}\t{fields[1]}\t{fields[2]}");
                            found = true;
                        }

                        record = sr.ReadLine();
                    }

                    if (!found)
                        throw new Exception("No records found in data file with that specification. Try again.");
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine($"\n{fe.Message} Try again.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"\n{e.Message}");
            }
        }
    }
}
