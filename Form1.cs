using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafika_Lab5
{
    public partial class Form1 : Form
    {
        private int width = 0;
        private int height = 0;
        private int jasnosc = 0;
        private Image temp;

        private int[] red = new int[256];
        private int[] green = new int[256];
        private int[] blue = new int[256];


        public Form1()
        {
            InitializeComponent();
            buttonsActive(false);

            // Tryby mieszania
            comboBox1.Items.Add("Suma");
            comboBox1.Items.Add("Odejmowanie");
            comboBox1.Items.Add("Różnica");
            comboBox1.Items.Add("Mnożenie");
            comboBox1.Items.Add("Mnożenie Odwrotności");
            comboBox1.Items.Add("Negacja");
            comboBox1.Items.Add("Ciemniejsze");
            comboBox1.Items.Add("Jaśniejsze");
            comboBox1.Items.Add("Wyłączenie");
            comboBox1.Items.Add("Nakładka");
            comboBox1.Items.Add("Ostre Światło");
            comboBox1.Items.Add("Łagodne Światło");
            comboBox1.Items.Add("Rozcieńczenie");
            comboBox1.Items.Add("Wypalanie");
            comboBox1.Items.Add("Reflect Mode");
            comboBox1.Items.Add("Przeroczystość");
        }

        //load
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);

                width = pictureBox1.Image.Width;
                height = pictureBox1.Image.Height;

                if (temp == null) pictureBox2.Image = new Bitmap(width, height);
                else pictureBox2.Image = temp;

                if (pictureBox1.Image.Width != pictureBox2.Image.Width && pictureBox1.Image.Height != pictureBox2.Image.Height) buttonsActive(false);
                else buttonsActive(true);
            }
        }

        //swap
        private void button2_Click(object sender, EventArgs e)
        {
            temp = pictureBox2.Image;
            pictureBox2.Image = pictureBox1.Image;
            pictureBox1.Image = temp;
            temp = pictureBox2.Image;
        }

        //negative
        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap b1 = (Bitmap)pictureBox1.Image;
            Bitmap b2 = (Bitmap)pictureBox2.Image;

            Color k;
            int r, g, b;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    k = b1.GetPixel(x, y);
                    r = 255 - k.R;
                    g = 255 - k.G;
                    b = 255 - k.B;

                    k = Color.FromArgb(r, g, b);

                    b2.SetPixel(x, y, k);
                }
            }

            pictureBox2.Refresh();
        }

        private void buttonsActive(Boolean b)
        {
            button2.Enabled = b; // zamiana stron
            button3.Enabled = b; // negatyw
            comboBox1.Enabled = b; // tryb mieszania
            trackBar1.Enabled = b; // jasnosc
        }

        //jasnosc
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            jasnosc = trackBar1.Value;

            Bitmap b1 = (Bitmap)pictureBox1.Image;
            Bitmap b2 = (Bitmap)pictureBox2.Image;

            Color k;
            int r, g, b;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    k = b1.GetPixel(x, y);

                    if (jasnosc + k.R > 255) r = 255;
                    else if (jasnosc + k.R < 0) r = 0;
                    else r = jasnosc + k.R;

                    if (jasnosc + k.G > 255) g = 255;
                    else if (jasnosc + k.G < 0) g = 0;
                    else g = jasnosc + k.G;

                    if (jasnosc + k.B > 255) b = 255;
                    else if (jasnosc + k.B < 0) b = 0;
                    else b = jasnosc + k.B;

                    k = Color.FromArgb(r, g, b);

                    b2.SetPixel(x, y, k);
                }
            }

            pictureBox2.Refresh();
        }

        //tryby
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bitmap b1 = (Bitmap)pictureBox1.Image;
            Bitmap b2 = (Bitmap)pictureBox2.Image;

            Color k, k1, k2;
            int r, g, b;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    k1 = b1.GetPixel(x, y);
                    k2 = b2.GetPixel(x, y);

                    if (comboBox1.Text == "Suma")
                    {
                        if (k1.R + k2.R > 255) r = 255;
                        else r = k1.R + k2.R;

                        if (k1.G + k2.G > 255) g = 255;
                        else g = k1.G + k2.G;

                        if (k1.B + k2.B > 255) b = 255;
                        else b = k1.B + k2.B;

                        k = Color.FromArgb(r, g, b);
                        b2.SetPixel(x, y, k);
                    }
                    else if (comboBox1.Text == "Odejmowanie")
                    {
                        if (k1.R - k2.R < 0) r = 0;
                        else r = k1.R - k2.R;

                        if (k1.G - k2.G < 0) g = 0;
                        else g = k1.G - k2.G;

                        if (k1.B - k2.B < 0) b = 0;
                        else b = k1.B - k2.B;

                        k = Color.FromArgb(r, g, b);
                        b2.SetPixel(x, y, k);
                    }
                    else if (comboBox1.Text == "Różnica")
                    {
                        r = k1.R - k2.R;
                        if (r < 0) r *= -1;

                        g = k1.G - k2.G;
                        if (g < 0) g *= -1;

                        b = k1.B - k2.B;
                        if (b < 0) b *= -1;

                        k = Color.FromArgb(r, g, b);
                        b2.SetPixel(x, y, k);
                    }
                    else if (comboBox1.Text == "Mnożenie")
                    {
                        r = k1.R * k2.R / 255;

                        g = k1.G * k2.G / 255;

                        b = k1.B * k2.B / 255;

                        k = Color.FromArgb(r, g, b);
                        b2.SetPixel(x, y, k);
                    }
                    else if (comboBox1.Text == "Mnożenie Odwrotności")
                    {
                        r = 255 - (((255 - k1.R) * (255 - k2.R)) / 255);
                        if (r > 255) r = 255;
                        if (r < 0) r = 0;

                        g = 255 - (((255 - k1.G) * (255 - k2.G)) / 255);
                        if (g > 255) g = 255;
                        if (g < 0) g = 0;

                        b = 255 - (((255 - k1.B) * (255 - k2.B)) / 255);
                        if (b > 255) b = 255;
                        if (b < 0) b = 0;

                        k = Color.FromArgb(r, g, b);
                        b2.SetPixel(x, y, k);
                    }
                    else if (comboBox1.Text == "Negacja")
                    {
                        r = 255 - k1.R - k2.R;
                        if (r < 0) r *= -1;
                        r = 255 - r;

                        g = 255 - k1.G - k2.G;
                        if (g < 0) g *= -1;
                        g = 255 - g;

                        b = 255 - k1.B - k2.B;
                        if (b < 0) b *= -1;
                        b = 255 - b;

                        k = Color.FromArgb(r, g, b);
                        b2.SetPixel(x, y, k);
                    }
                    else if (comboBox1.Text == "Ciemniejsze")
                    {
                        if (k1.R < k2.R) r = k1.R;
                        else r = k2.R;

                        if (k1.G < k2.G) g = k1.G;
                        else g = k2.G;

                        if (k1.B < k2.B) b = k1.B;
                        else b = k2.B;

                        k = Color.FromArgb(r, g, b);
                        b2.SetPixel(x, y, k);
                    }
                    else if (comboBox1.Text == "Jaśniejsze")
                    {
                        if (k1.R > k2.R) r = k1.R;
                        else r = k2.R;

                        if (k1.G > k2.G) g = k1.G;
                        else g = k2.G;

                        if (k1.B > k2.B) b = k1.B;
                        else b = k2.B;

                        k = Color.FromArgb(r, g, b);
                        b2.SetPixel(x, y, k);
                    }
                    else if (comboBox1.Text == "Wyłączenie")
                    {
                        r = k1.R + k2.R - (2 * k1.R * k2.R) / 255;
                        if (r < 0) r *= -1;

                        g = k1.G + k2.G - (2 * k1.G * k2.G) / 255;
                        if (g < 0) g *= -1;

                        b = k1.B + k2.B - (2 * k1.B * k2.B) / 255;
                        if (b < 0) b *= -1;

                        k = Color.FromArgb(r, g, b);
                        b2.SetPixel(x, y, k);
                    }
                    else if (comboBox1.Text == "Nakładka")
                    {
                        if (k1.R < 128) r = (2 * k1.R * k2.R) / 255;
                        else r = 255 - (2 * (255 - k1.R) * (255 - k2.R)) / 255;
                        if (r < 0) r *= -1;

                        if (k1.G < 128) g = (2 * k1.G * k2.G) / 255;
                        else g = 255 - (2 * (255 - k1.G) * (255 - k2.G)) / 255;
                        if (g < 0) g *= -1;

                        if (k1.B < 128) b = (2 * k1.B * k2.B) / 255;
                        else b = 255 - (2 * (255 - k1.B) * (255 - k2.B)) / 255;
                        if (b < 0) b *= -1;

                        k = Color.FromArgb(r, g, b);
                        b2.SetPixel(x, y, k);
                    }
                    else if (comboBox1.Text == "Ostre Światło")
                    {
                        if (k2.R < 0) r = (2 * k1.R * k2.R) / 255;
                        else r = 255 - (2 * (255 - k1.R) * (255 - k2.R)) / 255;
                        if (r < 0) r *= -1;

                        if (k2.G < 0) g = (2 * k1.G * k2.G) / 255;
                        else g = 255 - (2 * (255 - k1.G) * (255 - k2.G)) / 255;
                        if (g < 0) g *= -1;

                        if (k2.B < 0) b = (2 * k1.B * k2.B) / 255;
                        else b = 255 - (2 * (255 - k1.B) * (255 - k2.B)) / 255;
                        if (b < 0) b *= -1;

                        k = Color.FromArgb(r, g, b);
                        b2.SetPixel(x, y, k);
                    }
                    else if (comboBox1.Text == "Łagodne Światło")
                    {
                        if (k2.R < 0.5) r = ((2 * k1.R * k2.R) / 255) + ((((k1.R * k1.R) / 255) * (255 - (k2.R * k2.R) / 255)) / 255);
                        else r = ((k1.R * (((k2.R * 2) / 255) - 1)) / 255) + ((((k1.R * 2) / 255) * (255 - k2.R)) / 255);
                        if (r < 0) r *= -1;

                        if (k2.G < 0.5) g = ((2 * k1.G * k2.G) / 255) + ((((k1.G * k1.G) / 255) * (255 - (k2.G * k2.G) / 255)) / 255);
                        else g = ((k1.G * (((k2.G * 2) / 255) - 1)) / 255) + ((((k1.G * 2) / 255) * (255 - k2.G)) / 255);
                        if (g < 0) g *= -1;

                        if (k2.B < 0.5) b = ((2 * k1.B * k2.B) / 255) + ((((k1.B * k1.B) / 255) * (255 - (k2.B * k2.B) / 255)) / 255);
                        else b = ((k1.B * (((k2.B * 2) / 255) - 1)) / 255) + ((((k1.B * 2) / 255) * (255 - k2.B)) / 255);
                        if (b < 0) b *= -1;

                        k = Color.FromArgb(r, g, b);
                        b2.SetPixel(x, y, k);
                    }
                    else if (comboBox1.Text == "Rozcieńczenie")
                    {
                        if (k2.R == 0) r = 255;
                        else r = (k1.R * 255 / (255 - k2.R));
                        if (r < 0) r = 0; if (r > 255) r = 255;

                        if (k2.G == 0) g = 255;
                        else g = (k1.G * 255 / (255 - k2.G));
                        if (g < 0) g = 0; if (g > 255) g = 255;

                        if (k2.B == 0) b = 255;
                        else b = (k1.B * 255 / (255 - k2.B));
                        if (b < 0) b = 0; if (b > 255) b = 255;

                        k = Color.FromArgb(r, g, b);
                        b2.SetPixel(x, y, k);
                    }
                    else if (comboBox1.Text == "Wypalanie")
                    {
                        if (k2.R == 0) r = 255;
                        else r = (255 - (((255 - k1.R) * 255) / k2.R));
                        if (r < 0) r = 0; if (r > 255) r = 255;

                        if (k2.G == 0) g = 255;
                        else g = (255 - (((255 - k1.G) * 255) / k2.G));
                        if (g < 0) g = 0; if (g > 255) g = 255;

                        if (k2.B == 0) b = 255;
                        else b = (255 - (((255 - k1.B) * 255) / k2.B));
                        if (b < 0) b = 0; if (b > 255) b = 255;

                        k = Color.FromArgb(r, g, b);
                        b2.SetPixel(x, y, k);
                    }
                }
            }

            pictureBox2.Refresh();
        }
    }
}
