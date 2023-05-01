using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace MyBO3injector
{
    public partial class Form1 : Form
    {
        RegistryKey softwareKey;
        RegistryKey myAppSave; 

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {/* works
            string executablePath = Application.ExecutablePath;
            string modsPath = Path.Combine(Path.GetDirectoryName(executablePath).ToString(), "mod");
            string dllToInject = Path.Combine(modsPath, "Dx11 ImGui - Black Ops lll.dll");


            DllInjector dllInjector = DllInjector.Instance;
            dllInjector.Inject("BlackOps3", dllToInject);

            Application.Exit();
        */
            softwareKey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
            myAppSave = softwareKey.CreateSubKey("ssj4BlackOps3Mod");

            Thread th = new Thread(() => SearchForInjection());
            th.Start();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void UpdateLabel(string countValue)
        {
            if (label1.InvokeRequired)
            {
                label1.Invoke(new Action(() => label1.Text = countValue.ToString()));
            }
            else
            {
                label1.Text = countValue.ToString();
            }
        }

        public void SearchForInjection()
        {

            bool injected = false;

            string executablePath = Application.ExecutablePath;
            string modsPath = Path.Combine(Path.GetDirectoryName(executablePath).ToString(), "mod");
            string dllToInject = Path.Combine(modsPath, "Dx11 ImGui - Black Ops lll.dll");

            string processName = "BlackOps3";

            int TimesPassed = 0;
            int myCount = 45;

            bool LaunchedBO3 = false;

            string myBO3Path = myAppSave.GetValue("BO3Path") as string;

            while (!injected)
            {
                TimesPassed++;
                Process[] processes = Process.GetProcesses();
                foreach (Process p in processes)
                {
                    if (p.ProcessName == processName)
                    {
                        injected = true;
                        if(TimesPassed == 1)
                        {
                            myCount = 1;
                        } else
                        {
                            myBO3Path = p.MainModule.FileName;
                            myAppSave.SetValue("BO3Path", myBO3Path);
                        }
                        break;
                    }
                    else { continue; }
                }

                if(TimesPassed > 1 && myBO3Path != null && LaunchedBO3 == false)
                {
                    LaunchedBO3 = true;
                    Process process = new Process();
                    process.StartInfo.FileName = myBO3Path;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();

                    // Close the handle to the process
                    process.Dispose();
                }

                System.Threading.Thread.Sleep(1000);

            }

            
            while(myCount > 0)
            {
                UpdateLabel("Game Process Found, waiting " + myCount.ToString() + " seconds for it to launch.");
                myCount--;
                System.Threading.Thread.Sleep(1000);
            }

            UpdateLabel("Injecting DLL...");

            DllInjector dllInjector = DllInjector.Instance;
            dllInjector.Inject(processName, dllToInject);

            System.Threading.Thread.Sleep(2000);

            UpdateLabel("DLL Injected Successfully, closing application");

            System.Threading.Thread.Sleep(2000);

            Application.Exit();
        }
    }






}