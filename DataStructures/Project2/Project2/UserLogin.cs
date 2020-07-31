///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project2
//	File Name:         UserLogin.cs
//	Description:       provide a GUI front end for a user
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Justin Hopkins, Hopkinsjt@goldmail.etsu.edu, East Tennessee State University
//	Created:           Saturday, March 1, 2016
//	Copyright:         Justin Hopkins 2016
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;
using Project1;

namespace Project2
{
    /// <summary>
    /// Display form for user to give their name and email
    /// </summary>
    public partial class UserLogin : Form
    {

        private static User user;
        private static Name name;
        private static string strName = String.Empty;
        private static string email = String.Empty;

        /// <summary>
        /// initializes a new instance of the UserLogin form
        /// </summary>
        public UserLogin ( )
        {
            InitializeComponent ( );
        }



        /// <summary>
        /// if user user clicks okay validate information
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e">instance containing the event</param>
        private void button1_Click (object sender, EventArgs e)
        {
            bool emailCheck = false;
            int emailError = 0;
            int nameError = 0;
            strName = textBox1.Text;
            string phoneNumber = "423-626-0098";

            try
            {
                name = new Name (strName);
            }
            catch (Exception ex)
            {
                Console.WriteLine ("\n{0}", ex);
                nameError = 1;

            }
            if (nameError == 1)
                label3.Visible = true;
            else
                label3.Visible = false;

            email = textBox2.Text;
            emailCheck = User.ValidEmailCheck (email);
            if (!emailCheck)
                emailError = 1;
            if (emailError == 1)
                label4.Visible = true;
            else
                label4.Visible = false;

            if (nameError == 1 || emailError == 1)
                return;
            else
            {
                user = new User (strName, phoneNumber, email);
                MessageBox.Show ("We have comfirmed your information:\n" + name.LastNameLast() + "\n" + user.Email);
                this.Close ( );
            }

        }

        /// <summary>
        /// if user hits cancel exit the form
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e">instance containing the event</param>
        private void button2_Click (object sender, EventArgs e)
        {
            this.Close ( );
        }

      
    }
}
