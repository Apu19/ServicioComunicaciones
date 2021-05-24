using ServicioComunicacionApp.Hilos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioComunicacionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando hilo del server");
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            HiloServer hiloserver = new HiloServer(puerto);
            Thread t = new Thread(new ThreadStart(hiloserver.Ejecutar));
            t.IsBackground = false;
            t.Start();
            Console.ReadKey();
        }
    }
}
