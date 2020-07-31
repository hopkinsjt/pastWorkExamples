///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project1
//	File Name:         Name.cs
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

namespace Utils
{

    /// <summary>
    ///  formats a given name
    /// </summary>
    public class Name
    {


        /// <summary>
        /// property for a last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// property for a suffix if they have one EX. III MD. JR.
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// this property represents the rest of the unhandled name such as the first name and the middle name
        /// </summary>
        public string RestOfName{ get; set; }


        /// <summary>
        /// Default constructor
        /// </summary>
        public Name()
        {
            LastName = String.Empty;
            Suffix = String.Empty;
            RestOfName = String.Empty;

        }

        /// <summary>
        /// copy constructor
        /// </summary>
        /// <param name="name">holds an object of type Name</param>
        public Name(Name name)
        {
            LastName = name.LastName;
            Suffix = name.Suffix;
            RestOfName = name.RestOfName;

        }


        /// <summary>
        ///   paramaterized constructor
        /// </summary>
        /// <param name="input"> represents a given name</param>
        public Name(string input )
        {
            int LastComma = input.LastIndexOfAny (",".ToCharArray ( ));
            int LastSpace = input.LastIndexOfAny (" ".ToCharArray ( ));
            int FirstComma = input.IndexOfAny (",".ToCharArray ( ));
            int FirstSpace = input.IndexOfAny (" ".ToCharArray ( ));
            string temp = input;
            if(FirstSpace < 0 )
            {
                RestOfName = input;
                LastName = String.Empty;
                Suffix = String.Empty;

            }
            
            else if(LastComma<0)
            {
                Suffix = String.Empty;
                RestOfName = input.Substring (0, LastSpace).Trim ( );

            }
            else if(FirstSpace < FirstComma)
            {
                Suffix = input.Substring (FirstComma + 1).Trim ( );
                RestOfName = temp.Substring (0, FirstComma).Trim ( );

                LastName = RestOfName.Substring (LastSpace + 1).Trim ( );
                LastSpace = RestOfName.LastIndexOfAny (" ".ToCharArray ( ));
                RestOfName = temp.Substring (0, LastSpace).Trim ( );
                 
            }
            else if(LastComma > FirstComma)
            {
                Suffix = input.Substring (LastComma + 1).Trim ( );
                RestOfName = temp.Substring (0, LastComma).Trim ( );

                FirstComma = RestOfName.IndexOfAny (",".ToCharArray ( ));
                LastName = RestOfName.Substring (0, FirstComma).Trim ( );
                RestOfName = RestOfName.Substring (FirstComma + 2).Trim ( );
            }
            else if (FirstSpace> FirstComma)
            {
                LastName = input.Substring (0, FirstComma).Trim ( );
                Suffix = String.Empty;
                RestOfName = input.Substring (FirstComma + 2).Trim ( );

            }
        }

        /// <summary>
        /// formats a name in the order firstname lastname
        /// </summary>
        /// <returns>output</returns>
        public string LastNameLast ( )
        {
            string Output = String.Empty;

            setCase ( );
            if (String.IsNullOrEmpty (Suffix))
                Output = (RestOfName + " " + LastName);
            else
                Output = (RestOfName + " " + LastName + "," + Suffix);

            return Output;
        }

        /// <summary>
        /// formats a string in the order lastname, firstname
        /// </summary>
        /// <returns>output</returns>
        public string LastNameFirst()
        {
            string output = String.Empty;
            setCase ( );
            if (String.IsNullOrEmpty (Suffix))
                output = (LastName + ", " + RestOfName);
            else 
                output = (LastName + ", " + RestOfName + ", " + Suffix);


            return output;
        }

        /// <summary>
        /// capitaliazes the first letter of each string
        /// </summary>
        public void setCase()
        {
            if (!String.IsNullOrEmpty (LastName))
                LastName = char.ToUpper (LastName[0]) + LastName.Substring(1);

            if (!String.IsNullOrEmpty (RestOfName))
                RestOfName = char.ToUpper (RestOfName[0]) + RestOfName.Substring (1);
            if (!String.IsNullOrEmpty (Suffix))
                Suffix = char.ToUpper (Suffix[0]) + Suffix.Substring (1);
        }
    }
}
