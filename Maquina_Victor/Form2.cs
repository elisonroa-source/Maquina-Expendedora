using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Maquina_Victor
{
    public partial class Form2 : Form
    {
        private ProductoRepository repo = new ProductoRepository();

        public Form2()
        {
            InitializeComponent();
            CargarProductos(); // carga productos al abrir el form
        }

        private void CargarProductos()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = repo.ObtenerProductos();
        }

        private void txtId_TextChanged(object sender, EventArgs e) { }
        private void txtName_TextChanged(object sender, EventArgs e) { }
        private void txtPrice_TextChanged(object sender, EventArgs e) { }
        private void txtStock_TextChanged(object sender, EventArgs e) { }
        private void txtImagen_TextChanged(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Producto p = new Producto
            {
                Nombre = txtName.Text,
                Precio = decimal.Parse(txtPrice.Text),
                Stock = int.Parse(txtStock.Text),
                Imagen = txtImagen.Text
            };

            repo.InsertarProducto(p);
            CargarProductos();
            MessageBox.Show("Producto creado correctamente");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Producto p = new Producto
            {
                Id = int.Parse(txtId.Text),
                Nombre = txtName.Text,
                Precio = decimal.Parse(txtPrice.Text),
                Stock = int.Parse(txtStock.Text),
                Imagen = txtImagen.Text
            };

            repo.ActualizarProducto(p);
            CargarProductos();
            MessageBox.Show("Producto actualizado correctamente");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            repo.EliminarProducto(id);
            CargarProductos();
            MessageBox.Show("Producto eliminado correctamente");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 maquina = new Form1(); // tu form principal
            maquina.Show();
        }
    }
}
