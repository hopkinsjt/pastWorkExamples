///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project1
//	File Name:         Paragraph.cs
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
    /// The Paragraph represents a single paragraph from the give text
    /// </summary>
    class Paragraph
    {
        /// <summary>
        /// Number Of sentences in the paragraph
        /// </summary>
        public int NumberOfSent { get; private set; }
        
        /// <summary>
        /// Number of words in the paragraph
        /// </summary>
        public int NumberOfWords { get; private set; }

        /// <summary>
        /// represents the first index of the token list that starts the paragraph
        /// </summary>
        public int FirstSubscript { get; private set; }

        /// <summary>
        /// represents the last index of the token list that ends the paragraph
        /// </summary>
        public int LastSubscript { get; private set; }

        /// <summary>
        /// represents the average number words in a paragraph
        /// </summary>
        public double AverageLength { get; private set; }

        /// <summary>
        /// an instance of the text class
        /// </summary>
        public Text text;


        /// <summary>
        /// Default Constructor
        /// </summary>
        public Paragraph()
        {
            NumberOfSent = 0;
            NumberOfWords = 0;
            FirstSubscript = 0;
            LastSubscript = 0;
            AverageLength = 0;
            text = null;

        }

        /// <summary>
        /// parameterized constructor
        /// </summary>
        /// <param name="text">Text object given by the user</param>
        /// <param name="tokenLocation"> subscript location of the start of the text</param>
        public Paragraph(Text text, int tokenLocation)
        {
            this.text = text;
            FirstSubscript = tokenLocation;
            LastSubscript = 0;
            NumberOfSent = 0;
            NumberOfWords = 0;
            AverageLength = 0;

            int i = 0;
            int consecutive = 0;
            int last = text.tokens.Count ( );
            foreach(string token in text.tokens)
            {
                if(LastSubscript ==0)
                {
                    if(i>= tokenLocation)
                    {
                        if (token.Contains (@"\n"))
                        {
                            consecutive++;
                            if (consecutive == 2)
                            {
                                LastSubscript = i;

                            }
                        }
                        else
                            consecutive = 0;
                    }
                    if (i == last - 1)
                        LastSubscript = i;
                }
                i++;
            }
            CountSent ( );

        }
        /// <summary>
        /// counts the number of words and the number of sentences and also the average length of a sentence
        /// </summary>
        public void CountSent ( )
        {
            List<string> sentTokens = new List<string> ( );
            sentTokens = text.tokens.GetRange (FirstSubscript, (LastSubscript - FirstSubscript) + 1);
            foreach(string token in sentTokens)
            {
                if(!token.Contains(@"\n"))
                {
                    if (token.IndexOfAny (",!?.@#$%^&*()_-+=;:[]".ToCharArray ( )) < 0)
                        NumberOfWords++;
                }
                if (token.IndexOfAny (".!?".ToCharArray ( )) > -1)
                    NumberOfSent++;
            }
            if (NumberOfSent > 0)
                AverageLength = (double)NumberOfWords / (double)NumberOfSent;
        }

        /// <summary>
        /// an overridden ToString method
        /// </summary>
        /// <returns>Para- which represents a single instance of the Paragraph Class</returns>
        public override string ToString ( )
        {
            string Para = String.Empty;
            List<string> SentTokens = new List<string> ( );
            SentTokens = text.tokens.GetRange (FirstSubscript, (LastSubscript - FirstSubscript) + 1);
            bool start = true;
            foreach(string token in SentTokens)
            {
                if(!token.Contains(@"\n"))
                {
                    if (token.IndexOfAny (",!?.@#$%^&*()_-+=;:[]".ToCharArray ( )) < 0 && start == false)
                        Para += "";
                    Para += token;
                    start = false;
                }
            }

            return Para;
        }

    }
}
