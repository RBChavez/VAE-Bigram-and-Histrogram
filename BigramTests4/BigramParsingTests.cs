using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bigram;
using System.Collections.Generic;
using System.Linq;

namespace Bigram.Tests
{
    [TestClass()]
    public class BigramParsingTests
    {
        [TestCategory("method"), TestMethod()]
        public void bigramParsingTest()
        {
            string input = "The quick brown fox and the quick blue hare.";
            BigramParsing bg = new BigramParsing();
            List<string> bigrams = bg.bigramParsing(input);
            string[] actualResults = bigrams.ToArray();
            string[] ExpectedsBigrams = new string[8];

            ExpectedsBigrams[0] = "the quick";
            ExpectedsBigrams[1] ="quick brown";
            ExpectedsBigrams[2]= "brown fox";
            ExpectedsBigrams[3] = "fox and";
            ExpectedsBigrams[4] = "and the";
            ExpectedsBigrams[5] = "the quick";
            ExpectedsBigrams[6] = "quick blue";
            ExpectedsBigrams[7] = "blue hare";

            for(int i = 0;i <ExpectedsBigrams.Length;i++)
            {
                Assert.AreEqual(ExpectedsBigrams[i], actualResults[i]);
            }
        }

        [TestCategory("method"), TestMethod()]
        public void bigramHistogramTest()
        {

            List<string> input = new List<string>();

            input.Add("the quick");
            input.Add("quick brown");
            input.Add("brown fox");
            input.Add("fox and");
            input.Add("and the");
            input.Add("the quick");
            input.Add("quick blue");
            input.Add("blue hare");

            int[] ExpectedCount = { 2, 1, 1, 1, 1, 1,1 };

            BigramParsing bg = new BigramParsing();
            Dictionary<string, int> bigramResults  = bg.bigramHistogram(input);
            int[] Results = bigramResults.Values.ToArray();

            for (int i= 0; i<ExpectedCount.Length;i++)
            {

                Assert.AreEqual(ExpectedCount[i], Results[i]);
            }
        }
    }
}