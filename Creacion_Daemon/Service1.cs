using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;

namespace Creacion_Daemon
{
    public partial class Service1 : ServiceBase
    {
        public Timer tiempo;
        public string fecha;
        public string NombreArchivo;
        public Service1()
        {
            InitializeComponent();
            tiempo = new Timer();
            tiempo.Interval = 10000;
            tiempo.Elapsed += new ElapsedEventHandler(tiempo_espera);
            fecha = DateTime.Now.ToShortDateString().Replace(@"/", "-");
        }

        protected override void OnStart(string[] args)
        {
            tiempo.Enabled = true;
        }

        protected override void OnStop()
        {

        }
        public void tiempo_espera(object sender,EventArgs e)
        {
            if (!Directory.Exists(@"C:/PruebaOrdenamiento/"+fecha))
            {
                Directory.CreateDirectory(@"C:/PruebaOrdenamiento/" + fecha);
            }
            foreach(string file in Directory.GetFiles(@"C:/PruebaOrdenamiento/","*.*"))
            {
                NombreArchivo = Path.GetFileName(file);
                File.Move(file, @"C:/PruebaOrdenamiento/" + fecha + "/"+NombreArchivo);
            }
        }
    }
}
