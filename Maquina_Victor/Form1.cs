using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Maquina_Victor
{
    public partial class Form1 : Form
    {
        private ProductoRepository repo = new ProductoRepository();
        private VentaService ventaService = new VentaService();
        private string productoSeleccionado = "";
        private decimal saldo = 0;

        public Form1()
        {
            InitializeComponent();
        }

        // 🔧 Función para cargar imágenes desde dos rutas
        private Image CargarImagen(string nombreArchivo)
        {
            // Ruta dentro del proyecto compilado
            string rutaProyecto = Path.Combine(Application.StartupPath, "Resources", nombreArchivo);

            // Ruta absoluta en tu PC
            string rutaPC = Path.Combine(@"C:\Users\el_me\source\repos\Maquina_Victor\Maquina_Victor\Resources", nombreArchivo);

            if (File.Exists(rutaProyecto))
            {
                return Image.FromFile(rutaProyecto);
            }
            else if (File.Exists(rutaPC))
            {
                return Image.FromFile(rutaPC);
            }
            else
            {
                return null; // si no se encuentra, devuelve null
            }
        }

        // Botones numéricos
        private void btn0_Click(object sender, EventArgs e) { productoSeleccionado += "0"; lblID.Text = productoSeleccionado; }
        private void btn1_Click(object sender, EventArgs e) { productoSeleccionado += "1"; lblID.Text = productoSeleccionado; }
        private void btn2_Click(object sender, EventArgs e) { productoSeleccionado += "2"; lblID.Text = productoSeleccionado; }
        private void btn3_Click(object sender, EventArgs e) { productoSeleccionado += "3"; lblID.Text = productoSeleccionado; }
        private void btn4_Click(object sender, EventArgs e) { productoSeleccionado += "4"; lblID.Text = productoSeleccionado; }
        private void btn5_Click(object sender, EventArgs e) { productoSeleccionado += "5"; lblID.Text = productoSeleccionado; }
        private void btn6_Click(object sender, EventArgs e) { productoSeleccionado += "6"; lblID.Text = productoSeleccionado; }
        private void btn7_Click(object sender, EventArgs e) { productoSeleccionado += "7"; lblID.Text = productoSeleccionado; }
        private void btn8_Click(object sender, EventArgs e) { productoSeleccionado += "8"; lblID.Text = productoSeleccionado; }
        private void btn9_Click(object sender, EventArgs e) { productoSeleccionado += "9"; lblID.Text = productoSeleccionado; }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (productoSeleccionado.Length > 0)
            {
                productoSeleccionado = productoSeleccionado.Substring(0, productoSeleccionado.Length - 1);
                lblID.Text = productoSeleccionado;
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (int.TryParse(productoSeleccionado, out int productoId))
            {
                var producto = repo.ObtenerProductos().Find(p => p.Id == productoId);

                if (producto != null)
                {
                    lblProducto.Text = producto.Nombre;
                    lblPrecio.Text = producto.Precio.ToString("C");

                    // Cargar imagen desde las dos posibles rutas
                    PBProductoEntregado.Image = CargarImagen(producto.Imagen);
                    PBProductoEntregado.SizeMode = PictureBoxSizeMode.StretchImage;

                    if (saldo >= producto.Precio)
                    {
                        bool ventaOk = ventaService.RealizarVenta(productoId, 1);
                        if (ventaOk)
                        {
                            PBColors.BackColor = Color.Green; // entrega exitosa
                            decimal cambio = saldo - producto.Precio;
                            lblCambioResultado.Text = cambio.ToString("C");
                            saldo = 0;
                        }
                        else
                        {
                            PBColors.BackColor = Color.Red;
                        }
                    }
                    else
                    {
                        PBColors.BackColor = Color.Red;
                    }
                }
            }

            productoSeleccionado = "";
            lblID.Text = "";
        }

        private void btnIngresarSaldo_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtSaldo.Text, out decimal monto))
            {
                saldo += monto;
                lblCambioResultado.Text = "0";
            }
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            Form2 inv = new Form2();
            inv.Show();
            this.Hide();
        }

        // Métodos vacíos para evitar errores del diseñador
        private void PBColors_Click(object sender, EventArgs e) { }
        private void progressBar1_Click(object sender, EventArgs e) { }
        private void txtSaldo_TextChanged(object sender, EventArgs e) { }
        private void lblID_Click(object sender, EventArgs e) { }
        private void PBProductoEntregado_Click(object sender, EventArgs e) { }
        private void lblPrecio_Click(object sender, EventArgs e) { }
        private void lblProducto_Click(object sender, EventArgs e) { }
        private void lblCambioResultado_Click(object sender, EventArgs e) { }

        // Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Aquí puedes poner la lógica que quieras que se ejecute cada vez que el timer haga "tick"
            // Ejemplo: resetear el color de PBColors después de unos segundos
            PBColors.BackColor = Color.Transparent;
        }
    }
}
