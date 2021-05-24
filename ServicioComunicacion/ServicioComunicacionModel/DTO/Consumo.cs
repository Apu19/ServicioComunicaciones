using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DTO
{
    public class Consumo : Medidor
    {
        private int consumoActual;

        public int ConsumoActual { get => consumoActual; set => consumoActual = value; }


        public override string ToString()
        {
            return "{\n" +
                "   \"Fecha\": \"" + Fecha + "\" \n" +
                "   \"IdMedidor\": \"" + NroMedidor + "\" \n" +
                "   \"cantidad Consumo\": \"" + consumoActual + "\" \n" +
                "   \"numero Serie\": \"" + NroSerie + "\" \n" +
                "   \"estado\": \"" + Estado + "\"\n" +
                "   UPDATE \n" +
                "}";
        }

    }
}
