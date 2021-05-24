using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DTO
{
    public class Trafico : Medidor
    {
        private int cantVehiculo;

        public int CantVehiculo { get => cantVehiculo; set => cantVehiculo = value; }

        public override string ToString()
        {
            return "{\n" +
                "   \"Fecha\": \"" + Fecha + "\" \n" +
                "   \"IdMedidor\": \"" + NroMedidor + "\" \n" +
                "   \"cantidad Vehiculos\": \"" + CantVehiculo + "\" \n" +
                "   \"numero Serie\": \"" + NroSerie + "\" \n" +
                "   \"estado\": \"" + Estado + "\"\n" +
                "   UPDATE \n" +
                "}";
        }

    }
}
