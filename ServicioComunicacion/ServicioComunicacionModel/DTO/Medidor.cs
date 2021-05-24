using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DTO
{
    public class Medidor
    {
        {
        private DateTime fecha;
        private int nroMedidor;
        private int nroSerie;
        private string estado;

        public DateTime Fecha { get => fecha; set => fecha = value; }
        public int NroMedidor { get => nroMedidor; set => nroMedidor = value; }
        public int NroSerie { get => nroSerie; set => nroSerie = value; }
        public string Estado { get => estado; set => estado = value; }

        public override string ToString()
        {
            return fecha + "|" + nroMedidor + "|" + nroSerie + "|" + estado;
        }
    }
}
