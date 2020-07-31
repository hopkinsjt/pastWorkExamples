///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project3
//	File Name:         BaseConversionForm.cs
//	Description:       Convert a number from one base to another
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Justin Hopkins, Hopkinsjt@goldmail.etsu.edu, East Tennessee State University
//	Created:           Saturday, March 24, 2016
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
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Base_Converter
{
    /// <summary>
    /// provides functionality to a form
    /// </summary>
    public partial class BaseConversionForm : Form
    {
        public static int Num;
        public static int Base;
        public static int Digits;
        public static string temp;
        public BaseConversionForm ( )
        {
            InitializeComponent ( );
        }

        /// <summary>
        /// only allows the values to be between 2 and 16
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e"> instance containing the event data</param>
        private void numericUpDown1_ValueChanged (object sender, EventArgs e)
        {
            if (numericUpDown1.Value <= 2)
                numericUpDown1.Value = 2;
            if (numericUpDown1.Value >= 16)
                numericUpDown1.Value = 16;
            label2.Text = "Integer Value in Base ";
            label2.Text += numericUpDown1.Value.ToString ( );

        }

        /// <summary>
        /// only allows the values to be between 0 and 32
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e"> instance containing the event data</param>
        private void numericUpDown2_ValueChanged (object sender, EventArgs e)
        {
            if (numericUpDown2.Value < 0)
                numericUpDown2.Value = 0;
            if (numericUpDown2.Value > 32)
                numericUpDown2.Value = 32;


        }

        /// <summary>
        /// converts a a decimal to whatever desired base
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e"> instance containing the event data</param>
        private void button1_Click (object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show ("Please enter a number for conversion");
                    return;
            }
            temp = string.Empty;
            Num = 0;
            Base = 0;
            Digits = 0;

            Num = Int32.Parse (textBox2.Text);
            Digits = (int)numericUpDown2.Value;
            Base = (int)numericUpDown1.Value; 
            textBox1.Text = BaseConverter.FromDecimal (Base, Num, Digits);

        }

        /// <summary>
        /// converts a number of base x back to decimal
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e"> instance containing the event data</param>
        private void button2_Click (object sender, EventArgs e)
        {
            Num = 0;
            if (String.IsNullOrEmpty (textBox1.Text))
            {
                MessageBox.Show ("Please enter a number for conversion.");
                return;
            }
            Base = (int)numericUpDown1.Value;
            Num = BaseConverter.ToDecimal (Base, textBox1.Text);
            textBox2.Text = Num.ToString ( );

        }

        /// <summary>
        /// exits the application
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e"> instance containing the event data</param>
        private void button3_Click (object sender, EventArgs e)
        {
            Application.Exit ( );
        }




        /// <summary>
        /// only allows character 0-9 and a-z to be entered
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e"> instance containing the event data</param>
        private void textBox1_KeyPress (object sender, KeyPressEventArgs e)
        {
           
            if (!char.IsControl (e.KeyChar)
                && !char.IsDigit (e.KeyChar)
                && e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != '2'
                && e.KeyChar != '3' && e.KeyChar != '4' && e.KeyChar != '5'
                && e.KeyChar != '6' && e.KeyChar != '7' && e.KeyChar != '8' 
                && e.KeyChar != '9' && e.KeyChar != '0' && e.KeyChar != 'a' 
                && e.KeyChar != 'b' && e.KeyChar != 'c' && e.KeyChar != 'd'
                && e.KeyChar != 'e' && e.KeyChar != 'f' && e.KeyChar != 'A' 
                && e.KeyChar != 'B' && e.KeyChar != 'C' && e.KeyChar != 'D'
                && e.KeyChar != 'E' && e.KeyChar != 'F')
            {
                e.Handled = true;
                return;
            }
            e.Handled = false;
            return;
        }

        /// <summary>
        /// only allows characters 0-9 to be entered
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e"> instance containing the event data</param>
        private void textBox2_KeyPress (object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl (e.KeyChar)
       && !char.IsDigit (e.KeyChar)
       && e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != '2'
       && e.KeyChar != '3' && e.KeyChar != '4' && e.KeyChar != '5'
       && e.KeyChar != '6' && e.KeyChar != '7' && e.KeyChar != '8'
       && e.KeyChar != '9' && e.KeyChar != '0')
            {
                e.Handled = true;
                return;
            }
            e.Handled = false;
            return;

        }
        /// <summary>
        /// only allows uppercase letters to be entered
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e"> instance containing the event data</param>
        private void textBox1_TextChanged (object sender, EventArgs e)
        {
            textBox1.CharacterCasing = CharacterCasing.Upper;
        }

        /// <summary>
        /// Add a title to the caption bar from the Assembly.cs file
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e"> instance containing the event data</param>
        private void BAseConversionForm_Load (object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly ( );
            this.Text = assembly.GetName ( ).Name.ToString ( );
        }

       
    }
}
