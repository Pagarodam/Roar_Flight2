using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace Roar_Flight2
{
    public class PantallaDeJuego2
    {

        private bool invencible;
        private Player player1;
        private Disparo disparo;

        private PowerUp pocionVelocidadY;
        private PowerUp pocionVelocidadX;

        private Enemigo enemigo;
        private Enemigo enemigo2;
        private Enemigo enemigo3;

        private DisparoEnemigo disparoEnemigo;
        private int tiempoHastaSiguienteDisparo;

        //private Enemigo[] enemigos;
        private Fondo2 fondo;

        private SoundEffect sonidoDeDisparo;
        private SoundEffect hitPlayer;
        private SoundEffect hitEnemy1;
        private SoundEffect hitEnemy2;
        private SoundEffect pocionY;
        private SoundEffect pocionX;

        private SpriteFont fuente;
        //private Song musicaDeFondo;

        private int vidas;
        private int puntuacion;
        private int puntuacionPowerUps;


        public bool Terminado { get; set; }

        public PantallaDeJuego2()
        {
            vidas = 3;
            puntuacion = 0;
            puntuacionPowerUps = 0;
            tiempoHastaSiguienteDisparo = new System.Random().Next(2000);

            Terminado = false;
        }

        //------------------------------------------------------------- CARGAR CONTENIDOS

        public void CargarContenidos(ContentManager Content)
        {
            //MediaPlayer.Volume = 0.5f;
            //musicaDeFondo = Content.Load<Song>("Moonlight3");
            //MediaPlayer.Play(musicaDeFondo);
            //MediaPlayer.Stop();
            //MediaPlayer.IsRepeating = true;

            fuente = Content.Load<SpriteFont>("Arial1");

            player1 = new Player(Content);
            disparo = new Disparo(Content);
            invencible = false;
            fondo = new Fondo2(Content);

            pocionVelocidadY = new PowerUp(Content);
            pocionVelocidadX = new PowerUp(Content);
            pocionVelocidadY.Activo = false;
            pocionVelocidadX.Activo = false;


            enemigo = new Enemigo(Content);
            enemigo2 = new Enemigo(Content);
            enemigo3 = new Enemigo(Content);

            //enemigos = new Enemigo[3] { enemigo, enemigo2, enemigo3 };
            disparoEnemigo = new DisparoEnemigo(Content);

            sonidoDeDisparo = Content.Load<SoundEffect>("Fire");
            hitPlayer = Content.Load<SoundEffect>("Hit");
            hitEnemy1 = Content.Load<SoundEffect>("Enemigo1");
            hitEnemy2 = Content.Load<SoundEffect>("Enemigo2");
            pocionY = Content.Load<SoundEffect>("pocionVelocidadFondo");
            pocionX = Content.Load<SoundEffect>("pocionVelocidadX");
        }

        //------------------------------------------------------------- ACTUALIZAR

        public void Actualizar(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Terminado = true;

            MoverElementos(gameTime);
            ComprobarColisiones();
            ComprobarEntrada(gameTime);

            if (fondo.CiclosPantalla == 50)
            {
                fondo.CiclosPantalla = 0;
                Terminado = true;
            }


            //CargarMusica(musicaDeFondo);

            //posicionEnemigo.Y += 10; //TODO

            //posicionEnemigo.Y += 14;
            //if (posicionEnemigo.X > posicionPlayer1.X)
            //{
            //    posicionEnemigo.X--;
            //}
            //if (posicionEnemigo.X < posicionPlayer1.X)
            //{
            //    posicionEnemigo.X++;
            //}
        }

        //------------------------------------------------------------- DIBUJAR

        public void Dibujar(GameTime gameTime, SpriteBatch spriteBatch)
        {
            fondo.Dibujar1(spriteBatch);

            spriteBatch.DrawString(fuente, "Puntuacion :" + puntuacion,
               new Vector2(450, 0),
               Color.Red);

            spriteBatch.DrawString(fuente, "Vidas :" + vidas,
                new Vector2(0, 0),
                Color.Red);

            spriteBatch.DrawString(fuente, "Ciclos :" + fondo.CiclosPantalla,
                new Vector2(0, 200),
                Color.Red);

            pocionVelocidadY.Dibujar1(spriteBatch);
            pocionVelocidadX.Dibujar2(spriteBatch);

            //foreach (Enemigo enemigo in enemigos)
            //{

            //    enemigo.Dibujar1(spriteBatch);
            //    enemigo2.Dibujar2(spriteBatch);

            //}

            player1.Dibujar1(spriteBatch);
            enemigo.Dibujar1(spriteBatch);
            enemigo2.Dibujar2(spriteBatch);
            enemigo3.Dibujar3(spriteBatch);
            disparo.Dibujar1(spriteBatch);
            disparoEnemigo.Dibujar2(spriteBatch);

        }

        //------------------------------------------------------------- MOVER ELEMENTOS


        protected void MoverElementos(GameTime gameTime)
        {
            if (puntuacionPowerUps >= 3)
            {
                int numeroPocion = new System.Random().Next(2);
                if (numeroPocion == 1)
                {
                    pocionVelocidadY.Activo = true;
                    if (pocionVelocidadY.Activo)
                    {
                        pocionVelocidadY.Mover(gameTime);
                        pocionVelocidadY.MoverAbajo(gameTime);
                        if (pocionVelocidadY.Y > 1361)
                        {
                            puntuacionPowerUps = 0;
                            pocionVelocidadY.Activo = false;
                        }
                    }
                }
                else if (numeroPocion == 0)
                {
                    pocionVelocidadX.Activo = true;
                    if (pocionVelocidadX.Activo)
                    {
                        pocionVelocidadX.Mover(gameTime);
                        pocionVelocidadX.MoverAbajo(gameTime);
                        if (pocionVelocidadX.Y > 1361)
                        {
                            puntuacionPowerUps = 0;
                            pocionVelocidadX.Activo = false;
                        }

                    }
                }
            }

            if (enemigo.Activo)
            {
                enemigo.Mover(gameTime);
                enemigo.MoverAbajo(gameTime);
            }

            disparo.Mover(gameTime);
            fondo.Mover(gameTime);


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
            fondo.Mover(gameTime);


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
                    disparoEnemigo.X = enemigo2.X;
                    disparoEnemigo.Y = enemigo2.Y + 17;
                    disparoEnemigo.Activo = true;
                }
            }
        }

        //----------------------------------------------------- COMPROBAR COLISIONES

        protected void ComprobarColisiones()
        {
            if (pocionVelocidadY.ColisionaCon(player1))
            {
                fondo.VelocY = fondo.VelocY + 100;
                enemigo.VelocY += 100;
                pocionVelocidadY.Activo = false;
                pocionVelocidadX.Activo = false;
                puntuacionPowerUps = 0;
                pocionY.CreateInstance().Play();
            }

            if (pocionVelocidadX.ColisionaCon(player1))
            {
                player1.VelocX += 25;
                pocionVelocidadX.Activo = false;
                pocionVelocidadY.Activo = false;
                puntuacionPowerUps = 0;
                pocionX.CreateInstance().Play();
            }


            if (disparoEnemigo.ColisionaCon(player1) && !invencible)
            {

                disparoEnemigo.Activo = false;
                if (fondo.VelocY >= 100)
                {
                    fondo.VelocY -= 50;
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
                EnemyClear(enemigo);
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
            if (disparo.ColisionaCon(enemigo2))
            {

                disparo.Activo = false;
                hitEnemy2.CreateInstance().Play();
                puntuacion += 20;
            }
        }

        //------------------------------------------------------ COMPROBAR ENTRADA

        protected void ComprobarEntrada(GameTime gameTime)
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
            }
        }

        //------------------------------------------------------------- COSAS VARIAS

        //protected void PlayerHit(GameTime gameTime)
        //{
        //for (int i = (int) player1.X; i< new System.Random().Next((int) player1.X +50); i++)
        //{
        //    player1.MoverDerecha(gameTime);
        //}
        //}
        protected void PlayerClear()
        {
            player1.X = 350;
            player1.Y = 920;
            vidas--;
            invencible = true;

            invencible = false;
        }
        private void EnemyClear(Enemigo enemigo)
        {
            enemigo.X = new System.Random().Next(600);
            enemigo.Y = 0;
            enemigo.VelocX = new System.Random().Next(-400, 400);
            enemigo.VelocY = new System.Random().Next(100, 600);
            enemigo.Activo = true;
        }

        public int GetPuntuacion()
        {
            return puntuacion;
        }

        public void Reset()
        {
            PlayerClear();
            EnemyClear(enemigo);
            EnemyClear(enemigo2);
            puntuacion = 0;

            disparoEnemigo.Activo = false;
            puntuacionPowerUps = 0;
            vidas = 3;
            fondo.CiclosPantalla = 0;
        }
    }
}
