///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project1
//	File Name:         User.cs
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
using System.Text.RegularExpressions;


namespace Utils
{

    /// <summary>
    /// manages user information
    /// </summary>
    public class User
    {
        /// <summary>
        /// instance of the Name class
        /// </summary>
        public Name Name;

        /// <summary>
        /// holds a phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// holds an email address
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// default constructor
        /// </summary>
        public User()
        {
            Name = new Name();
            PhoneNumber = String.Empty;
            Email = String.Empty;

        }

        /// <summary>
        /// paramaterized constructor
        /// </summary>
        /// <param name="Name">users name</param>
        /// <param name="PhoneNumber">users phone number</param>
        /// <param name="Email">users email address</param>
        public User(string Name, string PhoneNumber, string Email)
        {
            this.Name = new Name (Name);
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
        }


        /// <summary>
        /// checks the validity of a given email address
        /// </summary>
        /// <param name="Email">a given email address</param>
        /// <returns>a boolean to verify the validity</returns>
        public static bool ValidEmailCheck(string Email)
        {
            Regex emailTest = new Regex (@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            return emailTest.IsMatch (Email);
        }

        /// <summary>
        /// checks the validity of a phone number 
        /// </summary>
        /// <param name="PhoneNumber"> given phone number</param>
        /// <returns>returns a boolean to confirm the validity of an email</returns>
        public static bool ValidPhoneCheck(string PhoneNumber)
        {
            Regex phoneTest = new Regex (@"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$");
            return phoneTest.IsMatch (PhoneNumber);
        }
    }

}
