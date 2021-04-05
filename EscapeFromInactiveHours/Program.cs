using Microsoft.Win32;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace EscapeFromInactiveHours
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            System.Timers.Timer myTimer = new System.Timers.Timer();
            myTimer.Elapsed += MyTimer_Elapsed;
            myTimer.Interval = TimeSpan.FromHours(4).TotalMilliseconds;
            myTimer.AutoReset = true;
            MyTimer_Elapsed(null, null);
            myTimer.Start();
            
            while (true) {
                Thread.Sleep((int)TimeSpan.FromHours(1).TotalMilliseconds);
            }
        }

        private static void MyTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var now = DateTime.Now;
            int inicio = now.AddHours(-1).AddMinutes(-now.Minute).AddSeconds(-now.Second).Hour;
            int fim = now.AddHours(8).AddMinutes(-now.Minute).AddSeconds(-now.Second).Hour;
            

            Console.Out.WriteLine();
            Console.Out.WriteLine(DateTime.Now + " Setting active hours to: " + inicio + " to " + fim);

            using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(@"SOFTWARE\Microsoft\WindowsUpdate\UX\Settings", true))
            {
                //foreach()

                //Console.WriteLine(registryKey.SubKeyCount);//registry is not null
                //foreach (var VARIABLE in registryKey.GetValueNames())
                //{
                //    Console.WriteLine(VARIABLE);//here I can see I have many keys
                //                                //no need to switch to x64 as suggested on other posts
                //}

                registryKey.SetValue("ActiveHoursStart", inicio, RegistryValueKind.DWord);
                registryKey.SetValue("ActiveHoursEnd", fim, RegistryValueKind.DWord);


            }

        }
    }
}
