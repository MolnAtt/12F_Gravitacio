using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace _12F_Mozgo_dolog
{
	public partial class Form1 : Form
	{
		Mozgo mozgo = new Mozgo(new Vektor(90, 90), new Vektor(1, 1), 30, Color.Orange);
		Mozgo mozgo2 = new Mozgo(new Vektor(30, 60), new Vektor(1, -1), 20, Color.Blue);
		Mozgo mozgo3 = new Mozgo(new Vektor(50, 30), new Vektor(-1, 1), 10, Color.Gray);

		public Form1()
		{
			InitializeComponent();

			//MessageBox.Show("szia!");

			Mozgo.Összes_lerajzolása(pictureBox1);
		}


		private void button1_Click(object sender, EventArgs e)
		{
			Mozgo.fut = true;
			Mozgo.Szimuláció(pictureBox1, label2);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Mozgo.fut = false;
		}
	}
}
