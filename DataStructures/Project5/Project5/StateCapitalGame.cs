///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project5
//	File Name:         StateCapitalGame.cs
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project5
{
    /// <summary>
    /// Allows the user when prompted to select the correct state capital that matches the state they were given.
    /// </summary>
    public partial class StateCapitalGame : Form
    {
        private static int time = 0;
        private static double score = 0;
        private string stateMatch = String.Empty;
        private static int attempts = 0;
        private static int correctAnswers = 0;
        Random randomState = new Random ( );
        public  static SortedDictionary<string, string> statesAndCapitals = new SortedDictionary<string, string> ( );


        /// <summary>
        /// initalizes the form
        /// </summary>
        public StateCapitalGame ( )
        {
            InitializeComponent ( );
            this.CenterToScreen ( );
            this.Text = Application.ProductName;
            GetData ( );
            listBox1.DataSource = new BindingSource (statesAndCapitals.Values, null);
        }
        /// <summary>
        /// Fills the SortedDictionary.
        /// </summary>
        public static void GetData()
        {
            string input = String.Empty;
            string capital = String.Empty;
            string state = String.Empty;
            try
            {


                StreamReader rdr = new StreamReader ("..\\..\\StateData\\states.txt");
                while (rdr.Peek ( ) != -1)
                {
                   input = rdr.ReadLine ( );
                    String[] fields = input.Split (',');
                    capital = fields[0];
                    state = fields[1];

                    statesAndCapitals[state] = capital;

                }
                rdr.Close ( );
            }
            catch (Exception ex)
            {
                MessageBox.Show ("File Not Found: " + ex);
            }

        }

        /// <summary>
        /// handles the event when the user makes a selection in the capital list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged (object sender, EventArgs e)
        {
            if (time > 0 && timer1.Enabled)
            {
                if (statesAndCapitals.TryGetValue (stateMatch, out stateMatch))
                {
                    attempts++;
                    if (stateMatch.Equals (listBox1.SelectedItem))
                    {
                        correctAnswers++;
                        time = 0;
                        timer1.Stop ( );
                        timer1.Enabled = false;
                        MessageBox.Show ("That is Correct!");
                    }
                    else
                    {
                        time = 0;
                        timer1.Enabled = false;
                        timer1.Stop ( );
                        MessageBox.Show ("Sorry, that is incorrect");
                    }

                }

            }
            Attempts.Text = attempts.ToString ( );
            textBox1.Text = correctAnswers.ToString ( );

        }

        /// <summary>
        /// Handles the event when the user clickss the next state button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click (object sender, EventArgs e)
        {
            stateMatch = statesAndCapitals.ElementAt (randomState.Next (0,50)).Key;
            StateBox.Text = stateMatch;
            time = 15;
            timer1.Enabled = true;
            timer1.Start ( );
            textBox2.Text = time.ToString ( );
        }

        /// <summary>
        /// countdowns the time the user has to select the correct state capital
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick (object sender, EventArgs e)
        {
            time--;
            textBox2.Text = time.ToString ( );

            if(time==0)
            {
                timer1.Stop ( );
                timer1.Enabled = false;
                MessageBox.Show ("Time has expired");
                attempts++;
                Attempts.Text = attempts.ToString ( );
            }
        }

        /// <summary>
        /// ends the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click (object sender, EventArgs e)
        {
            timer1.Stop ( );
            Application.Exit ( );
        }

        /// <summary>
        /// provides an exit message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing (object sender, FormClosingEventArgs e)
        {
            if (attempts > 0)
            {
                score = (double)correctAnswers / (double)attempts;
                score = score * 100;
            }
            
            MessageBox.Show ("Thank You for Testing Your Knowledge!\n\n\nYour score was " + String.Format ("{0:0.0}", score) + "% on " + attempts + " attempts", "Thank You");
        }
    }
}
