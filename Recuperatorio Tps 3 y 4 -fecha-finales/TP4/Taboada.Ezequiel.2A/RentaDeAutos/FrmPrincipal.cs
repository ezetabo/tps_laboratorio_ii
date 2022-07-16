using System;
using System.Windows.Forms;
using Entidades;
using ControlArchivos;
using System.Collections.Generic;

namespace RentaDeAutos
{
    public partial class FrmPrincipal : Form
    {
        private ControlListas<Vehiculo> vehiculos;       
        private Serializador<ControlListas<Vehiculo>> serializadorVehiculos;
       

       
        public ControlListas<Vehiculo> Vehiculos { get => vehiculos; set => vehiculos = value; }

        public FrmPrincipal()
        {
            InitializeComponent();
            this.vehiculos = new ControlListas<Vehiculo>();         
            this.serializadorVehiculos = new Serializador<ControlListas<Vehiculo>>(ETipoExtenxion.Xml);
            this.IsMdiContainer = true;
           

        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {            
            this.vehiculos = serializadorVehiculos.Leer(Archivo.ObtenerRuta("vehiculos.xml"));
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
                if (serializadorVehiculos.Escribir("vehiculos.xml",vehiculos))
                {
                    MessageBox.Show("Archivos Guardados con exito", "Guardado de archivos", MessageBoxButtons.OK);
                }
               
            }
        }

        private void toolClientes_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["FrmClientes"] != null)
            {
                Application.OpenForms["FrmClientes"].Activate();
            }
            else
            {
                FrmClientes clientes = new FrmClientes();
                clientes.MdiParent = this;
                clientes.Show();
            }
        }

        private void toolVehiculos_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["FrmVehiculos"] != null)
            {
                Application.OpenForms["FrmVehiculos"].Activate();
            }
            else
            {
                FrmVehiculos vehiculos = new FrmVehiculos(this.vehiculos);
                vehiculos.MdiParent = this;
                vehiculos.Show();
            }
        }

        private void toolAlquiler_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["FrmAlquilar"] != null)
            {
                Application.OpenForms["FrmAlquilar"].Activate();
            }
            else
            {
                FrmAlquilar frmAlquilar = new FrmAlquilar(this.vehiculos); ;
                frmAlquilar.MdiParent = this;
                frmAlquilar.Show();
            }
            
        }
              
    }
}
