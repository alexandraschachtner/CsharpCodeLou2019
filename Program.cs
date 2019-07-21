using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "Volunteers.csv");
            var file = new FileInfo(fileName);

            string[] values = File.ReadAllLines(fileName);

            var values2 = values.Skip(1)
                                          .Select(v => FromCsv(v))
                                          .ToList();

            Console.WriteLine("Hello please search for vounteer by name");
            var entry = Console.ReadLine();

            var found = values2.FirstOrDefault(c => c.First == entry);


            if (found != null)
            {
                Console.WriteLine(found.Print());
            }
            else
            {
                Console.WriteLine("Not Found");
                var volunteer = addVolunteer(entry);
                values2.Add(volunteer);
                var test = values2.Select(c => c.Convert()).ToArray();
                File.WriteAllLines(fileName, test);
              //  File.AppendAllText(fileName, test.ToString());
            }

            Console.WriteLine("Thank you");
            var fileContents = ReadFile(fileName);
            Console.WriteLine(fileContents);
        }

        private static string ReadFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
                return reader.ReadToEnd();
        }

        private static Volunteer addVolunteer(string first)
        {
            Volunteer returnValue = new Volunteer();
            returnValue.First = first;
            Console.WriteLine("last");
            returnValue.Last = Console.ReadLine();
            Console.WriteLine("phone");
            returnValue.Phone = Console.ReadLine();
            Console.WriteLine("licence");
            returnValue.Licence = Console.ReadLine();
            return returnValue;

        }

        public static Volunteer FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Volunteer volunteer = new Volunteer();
            volunteer.First = (values[0]);
            volunteer.Last = (values[1]);
            volunteer.Phone = (values[2]);
            volunteer.Licence = (values[3]);
            return volunteer;
        }
    }
    
}
    
