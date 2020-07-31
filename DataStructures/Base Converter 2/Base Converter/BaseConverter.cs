///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project3
//	File Name:         BaseConverter.cs
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
using System.Text;
using System.Threading.Tasks;

namespace Base_Converter
{
    /// <summary>
    /// using stacks the base converter class either converts a number
    /// from base ten to a number of a different base or vise versa
    /// </summary>
    class BaseConverter
    {
        /// <summary>
        /// converts a number of a different base to base ten
        /// </summary>
        /// <param name="baseX">base to be converted from</param>
        /// <param name="total">the string that is being converted back to base ten</param>
        /// <returns></returns>
        public static int ToDecimal(int baseX, string total)
        {
            int result = 0;
            int totalPositions = total.Length;
            Stack<string> toDecimal = new Stack<string>();
            Stack<int> final = new Stack<int> ( );
            int stackCount = 0;
            int count=0;
            
            
            
         
            
            for (int i = 0; i < totalPositions; i++)
            {
                toDecimal.Push (total[i].ToString());
               


            }
            stackCount = toDecimal.Count;
            while(count<stackCount)
            {
                int j;
                string currentNumber = toDecimal.Pop ( );
                 if(Int32.TryParse(currentNumber,out j)  )
                 {
                    
                    final.Push(j*(int)Math.Pow (baseX,count));
               
                 }
                else
                {
                    if(currentNumber =="A")
                    {
                        final.Push (10 * (int)Math.Pow (baseX, count));
                    }
                    else
                        if(currentNumber == "B")
                    {
                        final.Push (11 * (int)Math.Pow (baseX, count));
                    }
                    else
                        if (currentNumber == "C")
                    {
                        final.Push (12 * (int)Math.Pow (baseX, count));
                    }
                    else
                        if (currentNumber == "D")
                    {
                        final.Push (13 * (int)Math.Pow (baseX, count));
                    }
                    else
                        if (currentNumber == "E")
                    {
                        final.Push (14 * (int)Math.Pow (baseX, count));
                    }
                    else
                        if (currentNumber == "F")
                    {
                        final.Push (15 * (int)Math.Pow (baseX, count));
                    }

                }


                count++;
            }
            while(final.Count>0)
            {
                result += final.Pop ( );
            }

            

            
            return result;
        }
        /// <summary>
        /// converts a number of base ten to a string representation of a number in a different base
        /// </summary>
        /// <param name="baseX">base to convert to </param>
        /// <param name="total"> number being converted</param>
        /// <param name="bits">how many leading zeros to provide</param>
        /// <returns></returns>
        public static string FromDecimal (int baseX, int total, int bits)
        {
            Stack<string> baseConversion = new Stack<string> ( );
            string result = "";
            int reference;
            char[] NumbersArray = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            if (total <= 0)
                return result = "0";
            else
            {
                while (total > 0)
                {
                    reference = total % baseX;
                    total = total / baseX;
                    baseConversion.Push (NumbersArray[reference].ToString ( ));
                }

            }
            while (baseConversion.Count < bits)
                baseConversion.Push ('0'.ToString ( ));
            while (baseConversion.Count != 0)
            {                                                                         
                result += baseConversion.Pop ( ).ToString ( );
            }
            return result;
        }



    }
}
