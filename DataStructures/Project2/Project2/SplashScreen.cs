///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project2
//	File Name:         SplashScreen.cs
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

namespace Project2
{
    /// <summary>
    /// splash screen 
    /// </summary>
    public partial class SplashScreen : Form
    {
        /// <summary>
        /// initialize the splash screen
        /// </summary>
        public SplashScreen ( )
        {
            InitializeComponent ( );
        }

        /// <summary>
        /// timer gives a progress bar and closes the application
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e">instance containing the event</param>
        private void timer1_Tick (object sender, EventArgs e)
        {
            progressBar1.Increment (1);
            if (progressBar1.Value == 100)
            {
                timer1.Stop ( );
                this.Close ( );
            }
        }
    }
}
