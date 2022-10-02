using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;//libreria para música

namespace wumpus
{
    public partial class Form1 : Form
    {
        int newSize = 50; //variabe global
        int[,] matriz = new int[4, 4]; //matriz del juego
        int[,] matrizSecundaria = new int[4, 4]; //sirve para verificar wumpus,olor,brisa,hoyo y moneda
        int personaje = 0; //variable para ver si el personaje no ha muerto
        int juego = 0; // si esta en 0 el juego aun no ha iniciado ; 1 juego empezo
        //ubicación del personaje
        int x = 0; //eje x
        int y = 0; //eje y
        //int[,] ubicacionPersonaje = new int[1, 1];//permite identificar donde se encuentra el personaje
        public Form1()
        {
            InitializeComponent();
            this.Hide();
            //Mostramos primero el form
            Form1 frm = this;
            //frm.Show(); 


            //botones transparantes
            //Derecha
            btnDerecha.FlatStyle = FlatStyle.Flat;
            btnDerecha.BackColor = Color.Transparent;
            btnDerecha.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnDerecha.FlatAppearance.MouseOverBackColor = Color.Transparent;
           
            //Izquierda
            btnIzquierda.FlatStyle = FlatStyle.Flat;
            btnIzquierda.BackColor = Color.Transparent;
            btnIzquierda.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnIzquierda.FlatAppearance.MouseOverBackColor = Color.Transparent;

            //Arriba
            btnArriba.FlatStyle = FlatStyle.Flat;
            btnArriba.BackColor = Color.Transparent;
            btnArriba.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnArriba.FlatAppearance.MouseOverBackColor = Color.Transparent;

            //Abajo
            btnCentro.FlatStyle = FlatStyle.Flat;
            btnCentro.BackColor = Color.Transparent;
            btnCentro.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnCentro.FlatAppearance.MouseOverBackColor = Color.Transparent;

            MessageBox.Show("Bienvenido al juego de Wumpus!!!");
            
        
     
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        //BOTON DE INICIO
        private void button1_Click_1(object sender, EventArgs e)
        {
            //explicación del arreglo
            /*
             0 - casilla con signo ?
             1 - personaje
             2 - descubierto
             3 - olor
             4 - wumpus
             5 - brisa
             6 - hoyo
             7 - oro
            */


            //aqui se guarda la opción seleccionada por el usuario
            DialogResult seleccion;
            seleccion = MessageBox.Show("Se iniciará una nueva partida", "¿Está seguro de continuar?", MessageBoxButtons.YesNo);
        
            if(seleccion == DialogResult.Yes)//si el usuario desea iniciar nueva partida
            {
                limpiaCuadros();
                musica();//función para reproducir musica
                colocaWumpus_Y_Hoyo(); // función para generar al wumpus
               
            }
        
        }

        //funcion que limpia todas las casillas de juego y las pone por default con signo ?
        public void limpiaCuadros()
        {
           

            //incia el juego 
            juego = 1;

            //ubicacion personaje
            x = 0;
            y = 0;

            //da vida al personaje
            personaje = 1;

            //limpia los arreglos bidimensionales
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    matriz[i, j] = 0;
                    matrizSecundaria[i, j] = 0;
                    Console.WriteLine("matriz ["+ i + "]" + "[" + j + "] = " + matriz[i,j] );//visualizo en consola el arreglo limpio
                }
            }

            //limpia los cuadros de las picturebox
            p1_1.Image = System.Drawing.Image.FromFile("recursos/default.png");
            p1_2.Image = System.Drawing.Image.FromFile("recursos/default.png");
            p1_3.Image = System.Drawing.Image.FromFile("recursos/default.png");
            p1_4.Image = System.Drawing.Image.FromFile("recursos/default.png");
            p2_1.Image = System.Drawing.Image.FromFile("recursos/default.png");
            p2_2.Image = System.Drawing.Image.FromFile("recursos/default.png");
            p2_3.Image = System.Drawing.Image.FromFile("recursos/default.png");
            p2_4.Image = System.Drawing.Image.FromFile("recursos/default.png");
            p3_1.Image = System.Drawing.Image.FromFile("recursos/default.png");
            p3_2.Image = System.Drawing.Image.FromFile("recursos/default.png");
            p3_3.Image = System.Drawing.Image.FromFile("recursos/default.png");
            p3_4.Image = System.Drawing.Image.FromFile("recursos/default.png");
            p4_1.Image = System.Drawing.Image.FromFile("recursos/default.png");
            p4_2.Image = System.Drawing.Image.FromFile("recursos/default.png");
            p4_3.Image = System.Drawing.Image.FromFile("recursos/default.png");
            p4_4.Image = System.Drawing.Image.FromFile("recursos/default.png");

            //coloca al persona en casilla 1-1
            p1_1.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
            //matriz[0, 0] = 1; //ubicación inicial del personaje
        }


        public void musica()
        {
            SoundPlayer Player = new SoundPlayer();
            Player.SoundLocation = "recursos/mariomaze.wav";
            Player.PlayLooping();
        }

        //sonido de salto del personaje al cambiar el bloque
        public void salto()
        {
            SoundPlayer Player = new SoundPlayer();
            Player.SoundLocation = "recursos/salto.wav";
            Player.Play();
            System.Threading.Thread.Sleep(800);//retraso para escuchar el salto
            musica();
        }

        public void musicaGano()
        {
            SoundPlayer Player = new SoundPlayer();
            Player.SoundLocation = "recursos/oroEncontrado.wav";
            Player.Play();
        }

        public void musicaDeath()
        {
            SoundPlayer Player = new SoundPlayer();
            Player.SoundLocation = "recursos/muerte.wav";
            Player.Play();
            //System.Threading.Thread.Sleep(800);//retraso para escuchar el salto
            juego = 0; // juego termina
            personaje = 0;//personaje no tiene vida
           // musica();
        }

