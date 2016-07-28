using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Linq.Expressions;
using System.Timers;
using uQlustCore;
using uQlustCore.dcd;
using uQlustCore.Distance;
using uQlustCore.Profiles;


namespace uQlustTerminal
{
    class Program
    {
        static JobManager manager = new JobManager();
       // static Timer t = new Timer();
        private static void UpdateProgress(object sender, EventArgs e)
        {
           
            Dictionary<string, double> res = manager.ProgressUpdate();

            if (res == null)
            {
//                TimeInterval.Stop();
                Console.Write("\r                                                             ");
                return;
            }
            Console.Write("\r                                                                              ");
            foreach (var item in res)
                Console.Write("\rProgress " + item.Key + " " + (item.Value*100).ToString("0.00")+"%");

        }

        public static void ErrorMessage(string message)
        {
            Console.WriteLine(message);
        }
        static void Main(string[] args)
        {
            bool errors = false;
            bool times = false;
            bool binary = false;
            bool progress = false;
            bool automaticProfiles=false;
            string configFileName = "";

            //DebugMode.TurnOnDebugMode();

            Options opt = new Options();
            ClusterVis clusterOut = new ClusterVis();
            InternalProfilesManager.InitProfiles();

            if (args.Length == 0)
            {
                Console.WriteLine("Following argument is required:");
                Console.WriteLine("-f configuration_file");
                
                Console.WriteLine("Following options may be specified");
                Console.WriteLine("-e \n\t show all errors");
                Console.WriteLine("-m \n\t set the input mode\n\tRNA or PROTEIN [default PROTEIN]");
                Console.WriteLine("-n \n\t number of cores to be used");
                Console.WriteLine("-t \n\tshow time information");
                Console.WriteLine("-a \n\tgenerate automatic profiles (can be used only when aligned profile is set in configuration file)");
                Console.WriteLine("-b \n\tSave results to binary file (readable by GUI version)");
                Console.WriteLine("-p \n\tShow progres bar");
                return;
            }
            Settings set = new Settings();
            set.Load();
            if (set.profilesDir == null || set.profilesDir.Length == 0)
            {
                set.profilesDir = "generatedProfiles";
                set.Save();
            }

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-f":
                        if(i+1>=args.Length)
                        {
                            Console.WriteLine("After -f option you have to provide configuration file");
                                return;
                        }
                        if (!File.Exists(args[i + 1]))
                        {
                            Console.WriteLine("File " + args[i + 1] + " does not exist");
                            return;
                        }
                        configFileName = args[i + 1];
                        i++;
                        break;
                    case "-b":
                        binary = true;
                        break;
                    case "-n":
                        if (args.Length > i)
                        {
                            Settings s = new Settings();
                            s.Load();
                            int num;
                            try
                            {
                                num = Convert.ToInt32(args[i + 1]);
                                s.numberOfCores = num;
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine("Wrong definition of number of cores: " + ex.Message);
                                return;
                            }
                            s.Save();
                        }
                        else
                            Console.WriteLine("Number of cores has been not provided");
                        break;
                    case "-m":
                        if (args.Length > i)
                        {
                            Settings s = new Settings();
                            s.Load();
                            if (args[i + 1] == "PROTEIN")
                                s.mode = INPUTMODE.PROTEIN;
                            else
                                if (args[i + 1] == "RNA")
                                    s.mode = INPUTMODE.RNA;
                                else
                                {
                                    Console.WriteLine("Incorrect mode:" + args[i + 1]);
                                    return;
                                }
                            s.Save();
                        }
                        else
                            Console.WriteLine("No mode specified");

                        break;
                    case "-e":
                        errors = true;
                        break;
                    case "-t":
                        times = true;
                        break;
                    case "-a":
                        automaticProfiles=true;
                        break;
                    case "-p":
                        progress = true;
                        break;
                    default:
                        Console.WriteLine("Unknown option " + args[i]);
                        break;

                }
            }
            if (configFileName.Length == 0)
            {
                Console.WriteLine("Configurarion file has been not provided!");
                return;
            }

            string[] aux = null;
            try
            {
                Console.WriteLine("Configuration file " + configFileName);
                opt.ReadOptionFile(configFileName);
                if(automaticProfiles)
                    opt.GenerateAutomaticProfiles(null);
                aux = args[0].Split('.');
                manager.opt = opt;
                manager.message = ErrorMessage;
                if (progress)
                {
                    TimeIntervalTerminal.InitTimer(UpdateProgress);
                    TimeIntervalTerminal.Start();
                }
                manager.RunJob("");
                manager.WaitAllNotFinished();
                UpdateProgress(null,null);
                
                if(progress)
                    TimeIntervalTerminal.Stop();
                Console.Write("\r                                                                     ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
            }

            if (manager.clOutput.Count > 0)
            {
                foreach (var item in manager.clOutput.Keys)
                {
                    clusterOut.output = manager.clOutput[item];
                    clusterOut.SCluster(manager.clOutput[item].clusterType+"_"+opt.outputFile);
                    if (binary)
                        ClusterOutput.Save(opt.outputFile + "_" + item + ".cres", clusterOut.output);
                }
            }
            if (times)
            {
                foreach (var item in manager.clOutput)
                    Console.WriteLine(item.Value.dirName + " " + item.Value.measure + " " + item.Value.time);
            }
            if (errors)
            {
                foreach (var item in ErrorBase.GetErrors())
                    Console.WriteLine(item);
            }
            Console.WriteLine();
        }
    }
}
