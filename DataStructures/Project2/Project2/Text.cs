///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project1
//	File Name:         text.cs
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;



namespace Project1
{
    /// <summary>
    /// represents a text file given by the user
    /// </summary>
    class Text
    {


        /// <summary>
        /// the text file in it's original format no alterations
        /// </summary>
        public string original { get; set; }

        /// <summary>
        /// Tokens from a given text file
        /// </summary>
        public List<string> tokens { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public Text()
        {
            original = String.Empty;
            tokens = new List<string> ( );

        }

        /// <summary>
        ///   paramaterized constructor
        /// </summary>
        /// <param name="file">name of text to be tokenized</param>
        public Text(String file)
        {
            if (!File.Exists (file) )
                Console.WriteLine ("File Not Found");
            StreamReader reader = null;

            try
            {
                reader = new StreamReader (file);
                original = reader.ReadToEnd ( );
            }
            catch (Exception e)
            {
                Console.WriteLine (e);
            }
            finally
            {
                if (reader != null)
                    reader.Close ( );
            }
            tokens = Utils.Utility.Tokenize(original, " ,!?.@#$%^&*()_-+=;:[]\n");
                                                 
        }

        /// <summary>
        /// Displays the the entire text file in its original form
        /// </summary>
        public void DisplayOriginal()
        {
            Console.WriteLine (original);
        }

        /// <summary>
        ///   Displays the tokens in a list
        /// </summary>
        public void DisplayTokens()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine (Utils.Utility.FormatText ("This is the tokenized Text.\n\n", 5, 70));

            Console.ForegroundColor = ConsoleColor.Blue;
            int i = 1; // counter for the subscript
            foreach(string item in tokens)
            {
                Console.WriteLine (i.ToString ( ).PadLeft (3) + ". " + Utils.Utility.FormatText (item, 10));
                i++;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ("\nThere are " + tokens.Count + " tokens.");
        }

    }
    
}
