﻿using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            // connecting to directory of Volunteers.csv
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.Parent.Parent.Parent.FullName, @"Volunteers.csv");
            var file = new FileInfo(fileName);
            var fileContents = ReadFile(fileName);
            if (file.Exists)
            {
                // writing file contents to test
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = " ";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                        Console.WriteLine("-----------");
                        //Console.WriteLine("  ");
                    }
                }
            }
            else
            {
                Console.WriteLine("Somehting Went Wrong");
            }
            // This ends the first promt in the console. 

            //----------------------------------------------------------------
            //Convert to string array?  im still a bit confused with this part
            //string[] values = File.ReadAllLines(fileName);

            //var values2 = values.Skip(1)
            //                              .Select(v => FromCsv(v))
            //                              .ToList();
            //end of confusing bit ------------------------------------------
            //I've changed this to a list
            
             List<Volunteer> values = File.ReadAllLines(fileName)
                                           .Select(v => FromCsv(v))
                                           .ToList();

            //Start of input for program functions
            Console.WriteLine("Hello please search for vounteer by name");
            var entry = Console.ReadLine();

            var found = values.FirstOrDefault(c => c.First == entry);

            if (found != null)
            {
                Console.WriteLine(found.Print());
                Console.WriteLine("Did you want to delete this Volunteer?");
                Console.WriteLine("-----Y or N-------");
                // Want to make it delete the line if y is entered 
                var yes = Console.ReadLine();
                var yes2 = yes.ToLower();
                if (yes2 == "y")
                {
                    values.Remove(found);

                }
            }
            else
            {
                Console.WriteLine("Not Found!!! Please enter new Volunteer Info:");
                Console.WriteLine("--------------------");
                Console.WriteLine(" ");
                var volunteer = AddVolunteer(entry);
                values.Add(volunteer);
                var test = values.Select(c => c.Convert()).ToArray();
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    for (var i = 0; i < test.Length; i++)
                        sw.WriteLine(test[i]);
                }
              /* This last bit should write the new line to the csv, but the csv is not getting updated. Would replacing
               WriteAllLines with AppendAllLines?  Append requires an IEnumerator and im not sure how to change everyhting
               over to that or if its nessicary. WriteAllLines creates a new file, hence why it doesnt add to my csv, and 
               when the File.Delete is called, it only delets all the new information*/
            }

            Console.WriteLine("Thank you");
            Console.WriteLine("--------------------");
            Console.WriteLine(" ");
            return;
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
            Console.WriteLine("last");
            returnValue.Last = Console.ReadLine();
            Console.WriteLine("phone");
            returnValue.Phone = Console.ReadLine();
            Console.WriteLine("licence");
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
    
