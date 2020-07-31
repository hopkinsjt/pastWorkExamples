///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project1
//	File Name:         Word.cs
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

namespace Project1
{
    /// <summary>
    /// class to handle distinct words
    /// </summary>
    class Words
    {
        /// <summary>
        /// list of distinct words input by the user
        /// </summary>
        public static List<DistinctWord> wordList { get; private set; }


        /// <summary>
        /// number of words in the text
        /// </summary>
        public int count { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Words()
        {
            wordList = new List<DistinctWord> ( );
            count = 0;

        }

        /// <summary>
        /// paramaterized
        /// </summary>
        /// <param name="text"> takes in an instance of the Text object class</param>
        public Words(Text text)
        {
            count = 0;
            wordList = new List<DistinctWord> ( );
            DistinctWord word = new DistinctWord ( );
            int n = 0;
            int m =0;
            foreach(string token in text.tokens)
            {
                n = token.IndexOfAny (" , !?.@#$%^&*()_-+=;':[]/n/r".ToCharArray ( ));
                if(n<0)
                {
                    word = new DistinctWord (token);
                    m = wordList.IndexOf (word);
                    if (m > -1)
                        wordList[m].wordCount++;
                    else
                    {
                        wordList.Add (word);
                        count++;

                    }
                }

            }
            Alphabatize ( );
        }

        /// <summary>
        /// alphabatized the words in the list
        /// </summary>
        public void Alphabatize()
        {
            int i = 0;
            int j = 0;
            int count = wordList.Count ( );
            DistinctWord min = new DistinctWord ( );
            List<DistinctWord> sortedList = new List<DistinctWord> ( );
            while(j<count)
            {
                min = wordList[0];
                for(i=0; i<wordList.Count-1;i++)
                {
                    if (min.CompareTo (wordList[i + 1]) > 0)
                        min = wordList[i + 1];
                }
                sortedList.Add (min);
                wordList.Remove (min);
                j++;

            }
            
            if(sortedList.Count>0)
            {
                foreach (DistinctWord d in sortedList)
                    wordList.Add (d);
            }
        }


        /// <summary>
        /// formats and displays the words in the list
        /// </summary>
        public void Display ( )
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine (Utils.Utility.FormatText ("Distinct words were found in the text with"
                + " their Numbers of Occurences\n\n", 5, 70));
            Console.WriteLine (Utils.Utility.FormatText ("Word", 10, 41) + Utils.Utility.FormatText ("Count"));
            Console.WriteLine (Utils.Utility.FormatText ("------", 10, 41) + Utils.Utility.FormatText ("------"));
            Console.ForegroundColor = ConsoleColor.Blue;
            int i = 1;
            foreach(DistinctWord word in wordList)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine (Utils.Utility.FormatText (i.ToString ( ).PadLeft (3) + ". ", 3, 6)
                    + Utils.Utility.FormatText (word.ToString ( ), 5, 20));
                i++;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ("\nTotal of " + count + " Distinct Words.");

        }
        public List<string> DisplayList ( )
        {
            List<string> tempList = new List<string> ( );
            string temp = String.Empty;
            int i = 1;

            foreach (DistinctWord word in wordList)
            {

                //temp = i.ToString( ).PadLeft(3) + ". ".PadRight(4)
                //             temp = word.ToString( ).PadLeft(3);

                temp = i.ToString ( ).PadLeft (3) + ". "
                           + word.ToString ( );
                tempList.Add (temp);
                i++;
            }


            return tempList;
        }
    }
}
