///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project2
//	File Name:         TextAnalysisToolkitForm.cs
//	Description:       provide a GUI front end for a user
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Justin Hopkins, Hopkinsjt@goldmail.etsu.edu, East Tennessee State University
//	Created:           Saturday, March 1, 2016
//	Copyright:         Justin Hopkins 2016
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using Project1;

namespace Project2
{
    /// <summary>
    /// GUI Form
    /// </summary>
    public partial class TextAnalysisToolkitForm : Form
    {
        private static Text textInput = new Text ( );
        private static Words word = new Words ( );
        private static SentenceList sentence = new SentenceList ( );
        private static ParagraphList paragraph = new ParagraphList ( );
        private static int sentenceIndex;
        private static int paragraphIndex;
        private static string temporary = String.Empty;

        /// <summary>
        ///    Creates an instance of the TextAnalysisToolkitForm
        /// </summary>
        public TextAnalysisToolkitForm ( )
        {
            InitializeComponent ( );
        }



        /// <summary>
        /// allows user to select a text file
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e"> instance containing the event data</param>
        private void OpenFileMenu (object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog ( );
            dlg.Filter = "text files|*.txt;*.text|all files|*.*";//select either text files or all files
            dlg.InitialDirectory = Application.StartupPath + @"..\..\TextData";
            dlg.Title = "Select the input file to be processed.";
            if (dlg.ShowDialog ( ) == DialogResult.OK)    //User selected a file
            {
                try
                {
                    textInput = new Text (dlg.FileName);

                }
                catch (Exception ex)
                {
                    Console.WriteLine (ex);
                }
            }
            else
                return;
            if (String.IsNullOrEmpty (textInput.original))
                Console.WriteLine ("No File found. Please try again.");
            else
            {
                word = new Words (textInput);
                sentence = new SentenceList (textInput);
                paragraph = new ParagraphList (textInput);
                sentenceIndex = 0;
                paragraphIndex = 0;
                numericUpDown1.Value = 1;
                numericUpDown2.Value = 1;

                richTextBox1.Text = textInput.original;

                listBox1.DataSource = textInput.tokens;
                listBox2.DataSource = word.DisplayList ( );

                richTextBox4.Text = sentence.sentenceList[sentenceIndex].ToString ( );
                richTextBox5.Text = paragraph.ParList[paragraphIndex].ToString ( );

                //sentence tab
                textBox1.Text = sentence.sentenceList[sentenceIndex].NumberOfWords.ToString ( );
                textBox2.Text = String.Format ("{0:0.00}", sentence.sentenceList[sentenceIndex].AverageLength);

                //paragraph tab
                textBox4.Text = paragraph.ParList[paragraphIndex].NumberOfSent.ToString ( );
                textBox5.Text = paragraph.ParList[paragraphIndex].NumberOfWords.ToString ( );
                textBox3.Text = String.Format ("{0:0.00}", paragraph.ParList[paragraphIndex].AverageLength);

                toolStripStatusLabel1.Text = dlg.SafeFileName;

                temporary  = "Tokens: " + textInput.tokens.Count;
                temporary += " Distinct Words: " + word.count;
                temporary += " Sentences: " + sentence.NumberOfSentences;
                temporary += " Paragraphs: " + paragraph.NumberOfParagraphs;
                toolStripStatusLabel2.Text = temporary;
             }

        }

        /// <summary>
        /// close the application
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e">instance containing the event</param>
        private void exitToolStripMenuItem_Click (object sender, EventArgs e)
        {
            Application.Exit ( );
        }

        /// <summary>
        /// display about box
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e">instance containing the event</param>
        private void aboutToolStripMenuItem_Click (object sender, EventArgs e)
        {
            AboutBox frame = new AboutBox();
            frame.ShowDialog ( );
        }

