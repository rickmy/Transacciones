using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppTransacciones
{
    public partial class Form1 : Form
    {
        NpgsqlConnection conexion = new NpgsqlConnection("Server = localhost; User Id = postgres; Password = 1234; Database = DeberTransaccion;");
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Error(textBox1.Text, textBox2.Text, (float)Convert.ToInt64(textBox3.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Exito(textBox1.Text, textBox2.Text, (float)Convert.ToInt64(textBox3.Text));
        }
        public void Exito(string cuentaAcreditar, string cuentaAcreditada, float dinero)
        {
            conexion.Open();
            string Inicio = "BEGIN";
            string Sentencia1 = $"UPDATE cuenta SET saldo = saldo - { dinero } WHERE numero = '{ cuentaAcreditar }'";
            string Sentencia2 = $"UPDATE cuenta SET saldo = saldo + { dinero } WHERE numero = '{ cuentaAcreditada }'";
            string Fin = "COMMIT";
            NpgsqlCommand ejecucion = new NpgsqlCommand(Inicio, conexion);
            NpgsqlCommand ejecucion2 = new NpgsqlCommand(Sentencia1, conexion);
            NpgsqlCommand ejecucion3 = new NpgsqlCommand(Sentencia2, conexion);
            NpgsqlCommand ejecucion4 = new NpgsqlCommand(Fin, conexion);
            ejecucion.ExecuteNonQuery();
            ejecucion2.ExecuteNonQuery();
            ejecucion3.ExecuteNonQuery();
            ejecucion4.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show("Ejecución exitosa");
        }
        public void Error(string cuentaAcreditar, string cuentaAcreditada, float dinero)
        {
            conexion.Open();
            string Inicio = "BEGIN";
            string Sentencia1 = $"UPDATE cuenta SET saldo = saldo - { dinero } WHERE numero = '{ cuentaAcreditar }'";
            string Sentencia2 = $"UPDATE cuenta SET saldo = saldo + { dinero } WHERE numero = '{ cuentaAcreditada }'";
            string Fin = "ROLLBACK";
            NpgsqlCommand ejecucion = new NpgsqlCommand(Inicio, conexion);
            NpgsqlCommand ejecucion2 = new NpgsqlCommand(Sentencia1, conexion);
            NpgsqlCommand ejecucion3 = new NpgsqlCommand(Sentencia2, conexion);
            NpgsqlCommand ejecucion4 = new NpgsqlCommand(Fin, conexion);
            ejecucion.ExecuteNonQuery();
            ejecucion2.ExecuteNonQuery();
            ejecucion3.ExecuteNonQuery();
            ejecucion4.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show("La ejecución fracasó");
        }

    }
}
