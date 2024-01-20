using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EJClient
{
    class main
    {
        static AutoUpdate autoUpdate;
        [STAThread]//single thread apartment
        static void Main(string[] parameters)
        {
            Application app = new Application();

            if (parameters != null && parameters.Length > 0 && parameters[0] == "/sql")
            {
                app.Run(new Forms.DatabaseUpdate());
            }
            else
            {
                autoUpdate = new AutoUpdate();
                app.Run(new Login());
            }
        }
    }
}
