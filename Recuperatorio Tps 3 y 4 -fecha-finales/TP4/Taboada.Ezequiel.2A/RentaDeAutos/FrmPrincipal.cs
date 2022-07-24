using System;
using System.Windows.Forms;
using Entidades;
using ControlArchivos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace RentaDeAutos
{
    public partial class FrmPrincipal : Form
    {
        private ControlListas<Vehiculo> vehiculos;
        private Serializador<ControlListas<Vehiculo>> serializadorVehiculos;
        private bool ok;


        public ControlListas<Vehiculo> Vehiculos { get => vehiculos; set => vehiculos = value; }

        public FrmPrincipal()
        {
            InitializeComponent();
            this.vehiculos = new ControlListas<Vehiculo>();
            this.serializadorVehiculos = new Serializador<ControlListas<Vehiculo>>(ETipoExtenxion.Xml);
            this.IsMdiContainer = true;


        }

        private async void FrmPrincipal_Load(object sender, EventArgs e)
        {
            ActualizarHora();
            this.vehiculos = await TraerVehiculos();
            ok = true;
            if (this.vehiculos is null)
            {
                MessageBox.Show("No se encuentra la ruta de los archivos");
                ok = false;
            }

        }

        #region ToolStrip

        private void toolSalir_Click(object sender, EventArgs e)
        {
            Form frm = this.ActiveMdiChild;
            if (frm != null)
            {
                frm.Close();
            }
            else
            {
                this.Close();

            }
        }
        #endregion

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Desea salir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                if (ok)
                {
                    if (serializadorVehiculos.Escribir("vehiculos.xml", vehiculos))
                    {
                        MessageBox.Show("Archivos Guardados con exito", "Guardado de archivos", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void toolClientes_Click(object sender, EventArgs e)
        {
            AbrirForm(new FrmClientes());

        }

        private void toolVehiculos_Click(object sender, EventArgs e)
        {
            AbrirForm(new FrmVehiculos(this.vehiculos));

        }

        private void toolAlquiler_Click(object sender, EventArgs e)
        {
            AbrirForm(new FrmAlquilar(this.vehiculos));
        }

        private async Task<ControlListas<Vehiculo>> TraerVehiculos()
        {
            ControlListas<Vehiculo> vehiculos = await Task.Run(() =>
            {
                try
                {
                    return serializadorVehiculos.Leer(Archivo.ObtenerRuta("vehiculos.xml"));
                }
                catch (Exception)
                {

                    return null;
                }
            });
            return vehiculos;
        }

        private void AbrirForm(Form hijo)
        {
            if (Application.OpenForms[hijo.GetType().Name] != null)
            {
                Application.OpenForms[hijo.GetType().Name].Activate();
            }
            else
            {
                if (ok)
                {
                    hijo.MdiParent = this;
                    hijo.Show();
                }
                else
                {
                    MessageBox.Show("No se encuentra la ruta de los archivos");
                }
            }
        }

        private void AsignarHora()
        {

            if (this.lblHora.InvokeRequired)
            {
                this.Invoke(new Action(AsignarHora));
            }
            else
            {
                this.lblHora.Text = DateTime.Now.ToString("F");
            }
        }

        private void ActualizarHora()
        {
            Task.Run(new Action(() =>
             {
                 while (true)
                 {
                     AsignarHora();
                     Thread.Sleep(1000);
                 }
             }));
        }
    }
}
