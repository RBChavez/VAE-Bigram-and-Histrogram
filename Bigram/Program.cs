using System;
using System.Collections.Generic;

namespace Bigram_VAE
{
    class Program
    {
        static string filePath = "";
        static Dictionary<string, int> bigramResults = new Dictionary<string, int>();

        static void GetInput()
        {
            // promt to get input path
            Console.Write("\n Please enter the path of your text file: ");
            // get input path as a string
            filePath = Console.ReadLine();
            try
            {
                // initiate bigram class
                Bigram bigram = new Bigram();
                // validate input file path
                if (bigram.ValidateFilePath(filePath))
                {
                    bigram.BigramHistogram();
                    // assign the bigram results 
                    bigramResults = bigram.bigramResults;
                    // write error in log file
                    LogHistory.LogWrite(bigram.errorMessage);
                    // print out the results of the bigram and histogram
                    PrintOutput();
                }
                // file does not exist
                else
                {
                    PromptForFile();
                }
            }
            catch (Exception ex)
            {
                LogHistory.LogWrite(ex.Message);
            }
            
        }
        static  void PromptForFile()
        {
            // alert file does not exist
            string message = "\nfile \"" + filePath + "\" does not exist";
            Console.WriteLine(message);
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
                PromptForFile();
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

        static void Main(string[] args)
        {
            GetInput();
        }
       
    }

}