        public void musicaOro()
        {
            SoundPlayer Player = new SoundPlayer();
            Player.SoundLocation = "recursos/oroEncontrado.wav";
            Player.Play();
            //System.Threading.Thread.Sleep(800);//retraso para escuchar el salto
            juego = 0; // juego termina
            personaje = 0;//personaje no tiene vida
                          // musica();
        }

        public void musicaCaida()
        {
            SoundPlayer Player = new SoundPlayer();
            Player.SoundLocation = "recursos/grito.wav";
            Player.Play();
            //System.Threading.Thread.Sleep(800);//retraso para escuchar el salto
            juego = 0; // juego termina
            personaje = 0;//personaje no tiene vida
                          // musica();
        }


        //funcion que valida los movimientos que realiza el personaje
        public void movimientos()
        {

            // si el personaje
            // == 1 con vida
            // == 0 muerto
            while(personaje == 1)
            {
                //realizar las validaciones de botones
            }
            
            


        }

        //función que distribuye los wumpus aleatoriamente en el mapa tambien conlocando su hedor
        public void colocaWumpus_Y_Hoyo()
        {
            //número aleatorio para WUMPUS
            Random myObject = new Random();
            Random myObject2 = new Random();
            int i = myObject.Next(1,3);// número para fila 1 a 3
            int j = myObject2.Next(1,3);// número para columna 1 a 3

            matriz[i, j] = 4; // se coloca a un wumpus
         

                    //coloca hedores
                    matriz[i - 1, j] = 3;
                    matriz[i + 1, j] = 3;

           


            //número aleatorio para HOYO
            Random myObject3 = new Random();
            Random myObject4 = new Random();
            int n = myObject.Next(1, 3);// número para fila 1 a 3
            int m = myObject2.Next(1, 3);// número para columna 1 a 3


            //valida que no coloque hoyos encima del wumpus

            do
            {
                Console.WriteLine("se queda en ciclo 1");
                n = myObject.Next(1, 3);
            } while(i == n);

            do
            {
                Console.WriteLine("se queda en ciclo 2");
                m = myObject.Next(1, 3);
            } while (j == m);

            matriz[n, m] = 6; // se coloca el hoyo
           

            //coloca brisas
            matriz[n - 1, m] = 5;
            matriz[n + 1, m] = 5;

           


            //colocamos el oro , recorro mi arreglo de forma inversa para colocar la ficha
            for (int a = 3; a >=0; a--)
            {
                for (int b = 3; b>=0; b--)
                {
                    if(matriz[a,b] == 0)//encontró un espacio para colocar el oro
                    {
                        matriz[a, b] = 7;//7 = oro
                        
                        return;//sale de la función
                    }
                }
            }

        }

        

       

