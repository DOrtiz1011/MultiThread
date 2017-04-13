﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MultiThread1
{
    /*
     * Using the codebase provided below please create a multi-threaded application based on the following:
     * 
     * Synopsis:
     *      A multi-threaded application with two threads; primary and secondary. The application will copy an input list 
     *      of data to an output list in a timed manner (secondary thread), while the primary thread displays the copied 
     *      data as it is copied from the input list to the output list.
     *      
     * APPLICATION REQUIREMENTS:
     *      Using the two Lists provided below create a multi-threaded application using the following requirements
     *      (order of items below is for discussion only, please see code below for variables noted)
     *      
     *      1.
     *      The application MUST create a secondary thread to process input data (input thread)
     *      
     *          INPUT THREAD REQUIREMENTS:
     *          
     *           a. The input thread WILL copy each string from the _inputList and place it 
     *              in the _outputList at the same position the string was found in the _inputList.
     *                      Source Data List: _inputList
     *                      Output Data List: _outputList
     * 
     *           b. The input thread WILL copy data from the beginning of the _inputList (i.e. index zero (0))
     *           
     *           c. There MUST be a 5 second delay between the time when a line is copied from the 
     *              input list to the output list. (e.g. Copy line 1 wait 5 seconds copy line 2, ..., etc.)
     *              
     *           d. The input thread MAY NOT be "contained" in the primary thread's method via lamdas/closures
     * 
     *      2.
     *      ALL created threads MUST end prior to the primary thread exiting
     * 
     *      3.
     *      The primary thread MUST display the strings of data as they are placed on the _outputList without delay ("real time")
     * 
     *      4.
     *      All lines of data must be displayed by the primary thread prior to application exit (see Expected Final Output below)
     * 
     *      5. 
     *      The primary thread MAY NOT use the known 5 second delay noted in #2 above 
     *      OR the known number of lines in the input list to assist in determining when to end
     *      (e.g. The primary thread must operate in a manner where the delay length and data size is unknown)
     *
     *
     * Expected Final Output
     *      String 1 
     *      String 2
     *      String 3
     *      String 4
     *      String 5
     */

    internal sealed class Program
    {
        private static readonly List<string> _inputList = new List<string> { "String 1", "String 2", "String 3", "String 4", "String 5" };
        private readonly List<string> _outputList = new List<string>();
        private readonly int inputCount = _inputList.Count;
        private int outputCount;
        private const int sleep = 5000;

        /// <summary>
        /// 
        /// </summary>
        private void CopyValue()
        {
            if (_inputList.Count > 0)
            {
                var value = _inputList.First();
                _inputList.RemoveAt(0);
                _outputList.Add(value);
                Thread.Sleep(sleep);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void WriteValue()
        {
            if (outputCount < _outputList.Count)
            {
                Console.WriteLine($"{_outputList.Last()} {DateTime.Now}");
                outputCount = _outputList.Count;
            }
        }

        /// <summary>
        /// Use this Method as the program's primary thread
        /// </summary>
        private void Run()
        {
            Console.WriteLine($"*** START {DateTime.Now} ***");
            Console.WriteLine();

            var thread = new Thread(CopyValue);

            while (outputCount < inputCount)
            {
                if (!thread.IsAlive)
                {
                    thread = new Thread(CopyValue) { IsBackground = true };
                    thread.Start();
                }

                WriteValue();
            }

            Console.WriteLine();
            Console.WriteLine($"*** END {DateTime.Now} ***");
            Console.ReadLine();
        }

        /// <summary>
        /// 
        /// </summary>
        private static void Main()
        {
            new Program().Run();
        }
    }
}
