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

            // Mostrar todos los usuarios
            List<Usuario> listaUsuario = new CN_Usuario().Listar();
            foreach (Usuario usuario in listaUsuario)
            {
                dgvdata.Rows.Add(
                    new object[] {
                        "",
                        usuario.IdUsuario,
                        usuario.Documento,
                        usuario.NombreCompleto,
                        usuario.Correo,
                        usuario.Clave,
                        usuario.oRol.IdRol,
                        usuario.oRol.Descripcion,
                        usuario.Estado ? 1: 0,
                        usuario.Estado ? "Activo": "Inactivo"
                    }
                );
            }
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
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtDocumento.Text = "";
            txtNombreCompleto.Text = "";
            txtCorreo.Text = "";
            txtClave.Text = "";
            txtConfirmarClave.Text = "";
            cboRol.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvdata.Columns[e.ColumnIndex].Name == "btnSeleccionar" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check20.Width;
                var h = Properties.Resources.check20.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check20, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvdata.Columns[e.ColumnIndex].Name == "btnSeleccionar" && e.RowIndex >= 0)
            {
                int indice = e.RowIndex;
                txtIndice.Text = indice.ToString();
                txtId.Text = dgvdata.Rows[indice].Cells["Id"].Value.ToString();
                txtDocumento.Text = dgvdata.Rows[indice].Cells["Documento"].Value.ToString();
                txtNombreCompleto.Text = dgvdata.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                txtCorreo.Text = dgvdata.Rows[indice].Cells["Correo"].Value.ToString();
                txtClave.Text = dgvdata.Rows[indice].Cells["Clave"].Value.ToString();
                txtConfirmarClave.Text = dgvdata.Rows[indice].Cells["Clave"].Value.ToString();

                cboRol.SelectedIndex = cboRol.FindString(dgvdata.Rows[indice].Cells["Rol"].Value.ToString());
                cboEstado.SelectedIndex = cboEstado.FindString(dgvdata.Rows[indice].Cells["Estado"].Value.ToString());
            }
        }
    }
}
