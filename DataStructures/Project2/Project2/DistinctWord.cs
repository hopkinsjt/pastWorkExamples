///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project1
//	File Name:         DistinctWord.cs
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
    /// The DistinctWord class finds distince words and the number of occurances of said words. It uses the IEquatable and
    /// IComparable interfaces and it also has an overridden Tostring method.
    /// </summary>
    class DistinctWord : IEquatable<DistinctWord> , IComparable<DistinctWord>
    {
        /// <summary>
        /// Represents a single word from the text
        /// </summary>
        public string word { get; set; }

        /// <summary>
        /// Number of occurances of a word
        /// </summary>
        public int wordCount { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DistinctWord()
        {
            word = String.Empty;
            wordCount = 0;

        }
        /// <summary>
        /// Parametarized constructor
        /// </summary>
        /// <param name="word">represents a single word</param>
        public DistinctWord(string word)
        {
            this.word = word.ToLower ( );
            wordCount = 1;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns><see cref="System.String" /> that represents this instance.</returns>
        public override string ToString ( )
        {
            return Utils.Utility.FormatText (word, 0, 38) + Utils.Utility.FormatText (wordCount.ToString ( ));
        }
        /// <summary>
        ///      Specified distinct word
        /// </summary>
        /// <param name="other">distinct word</param>
        /// <returns></returns>
        bool IEquatable <DistinctWord>.Equals(DistinctWord other)
        {
            return this.word == other.word;
        }
        /// <summary>
        /// compares the given distinct words
        /// </summary>
        /// <param name="other">a Distinct word</param>
        /// <returns></returns>
        public int CompareTo (DistinctWord other)
        {
            return this.word.CompareTo (other.word);
        }

    }
}
