using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Categoria
    {
        private CD_Categoria objcd_categoria = new CD_Categoria();

        public List<Categoria> Listar()
        {
            return objcd_categoria.Listar();
        }

        public int Registrar(Categoria obj, out string mensaje)
        {
            mensaje = "";
            if (obj.Descripcion == "")
            {
                mensaje += "Es necesario la descripcion de la categoria\n";
            }

            if (mensaje != "")
            {
                return 0;
            }

            return objcd_categoria.Registrar(obj, out mensaje);
        }

        public bool Editar(Categoria obj, out string mensaje)
        {
            mensaje = "";
            if (obj.Descripcion == "")
            {
                mensaje += "Es necesario la descripcion de la categoria\n";
            }

            if (mensaje != "")
            {
                return false;
            }

            return objcd_categoria.Editar(obj, out mensaje);
        }

        public bool Eliminar(Categoria obj, out string mensaje)
        {
            return objcd_categoria.Eliminar(obj, out mensaje);
        }
    }
}
