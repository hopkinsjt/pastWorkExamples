///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project1
//	File Name:         SentenceList.cs
//	Description:       Analyize text that user inputs.  The text can then be sorted by distinct word, sentence, and
//                     paragraph.
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Justin Hopkins, Hopkinsjt@goldmail.etsu.edu, East Tennessee State University
//	Created:           Saturday, February 23, 2016
//	Copyright:         Justin Hopkins 2016
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Project1
{
    /// <summary>
    /// class to hold a collection of Sentence objects
    /// </summary>
    class SentenceList
    {

        /// <summary>
        /// containor for Sentence objects
        /// </summary>
        public List<Sentence> sentenceList { get; private set; }
        /// <summary>
        /// represents a total number of sentence in a lise
        /// </summary>
        public int NumberOfSentences { get; private set; }

        /// <summary>
        /// represents the average length of a sentence
        /// </summary>
        public double Averagelength { get; private set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public SentenceList()
        {
            sentenceList = new List<Sentence> ( );
            NumberOfSentences = 0;
            Averagelength = 0;

        }

        /// <summary>
        /// paramaterized Constructor
        /// </summary>
        /// <param name="text"> holds a text object</param>
        public SentenceList(Text text)
        {
            sentenceList = new List<Sentence> ( );
            NumberOfSentences = 0;
            Averagelength = 0;
            int totalWords = 0;
            int final = text.tokens.Count ( );
            int i = 0;
            Sentence sent = new Sentence (text, i);
            sentenceList.Add (sent);
            NumberOfSentences++;
            totalWords = totalWords + sent.NumberOfWords;

            foreach(string token in text.tokens)
            {
                if (token.IndexOfAny(".!?".ToCharArray())>-1)
                {
                    if(i!=(final-1))
                    {
                        sent = new Sentence (text, i + 1);
                        sentenceList.Add (sent);
                        NumberOfSentences++;
                        totalWords = totalWords + sent.NumberOfWords;

                    }
                }
                i++;
            }
            if (NumberOfSentences>0)
            {
                Averagelength = (double)totalWords / (double)NumberOfSentences;
            }
        }

        /// <summary>
        /// generates a formatted list of sentence objects with statistics 
        /// </summary>
        public void Display ( )
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ("Sentences found in the text:\n");

            int i = 1;
            foreach(Sentence sent in sentenceList)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine ("Sentence {0}.\n", i);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine (Utils.Utility.FormatText (sent.ToString ( ), 0, 70));
                Console.WriteLine ("\n\n");
                i++;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ("There are at total of {0} sentences with an average number of {1} words.", NumberOfSentences, String.Format ("{0:0.00}", Averagelength));
        }

    }
}
