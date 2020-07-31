///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project5
//	File Name:         SplashScreen.cs
//	Description:       Game to help user learn state capitals
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Justin Hopkins, Hopkinsjt@goldmail.etsu.edu, East Tennessee State University
//	Created:           Saturday, April 23, 2016
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

namespace Project5
{
    /// <summary>
    /// Display a splash screen to the user
    /// </summary>
    public partial class SplashScreen : Form
    {
        private int count = 0;
        /// <summary>
        /// Initializes the splash screen
        /// </summary>
        public SplashScreen ( )
        {
            InitializeComponent ( );
            this.CenterToScreen ( );
        }

        /// <summary>
        /// timer closes the splash screen once count hits three
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick (object sender, EventArgs e)
        {
            count++;
            if(count==3)
            {
                this.Close();

            }    
           
        }
    }
}
