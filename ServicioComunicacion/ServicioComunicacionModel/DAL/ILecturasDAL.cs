﻿using ServicioComunicacionModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DAL
{
    public interface ILecturasDAL
    {
        void RegistrarLectura();
        List<Lectura> ObtenerLecturasTrafico();
        List<Lectura> ObtenerLecturasConsumo();
    }
}
