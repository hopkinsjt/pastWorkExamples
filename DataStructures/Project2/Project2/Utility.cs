
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project1
//	File Name:         Utility.cs
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
    /// basic methods that can be used accross multiple projects
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// breaks a text file up into tokens based a given delimeter
        /// </summary>
        /// <param name="lines"> the given text</param>
        /// <param name="delims"> the character or characters separating the text</param>
        /// <returns></returns>
        public static List<String> Tokenize (string lines, string delims)
        {
            List<string> Tokens = new List<string> ( );
            string Work = lines;
            Work = CleanUp (Work);   
            String token;
            while (!String.IsNullOrEmpty (Work))
            {
                int Col = Work.IndexOfAny (delims.ToCharArray ( ));
                if (Col == 0)
                    Col = 1;
                token = Work.Substring (0, Col);
                Tokens.Add (token);
                Work = Work.Substring (Col);
                Work = Work.Trim (" \t".ToCharArray ( ));
            }

            return Tokens;
        }

        /// <summary>
        /// cleanes up blank space in a file
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public static string CleanUp (string work)
        {
            work = work.Trim (" \t".ToCharArray ( ));
            int col = work.IndexOf ("\r\n");
            while (col != -1)
            {
                work = work.Remove (col, 1);
                col = work.IndexOf ("\r\n");
            }
            return work;
        }

        /// <summary>
        /// lays out text with given margins
        /// </summary>
        /// <param name="text"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static string FormatText(string text, int left = 0, int right = 80)
        {
            return text.PadLeft (left).PadRight (right);
        }


        /// <summary>
        /// introduction for the user
        /// </summary>
        /// <param name="program"></param>
        /// <param name="description"></param>
        /// <param name="assignment"></param>
        public static void WelcomeMessage(string program, string description, string assignment)
        {
            string Title = "Welcome";
            string author = "Justin Hopkins";
            string authorsEmail = "hopkinsjt@goldmail.etsu.edu";
            string course = "CSCI 2210 Data Structures";
            string start = "";
            Console.Clear ( );
            start = DateTime.Today.ToLongDateString ( );
            Console.SetCursorPosition (Console.WindowWidth - start.Length, 0);
            Console.WriteLine (start);
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine ("\n\n\t  " + Title);
            Console.Write ("\t  ");
            for (int i = 0; i < Title.Length; i++)
                Console.Write ("-");
            Console.WriteLine ("\n");
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine ("\nProgram Name:     " + program.PadLeft (5));
            Console.WriteLine ("\nAuthor:     " + author.PadLeft (5));
            Console.WriteLine ("\nEmail:     " + authorsEmail.PadLeft (5));
            Console.WriteLine ("\nCourse:     " + course.PadLeft (5));
            Console.WriteLine ("\nAssignment:     " + assignment.PadLeft (5));
            Console.WriteLine ("\nDescription:     " + description.PadLeft (5));
            Console.WriteLine ("\n\n\n\n\n\n\nPress Any Key To Continue");
            Console.ReadKey ( );
            Console.Clear ( );

        }


        /// <summary>
        /// goodbye message for the user
        /// </summary>
        /// <param name="user"></param>
        public static void ExitMessage(User user)
        {
            Console.WriteLine ("\nThanks, " + user.Name.LastNameLast ( ) + ", for using this program."
                + " Remember, we are watching. \n\nEmail: "+user.Email+"\nPhone: "+user.PhoneNumber);
        }


    }
    
    
}
