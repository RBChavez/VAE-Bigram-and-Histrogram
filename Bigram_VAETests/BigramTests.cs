using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Bigram_VAE.Tests
{
    [TestClass()]
    public class BigramTests
    {
        #region BigramParsing
        List<string> expectedsBigrams = new List<string>();
        private void GetExpectedsBigrams()
        {
            expectedsBigrams.Add("the quick");
            expectedsBigrams.Add("quick brown");
            expectedsBigrams.Add("brown fox");
            expectedsBigrams.Add("fox and");
            expectedsBigrams.Add("and the");
            expectedsBigrams.Add("the quick");
            expectedsBigrams.Add("quick blue");
            expectedsBigrams.Add("blue hare");
        }

        private void CompareEqual(string input)
        {
            Bigram bg = new Bigram();
            List<string> bigrams = bg.BigramParsing(input);
            string[] actualResults = bigrams.ToArray();

            GetExpectedsBigrams();

            for (int i = 0; i < expectedsBigrams.Count; i++)
            {
                Assert.AreEqual(expectedsBigrams[i], actualResults[i]);
            }
        }
        [TestCategory("basic"), TestMethod()]
        public void BigramParsing_ExampleInput_ReturnEqual()
        {
            string input = "The quick brown fox and the quick blue hare.";
            Bigram bg = new Bigram();
            List<string> bigrams = bg.BigramParsing(input);
            string[] actualResults = bigrams.ToArray();
            GetExpectedsBigrams();
            for (int i = 0; i < expectedsBigrams.Count; i++)
            {
                Assert.AreEqual(expectedsBigrams[i], actualResults[i]);
            }
        }

        [TestCategory("repeat"), TestMethod()]
        public void BigramParsing_ExampleInputRepeat1000_ReturnEqual()
        {
            string input = "";
            for (int i = 0; i <= 1000; i++)
            {
                input = input + "The quick brown fox and the quick blue hare. ";
            }
            Bigram bg = new Bigram();
            List<string> bigrams = bg.BigramParsing(input);
            string[] actualResults = bigrams.ToArray();
            for (int i = 0; i <= 1000; i++)
            {
                GetExpectedsBigrams();
                if (i < 1000)
                    expectedsBigrams.Add("hare the");
            }
            for (int i = 0; i < expectedsBigrams.Count; i++)
            {
                Assert.AreEqual(expectedsBigrams[i], actualResults[i]);
            }
        }

        [TestCategory("Qoute"), TestMethod()]
        public void BigramParsing_DoubleQouteInput_ReturnEqual()
        {
            string input = "\"The quick brown fox and the quick blue hare.\"";
            CompareEqual(input);
        }

        [TestCategory("Qoute"), TestMethod()]
        public void BigramParsing_SingleQouteInput_ReturnEqual()
        {
            string input = "The quick brown fox and the quick blue hare.";
            CompareEqual(input);
        }

        [TestCategory("Qoute"), TestMethod()]
        public void BigramParsing_SingleQouteSInput_ReturnEqual()
        {
            string input = "It's the quick brown fox and the quick blue hare.";
            expectedsBigrams.Add("it's the");
            CompareEqual(input);
        }

        [TestCategory("Qoute"), TestMethod()]
        public void BigramParsing_SingleQouteTInput_ReturnEqual()
        {
            string input = "Can't the quick brown fox and the quick blue hare.";
            expectedsBigrams.Add("can't the");
            CompareEqual(input);
        }

        [TestCategory("parentheses"), TestMethod()]
        public void BigramParsing_ParenthesesInput_ReturnEqual()
        {
            string input = "(The quick brown fox and the quick blue hare.)";
            CompareEqual(input);
        }

        [TestCategory("parentheses"), TestMethod()]
        public void BigramParsing_CurlyParenthesesInput_ReturnEqual()
        {
            string input = "{The quick brown fox and the quick blue hare.}";
            CompareEqual(input);
        }

        [TestCategory("parentheses"), TestMethod()]
        public void BigramParsing_BracketsParenthesesInput_ReturnEqual()
        {
            string input = "[The quick brown fox and the quick blue hare.]";
            CompareEqual(input);
        }

        [TestCategory("space"), TestMethod()]
        public void BigramParsing_DoubleSpaceInput_ReturnEqual()
        {
            string input = "          The quick brown fox and the quick blue hare.";
            CompareEqual(input);
        }

        [TestCategory("space"), TestMethod()]
        public void BigramParsing_DoubleSpaceDoubleQuoteInput_ReturnEqual()
        {
            string input = "\"   The quick   brown fox    and the    quick blue   hare. \"";
            CompareEqual(input);
        }

        [TestCategory("casesensative"), TestMethod()]
        public void BigramParsing_CaseSensativeInput_ReturnEqual()
        {
            string input = "THE qUICK BROWN FOX AND tHE QUICK BLUE HARE.";
            CompareEqual(input);
        }

        [TestCategory("assign"), TestMethod()]
        public void BigramParsing_AssignInput_ReturnEqual()
        {
            string input = "= The quick brown fox and the quick blue hare.";
            expectedsBigrams.Add("= the");
            CompareEqual(input);
        }

        [TestCategory("separator"), TestMethod()]
        public void BigramParsing_CommaInput_ReturnEqual()
        {
            string input = "The,quick,brown,fox and the quick blue hare.";
            CompareEqual(input);
        }

        [TestCategory("Number"), TestMethod()]
        public void BigramParsing_NumberInput_ReturnEqual()
        {
            string input = "1 The quick brown fox and the quick blue hare.";
            expectedsBigrams.Add("1 the");
            CompareEqual(input);
        }

        [TestCategory("Slash"), TestMethod()]
        public void BigramParsing_DoubleSlashInput_ReturnEqual()
        {
            string input = " \\ The quick brown fox and the quick blue hare.";
            expectedsBigrams.Add("\\ the");
            CompareEqual(input);
        }

        [TestCategory("Slash"), TestMethod()]
        public void BigramParsing_SingleSlashInput_ReturnEqual()
        {
            string input = @"\ The quick brown fox and the quick blue hare.";
            expectedsBigrams.Add(@"\ the");
            CompareEqual(input);
        }

        [TestCategory("Slash"), TestMethod()]
        public void BigramParsing_SingleSlashWIthWorldInput_ReturnEqual()
        {
            string input = @"the\ The quick brown fox and the quick blue hare.";
            expectedsBigrams.Add(@"the\ the");
            CompareEqual(input);
        }
        [TestCategory("email"), TestMethod()]
        public void BigramParsing_EmailInput_ReturnEqual()
        {
            string input = @"john@gmail.com The quick brown fox and the quick blue hare.";
            expectedsBigrams.Add(@"john@gmail.com the");
            CompareEqual(input);
        }
        [TestCategory("dash"), TestMethod()]
        public void BigramParsing_dashInWorldInput_ReturnEqual()
        {
            string input = @"co-worker The quick brown fox and the quick blue hare.";
            expectedsBigrams.Add(@"co-worker the");
            CompareEqual(input);
        }
        [TestCategory("dash"), TestMethod()]
        public void BigramParsing_dashBolletInput_ReturnEqual()
        {
            string input = @"-The quick brown fox and the quick blue hare.";
            CompareEqual(input);
        }
#endregion

        #region BigramHistogram
        int[] expectedCount = { 2, 1, 1, 1, 1, 1, 1 };

        [TestCategory("basic"), TestMethod()]
        public void BigramHistogram_ExampleInput_ReturnEqual()
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


            Bigram bg = new Bigram();
            Dictionary<string, int> bigramResults = bg.BigramHistogram(input);
            List<int> Results = new List<int>(bigramResults.Values);

            for (int i = 0; i < expectedCount.Length; i++)
            {
                Assert.AreEqual(expectedCount[i], Results[i]);
            }
        }

        [TestCategory("basic"), TestMethod()]
        public void BigramHistogram_ExampleShuffleInput_ReturnEqual()
        {
            List<string> input = new List<string>();

            input.Add("fox and");
            input.Add("the quick");
            input.Add("quick brown");
            input.Add("brown fox");
            input.Add("and the");
            input.Add("quick blue");
            input.Add("blue hare");
            input.Add("the quick");


            Bigram bg = new Bigram();
            Dictionary<string, int> bigramResults = bg.BigramHistogram(input);
            List<int> Results = new List<int>(bigramResults.Values);
            int[] ExpectedCount = { 1, 2, 1, 1, 1, 1, 1 };
            for (int i = 0; i < ExpectedCount.Length; i++)
            {
                Assert.AreEqual(ExpectedCount[i], Results[i]);
            }
        }

        [TestCategory("repeat"), TestMethod()]
        public void BigramHistogram_Repleat1000_ReturnEqual()
        {
            List<string> input = new List<string>();

            for (int i = 0; i < 1000; i++)
            {
                input.Add("the quick");
                input.Add("quick brown");
                input.Add("brown fox");
                input.Add("fox and");
                input.Add("and the");
                input.Add("the quick");
                input.Add("quick blue");
                input.Add("blue hare");
            }

            Bigram bg = new Bigram();
            Dictionary<string, int> bigramResults = bg.BigramHistogram(input);
            List<int> Results = new List<int>(bigramResults.Values);
            int[] ExpectedCount = { 2000, 1000, 1000, 1000, 1000, 1000, 1000 };
            for (int i = 0; i < ExpectedCount.Length; i++)
            {
                Assert.AreEqual(ExpectedCount[i], Results[i]);
            }
        }

        [TestCategory("basic"), TestMethod()]
        public void BigramHistogram_ExampleInputCallTwice_ReturnEqual()
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
            
            Bigram bg = new Bigram();
            Dictionary<string, int> bigramResults = bg.BigramHistogram(input);
            List<int> Results = new List<int>(bigramResults.Values);
            
            bigramResults = bg.BigramHistogram(input);
            Results = new List<int>(bigramResults.Values);
            int[] ExpectedCount = { 4, 2, 2, 2, 2, 2, 2 };
            for (int i = 0; i < ExpectedCount.Length; i++)
            {
                Assert.AreEqual(ExpectedCount[i], Results[i]);
            }
        }

        [TestCategory("mirror"), TestMethod()]
        public void BigramHistogram_Mirror_ReturnEqual()
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

            #region Mirror example
            input.Add("blue hare");
            input.Add("quick blue");
            input.Add("the quick");
            input.Add("and the");
            input.Add("fox and");
            input.Add("brown fox");
            input.Add("quick brown");
            input.Add("the quick");
            #endregion

            Bigram bg = new Bigram();
            Dictionary<string, int> bigramResults = bg.BigramHistogram(input);
            List<int> Results = new List<int>(bigramResults.Values);
            int[] ExpectedCount = { 4, 2, 2, 2, 2, 2, 2 };
            for (int i = 0; i < ExpectedCount.Length; i++)
            {
                Assert.AreEqual(ExpectedCount[i], Results[i]);
            }
        }

        [TestCategory("oppsit"), TestMethod()]
        public void BigramHistogram_Opposit_ReturnEqual()
        {
            List<string> input = new List<string>();

            input.Add("the quick");
            input.Add("quick the");
            input.Add("the quick");
            input.Add("quick the");
            input.Add("quick the");


            Bigram bg = new Bigram();
            Dictionary<string, int> bigramResults = bg.BigramHistogram(input);
            List<int> Results = new List<int>(bigramResults.Values);
            int[] ExpectedCount = { 2,3 };
            for (int i = 0; i < ExpectedCount.Length; i++)
            {
                Assert.AreEqual(ExpectedCount[i], Results[i]);
            }
        }
        #endregion
    }
}