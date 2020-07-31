///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project5
//	File Name:         Driver.cs
//	Description:       Game to help user learn state capitals
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Justin Hopkins, Hopkinsjt@goldmail.etsu.edu, East Tennessee State University
//	Created:           Saturday, April 23, 2016
//	Copyright:         Justin Hopkins 2016
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project5
{
    /// <summary>
    /// main entry point for the application
    /// </summary>
    static class Driver
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main ( )
        {
            Application.EnableVisualStyles ( );
            Application.SetCompatibleTextRenderingDefault (false);
            Application.Run (new SplashScreen ( ));
            Application.Run (new StateCapitalGame ( ));
        }
    }
}
