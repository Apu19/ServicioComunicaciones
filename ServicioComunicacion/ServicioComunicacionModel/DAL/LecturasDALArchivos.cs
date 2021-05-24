using ServicioComunicacionModel.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DAL
{
    public class LecturasDALArchivos : ILecturasDAL
    {

        private string traficoTxt = Directory.GetCurrentDirectory()
        + Path.DirectorySeparatorChar + "Trafico.txt";

        private string consumoTxt = Directory.GetCurrentDirectory()
            + Path.DirectorySeparatorChar + "Consumo.txt";
        private LecturasDALArchivos()
        {

        }
        private static ILecturasDAL instancia;

        public static ILecturasDAL GetInstance()
        {
            if (instancia == null)
                instancia = new LecturasDALArchivos();
            return instancia;
        }
        public List<Trafico> ObtenerLecturasTrafico()
        {
            List<Trafico> lista = new List<Trafico>();
            try
            {
                using (StreamReader reader = new StreamReader(traficoTxt))
                {
                    string linea = null;
                    do
                    {
                        linea = reader.ReadLine();
                        if (linea != null)
                        {
                            string[] textoArray = linea.Split('|');
                            Trafico m = new Trafico()
                            {
                                Fecha = DateTime.Parse(textoArray[0]),
                                NroMedidor = Int32.Parse(textoArray[1]),
                                CantVehiculo = Int32.Parse(textoArray[2]),
                                NroSerie = Int32.Parse(textoArray[3]),
                                Estado = textoArray[4]
                            };
                            lista.Add(m);
                        }

                    } while (linea != null);
                }
            }
            catch (IOException ex)
            {
                lista = null;
            }
            return lista;
        }



        public void RegistrarLecturaConsumo(Consumo m)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(consumoTxt, true))
                {
                    writer.WriteLine(m);
                    writer.Flush();
                }
            }
            catch (IOException ex)
            {

            }
        }


        public void RegistrarLecturaTrafico(Trafico m)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(traficoTxt, true))
                {
                    writer.WriteLine(m);
                    writer.Flush();
                }
            }
            catch (IOException ex)
            {

            }
        }

        public List<Consumo> RegistrarLectura()
        {
            List<Consumo> lista = new List<Consumo>();
            try
            {
                using (StreamReader reader = new StreamReader(consumoTxt))
                {
                    string linea = null;
                    do
                    {
                        linea = reader.ReadLine();
                        if (linea != null)
                        {
                            string[] textoArray = linea.Split('|');
                            Consumo m = new Consumo()
                            {
                                Fecha = DateTime.Parse(textoArray[0]),
                                NroMedidor = Int32.Parse(textoArray[1]),
                                ConsumoActual = Int32.Parse(textoArray[2]),
                                NroSerie = Int32.Parse(textoArray[3]),
                                Estado = textoArray[4]
                            };
                            lista.Add(m);
                        }

                    } while (linea != null);
                }
            }
            catch (IOException ex)
            {
                lista = null;
            }
            return lista;
        }
    }
    }
