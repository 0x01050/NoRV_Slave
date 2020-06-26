using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;

namespace Slave
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Thread thread = new Thread(new ThreadStart(reportStatus));
            thread.Start();

            RegisterScreen prompt = new RegisterScreen();
            Application.Run(prompt);
            //if (prompt.DialogResult != DialogResult.OK)
            //    return;
            //Application.Run();

            thread.Abort();
        }

        private static void reportStatus()
        {
            while(true)
            {
                try
                {
                    var httpClient = new HttpClient();
                    string url = Config.getInstance().getServerUrl() + "/status/slave";
                    MultipartFormDataContent httpContent = new MultipartFormDataContent();
                    httpContent.Add(new StringContent(L.v()), "id");
                    httpClient.PostAsync(url, httpContent);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                Thread.Sleep(30 * 1000);
            }    
        }
    }
}
