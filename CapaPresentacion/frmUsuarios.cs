using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            // Cargamos el estado
            cboEstado.Items.Add(new OpcionCombo("Activo", 1));
            cboEstado.Items.Add(new OpcionCombo("Inactivo", 0));
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;
            
            // Cargamos los roles
            List<Rol> listaRol = new CN_Rol().Listar();
            foreach (Rol rol in listaRol)
            {
                cboRol.Items.Add(new OpcionCombo(rol.Descripcion, rol.IdRol));
            }
            cboRol.DisplayMember = "Texto";
            cboRol.ValueMember = "Valor";
            cboRol.SelectedIndex = 0;

            // Cargamos los filtros de busqueda
            foreach (DataGridViewColumn col in dgvdata.Columns)
            {
                if (col.Visible == true && col.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo()
                    {
                        Valor = col.Name,
                        Texto = col.HeaderText
                    });
                }
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            dgvdata.Rows.Add(
                new object[] {
                    "",
                    txtId.Text,
                    txtDocumento.Text,
                    txtNombreCompleto.Text,
                    txtCorreo.Text,
                    txtClave.Text,
                    ((OpcionCombo) cboRol.SelectedItem).Valor.ToString(),
                    ((OpcionCombo) cboRol.SelectedItem).Texto.ToString(),
                    ((OpcionCombo) cboEstado.SelectedItem).Valor.ToString(),
                    ((OpcionCombo) cboEstado.SelectedItem).Texto.ToString(),
                }
            );

            Limpiar();
        }

        private void Limpiar()
        {
            // borramos los campos
            txtId.Text = "0";
            txtDocumento.Text = "";
            txtNombreCompleto.Text = "";
            txtCorreo.Text = "";
            txtClave.Text = "";
            txtConfirmarClave.Text = "";
            cboRol.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;
        }
    }
}
