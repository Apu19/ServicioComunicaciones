using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DAL
{
    public class MedidoresDALFactory
    {
        public static ILecturasDAL CreateDAL()
        {
            return LecturasDALArchivos.GetInstance();
        }
    }
}
