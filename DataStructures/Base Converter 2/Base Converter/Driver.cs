///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project3
//	File Name:         Driver.cs
//	Description:       Convert a number from one base to another
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Justin Hopkins, Hopkinsjt@goldmail.etsu.edu, East Tennessee State University
//	Created:           Saturday, March 24, 2016
//	Copyright:         Justin Hopkins 2016
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Base_Converter
{
    /// <summary>
    /// handles IO for the base converter
    /// </summary>
    static class Driver
    {
        [STAThread]
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main ( )
        {
            Application.EnableVisualStyles ( );
            Application.SetCompatibleTextRenderingDefault (false);
            Application.Run (new BaseConversionForm ( ));
             
            
        }
    }
}