        /// <summary>
        ///    change selected sentence when user chances a value
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e">instance containing the event</param>
        private void numericUpDown1_ValueChanged (object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(textInput.original))
            {
                MessageBox.Show ("No data found please select a text file to analyze.");
                numericUpDown1.ValueChanged -= numericUpDown1_ValueChanged;
                numericUpDown1.Value = 0;
                numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
                return;
            }
            if (numericUpDown1.Value < 1)
                numericUpDown1.Value = 1;
            if (numericUpDown1.Value >= sentence.sentenceList.Count)
                numericUpDown1.Value = sentence.sentenceList.Count;

            sentenceIndex = (int)numericUpDown1.Value - 1;
            richTextBox4.Text = sentence.sentenceList[sentenceIndex].ToString ( );
            textBox1.Text = sentence.sentenceList[sentenceIndex].NumberOfWords.ToString ( );
            textBox2.Text = string.Format ("{0:0.00}", sentence.sentenceList[sentenceIndex].AverageLength);
        }


        /// <summary>
        /// go to the next sentence 
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e">instance containing the event</param>
        private void button2_Click (object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(textInput.original))
            {
                MessageBox.Show ("No data found please select a text file to analyze.");
                return;
            }
            if (sentenceIndex < sentence.sentenceList.Count - 1)
                sentenceIndex = sentenceIndex + 1;

            numericUpDown1.Value = sentenceIndex + 1;
        }


        /// <summary>
        /// go to the previous sentence
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e">instance containing the event</param>
        private void button1_Click (object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty (textInput.original))
            {
                MessageBox.Show ("No data found please select a text file to analyze.");
                return;
            }
            if (sentenceIndex != 0)
                sentenceIndex = sentenceIndex - 1;

            numericUpDown1.Value = sentenceIndex + 1;

        }


        /// <summary>
        /// change the paragraph when the user clicks either the up or down button
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e">instance containing the event</param>
        private void numericUpDown2_ValueChanged (object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty (textInput.original))
            {
                MessageBox.Show ("No data found please select a text file to analyze.");
                numericUpDown2.ValueChanged -= numericUpDown1_ValueChanged;
                numericUpDown2.Value = 0;
                numericUpDown2.ValueChanged += numericUpDown1_ValueChanged;
                return;
            }
            if (numericUpDown2.Value < 1)
                numericUpDown2.Value = 1;
            if (numericUpDown2.Value >= paragraph.ParList.Count)
                numericUpDown2.Value = paragraph.ParList.Count;
            paragraphIndex = (int)numericUpDown2.Value - 1;

            richTextBox5.Text = paragraph.ParList[paragraphIndex].ToString ( );
            textBox4.Text = paragraph.ParList[paragraphIndex].NumberOfSent.ToString ( );
            textBox5.Text = paragraph.ParList[paragraphIndex].NumberOfWords.ToString ( );
            textBox3.Text = String.Format ("{0:0.00}", paragraph.ParList[paragraphIndex].AverageLength);



        }

        /// <summary>
        /// go to the next paragraph
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e">instance containing the event</param>
        private void button3_Click (object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty (textInput.original))
            {
                MessageBox.Show ("No data found please select a text file to analyze.");
                return;
            }
            if (paragraphIndex < paragraph.ParList.Count - 1)
                paragraphIndex = paragraphIndex + 1;
            numericUpDown2.Value = paragraphIndex + 1;

        }

        /// <summary>
        /// go to the previous paragraph
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e">instance containing the event</param>
        private void button4_Click (object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty (textInput.original))
            {
                MessageBox.Show ("No data found please select a text file to analyze.");
                return;
            }
            if (paragraphIndex != 0)
                paragraphIndex = paragraphIndex -1;
            numericUpDown2.Value = paragraphIndex + 1;
        }

        /// <summary>
        /// application closing message 
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e">instance containing the event</param>
        private void TextAnalysisForm_FormClosing (object sender, FormClosingEventArgs e)
        {
            MessageBox.Show ("Thanks for using this program!");
        }


        /// <summary>
        /// Welcome message
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e">instance containing the event</param>
        private void TextAnalysisForm_Load (object sender, EventArgs e)
        {
            MessageBox.Show ("Welcome to the Text analysis toolkit");
            
            toolStripStatusLabel2.Text =DateTime.Today.ToLongDateString ( );

        }

        
    }
}
