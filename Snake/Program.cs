﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Serilog;
using QLearning;

namespace Snake
{
    internal class Program
    {
        #region Logger
        private static Serilog.ILogger s_log = new LoggerConfiguration().WriteTo.Console().MinimumLevel.Verbose().CreateLogger().ForContext(typeof(Program));
        #endregion
       
        [STAThread]
        static void Main(string[] args)
        {
            ParseOptions(args);
        }

        public static void ParseOptions(string[] args)
        {
            var o = new Options();
            try
            {
                o.Parse(args);
                if (o.auto && o.manual)
                {
                    throw new ArgumentException("Invalid options: Choose either -a (auto) or -m (manual), not both.");
                }
                if (o.manual)
                {
                    GameHandler.StartGame();
                }
                else if (o.auto)
                {
                    QLearningProgram.Start();
                }
                else
                {
                    s_log.Error("No arguments passed to the debugger.");
                    Console.ReadKey();
                    throw new ArgumentException("No valid option selected. Use -a for auto or -m for manual.");
                }
            }
            catch (Exception ex)
            {
                s_log.Error($"Error parsing options: {ex.Message}");
            }
        }

    }
}
