using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication8
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Bitmap bmpBK;
        //int x, y;
        int []xx=new int[10];
        int []yy=new int[10];

        int[] rr = new int[4];
        int[] gg = new int[4];
        int[] bb = new int[4];


        //BufferedImage Nueva_Imagen,imagen;
         Bitmap Nueva_Imagen;
	    int i,j,r,g,b,w,h;
	    float sum = 0;
	    int[,] rojo,verde,azul,HorizontalR,VerticalR,HorizontalG,VerticalG,HorizontalB,VerticalB;
	    //int a[];    
	    Color colorAuxiliar;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Todos|*.*|Archivos JPEG|*.jpg|Archivos GIF|*.gif";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            bmp = new Bitmap(openFileDialog1.FileName);
            bmpBK = bmp;
            pictureBox1.Image = bmp;
            inicio();
        }
        void inicio(){
        	Nueva_Imagen=new Bitmap(bmp.Width, bmp.Height) ;
		    h=bmp.Height;
		    w=bmp.Width;
		    rojo=new int[w,h];
		    verde=new int[w,h];
		    azul=new int[w,h];
		    HorizontalR=new int[w,h];
		    HorizontalG=new int[w,h];
		    HorizontalB=new int[w,h];
		    VerticalR=new int[w,h];
		    VerticalG=new int[w,h];
		    VerticalB=new int[w,h];
            int asa=w*h;
		    int []a=new int [asa];
		    for( i=0;i<bmp.Width;i++){
			    for(j=0;j<bmp.Height;j++){
				    colorAuxiliar=new Color();
                    colorAuxiliar = bmp.GetPixel(i, j);
				    r = colorAuxiliar.R;
				    g= colorAuxiliar.G;
				    b = colorAuxiliar.B;
				    rojo[i,j]=r; 
				    verde[i,j]=g; 
				    azul[i,j]=b; 
			    }
		    }
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }


        void OperacionVertical() {
            for (i = 0; i < bmp.Width; i++)
            {
                for (j = 0; j < bmp.Height - 1; j++)
                {
                    VerticalR[i,j] = Math.Abs((rojo[i,j + 1] - rojo[i,j]));
                    VerticalG[i,j] = Math.Abs((verde[i,j + 1] - verde[i,j]));
                    VerticalB[i,j] = Math.Abs((azul[i,j + 1] - azul[i,j]));
                    Nueva_Imagen.SetPixel(i, j, Color.FromArgb(VerticalR[i,j], VerticalG[i,j], VerticalB[i,j]));
                    //Nueva_Imagen.SetPixel(i, j, Color.FromArgb(VerticalR[i,j], VerticalG[i,j], VerticalB[i,j]).getPixel());
                }
            }
        }
        void OperacionHorizontal(){
            for (i = 0; i < bmp.Width; i++)
            {
                for (j = 0; j < bmp.Height - 1; j++)
                {
                    VerticalR[i,j] = Math.Abs((rojo[i,j + 1] - rojo[i,j]));
                    VerticalG[i,j] = Math.Abs((verde[i,j + 1] - verde[i,j]));
                    VerticalB[i,j] = Math.Abs((azul[i,j + 1] - azul[i,j]));
                    Nueva_Imagen.SetPixel(i, j, Color.FromArgb(VerticalR[i,j], VerticalG[i,j], VerticalB[i,j]));
                }
            }
        }

        int calculagradiente(int x, int y)
        {
            double a, b, c;
            a = x * x;
            b = y * y;
            c = .5 * Math.Sqrt(a + b);
            return (int)Math.Ceiling(c);
            
        }

        void Gradiente() {
            OperacionVertical();
            OperacionHorizontal();

            for (i = 0; i < bmp.Width - 1; i++)
            {
                for (j = 0; j < bmp.Height; j++)
                {
                    r = calculagradiente(HorizontalR[i,j], VerticalR[i,j]);
                    g = calculagradiente(HorizontalG[i,j], VerticalG[i,j]);
                    b = calculagradiente(HorizontalB[i,j], VerticalB[i,j]);
                    //Nueva_Imagen.SetPixel(i, j, Color.FromArgb(VerticalR[i, j], VerticalG[i, j], VerticalB[i, j]));
                    Nueva_Imagen.SetPixel(i, j, Color.FromArgb(r, g, b));
                    sum = sum + r + g + b;
                }
            }

            bmp = Nueva_Imagen;
            //mostrar
            pictureBox1.Image = bmp;
        }

        private void button7_Click(object sender, EventArgs e)
        {                
                Gradiente();
        }
        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {   //limpiar
            pictureBox1.Image = bmpBK;
            bmp = bmpBK;
        }
    }
}
