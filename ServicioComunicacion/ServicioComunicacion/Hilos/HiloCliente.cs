using ServicioComunicacionModel.DAL;
using ServicioComunicacionModel.DTO;
using SocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionApp.Hilos
{
    public class HiloCliente
    {
        private ClienteSocket clienteSocket;
        private ILecturasDAL dal = LecturasDALFactory.CreateDAL();
        public HiloCliente(ClienteSocket clienteSocket)
        {
            this.clienteSocket = clienteSocket;
        }

        public void Ejecutar()
        {
            string fecha;
            int id;
            int nroMedidor;
            int valor;
            string tipo;
            string estado = null;
            DateTime dt;
            string dato;


            clienteSocket.Escribir("fecha|numero_medidor|tipo");
            dato = clienteSocket.Leer().Trim();
            string[] arreglo = dato.Split('|');
            string formato = "yyyyMMddHHmmss";
            DateTime ahora = DateTime.Now;

            fecha = arreglo[0].Trim().Replace("-", "");
            nroMedidor = Int32.Parse(arreglo[1].Trim());
            tipo = arreglo[2].ToLower().Trim();

            bool result = DateTime.TryParseExact(fecha, formato, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt);


            if (result && (((ahora - dt).TotalMinutes) < 30) && comprobarTipoMedidor(tipo) && comprobarNro(tipo, nroMedidor))
            {
                clienteSocket.Escribir(ActualizarHoraActual() + "| WAIT");
                clienteSocket.Escribir("nroSerie|fecha|tipo|valor|{estado}|UPDATE");
                dato = clienteSocket.Leer().Trim();
                string[] arreglo2 = dato.Split('|');

                id = Int32.Parse(arreglo2[0]);
                fecha = arreglo2[1].Trim().Replace("-", "");
                valor = Int32.Parse(arreglo2[3]);
                if (arreglo2[4].Contains(" "))
                {
                    arreglo2 = arreglo2.Where((source, index) => index != 4).ToArray();
                }


                result = DateTime.TryParseExact(fecha, formato, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt);

                if (result && (valor <= 1000 && valor >= 0) && comprobarTipoMedidor(arreglo2[2].ToLower()))
                {
                    tipo = arreglo2[2].ToLower();
                    int largo = arreglo2.Length;

                    if (largo.Equals(6))
                    {
                        valor = Int32.Parse(arreglo2[4]);
                        switch (valor)
                        {
                            case -1:
                                estado = "Error Lectura";
                                break;
                            case 0:
                                estado = "OK";
                                break;
                            case 1:
                                estado = "Punto de carga LLeno";
                                break;
                            case 2:
                                estado = "Requiere mantencion preventiva";
                                break;
                            default:
                                clienteSocket.Escribir(arreglo[0] + "|" + nroMedidor + "|" + "ERROR");
                                clienteSocket.CerrarConexion();
                                break;
                        }

                    }

                    switch (tipo)
                    {
                        case "trafico":
                            Trafico t = new Trafico();
                            t.Fecha = dt;
                            t.NroMedidor = nroMedidor;
                            t.CantVehiculo = valor;
                            t.Estado = estado;
                            t.NroSerie = id;

                            lock (dal)
                            {
                                dal.(t);
                            }   
                            clienteSocket.Escribir(id + "|" + "OK");
                            clienteSocket.CerrarConexion();
                            break;

                        case "consumo":
                           Consumo c = new Consumo();
                            c.Fecha = dt;
                            c.NroMedidor = nroMedidor;
                            c.ConsumoActual = valor;
                            c.Estado = estado;
                            c.NroSerie = id;

                            lock (dal)
                            {
                                dal.RegistrarLectura(c);
                            }
                            clienteSocket.Escribir(id + "|" + "OK");
                            clienteSocket.CerrarConexion();
                            break;
                        default: // por algun error que pueda pasar
                            clienteSocket.Escribir(arreglo[0] + "|" + nroMedidor + "|" + "ERROR");
                            clienteSocket.CerrarConexion();
                            break;
                    }

                }
                else
                {
                    clienteSocket.Escribir(arreglo[0] + "|" + nroMedidor + "|" + "ERROR");
                    clienteSocket.CerrarConexion();
                }

            }
            else
            {
                clienteSocket.Escribir(arreglo[0] + "|" + nroMedidor + "|" + "ERROR");
                clienteSocket.CerrarConexion();
            }

        }





        private string ActualizarHoraActual()
        {
            string fechaAhora;
            DateTime ahora = DateTime.Now;
            fechaAhora = (ahora.Year + "-" + ahora.Month + "-" + ahora.Day + "-" + ahora.Hour + "-" + ahora.Minute + "-" + ahora.Second);
            return fechaAhora;
        }

        private Boolean comprobarNro(string t, int n)
        {
            Boolean res = false;
            switch (t)
            {
                case "trafico":
                    if (comprobarNroTrafico(n))
                    {
                        res = true;
                    }
                    break;
                case "consumo":
                    if (comprobarNroConsumo(n))
                    {
                        res = true;
                    }
                    break;
            }
            return res;
        }

        private Boolean comprobarNroTrafico(int n)
        {
            var lista = MedidorTraficoDALArchivos.listMedidoresTrafico.ToList();

            if (lista.Contains(n))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Boolean comprobarNroConsumo(int n)
        {
            var lista = MedidorConsumoDALArchivos.listMedidoresConsumo.ToList();
            if (lista.Contains(n))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Boolean comprobarTipoMedidor(string s)
        {
            if (s.Equals("trafico") || s.Equals("consumo"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

