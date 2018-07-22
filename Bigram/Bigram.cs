using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bigram_VAE
{
    public class BigramResults
    {
        public string errorMessage{ get; set; }
        public Dictionary<string, int> bigramResults { get; set; }
    }
    sealed public class Bigram : BigramResults
    {
        private string filePath { get; set; }
        string lastWorldInLine = "";

        /// <summary>
        /// initial instance of Bigram class
        /// </summary>
        public Bigram()
        {
            errorMessage = "";
            bigramResults = new Dictionary<string, int>();
        }
        /// <summary>
        /// Validate file path input
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ValidateFilePath(string filePath)
        {
            this.filePath = filePath;
            // validate file path
            if ((File.Exists(this.filePath)))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Count Histogram of the bigrams in the text.
        /// </summary>
        /// 
        public void BigramHistogram()
        {
            Console.WriteLine("\n processing..");
            List<string> listOfBigram = new List<string>();
            //readfile line by line
            foreach (string line in File.ReadLines(this.filePath))
            {
                listOfBigram = BigramParsing(line);
                BigramHistogram(listOfBigram);
            }
                
        }
        /// <summary>
        /// Count List of bigram
        /// </summary>
        /// <param name="bigrams"></param>
        /// <returns></returns>
        public Dictionary<string, int> BigramHistogram(List<string> bigrams)
        {
            try
            {
                // exit recursive function
                if (bigrams.Count == 0) return bigramResults;
                // To avoid a collision error from the RemoveAll method we should not share the resource.
                var newList = new List<string>(bigrams);
                // count bigrams in the list
                var count = bigrams.Count(n => n == bigrams[0]);
                // if the bigram is exist in the bigramResults 
                // count of the bigram is the previous count + new count;
                if (bigramResults.ContainsKey(bigrams[0]))
                {
                    int previousCount = 0;
                    bigramResults.TryGetValue(bigrams[0], out previousCount);
                    bigramResults[bigrams[0]] = previousCount + count;
                }
                else
                {
                    // collect the distinct bigrams and count of that bigram
                    bigramResults.Add(bigrams[0], count);
                }
                // to improve the Time Complexity O notation 
                // remove all of the bigrams that have been counted
                // So O(n-m) :m = number of the items that have been removed
                newList.RemoveAll(n => n == bigrams[0]);

                BigramHistogram( newList);
                return bigramResults; 
            }
            catch (Exception ex)
            {
                errorMessage = errorMessage + ", Error: " + ex.Message;
                return bigramResults;
            }
        }
        /// <summary>
        /// Get list of bigrams
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public List<string> BigramParsing(string strInput)
        {
            List<string> listOfBigram = new List<string>();
            try
            {
                TextTools textTools = new TextTools();

                #region Clear strInput
                strInput = textTools.RemoveParentheses(strInput);   //remove parentheses
                strInput = textTools.RemoveDoubleQoute(strInput);   //remove double qoute
                #endregion

                //split the world with space and comma, and collect in array without empty value
                string[] listOfWorld = strInput.ToLower().Split(new[] { ' ',',' }, StringSplitOptions.RemoveEmptyEntries);
                //creat list of bigrams
                for (int i = 0; i < listOfWorld.Length; i++)
                {
                    string firstWorld = lastWorldInLine;
                    firstWorld = textTools.Apostrophes(firstWorld);         // removeqoute,keep apostrophes
                    firstWorld = textTools.RemovePeriod(firstWorld);        // remove period, keep email 
                    firstWorld = textTools.RemoveDash(firstWorld);          // remove dash bollet , keep dash for a world; co-worker
                    if (firstWorld != "")
                    {
                        string secondWorld = listOfWorld[i];
                        secondWorld = textTools.Apostrophes(secondWorld);         // removeqoute,keep apostrophes
                        secondWorld = textTools.RemovePeriod(secondWorld);        // remove period, keep email 
                        secondWorld = textTools.RemoveDash(secondWorld);          // remove dash bollet , keep dash for a world; co-worker
                        // bigram
                        string bigram = firstWorld + " " + secondWorld;
                        // collect bigram
                        listOfBigram.Add(bigram);
                    }
                    // store last world for the next line
                    lastWorldInLine = listOfWorld[i]; 
                }
                errorMessage = "";
            }
            catch (Exception ex)
            {
                errorMessage = errorMessage + ", Error: " + ex.Message;
            }
            return listOfBigram;
        }        
    }
}
