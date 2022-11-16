using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace _12F_Mozgo_dolog
{
	class Mozgo
	{
		Vektor hely;
		Vektor sebesseg;
		int tomeg;
		Color szin;
		SolidBrush kemenyecset;
		static List<Mozgo> lista = new List<Mozgo>(); // ezt muszáj itt inicializálni most.
		List<Vektor> elmozdulasai;

		public Mozgo(Vektor hely, Vektor sebesseg, int tomeg, Color szin)
		{
			this.hely = hely;
			this.sebesseg = sebesseg;
			this.tomeg = tomeg;
			this.szin = szin;
			this.kemenyecset = new SolidBrush(szin);
			this.elmozdulasai = new List<Vektor>();
			Mozgo.lista.Add(this);
		}

		internal void Lépj()
		{
			this.hely += this.sebesseg;
		}

		public static bool fut = false;
		public static int idő = 0;
		internal static void Szimuláció(PictureBox pictureBox1, Label időlabel)
		{
			while (fut)
			{
				Thread.Sleep(20);
				idő++;
				Mozgo.Összes_eredő_kiszámítása();
				Mozgo.Összes_sebesség_update();
				Mozgo.Összes_léptetése();
				Mozgo.Összes_lerajzolása(pictureBox1);
				időlabel.Text = idő.ToString();
				időlabel.Refresh();
				Application.DoEvents();
			}
		}




		private static void Összes_sebesség_update()
		{
			for (int i = 0; i < Mozgo.lista.Count; i++)
			{
				Mozgo.lista[i].sebesség_update();
			}
		}

		private void sebesség_update()
		{
			for (int i = 0; i < this.elmozdulasai.Count; i++)
			{
				this.sebesseg += this.elmozdulasai[i];
			}
		}

		private static void Összes_eredő_kiszámítása()
		{
			for (int i = 0; i < Mozgo.lista.Count; i++)
			{
				for (int j = 0; j < Mozgo.lista.Count; j++)
				{
					if (i!=j)
					{
						Mozgo.lista[i].elmozdulasai.Add(Mozgo.lista[i].Tömegvonzás(Mozgo.lista[j]));
					}
				}
			}
		}

		Vektor Tömegvonzás(Mozgo that)
		{
			double d = this.Távolsága_ettől(that); // ez is vektoraritmetika!
			double dnegyzet = d * d;
			double elmozdulasvektor_nagysaga = that.tomeg / dnegyzet;

			Vektor jó_irányba_mutató_vektor = that.hely - this.hely; // egy komplett vektoraritmetikát ki kell majd dolgoznunk!
			Vektor egységvektor = jó_irányba_mutató_vektor / d;// John Carmack! 
			return egységvektor * elmozdulasvektor_nagysaga;
		}



		private double Távolsága_ettől(Mozgo that) => (this.hely - that.hely).Hossz();

		public void Lerajzol(Graphics g)
		{
			Point h = hely.ToPoint();
			g.FillEllipse(kemenyecset, h.X - tomeg / 2, h.Y - tomeg / 2, tomeg, tomeg);
		}

		public static void Összes_léptetése()
		{
			foreach (Mozgo mozgo in Mozgo.lista)
			{
				mozgo.Lépj();
			}
		}
		public static void Összes_lerajzolása(PictureBox pictureBox1)
		{
			Size vaszonmeret = pictureBox1.Size;
			Bitmap bmp = new Bitmap(vaszonmeret.Width, vaszonmeret.Height);
			pictureBox1.Image = bmp;
			using (Graphics g = Graphics.FromImage(bmp))
			{
				foreach (Mozgo mozgo in Mozgo.lista)
				{
					mozgo.Lerajzol(g);
				}
			}
			pictureBox1.Refresh();
		}
	}
}
