///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project1
//	File Name:         Sentence.cs
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
    /// Represents a sentence  from a given text
    /// </summary>
    class Sentence
    {
        /// <summary>
        /// represents the Number of words in a sentence
        /// </summary>
        public int NumberOfWords { get; private set; }

        /// <summary>
        /// represent the subscript of the token at the start of the sentence
        /// </summary>
        public int FirstSubscript { get; private set; }

        /// <summary>
        /// represents the subscript of the token at the end of the sentence
        /// </summary>
        public int LastSubscript { get; private set; }

        /// <summary>
        /// represent the acerage length of a sentence
        /// </summary>
        public double AverageLength { get; private set; }

        /// <summary>
        /// Instance of the text class
        /// </summary>
        public Text text;


        /// <summary>
        /// default constructor
        /// </summary>
        public Sentence ( )
        {
            NumberOfWords = 0;
            FirstSubscript = 0;
            LastSubscript = 0;
            AverageLength = 0;
            text = null;
        }

        /// <summary>
        /// Parametarized constructor
        /// </summary>
        /// <param name="text"> Text Object</param>
        /// <param name="location">subscript location of the start of the text</param>
        public Sentence(Text text, int location)
        {
            NumberOfWords = 0;
            FirstSubscript = location;
            LastSubscript = 0;
            AverageLength = 0;
            this.text = text;
            int i = 0;
            int last = text.tokens.Count ( );

            foreach (string token in text.tokens)
            {
                if(LastSubscript == 0)
                {
                    if(i >= location)
                    {
                        if (token.IndexOfAny (".!?".ToCharArray ( )) > -1)
                            LastSubscript = i;
                        else if (i == last)
                            LastSubscript = i;

                    }
                }
                i++;
            }
            CountWords ( );
        }

        /// <summary>
        /// count the number of words in a sentence and finds the average length of a sentence
        /// </summary>
        public void CountWords()
        {
            string sent = String.Empty;
            List<string> sentTokens = new List<string> ( );
            sentTokens = text.tokens.GetRange (FirstSubscript, (LastSubscript - FirstSubscript) + 1);
            int totalWordLength = 0;
            foreach(string token in sentTokens)
            {
                if(!token.Contains(@"\n"))
                {
                    if(token.IndexOfAny(",!?.@#$%^&*()_-+=;:[]".ToCharArray())<0)
                    {
                        NumberOfWords++;
                        totalWordLength = totalWordLength + token.Length;
                    }
                }
            }
            if(NumberOfWords>0)
            {
                AverageLength = (double)totalWordLength / (double)NumberOfWords;
            }
        }

        /// <summary>
        /// Overridden instance of the ToString class represents a single instance
        /// </summary>
        /// <returns></returns>
        public override string ToString ( )
        {
            string sent = String.Empty;
            List<string> sentTokens = new List<string> ( );
            sentTokens = text.tokens.GetRange (FirstSubscript, (LastSubscript - FirstSubscript) + 1);
            bool start = true;

            foreach (string token in sentTokens)
            {
                if (!token.Contains (@"\n"))
                {
                    if (token.IndexOfAny (",!?.@#$%^&*()_-+=;:[]".ToCharArray ( )) < 0 && start == false)
                        sent += " ";
                    sent += token;
                    start = false;

                }
            }
            return sent;

            
        }
    }
}
