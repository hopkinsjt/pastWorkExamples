///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project1
//	File Name:         ParagraphList.cs
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
    /// Manages a List of Paragraph objects
    /// </summary>
    class ParagraphList
    {
        /// <summary>
        /// Container for Paragraph objects
        /// </summary>
        public List<Paragraph> ParList { get; set; }

        /// <summary>
        /// Holds a count for the number of paragraphs in the list
        /// </summary>
        public int NumberOfParagraphs { get; set; }

        /// <summary>
        /// Represents the average length of a paragraph
        /// </summary>
        public double Averagelength { get; set; }


        /// <summary>
        /// Default constructor
        /// </summary>
        public ParagraphList()
        {
            ParList = new List<Paragraph> ( );
            NumberOfParagraphs = 0;
            Averagelength = 0;

        }

        /// <summary>
        /// Paramaterized constructor 
        /// </summary>
        /// <param name="text">Instance of the text object class</param>
        public ParagraphList(Text text)
        {
            ParList = new List<Paragraph> ( );
            NumberOfParagraphs = 0;
            Averagelength = 0;
            int TotalWords = 0;
            int last = text.tokens.Count ( );
            int n = 0;
            Paragraph par = new Paragraph (text, n);
            ParList.Add (par);
            NumberOfParagraphs++;
            TotalWords = TotalWords + par.NumberOfWords;
            int consecutive = 0;
            foreach(string token in text.tokens)
            {
                if(token.Contains(@"/n"))
                {
                    consecutive++;
                    if (consecutive == 2)
                    {
                        if (n != (last - 1))
                        {
                            par = new Paragraph (text, n + 1);
                            ParList.Add (par);
                            NumberOfParagraphs++;
                            TotalWords = TotalWords + par.NumberOfWords;
                            consecutive = 0;
                        }
                    }
                    else
                        consecutive = 0;
                    n++;
                }
            }
            Averagelength = (double)TotalWords / (double)NumberOfParagraphs;


        }

        /// <summary>
        /// Formats and displays paragraphs with statistics
        /// </summary>
        public void Display()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ("Number of Paragraphs Found in the Given text are:\n");

            int i = 1;
            foreach(Paragraph par in ParList)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine ("Paragraph {0}.\n", i);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine (Utils.Utility.FormatText (par.ToString ( ), 0, 70));
                Console.WriteLine ("\n\n");
                i++;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ("There are a total of {0} paragraphs. The average number"
                +" of words in the paragraphs is {1}.", NumberOfParagraphs, 
                String.Format ("{0:0.00}", Averagelength));
        }

    }
}
