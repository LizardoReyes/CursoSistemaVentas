using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion.Utilidades
{
    public class OpcionCombo
    {
        public string Texto { get; set; }
        public object Valor { get; set; }

        public OpcionCombo()
        {
        }

        public OpcionCombo(string texto, object valor)
        {
            Texto = texto;
            Valor = valor;
        }

        public override string ToString()
        {
            return Texto;
        }
    }
}
