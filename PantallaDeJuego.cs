using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Roar_Flight2
{
    public class PantallaDeJuego
    {
        ////--------------------------------------- Jugador
        private bool invencible;
        private Player player1;
        private Disparo disparo;
        ////--------------------------------------- Pociones
        private PowerUp pocionVelocidadY;
        private PowerUp pocionVelocidadX;
        private PowerUp pocionFuego;
        ////--------------------------------------- Enemigos
        private Enemigo[] enemigos;
        private Enemigo enemigo;
        private Enemigo enemigo2;
        private Enemigo enemigo3;
        private DisparoEnemigo disparoEnemigo;
        private int tiempoHastaSiguienteDisparo;

        //----------------------------------------- Pantallas
        private Fondo fondo1;
        private Fondo fondo2;
        private Fondo fondo3;
        //Fondo[] fondos;

        private SoundEffect sonidoDeDisparo;
        private SoundEffect hitPlayer;
        private SoundEffect hitEnemy1;
        private SoundEffect hitEnemy2;
        private SoundEffect pocionY;
        private SoundEffect pocionX;
        private SoundEffect pocionF;

        private SpriteFont fuente;
        private Song musicaDeFondo;

        private int vidas;
        private int puntuacion;
        private int puntuacionPowerUps;
        public bool Terminado { get; set; }
        public bool Pasado { get; set; }

        ////-----------------------------------Pruebas

        //private List<Disparo> disparos;
        private DateTime instanteUltimoDisparo;
        private DateTime ultimaVidaPerdida;

        public PantallaDeJuego(GestorDePantallas gestor) //Prueba gestor
        {
            vidas = 10;
            puntuacion = 0;
            puntuacionPowerUps = 0;
            tiempoHastaSiguienteDisparo = new System.Random().Next(1000, 2000);
            invencible = true;
            Terminado = false;

            //-----------------------------------Pruebas
            instanteUltimoDisparo = DateTime.Now;
            ultimaVidaPerdida = DateTime.Now;
        }

        //------------------------------------------------------------- CARGAR CONTENIDOS

        public void CargarContenidos(ContentManager Content)
        {
            MediaPlayer.Volume = 0.5f;
            musicaDeFondo = Content.Load<Song>("Moonlight3");
            MediaPlayer.Play(musicaDeFondo);
            MediaPlayer.Stop();
            MediaPlayer.IsRepeating = true;

            fuente = Content.Load<SpriteFont>("Arial1");

            //fondo1 = new Fondo(Content, "Stage1 continua-min"); //cambio
            fondo1 = new Fondo(Content, "Stage4 continua");
            //fondo3 = new Fondo(Content, "Stage3 continua");

            //fondos = new Fondo[3] { fondo1, fondo2, fondo3 };

            //------------------------------------------------------------ PRUEBA NIVELES

            player1 = new Player(Content);
            

            pocionVelocidadY = new PowerUp(Content);
            pocionVelocidadX = new PowerUp(Content);
            pocionFuego = new PowerUp(Content);
            pocionVelocidadY.Activo = false;
            pocionVelocidadX.Activo = false;
            pocionFuego.Activo = false;

            enemigo = new Enemigo(Content);
            enemigo2 = new Enemigo(Content);
            enemigo3 = new Enemigo(Content);
            enemigo3.SetVelocidad(0, 700);
            enemigos = new Enemigo[3] { enemigo, enemigo2, enemigo3 };
            disparoEnemigo = new DisparoEnemigo(Content);

            sonidoDeDisparo = Content.Load<SoundEffect>("Fire");
            hitPlayer = Content.Load<SoundEffect>("Hit");
            hitEnemy1 = Content.Load<SoundEffect>("Enemigo1");
            hitEnemy2 = Content.Load<SoundEffect>("Enemigo2");
            pocionY = Content.Load<SoundEffect>("pocionVelocidadFondo");
            pocionX = Content.Load<SoundEffect>("pocionVelocidadX");
            pocionF = Content.Load<SoundEffect>("pocionVelocidadX");

            //-----------------------------------Pruebas
            disparo = new Disparo(Content);
            disparo.Activo = false;
        }

        //------------------------------------------------------------- ACTUALIZAR

        public void Actualizar(GameTime gameTime, ContentManager Content)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Terminado = true;

            MoverElementos(gameTime);
            ComprobarColisiones();
            ComprobarEntrada(gameTime, Content);

            if (fondo1.CiclosPantalla == 20)
            {
                fondo1.CiclosPantalla = 0;
                Pasado = true;
            }

            TimeSpan tiempoInvencible = DateTime.Now - ultimaVidaPerdida;

            if (tiempoInvencible.TotalMilliseconds > 4000)
            {
                ultimaVidaPerdida = DateTime.Now;
                invencible = false;
            }

            if (puntuacionPowerUps >= 3)
            {
                puntuacionPowerUps = 0;

                pocionVelocidadX.Y = 0;
                pocionVelocidadY.Y = 0;
                pocionFuego.Y = 0;

                pocionVelocidadY.Activo = true;
                pocionVelocidadX.Activo = true;
                pocionFuego.Activo = true;
            }
        }

        //------------------------------------------------------------- DIBUJAR

        public void Dibujar(GameTime gameTime, SpriteBatch spriteBatch)
        {
            fondo1.Dibujar(spriteBatch, Color.White);

            spriteBatch.DrawString(fuente, "Puntuacion :" + puntuacion,
               new Vector2(450, 0),
               Color.Red);

            spriteBatch.DrawString(fuente, "Vidas :" + vidas,
                new Vector2(0, 0),
                Color.Red);

            spriteBatch.DrawString(fuente, "Ciclos :" + fondo1.CiclosPantalla,
                new Vector2(0, 200),
                Color.Red);

            pocionVelocidadY.Dibujar(spriteBatch, Color.White);
            pocionVelocidadX.Dibujar(spriteBatch, Color.Blue);
            pocionFuego.Dibujar(spriteBatch,Color.Red);          

            foreach (Enemigo enemigo in enemigos)
            {
                enemigo.Dibujar(spriteBatch, Color.White);
                enemigo2.Dibujar(spriteBatch,Color.Cyan);
                enemigo3.Dibujar(spriteBatch, Color.Red);
            }

            if (invencible)
            {
                player1.Dibujar(spriteBatch, Color.Red);
            }
            else
            {
                player1.Dibujar(spriteBatch, Color.White);
            }

            disparo.Dibujar(spriteBatch, Color.White);
            disparoEnemigo.Dibujar(spriteBatch, Color.Cyan);
        }

        //------------------------------------------------------------- MOVER ELEMENTOS


        protected void MoverElementos(GameTime gameTime)
        {

            //----------------------------------------------------- MOVER POCIONES

            if (pocionVelocidadY.Activo)
            {
                pocionVelocidadY.Mover(gameTime);
                pocionVelocidadY.MoverAbajo(gameTime);
                if (pocionVelocidadY.Y > 1361)
                {
                    pocionVelocidadY.Activo = false;
                }
            }
            if (pocionVelocidadX.Activo)
            {
                pocionVelocidadX.Mover(gameTime);
                pocionVelocidadX.MoverAbajo(gameTime);
                if (pocionVelocidadX.Y > 1361)
                {
                    pocionVelocidadX.Activo = false;
                }
            }
            if (pocionFuego.Activo)
            {
                pocionFuego.Mover(gameTime);
                pocionFuego.MoverAbajo(gameTime);
                if (pocionFuego.Y > 1361)
                {
                    pocionFuego.Activo = false;
                }
            }
            // -------------------------------- Movimiento Enemigos
            if (enemigo.Activo)
            {
                enemigo.Mover(gameTime);
                enemigo.MoverAbajo(gameTime);
            }

            if (disparoEnemigo.Activo)
            {
                disparoEnemigo.Mover(gameTime);

                if (disparoEnemigo.Y > 1361)
                {
                    disparoEnemigo.Activo = false;
                    tiempoHastaSiguienteDisparo = new System.Random().Next(2000);
                }
            }
            else
            {
                tiempoHastaSiguienteDisparo -= gameTime.ElapsedGameTime.Milliseconds;
                if (tiempoHastaSiguienteDisparo <= 0)
                {
                    sonidoDeDisparo.CreateInstance().Play();
                    disparoEnemigo.X = enemigo.X;
                    disparoEnemigo.Y = enemigo.Y + 17;
                    disparoEnemigo.Activo = true;
                }
            }

            //PRUEBAS 

            if (enemigo2.Activo)
            {
                enemigo2.Mover(gameTime);
                enemigo2.MoverAbajo(gameTime);
            }

            disparo.Mover(gameTime);
            //fondo.Mover(gameTime);//Mirar esto


            //if (disparoEnemigo.Activo)
            //{
            //    disparoEnemigo.Mover(gameTime);

            //    if (disparoEnemigo.Y > 1361)
            //    {
            //        disparoEnemigo.Activo = false;
            //        tiempoHastaSiguienteDisparo = new System.Random().Next(2000);
            //    }
            //}
            //else
            //{
            //    tiempoHastaSiguienteDisparo -= gameTime.ElapsedGameTime.Milliseconds;
            //    if (tiempoHastaSiguienteDisparo <= 0)
            //    {
            //        disparoEnemigo.X = enemigo2.X;
            //        disparoEnemigo.Y = enemigo2.Y + 17;
            //        disparoEnemigo.Activo = true;
            //    }
            //}



            if (enemigo3.Activo)
            {
                enemigo3.Mover2(gameTime, player1.X); //Esto
                enemigo3.MoverAbajo2(gameTime, player1.X);
              
            }

            fondo1.Mover(gameTime);//Mirar esto



            //-------------------------------------------PRUEBAS

        }

        //----------------------------------------------------- COMPROBAR COLISIONES

        protected void ComprobarColisiones()
        {
            if (!invencible)
            {
                if (pocionVelocidadY.ColisionaCon(player1))
                {
                    fondo1.VelocY = fondo1.VelocY + 100;
                    enemigo.VelocY += 100;
                    pocionVelocidadY.Activo = false;
                    pocionVelocidadX.Activo = false;
                    pocionFuego.Activo = false;
                    puntuacionPowerUps = 0;
                    pocionY.CreateInstance().Play();
                }

                if (pocionVelocidadX.ColisionaCon(player1))
                {
                    player1.VelocX += 100;                
                    pocionVelocidadX.Activo = false;
                    pocionVelocidadY.Activo = false;
                    pocionFuego.Activo = false;
                    puntuacionPowerUps = 0;
                    pocionX.CreateInstance().Play();
                }

                if (pocionFuego.ColisionaCon(player1))
                {
                    if (disparo.VelocY < 1000)
                    {
                        disparo.VelocY += 200;
                    }
                    pocionVelocidadX.Activo = false;
                    pocionVelocidadY.Activo = false;
                    pocionFuego.Activo = false; 
                    puntuacionPowerUps = 0;
                    pocionX.CreateInstance().Play();
                }

                if (disparoEnemigo.ColisionaCon(player1))
                {               
                    disparoEnemigo.Activo = false;
                    if (fondo1.VelocY>=200)
                    {
                        fondo1.VelocY -= 50;
                        hitPlayer.CreateInstance().Play();

                    }                

                    if (player1.X == 2 || player1.X == 600)
                    {
                        PlayerClear();
                    }

                    if (vidas <= 0)
                    {
                        Terminado = true;
                        vidas = 3;
                    }
                }

                if (player1.ColisionaCon(enemigo))
                {
                    disparoEnemigo.Activo = false;
                    EnemyClear(enemigo);
                    PlayerClear();
                    if (vidas <= 0)
                    {
                        Terminado = true;
                    }
                }

                if (player1.ColisionaCon(enemigo2))
                {
                    disparoEnemigo.Activo = false;        
                    EnemyClear(enemigo2);

                    PlayerClear();
                    if (vidas <= 0)
                    {
                        Terminado = true;
                    }
                }

                if (player1.ColisionaCon(enemigo3))
                {
                    disparoEnemigo.Activo = false;
                    EnemyClear(enemigo3);
                    PlayerClear();
                    if (vidas <= 0)
                    {
                        Terminado = true;
                    }
                }

                if (disparo.ColisionaCon(enemigo))
                {
                    enemigo.Activo = false;
                    disparo.Activo = false;
                    hitEnemy1.CreateInstance().Play();
                    puntuacion += 100;
                    puntuacionPowerUps++;
                    EnemyClear(enemigo); //Cosas mias
                }
                if (disparo.ColisionaCon(enemigo2) || disparo.ColisionaCon(enemigo3))
                {
                    disparo.Activo = false;
                    hitEnemy2.CreateInstance().Play();
                    puntuacion += 20;
                }            
            }
        }

        //------------------------------------------------------ COMPROBAR ENTRADA

        protected void ComprobarEntrada(GameTime gameTime, ContentManager Content)
        {
            var estadoTeclado = Keyboard.GetState();
            var estadoGamePad = GamePad.GetState(PlayerIndex.One);

            if (estadoTeclado.IsKeyDown(Keys.Left)
                || estadoGamePad.DPad.Left > 0
                || estadoGamePad.ThumbSticks.Left.X < 0)
            {
                if (player1.X > 0)
                {
                    player1.MoverIzquierda(gameTime);
                }
            }
            if (estadoTeclado.IsKeyDown(Keys.Right)
                || estadoGamePad.DPad.Left > 0
                || estadoGamePad.ThumbSticks.Left.X > 0)
            {
                if (player1.X < 623)
                {
                    player1.MoverDerecha(gameTime);
                }
            }
            if (!disparo.Activo && estadoTeclado.IsKeyDown(Keys.Space)
                || estadoGamePad.Buttons.A > 0)
            {
                disparo.X = player1.X + 10;
                disparo.Y = player1.Y + 17;
                disparo.Activo = true;
                sonidoDeDisparo.CreateInstance().Play();
                //-----------------------------------Pruebas

            }
        }


        //-----------------------------------Pruebas




        //------------------------------------------------------------- COSAS VARIAS


        protected void PlayerClear()
        {
            player1.X = 350;
            player1.Y = 920;
            vidas--;
            invencible = true;
        }
        private void EnemyClear(Enemigo enemigo)
        {
            enemigo.X = new System.Random().Next(600);
            enemigo.Y = 0;
            enemigo.VelocX = new System.Random().Next(-400,400);
            enemigo.VelocY = new System.Random().Next(100, 600);
            enemigo.Activo = true;
        }

        public int GetPuntuacion()
        {
            return puntuacion;
        }

        //public void PasarNivel(ContentManager Content)
        //{
        //    contadorNiveles++;
        //    switch (contadorNiveles)
        //    {
        //        case 1:
        //            fondo1 = new Fondo(Content, "Stage1 continua-min");
        //            break;
        //        case 2:
        //            fondo1 = new Fondo(Content, "Stage2 continua");
        //            break;
        //        default:
        //            fondo1 = new Fondo(Content, "Stage3 continua");
        //            break;
        //    }
        //    disparoEnemigo.Activo = false;            
        //    puntuacionPowerUps = 0;
        //    Pasado = false;            
        //    fondo1.CiclosPantalla = 0;
        //}
    }
}
