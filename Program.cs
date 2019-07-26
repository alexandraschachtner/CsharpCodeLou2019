using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, @"Volunteers.csv");
            var file = new FileInfo(fileName);

        Start:
            if (file.Exists)
            {
                OpenFile(fileName);
                Console.WriteLine("Welcome");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Search by First Name to View Add or Delete a Volunteer");
                Console.WriteLine("---------Type ALL to view all Volunteers---------------");
                Console.WriteLine("");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Oops! Something went wrong.");
            }

            List<Volunteer> values = File.ReadAllLines(fileName)
                                          .Select(v => FromCsv(v))
                                          .ToList();

            var entry = Console.ReadLine();
            var entry2 = entry.ToLower();

            if (entry2 == "all")
            {
                var fileContents = ReadFile(fileName);
                Console.WriteLine("----All Volunteers----");
                Console.WriteLine("");
                Console.WriteLine(fileContents);
                Console.WriteLine("");
                Console.WriteLine("----------------------");
                goto Start;
            }
            else
            {
                var found = values.FirstOrDefault(c => c.First == entry2);
                if (found != null)
                {
                    Console.WriteLine("Volunteer Found : ");
                    Console.WriteLine(found.Print());
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine("Type DELETE to remove this Volunteer from the list.");
                    Console.WriteLine("Or press any key to continue.");
                    var yes = Console.ReadLine();
                    var yes2 = yes.ToLower();
                    if (yes2 == "delete")
                    {
                        values.Remove(found);
                        var test = values.Select(c => c.Convert()).ToArray();
                        using (StreamWriter sw = new StreamWriter(fileName))
                        {
                            for (var i = 0; i < test.Length; i++)
                                sw.WriteLine(test[i]);
                        }
                        Console.WriteLine("");
                        Console.WriteLine("Volunteer was Succesfully Deleted");
                        Console.WriteLine("");
                    }
                    else
                    {
                        goto Start;
                    }
                }
                else
                {
                    Console.WriteLine("--------------------");
                    Console.WriteLine(entry + " was not Found!!!");
                    Console.WriteLine("Please enter Last Name to continue and add new Volunteer.");
                    Console.WriteLine(" ");
                    var volunteer = AddVolunteer(entry2);
                    values.Add(volunteer);
                    var test = values.Select(c => c.Convert()).ToArray();
                    using (StreamWriter sw = new StreamWriter(fileName))
                    {
                        for (var i = 0; i < test.Length; i++)
                            sw.WriteLine(test[i]);
                    }
                    Console.WriteLine("");
                    Console.WriteLine("Volunteer was Succesfully Added");
                    Console.WriteLine("");
                }
            }





            //break
            Console.WriteLine("Thank you");
            // } while (entry != "exit");
            Console.WriteLine("--------------------");
            Console.WriteLine("press any key to exit");
            Console.ReadKey();

        }



        //Open File
        private static void OpenFile(string fileName)
        {
            using (StreamReader sr = File.OpenText(fileName))
            {
                return;
            }
        }

        // ReadFile
        private static string ReadFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }

        // AddVolunteer
        private static Volunteer AddVolunteer(string first)
        {
            Volunteer returnValue = new Volunteer();
            returnValue.First = first;
            Console.WriteLine("Last Name : ");
            returnValue.Last = Console.ReadLine();
            Console.WriteLine("Phone Number : ");
            returnValue.Phone = Console.ReadLine();
            Console.WriteLine("Licence : ");
            returnValue.Licence = Console.ReadLine();
            return returnValue;

        }
        //FromCsv
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