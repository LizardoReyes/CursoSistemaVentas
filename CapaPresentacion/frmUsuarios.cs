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
            string mensaje = "";

            Usuario objUsuario = new Usuario()
            {
                IdUsuario = Convert.ToInt32(txtId.Text),
                Documento = txtDocumento.Text,
                NombreCompleto = txtNombreCompleto.Text,
                Correo = txtCorreo.Text,
                Clave = txtClave.Text,
                oRol = new Rol()
                {
                    IdRol = Convert.ToInt32(((OpcionCombo)cboRol.SelectedItem).Valor),
                    Descripcion = ((OpcionCombo)cboRol.SelectedItem).Texto
                },
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
            };

            if(objUsuario.IdUsuario == 0)
            {
                int idUsuarioGenerado = new CN_Usuario().Registrar(objUsuario, out mensaje);
                if (idUsuarioGenerado != 0)
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
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {
                bool resultado = new CN_Usuario().Editar(objUsuario, out mensaje);
                if (resultado)
                {
                    DataGridViewRow fila = dgvdata.Rows[Convert.ToInt32(txtIndice.Text)];
                    fila.Cells["Id"].Value = txtId.Text;
                    fila.Cells["Documento"].Value = txtDocumento.Text;
                    fila.Cells["NombreCompleto"].Value = txtNombreCompleto.Text;
                    fila.Cells["Correo"].Value = txtCorreo.Text;
                    fila.Cells["Clave"].Value = txtClave.Text;
                    fila.Cells["IdRol"].Value = ((OpcionCombo)cboRol.SelectedItem).Valor.ToString();
                    fila.Cells["Rol"].Value = ((OpcionCombo)cboRol.SelectedItem).Texto.ToString();
                    fila.Cells["EstadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    fila.Cells["Estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

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
            txtDocumento.Focus();
            txtDocumento.Select();
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Está seguro de eliminar el registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = "";
                    Usuario objUsuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(txtId.Text)
                    };

                    bool respuesta = new CN_Usuario().Eliminar(objUsuario, out mensaje);

                    if (respuesta)
                    {
                        dgvdata.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

            if(dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dgvdata.Rows)
                {
                    if (fila.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                    {
                        fila.Visible = true;
                    }
                    else
                    {
                        fila.Visible = false;
                    }
                }
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                fila.Visible = true;
            }
        }
    }
}
