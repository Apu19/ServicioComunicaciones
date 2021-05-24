using SocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioComunicacionApp.Hilos
{
    public class HiloServer
    {
        private int puerto;
        private ServidorSocket server;
        public HiloServer(int puerto)
        {
            this.puerto = puerto;
        }
        public void Ejecutar()
        {
            Console.WriteLine("Iniciando servidor en el puerto {0}", puerto);
            this.server = new ServidorSocket(puerto);
            if (this.server.Iniciar())
            {
                Console.WriteLine("Servidor iniciado");
                while (true)
                {
                    ClienteSocket cliente = this.server.ObtenerCliente();
                    HiloCliente hiloCliente = new HiloCliente(cliente);
                    Thread t = new Thread(new ThreadStart(hiloCliente.Ejecutar));
                    t.IsBackground = false;
                    t.Start();
                }
            }
        }
    }
}
