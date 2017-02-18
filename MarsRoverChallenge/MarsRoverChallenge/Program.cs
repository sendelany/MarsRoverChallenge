using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverChallenge
{
    class Program
    {
        private static Logger logger = LogManager.GetLogger("Program");
        private static FileSystemWatcher watcher = new FileSystemWatcher();
        private static TerrainZone terrainZone;
        private static CommandFileLoader cmdLoader;

        static void Main(string[] args)
        {
            Console.WriteLine("Mars rover challenge");
            Console.WriteLine("");

            string commandFilePath = ConfigurationManager.AppSettings["CommandFile"];

            //First things first. Check if our command file exists.
            if (File.Exists(commandFilePath))
            {
                try
                {
                    cmdLoader = new CommandFileLoader(commandFilePath);
                    MoveRover(cmdLoader.Rover);
                    CreateFileWatcher(commandFilePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            else
            {
                string cmdFileMissing = "Error: The command file: '" + commandFilePath + "' does not exist. Please create the file and change the application setting 'CommandFile' to point to the new Command file then restart the Rover";

                logger.Error(cmdFileMissing);
                Console.WriteLine(cmdFileMissing);
            }

            Console.ReadLine();
        }

        public static void CreateFileWatcher(string path)
        {

            watcher.Path = Path.GetDirectoryName(path);
            watcher.Filter = Path.GetFileName(path);

            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            watcher.Changed += new FileSystemEventHandler(OnChanged);

            watcher.EnableRaisingEvents = true;
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                watcher.EnableRaisingEvents = false;
                Console.WriteLine("");
                Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
                logger.Log(LogLevel.Info, "File: " + e.FullPath + " " + e.ChangeType);

                cmdLoader.Load();
                MoveRover(cmdLoader.Rover);
            }
            finally
            {
                watcher.EnableRaisingEvents = true;
            }
        }

        private static void MoveRover(Rover rover)
        {
            Console.WriteLine("Initial Rover Position: '" + rover.CurrentPosition + "'");
            Console.WriteLine("");
            Console.WriteLine("Ready to execute movement commands " + new string(rover.MovementCommands));
            Console.WriteLine("");
            foreach (var command in rover.MovementCommands)
            {
                try
                {
                    Console.WriteLine(rover.ExecuteCommand(command) + "'. Rover is in transit.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " Command execution has been terminated.");
                    break;
                }
            }
            Console.WriteLine("");
            Console.WriteLine("The Rover is at rest in position: '" + rover.CurrentPosition + "'. Awaiting further instructions....");
            Console.WriteLine("");



            int rowLength = rover.TerrainZone.CartesianZoneGrid.GetLength(0);
            int colLength = rover.TerrainZone.CartesianZoneGrid.GetLength(1);

            for (int i = 0; i < colLength; i++)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    if (j == rover.HorizontalPosition && i == ((rover.TerrainZone.VerticalLength - rover.VerticalPosition) - 1)) Console.Write(string.Format("  {0} ", rover.Facing));
                    else Console.Write(string.Format(" {0}", " * "));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            
        }
    }
}