        //permite localizar la ubicación del personaje dentro del mapa
        public void localizarpersonaje()
        {
           
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if(matriz[i, j] == 1)//busca al personaje(el personaje tiene valor de 1)
                    Console.WriteLine("matriz [" + i + "]" + "[" + j + "] = " + matriz[i, j]);//visualizo en consola el arreglo limpio
                    x = i;
                    y = j;
                    return;

                }
            }

            
        }

        public void actualizarImagenes()//permite mostrar una imagen de enemigos e indicadores, tambien con el oro
        {

            //P1_1
            if (matrizSecundaria[0, 0] == 1)
            {
                switch(matriz[0,0])
                {
                    case 3:
                        p1_1.Image = System.Drawing.Image.FromFile("recursos/olor.png");
                        break;
                    case 4:
                        p1_1.Image = System.Drawing.Image.FromFile("recursos/wumpus.jpg");
                        break;
                    case 5:
                        p1_1.Image = System.Drawing.Image.FromFile("recursos/brisa.png");
                        break;
                    case 6:
                        p1_1.Image = System.Drawing.Image.FromFile("recursos/hoyo.png");
                        break;
                    case 7:
                        p1_1.Image = System.Drawing.Image.FromFile("recursos/oro.jpg");
                        break;
                }
            }//P1_1

            //P1_2
            if (matrizSecundaria[1, 0] == 1)
            {
                switch (matriz[1, 0])
                {
                    case 3:
                        p1_2.Image = System.Drawing.Image.FromFile("recursos/olor.png");
                        break;
                    case 4:
                        p1_2.Image = System.Drawing.Image.FromFile("recursos/wumpus.jpg");
                        break;
                    case 5:
                        p1_2.Image = System.Drawing.Image.FromFile("recursos/brisa.png");
                        break;
                    case 6:
                        p1_2.Image = System.Drawing.Image.FromFile("recursos/hoyo.png");
                        break;
                    case 7:
                        p1_2.Image = System.Drawing.Image.FromFile("recursos/oro.jpg");
                        break;
                }
            }//P1_2

            //P1_3
            if (matrizSecundaria[2, 0] == 1)
            {
                switch (matriz[2, 0])
                {
                    case 3:
                        p1_3.Image = System.Drawing.Image.FromFile("recursos/olor.png");
                        break;
                    case 4:
                        p1_3.Image = System.Drawing.Image.FromFile("recursos/wumpus.jpg");
                        break;
                    case 5:
                        p1_3.Image = System.Drawing.Image.FromFile("recursos/brisa.png");
                        break;
                    case 6:
                        p1_3.Image = System.Drawing.Image.FromFile("recursos/hoyo.png");
                        break;
                    case 7:
                        p1_3.Image = System.Drawing.Image.FromFile("recursos/oro.jpg");
                        break;
                }
            }//P1_3

            //P1_4
            if (matrizSecundaria[3, 0] == 1)
            {
                switch (matriz[3, 0])
                {
                    case 3:
                        p1_4.Image = System.Drawing.Image.FromFile("recursos/olor.png");
                        break;
                    case 4:
                        p1_4.Image = System.Drawing.Image.FromFile("recursos/wumpus.jpg");
                        break;
                    case 5:
                        p1_4.Image = System.Drawing.Image.FromFile("recursos/brisa.png");
                        break;
                    case 6:
                        p1_4.Image = System.Drawing.Image.FromFile("recursos/hoyo.png");
                        break;
                    case 7:
                        p1_4.Image = System.Drawing.Image.FromFile("recursos/oro.jpg");
                        break;
                }
            }//P1_3

            //P2_1
            if (matrizSecundaria[0, 1] == 1)
            {
                switch (matriz[0, 1])
                {
                    case 3:
                        p2_1.Image = System.Drawing.Image.FromFile("recursos/olor.png");
                        break;
                    case 4:
                        p2_1.Image = System.Drawing.Image.FromFile("recursos/wumpus.jpg");
                        break;
                    case 5:
                        p2_1.Image = System.Drawing.Image.FromFile("recursos/brisa.png");
                        break;
                    case 6:
                        p2_1.Image = System.Drawing.Image.FromFile("recursos/hoyo.png");
                        break;
                    case 7:
                        p2_1.Image = System.Drawing.Image.FromFile("recursos/oro.jpg");
                        break;
                }
            }//P2_1

            //P2_2
            if (matrizSecundaria[1, 1] == 1)
            {
                switch (matriz[1, 1])
                {
                    case 3:
                        p2_2.Image = System.Drawing.Image.FromFile("recursos/olor.png");
                        break;
                    case 4:
                        p2_2.Image = System.Drawing.Image.FromFile("recursos/wumpus.jpg");
                        break;
                    case 5:
                        p2_2.Image = System.Drawing.Image.FromFile("recursos/brisa.png");
                        break;
                    case 6:
                        p2_2.Image = System.Drawing.Image.FromFile("recursos/hoyo.png");
                        break;
                    case 7:
                        p2_2.Image = System.Drawing.Image.FromFile("recursos/oro.jpg");
                        break;
                }
            }//P2_2

            //P2_3
            if (matrizSecundaria[2, 1] == 1)
            {
                switch (matriz[2, 1])
                {
                    case 3:
                        p2_3.Image = System.Drawing.Image.FromFile("recursos/olor.png");
                        break;
                    case 4:
                        p2_3.Image = System.Drawing.Image.FromFile("recursos/wumpus.jpg");
                        break;
                    case 5:
                        p2_3.Image = System.Drawing.Image.FromFile("recursos/brisa.png");
                        break;
                    case 6:
                        p2_3.Image = System.Drawing.Image.FromFile("recursos/hoyo.png");
                        break;
                    case 7:
                        p2_3.Image = System.Drawing.Image.FromFile("recursos/oro.jpg");
                        break;
                }
            }//P2_3


            //P2_4
            if (matrizSecundaria[3, 1] == 1)
            {
                switch (matriz[3, 1])
                {
                    case 3:
                        p2_4.Image = System.Drawing.Image.FromFile("recursos/olor.png");
                        break;
                    case 4:
                        p2_4.Image = System.Drawing.Image.FromFile("recursos/wumpus.jpg");
                        break;
                    case 5:
                        p2_4.Image = System.Drawing.Image.FromFile("recursos/brisa.png");
                        break;
                    case 6:
                        p2_4.Image = System.Drawing.Image.FromFile("recursos/hoyo.png");
                        break;
                    case 7:
                        p2_4.Image = System.Drawing.Image.FromFile("recursos/oro.jpg");
                        break;
                }
            }//P2_4

            //P3_1
            if (matrizSecundaria[0, 2] == 1)
            {
                switch (matriz[0, 2])
                {
                    case 3:
                        p3_1.Image = System.Drawing.Image.FromFile("recursos/olor.png");
                        break;
                    case 4:
                        p3_1.Image = System.Drawing.Image.FromFile("recursos/wumpus.jpg");
                        break;
                    case 5:
                        p3_1.Image = System.Drawing.Image.FromFile("recursos/brisa.png");
                        break;
                    case 6:
                        p3_1.Image = System.Drawing.Image.FromFile("recursos/hoyo.png");
                        break;
                    case 7:
                        p3_1.Image = System.Drawing.Image.FromFile("recursos/oro.jpg");
                        break;
                }
            }//P3_1

            //P3_2
            if (matrizSecundaria[1, 2] == 1)
            {
                switch (matriz[1, 2])
                {
                    case 3:
                        p3_2.Image = System.Drawing.Image.FromFile("recursos/olor.png");
                        break;
                    case 4:
                        p3_2.Image = System.Drawing.Image.FromFile("recursos/wumpus.jpg");
                        break;
                    case 5:
                        p3_2.Image = System.Drawing.Image.FromFile("recursos/brisa.png");
                        break;
                    case 6:
                        p3_2.Image = System.Drawing.Image.FromFile("recursos/hoyo.png");
                        break;
                    case 7:
                        p3_2.Image = System.Drawing.Image.FromFile("recursos/oro.jpg");
                        break;
                }
            }//P3_2

            //P3_3
            if (matrizSecundaria[2, 2] == 1)
            {
                switch (matriz[2, 2])
                {
                    case 3:
                        p3_3.Image = System.Drawing.Image.FromFile("recursos/olor.png");
                        break;
                    case 4:
                        p3_3.Image = System.Drawing.Image.FromFile("recursos/wumpus.jpg");
                        break;
                    case 5:
                        p3_3.Image = System.Drawing.Image.FromFile("recursos/brisa.png");
                        break;
                    case 6:
                        p3_3.Image = System.Drawing.Image.FromFile("recursos/hoyo.png");
                        break;
                    case 7:
                        p3_3.Image = System.Drawing.Image.FromFile("recursos/oro.jpg");
                        break;
                }
            }//P3_3

            //P3_4
            if (matrizSecundaria[3, 2] == 1)
            {
                switch (matriz[3, 2])
                {
                    case 3:
                        p3_4.Image = System.Drawing.Image.FromFile("recursos/olor.png");
                        break;
                    case 4:
                        p3_4.Image = System.Drawing.Image.FromFile("recursos/wumpus.jpg");
                        break;
                    case 5:
                        p3_4.Image = System.Drawing.Image.FromFile("recursos/brisa.png");
                        break;
                    case 6:
                        p3_4.Image = System.Drawing.Image.FromFile("recursos/hoyo.png");
                        break;
                    case 7:
                        p3_4.Image = System.Drawing.Image.FromFile("recursos/oro.jpg");
                        break;
                }
            }//P3_4

            //P4_1
            if (matrizSecundaria[0, 3] == 1)
            {
                switch (matriz[0, 3])
                {
                    case 3:
                        p4_1.Image = System.Drawing.Image.FromFile("recursos/olor.png");
                        break;
                    case 4:
                        p4_1.Image = System.Drawing.Image.FromFile("recursos/wumpus.jpg");
                        break;
                    case 5:
                        p4_1.Image = System.Drawing.Image.FromFile("recursos/brisa.png");
                        break;
                    case 6:
                        p4_1.Image = System.Drawing.Image.FromFile("recursos/hoyo.png");
                        break;
                    case 7:
                        p4_1.Image = System.Drawing.Image.FromFile("recursos/oro.jpg");
                        break;
                }
            }//P4_1

            //P4_2
            if (matrizSecundaria[1, 3] == 1)
            {
                switch (matriz[1, 3])
                {
                    case 3:
                        p4_2.Image = System.Drawing.Image.FromFile("recursos/olor.png");
                        break;
                    case 4:
                        p4_2.Image = System.Drawing.Image.FromFile("recursos/wumpus.jpg");
                        break;
                    case 5:
                        p4_2.Image = System.Drawing.Image.FromFile("recursos/brisa.png");
                        break;
                    case 6:
                        p4_2.Image = System.Drawing.Image.FromFile("recursos/hoyo.png");
                        break;
                    case 7:
                        p4_2.Image = System.Drawing.Image.FromFile("recursos/oro.jpg");
                        break;
                }
            }//P4_2

            //P4_3
            if (matrizSecundaria[2, 3] == 1)
            {
                switch (matriz[2, 3])
                {
                    case 3:
                        p4_3.Image = System.Drawing.Image.FromFile("recursos/olor.png");
                        break;
                    case 4:
                        p4_3.Image = System.Drawing.Image.FromFile("recursos/wumpus.jpg");
                        break;
                    case 5:
                        p4_3.Image = System.Drawing.Image.FromFile("recursos/brisa.png");
                        break;
                    case 6:
                        p4_3.Image = System.Drawing.Image.FromFile("recursos/hoyo.png");
                        break;
                    case 7:
                        p4_3.Image = System.Drawing.Image.FromFile("recursos/oro.jpg");
                        break;
                }
            }//P4_3

            //P4_4
            if (matrizSecundaria[3, 3] == 1)
            {
                switch (matriz[3, 3])
                {
                    case 3:
                        p4_4.Image = System.Drawing.Image.FromFile("recursos/olor.png");
                        break;
                    case 4:
                        p4_4.Image = System.Drawing.Image.FromFile("recursos/wumpus.jpg");
                        break;
                    case 5:
                        p4_4.Image = System.Drawing.Image.FromFile("recursos/brisa.png");
                        break;
                    case 6:
                        p4_4.Image = System.Drawing.Image.FromFile("recursos/hoyo.png");
                        break;
                    case 7:
                        p4_4.Image = System.Drawing.Image.FromFile("recursos/oro.jpg");
                        break;
                }
            }//P4_4
    }

        //actualiza las imagenes de la tabla cuando el personaje se mueve
        public void actualizarTabla()
        {
            //p1_1
            switch (matriz[0, 0])
            {
                case 0:
                    p1_1.Image = System.Drawing.Image.FromFile("recursos/default.png");
                    break;
                case 1:
                    p1_1.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
                    break;
                case 2:
                    p1_1.Image = System.Drawing.Image.FromFile("recursos/descubierto.jpg");
                    break;
              
               
            }

            //p1_2
            switch (matriz[1, 0])
            {
                case 0:
                    p1_2.Image = System.Drawing.Image.FromFile("recursos/default.png");
                    break;
                case 1:
                    p1_2.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
                    break;
                case 2:
                    p1_2.Image = System.Drawing.Image.FromFile("recursos/descubierto.jpg");
                    break;
               
              
            }

            //p1_3
            switch (matriz[2, 0])
            {
                case 0:
                    p1_3.Image = System.Drawing.Image.FromFile("recursos/default.png");
                    break;
                case 1:
                    p1_3.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
                    break;
                case 2:
                    p1_3.Image = System.Drawing.Image.FromFile("recursos/descubierto.jpg");
                    break;
              
                
            }
            
            //p1_4
            switch (matriz[3, 0])
            {
                case 0:
                    p1_4.Image = System.Drawing.Image.FromFile("recursos/default.png");
                    break;
                case 1:
                    p1_4.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
                    break;
                case 2:
                    p1_4.Image = System.Drawing.Image.FromFile("recursos/descubierto.jpg");
                    break;
              
                
            }
            
            //p2_1
            switch (matriz[0, 1])
            {
                case 0:
                    p2_1.Image = System.Drawing.Image.FromFile("recursos/default.png");
                    break;
                case 1:
                    p2_1.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
                    break;
                case 2:
                    p2_1.Image = System.Drawing.Image.FromFile("recursos/descubierto.jpg");
                    break;
                
                
            }

            //p2_2
            switch (matriz[1, 1])
            {
                case 0:
                    p2_2.Image = System.Drawing.Image.FromFile("recursos/default.png");
                    break;
                case 1:
                    p2_2.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
                    break;
                case 2:
                    p2_2.Image = System.Drawing.Image.FromFile("recursos/descubierto.jpg");
                    break;
              
                
            }

            //p2_3
            switch (matriz[2, 1])
            {
                case 0:
                    p2_3.Image = System.Drawing.Image.FromFile("recursos/default.png");
                    break;
                case 1:
                    p2_3.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
                    break;
                case 2:
                    p2_3.Image = System.Drawing.Image.FromFile("recursos/descubierto.jpg");
                    break;
                
               
            }

            //p2_4
            switch (matriz[3, 1])
            {
                case 0:
                    p2_4.Image = System.Drawing.Image.FromFile("recursos/default.png");
                    break;
                case 1:
                    p2_4.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
                    break;
                case 2:
                    p2_4.Image = System.Drawing.Image.FromFile("recursos/descubierto.jpg");
                    break;
                
             
            }

            //p3_1
            switch (matriz[0, 2])
            {
                case 0:
                    p3_1.Image = System.Drawing.Image.FromFile("recursos/default.png");
                    break;
                case 1:
                    p3_1.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
                    break;
                case 2:
                    p3_1.Image = System.Drawing.Image.FromFile("recursos/descubierto.jpg");
                    break;
              
                
            }

            //p3_2
            switch (matriz[1, 2])
            {
                case 0:
                    p3_2.Image = System.Drawing.Image.FromFile("recursos/default.png");
                    break;
                case 1:
                    p3_2.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
                    break;
                case 2:
                    p3_2.Image = System.Drawing.Image.FromFile("recursos/descubierto.jpg");
                    break;
                
               
            }

            //p3_3
            switch (matriz[2, 2])
            {
                case 0:
                    p3_3.Image = System.Drawing.Image.FromFile("recursos/default.png");
                    break;
                case 1:
                    p3_3.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
                    break;
                case 2:
                    p3_3.Image = System.Drawing.Image.FromFile("recursos/descubierto.jpg");
                    break;
              
                
            }

            //p3_4
            switch (matriz[3, 2])
            {
                case 0:
                    p3_4.Image = System.Drawing.Image.FromFile("recursos/default.png");
                    break;
                case 1:
                    p3_4.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
                    break;
                case 2:
                    p3_4.Image = System.Drawing.Image.FromFile("recursos/descubierto.jpg");
                    break;
                
               
            }

            //p4_1
            switch (matriz[0, 3])
            {
                case 0:
                    p4_1.Image = System.Drawing.Image.FromFile("recursos/default.png");
                    break;
                case 1:
                    p4_1.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
                    break;
                case 2:
                    p4_1.Image = System.Drawing.Image.FromFile("recursos/descubierto.jpg");
                    break;
                
               
            }

            //p4_2
            switch (matriz[1, 3])
            {
                case 0:
                    p4_2.Image = System.Drawing.Image.FromFile("recursos/default.png");
                    break;
                case 1:
                    p4_2.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
                    break;
                case 2:
                    p4_2.Image = System.Drawing.Image.FromFile("recursos/descubierto.jpg");
                    break;
                
               
            }

            //p4_3
            switch (matriz[2, 3])
            {
                case 0:
                    p4_3.Image = System.Drawing.Image.FromFile("recursos/default.png");
                    break;
                case 1:
                    p4_3.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
                    break;
                case 2:
                    p4_3.Image = System.Drawing.Image.FromFile("recursos/descubierto.jpg");
                    break;
               
              
            }

            //p4_4
            switch (matriz[3, 3])
            {
                case 0:
                    p4_4.Image = System.Drawing.Image.FromFile("recursos/default.png");
                    break;
                case 1:
                    p4_4.Image = System.Drawing.Image.FromFile("recursos/personaje.jpg");
                    break;
                case 2:
                    p4_4.Image = System.Drawing.Image.FromFile("recursos/descubierto.jpg");
                    break;
               

            }


        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void btnDerecha_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("X vale: " + x);
            if (juego == 0)
            {
                MessageBox.Show("Favor inicie un nuevo juego antes de continuar");
                return;
            }
            else
            {
                salto();
                //localizarpersonaje();//busca donde se encuentra el personaje

                //debo poner aca una funcion exclusiva para validar cada picture box

                if (x == 3)//se esta saliendo del limite del juego
                {
                    MessageBox.Show("Se esta saliendo del limite");
                    return;
                }

                //función para ganar
                
                if (matriz[x + 1, y] == 7)// si la siguiente casilla del jugador ES ORO
                {
                    juego = 0;
                    personaje = 0;
                    //matriz[x + 1, y] = 4;
                    matrizSecundaria[x + 1, y] = 1;//activa para mostrar el oro


                    actualizarImagenes();//mostramos al wumpus

                    // actualizarTabla
                    musicaGano();//invoco la canción para victorio
                    MessageBox.Show("Gano el Juego!!!! FELICIDADES :D");
                    return;//vuelve la boton
                }



                ///NUEVOS METODOS
                ///
                //si el personaje cae en un hoyo
                if (matriz[x + 1, y] == 6)// si la siguiente casilla del jugador es el wumpus
                {
                    matriz[x, y] = 5;//limpio la localización de personaje
                    matrizSecundaria[x, y] = 1;//activo la localización de la brisa
                    actualizarImagenes();
                    juego = 0;
                    personaje = 0;
                    matriz[x + 1, y] = 6;
                    matrizSecundaria[x + 1, y] = 1;//activa para mostrar wumpus


                    actualizarImagenes();//mostramos al wumpus

                    // actualizarTabla
                    musicaCaida();//invoco la canción de muerte
                    MessageBox.Show("Su personaje a caído en un hoyo D:");
                    return;//vuelve la boton
                }

                //si el personaje actualmente se encuentra encima de la brisa
                if (matriz[x, y] == 5) // si actualmente se encuentra encima
                {//no se debe hacer cambios
                    matriz[x + 1, y] = 1; //el personaje se corre una casilla
                    x++;
                    actualizarTabla();
                    actualizarImagenes();//mostramos el olor

                    return;
                }

                //si se detecta una brisa
                if (matriz[x + 1, y] == 5)
                {
                    MessageBox.Show("Se siente una brisa");
                    matriz[x, y] = 2;//ya visitada
                    matriz[x + 1, y] = 1;//colocamos al personaje
                    x++;//sumo 1 a la localización del personaje
                    actualizarTabla();

                    //vuelvo a poner el valor del olor
                    matriz[x, y] = 5; //olor a wumpus en donde se encuentra el personaje
                    matrizSecundaria[x, y] = 1;// se activa por que descubrio el olor

                    return;

                }

                ///////////////


                //si el personaje detecta al wumpus
                if (matriz[x + 1, y] == 4)// si la siguiente casilla del jugador es el wumpus
                {
                    juego = 0;
                    personaje = 0;
                    matriz[x + 1, y] = 4;
                    matrizSecundaria[x + 1, y] = 1;//activa para mostrar wumpus
                    
                    
                    actualizarImagenes();//mostramos al wumpus
                    
                   // actualizarTabla
                    musicaDeath();//invoco la canción de muerte
                    MessageBox.Show("Ha encontrado al wumpus fin del juego D:");
                    return;//vuelve la boton
                }


                //si el personaje actualmente se encuentra encima del olor
                if (matriz[x,y] == 3) // si actualmente se encuentra encima
                {//no se debe hacer cambios
                    matriz[x + 1, y] = 1; //el personaje se corre una casilla
                    x++;
                    //actualizarTabla();
                    actualizarTabla();//actualizamos todo el tablero
                    actualizarImagenes();//actualizamos la imagen del olor
                    return;
                }
                //identifica si la siguiente casilla a la izquierda es olor a wumpus
                if (matriz[x + 1, y] == 3)
                {
                    MessageBox.Show("Cuidado huele a Wumpus");
                    matriz[x, y] = 2;//ya visitada
                    matriz[x + 1, y] = 1;//colocamos al personaje
                    x++;//sumo 1 a la localización del personaje
                    actualizarTabla();
                    
                    //vuelvo a poner el valor del olor
                    matriz[x, y] = 3; //olor a wumpus en donde se encuentra el personaje
                    matrizSecundaria[x, y] = 1;// se activa por que descubrio el olor

                    return;

                }
                else
                {
                    matriz[x, y] = 2;//ya visitada
                    matriz[x + 1, y] = 1;//colocamos al personaje
                    x++;//sumo 1 a la localización del personaje
                }
                actualizarTabla();

            }
            

        }

        private void btnCentro_Click(object sender, EventArgs e)
        {
            if (juego == 0)
            {
                MessageBox.Show("Favor inicie un nuevo juego antes de continuar");
                return;
            }
            else
            {
                salto();
                //localizarpersonaje();//busca donde se encuentra el personaje

                //debo poner aca una funcion exclusiva para validar cada picture box

                if (y == 3)//se esta saliendo del limite del juego
                {
                    MessageBox.Show("Se esta saliendo del limite");
                    return;
                }

                //función para ganar

                if (matriz[x, y + 1] == 7)// si la siguiente casilla del jugador ES ORO
                {
                    juego = 0;
                    personaje = 0;
                    //matriz[x + 1, y] = 4;
                    matrizSecundaria[x, y + 1] = 1;//activa para mostrar el oro


                    actualizarImagenes();//mostramos al wumpus

                    // actualizarTabla
                    musicaGano();//invoco la canción para victorio
                    MessageBox.Show("Gano el Juego!!!! FELICIDADES :D");
                    return;//vuelve la boton
                }

                ///NUEVOS METODOS
                ///
                //si el personaje cae en un hoyo
                if (matriz[x, y + 1] == 6)// si la siguiente casilla del jugador es el wumpus
                {
                    //matriz[x, y] = 5;//limpio la localización de personaje
                    //matrizSecundaria[x, y] = 1;//activo la localización de la brisa
                    actualizarImagenes();
                    juego = 0;
                    personaje = 0;
                    matriz[x, y + 1] = 6;
                    matrizSecundaria[x, y + 1] = 1;//activa para mostrar wumpus

                    matriz[x, y] = 2;//quito al personaje

                    actualizarTabla();

                    actualizarImagenes();//mostramos al wumpus

                    // actualizarTabla
                    musicaCaida();//invoco la canción de muerte
                    MessageBox.Show("Su personaje a caído en un hoyo D:");
                    return;//vuelve la boton
                }

                //si el personaje actualmente se encuentra encima de la brisa
                if (matriz[x, y] == 5) // si actualmente se encuentra encima
                {//no se debe hacer cambios
                    matriz[x , y + 1] = 1; //el personaje se corre una casilla
                    y++;
                    actualizarTabla();
                    actualizarImagenes();//mostramos el olor

                    return;
                }

                //si se detecta una brisa
                if (matriz[x, y + 1] == 5)
                {
                    MessageBox.Show("Se siente una brisa");
                    matriz[x, y] = 2;//ya visitada
                    matriz[x, y + 1] = 1;//colocamos al personaje
                    y++;//sumo 1 a la localización del personaje
                    actualizarTabla();

                    //vuelvo a poner el valor del olor
                    matriz[x, y] = 5; //olor a wumpus en donde se encuentra el personaje
                    matrizSecundaria[x, y] = 1;// se activa por que descubrio el olor

                    return;

                }

                ///////////////

                //si el personaje detecta al wumpus
                if (matriz[x, y + 1] == 4)// si la casilla abajo del jugador es el wumpus
                {
                    juego = 0;
                    personaje = 0;
                    matrizSecundaria[x, y + 1] = 1; 

                    actualizarImagenes();

                    musicaDeath();//invoco la canción de muerte
                    MessageBox.Show("Ha encontrado al wumpus fin del juego D:");
                    return;//vuelve la boton
                }

                //si el personaje actualmente se encuentra encima del olor
                if (matriz[x, y] == 3) // si actualmente se encuentra encima
                {//no se debe hacer cambios
                    matriz[x, y + 1] = 1; //el personaje se corre una casilla
                    y++;
                    actualizarTabla();
                    actualizarImagenes();//actualizamos imagen de olor

                    return;
                }
                //identifica si la casilla de abajo hay olor wumpus
                if (matriz[x, y + 1] == 3)
                {
                    MessageBox.Show("Cuidado huele a Wumpus");
                    matriz[x, y] = 2;//ya visitada
                    matriz[x, y + 1] = 1;//colocamos al personaje
                    y++;//sumo 1 a la localización del personaje
                    actualizarTabla();
                    //vuelvo a poner el valor del olor
                    matriz[x, y] = 3; //olor a wumpus en donde se encuentra el personaje
                    matrizSecundaria[x ,y ]= 1;

                    return;

                }
                else
                {
                    matriz[x, y] = 2;//ya visitada
                    matriz[x, y + 1] = 1;//colocamos al personaje
                    y++;//sumo 1 a la localización del personaje
                }
                actualizarTabla();

            }



            /*else
                salto();
            if (y == 3)//se esta saliendo del limite del juego
            {
                MessageBox.Show("Se esta saliendo del limite");
                return;
            }

            // verificamos si hay olor

            matriz[x, y] = 2;//ya visitada
            matriz[x, y + 1] = 1;//colocamos al personaje
            y++;//sumo 1 a la localización del personaje

            actualizarTabla();*/
        }

        private void btnArriba_Click(object sender, EventArgs e)
        {
            if (juego == 0)
            {
                MessageBox.Show("Favor inicie un nuevo juego antes de continuar");
                return;
            }
            else
            {
                salto();
                //localizarpersonaje();//busca donde se encuentra el personaje

                //debo poner aca una funcion exclusiva para validar cada picture box

                if (y == 0)//se esta saliendo del limite del juego
                {
                    MessageBox.Show("Se esta saliendo del limite");
                    return;
                }

                //función para ganar

                if (matriz[x, y - 1] == 7)// si la siguiente casilla del jugador ES ORO
                {
                    juego = 0;
                    personaje = 0;
                    //matriz[x + 1, y] = 4;
                    matrizSecundaria[x, y - 1] = 1;//activa para mostrar el oro


                    actualizarImagenes();//mostramos al wumpus

                    // actualizarTabla
                    musicaGano();//invoco la canción para victorio
                    MessageBox.Show("Gano el Juego!!!! FELICIDADES :D");
                    return;//vuelve la boton
                }

                ///NUEVOS METODOS
                ///
                //si el personaje cae en un hoyo
                if (matriz[x, y - 1] == 6)// si la siguiente casilla del jugador es el wumpus
                {
                    //matriz[x, y] = 5;//limpio la localización de personaje
                    //matrizSecundaria[x, y] = 1;//activo la localización de la brisa
                    actualizarImagenes();
                    juego = 0;
                    personaje = 0;
                    matriz[x, y - 1] = 6;
                    matrizSecundaria[x, y - 1] = 1;//activa para mostrar wumpus

                    matriz[x, y] = 2;
                    actualizarTabla();

                    actualizarImagenes();//mostramos al wumpus

                    // actualizarTabla
                    musicaCaida();//invoco la canción de muerte
                    MessageBox.Show("Su personaje a caído en un hoyo D:");
                    return;//vuelve la boton
                }

                //si el personaje actualmente se encuentra encima de la brisa
                if (matriz[x, y] == 5) // si actualmente se encuentra encima
                {//no se debe hacer cambios
                    matriz[x, y - 1] = 1; //el personaje se corre una casilla
                    y--;
                    actualizarTabla();
                    actualizarImagenes();//mostramos el olor

                    return;
                }

                //si se detecta una brisa
                if (matriz[x, y - 1] == 5)
                {
                    MessageBox.Show("Se siente una brisa");
                    matriz[x, y] = 2;//ya visitada
                    matriz[x, y - 1] = 1;//colocamos al personaje
                    y--;//sumo 1 a la localización del personaje
                    actualizarTabla();

                    //vuelvo a poner el valor del olor
                    matriz[x, y] = 5; //olor a wumpus en donde se encuentra el personaje
                    matrizSecundaria[x, y] = 1;// se activa por que descubrio el olor

                    return;

                }

                ///////////////

                //si el personaje detecta al wumpus
                if (matriz[x , y - 1] == 4)// si la casilla arriba del jugador es el wumpus
                {
                    juego = 0;
                    personaje = 0;

                    matrizSecundaria[x, y - 1] = 1;

                    actualizarImagenes();

                    musicaDeath();//invoco la canción de muerte
                    MessageBox.Show("Ha encontrado al wumpus fin del juego D:");
                    return;//vuelve la boton
                }



                //si el personaje actualmente se encuentra encima del olor
                if (matriz[x, y] == 3) // si actualmente se encuentra encima
                {//no se debe hacer cambios
                    matriz[x, y - 1] = 1; //el personaje se corre una casilla
                    y--;
                    actualizarTabla();
                    actualizarImagenes();//actualizamos que se muestre el olor

                    return;
                }
                //identifica si la casilla de abajo hay olor wumpus
                if (matriz[x, y - 1] == 3)
                {
                    MessageBox.Show("Cuidado huele a Wumpus");
                    matriz[x, y] = 2;//ya visitada
                    matriz[x, y - 1] = 1;//colocamos al personaje
                    y--;//sumo 1 a la localización del personaje
                    
                    actualizarTabla();
                    
                    //vuelvo a poner el valor del olor
                    matriz[x, y] = 3; //olor a wumpus en donde se encuentra el personaje
                    matrizSecundaria[x , y] = 1;//se activa el olor

                    return;

                }
                else
                {
                    matriz[x, y] = 2;//ya visitada
                    matriz[x, y - 1] = 1;//colocamos al personaje
                    y--;//sumo 1 a la localización del personaje
                }
                actualizarTabla();

            }

            /*else
                salto();
            if (y == 0)//se esta saliendo del limite del juego
            {
                MessageBox.Show("Se esta saliendo del limite");
                return;
            }*/

            /*
            matriz[x, y] = 2;//ya visitada
            matriz[x, y - 1] = 1;//colocamos al personaje
            y--;//resto 1 a la localización del personaje

            actualizarTabla();*/
        }

        private void btnIzquierda_Click(object sender, EventArgs e)
        {
            if (juego == 0)
            {
                MessageBox.Show("Favor inicie un nuevo juego antes de continuar");
                return;
            }
            else
                salto();

            if (x == 0)//se esta saliendo del limite del juego
            {
                MessageBox.Show("Se esta saliendo del limite");
                return;
            }
            else
            {
                //salto();
                //localizarpersonaje();//busca donde se encuentra el personaje

                //debo poner aca una funcion exclusiva para validar cada picture box

                if (x == 0)//se esta saliendo del limite del juego
                {
                    MessageBox.Show("Se esta saliendo del limite");
                    return;
                }

                //función para ganar

                if (matriz[x - 1, y] == 7)// si la siguiente casilla del jugador ES ORO
                {
                    juego = 0;
                    personaje = 0;
                    //matriz[x + 1, y] = 4;
                    matrizSecundaria[x - 1, y] = 1;//activa para mostrar el oro


                    actualizarImagenes();//mostramos al wumpus

                    // actualizarTabla
                    musicaGano();//invoco la canción para victorio
                    MessageBox.Show("Gano el Juego!!!! FELICIDADES :D");
                    return;//vuelve la boton
                }

                ///NUEVOS METODOS
                ///
                //si el personaje cae en un hoyo
                if (matriz[x - 1 , y] == 6)// si la siguiente casilla del jugador es el wumpus
                {
                    matriz[x, y] = 5;//limpio la localización de personaje
                    matrizSecundaria[x, y] = 1;//activo la localización de la brisa
                    actualizarImagenes();
                    juego = 0;
                    personaje = 0;
                    matriz[x - 1, y] = 6;
                    matrizSecundaria[x - 1, y] = 1;//activa para mostrar wumpus


                    actualizarImagenes();//mostramos al wumpus

                    // actualizarTabla
                    musicaCaida();//invoco la canción de muerte
                    MessageBox.Show("Su personaje a caído en un hoyo D:");
                    return;//vuelve la boton
                }

                //si el personaje actualmente se encuentra encima de la brisa
                if (matriz[x, y] == 5) // si actualmente se encuentra encima
                {//no se debe hacer cambios
                    matriz[x - 1, y] = 1; //el personaje se corre una casilla
                    x--;
                    actualizarTabla();
                    actualizarImagenes();//mostramos el olor

                    return;
                }

                //si se detecta una brisa
                if (matriz[x - 1, y] == 5)
                {
                    MessageBox.Show("Se siente una brisa");
                    matriz[x, y] = 2;//ya visitada
                    matriz[x - 1, y] = 1;//colocamos al personaje
                    x--;//sumo 1 a la localización del personaje
                    actualizarTabla();

                    //vuelvo a poner el valor del olor
                    matriz[x, y] = 5; //olor a wumpus en donde se encuentra el personaje
                    matrizSecundaria[x, y] = 1;// se activa por que descubrio el olor

                    return;

                }

                ///////////////

                //si el personaje detecta al wumpus
                if (matriz[x - 1, y] == 4)// si la anterior casilla del jugador es el wumpus
                {
                    juego = 0;
                    personaje = 0;
                    matrizSecundaria[x-1, y] = 1;

                    actualizarImagenes();

                    musicaDeath();//invoco la canción de muerte
                    MessageBox.Show("Ha encontrado al wumpus fin del juego D:");
                    return;//vuelve la boton
                }



                //si el personaje actualmente se encuentra encima del olor
                if (matriz[x, y] == 3) // si actualmente se encuentra encima
                {//no se debe hacer cambios
                    matriz[x - 1, y] = 1; //el personaje se corre una casilla
                    x--;
                    actualizarTabla();
                    actualizarImagenes();//mostramos el olor

                    return;
                }
                //identifica si la siguiente casilla a la izquierda es olor a wumpus
                if (matriz[x - 1, y] == 3)
                {
                    MessageBox.Show("Cuidado huele a Wumpus");
                    matriz[x, y] = 2;//ya visitada
                    matriz[x - 1, y] = 1;//colocamos al personaje
                    x--;//sumo 1 a la localización del personaje
                    actualizarTabla();

                    //vuelvo a poner el valor del olor
                    matriz[x, y] = 3; //olor a wumpus en donde se encuentra el personaje
                    matrizSecundaria[x, y] = 1;

                    return;

                }
                else
                {
                    matriz[x, y] = 2;//ya visitada
                    matriz[x - 1, y] = 1;//colocamos al personaje
                    x--;//sumo 1 a la localización del personaje
                }
                actualizarTabla();
               

            }

            /*
            matriz[x, y] = 2;//ya visitada
            matriz[x - 1, y] = 1;//colocamos al personaje
            x--;//resto 1 a la localización del personaje

            actualizarTabla();
            */
        }
    }
}
