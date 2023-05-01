using System.Diagnostics;
using System.DirectoryServices;

namespace MyBO3injector
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Form1 form1= new Form1();
            Application.Run(form1);

            /*
            th.Start();

            bool injected = false;

            string executablePath = Application.ExecutablePath;
            string modsPath = Path.Combine(Path.GetDirectoryName(executablePath).ToString(), "mod");
            string dllToInject = Path.Combine(modsPath, "Dx11 ImGui - Black Ops lll.dll");

            string processName = "BlackOps3";

            while (!injected)
            {
                Process[] processes = Process.GetProcesses();
                foreach (Process p in processes)
                {
                    if (p.ProcessName == processName)
                    {
                        injected = true;
                        break;
                    } else { continue; }
                }

                System.Threading.Thread.Sleep(1000);

            }
            form1.label1.Text = "Game Process Found, waiting 10 seconds for it to launch.";

            System.Threading.Thread.Sleep(10000);

            form1.label1.Text = "Injecting DLL...";

            DllInjector dllInjector = DllInjector.Instance;
            dllInjector.Inject(processName, dllToInject);

            System.Threading.Thread.Sleep(2000);

            form1.label1.Text = "DLL Injected Successfully, closing application";

            System.Threading.Thread.Sleep(2000);


            th.Join();
            Application.Exit();
            */

        }
    }
}