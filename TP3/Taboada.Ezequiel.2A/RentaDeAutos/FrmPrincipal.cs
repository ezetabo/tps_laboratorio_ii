﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using ControlArchivos;


namespace RentaDeAutos
{
    public partial class FrmPrincipal : Form
    {

        private ControlListas<Cliente> clientes;
        private ControlListas<Vehiculo> vehiculos;
        private Serializador<ControlListas<Cliente>> serializadorClientes;
        private Serializador<ControlListas<Vehiculo>> serializadorVehiculos;


        public ControlListas<Cliente> Clientes { get => clientes; set => clientes = value; }
        public ControlListas<Vehiculo> Vehiculos { get => vehiculos; set => vehiculos = value; }

        public FrmPrincipal()
        {
            InitializeComponent();
            this.clientes = new ControlListas<Cliente>();
            this.vehiculos = new ControlListas<Vehiculo>();
            this.serializadorClientes = new Serializador<ControlListas<Cliente>>(ETipoExtenxion.Json);
            this.serializadorVehiculos = new Serializador<ControlListas<Vehiculo>>(ETipoExtenxion.Xml);
            this.IsMdiContainer = true;

        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            this.clientes = serializadorClientes.Leer("clientes.json");
            this.vehiculos = serializadorVehiculos.Leer("vehiculos.xml");
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
                if (serializadorClientes.Escribir("clientes.json", clientes) && serializadorVehiculos.Escribir("vehiculos.xml",vehiculos))
                {
                    MessageBox.Show("Archivos Guardados con exito", "Guardado de archivos", MessageBoxButtons.OK);
                }
               
            }
        }

        private void toolClientes_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["FrmInformes"] != null)
            {
                Application.OpenForms["FrmInformes"].Activate();
            }
            else
            {
                FrmClientes clientes = new FrmClientes(this.clientes);
                clientes.MdiParent = this;
                clientes.Show();
            }
        }

        private void toolInformes_Click(object sender, EventArgs e)
        {
        }

        private void toolVehiculos_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["FrmInformes"] != null)
            {
                Application.OpenForms["FrmInformes"].Activate();
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
                FrmAlquilar frmAlquilar = new FrmAlquilar(this.clientes, this.vehiculos); ;
                frmAlquilar.MdiParent = this;
                frmAlquilar.Show();
            }
            
        }
    }
}
