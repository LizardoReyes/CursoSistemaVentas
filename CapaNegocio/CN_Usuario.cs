using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Usuario
    {
        private CD_Usuario objcd_usuario = new CD_Usuario();

        public List<Usuario> Listar()
        {
            return objcd_usuario.Listar();
        }

        public int Registrar(Usuario obj, out string mensaje)
        {
            mensaje = "";
            if (obj.NombreCompleto == "")
            {
                mensaje += "Es necesario el nombre del usuario\n";
            }

            if (obj.Documento == "")
            {
                mensaje += "Es necesario el documento del usuario\n";
            }

            if (obj.Clave == "")
            {
                mensaje += "Es necesario la clave del usuario\n";
            }

            if(mensaje != "")
            {
                return 0;
            }

            return objcd_usuario.Registrar(obj, out mensaje);
        }

        public bool Actualizar(Usuario obj, out string mensaje)
        {
            mensaje = "";
            if (obj.NombreCompleto == "")
            {
                mensaje += "Es necesario el nombre del usuario\n";
            }

            if (obj.Documento == "")
            {
                mensaje += "Es necesario el documento del usuario\n";
            }

            if (obj.Clave == "")
            {
                mensaje += "Es necesario la clave del usuario\n";
            }

            if (mensaje != "")
            {
                return false;
            }

            return objcd_usuario.Editar(obj, out mensaje);
        }

        public bool Eliminar(Usuario obj, out string mensaje)
        {
            return objcd_usuario.Eliminar(obj, out mensaje);
        }
    }
}
