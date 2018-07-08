using System;
using System.IO;
using System.Collections.Generic;

namespace Bigram
{
    class Program
    {
        static string filePath = "";
        static Dictionary<string, int> bigramResults = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            GetInput();
        }
        static void GetInput()
        {

            // promt to get input path
            Console.Write("\n Please enter path of your text file: ");
            // get input path as a string
            filePath = Console.ReadLine();

            OpenFile();
        }
       
        static void FileNotExist()
        {
            // alert file is not exist
            Console.WriteLine("\nfile \"{0}\" is not exist", filePath);
            // ask please n to exist or press y to continue
            Console.Write("Press y and enter to continue | n and enter to exit: ");
            string keyInput = Console.ReadLine();
            Console.WriteLine();
            if (keyInput == "n")
            {
                return;
            }
            else if (keyInput == "y")
            {
                GetInput();
            }
            else
            {
                FileNotExist();
            }
        }

        static void OpenFile()
        {
            // open file

            try
            {
                // check if it is exist
                if ((File.Exists(filePath)))
                {
                    // count bigram and collect histrogram
                    BigramParsing BP = new BigramParsing();
                    bigramResults = BP.ReadFile(filePath);
                    PrintOutput();
                }
                // file is not exist
                else
                {
                    FileNotExist();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void PrintOutput()
        {
            foreach (KeyValuePair<string, int> keyValue in bigramResults)
            {
                Console.WriteLine("\"{0}\"{1}", keyValue.Key, keyValue.Value);
            }
            Console.Read();
        }
    }
   public class BigramParsing
    {
        public Dictionary<string, int> bigramResults = new Dictionary<string, int>();
        public string lastWorld = "";
        public Dictionary<string, int> ReadFile(string filePath)
        {
            Console.WriteLine("\n processing..");
            //readfile line by line
           
            foreach (string line in File.ReadLines(filePath))
            {

                bigramHistogram(bigramParsing(line));
            }

            return bigramResults;

        }

        public List<string> bigramParsing(string line)
        {
            List<string> output = new List<string>();
            try
            {
                //replace special charactor
                string inputTex = line.Replace(" '", " ").Replace("' ", " ").Replace(" @ ", " ").Replace(". ", " ").Replace(" (", " ").Replace(") ", " ").Replace(" \"", " ").Replace("\" ", " ");

                //split the world and collect in array
                string[] arr = inputTex.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < arr.Length; i++)
                {

                    string target_1 = lastWorld;
                    if (target_1 != "")
                    {

                        if (target_1.EndsWith(".")) target_1 = target_1.Remove(target_1.Length - 1, 1);
                        string target_2 = arr[i]; 
                        if (target_2.EndsWith(".")) target_2 = target_2.Remove(target_2.Length - 1, 1);
                        // bigram
                        string Key = target_1 + " " + target_2;
                        output.Add(Key);
                    }
                    lastWorld = arr[i]; // store last world for the next line
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " +ex.Message);
            }
            return output;
            }
        public Dictionary<string, int> bigramHistogram(List<string> bigrams)
        {
            try
            {
                foreach (string bigram in bigrams)
                {
                    if (bigramResults.ContainsKey(bigram)) // bigram which is already exist
                    {
                        bigramResults[bigram] = bigramResults[bigram] + 1; 
                    }
                    else // new bigram
                    {
                        bigramResults.Add(bigram, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return bigramResults;
        }
    }
}
