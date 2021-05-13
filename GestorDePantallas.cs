using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Roar_Flight2
{
    public class GestorDePantallas : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private PantallaDeBienvenida bienvenida;
        private PantallaDeJuego juego;


        //private PantallaDeJuego2 juego1;
        private PantallaCreditos creditos;
        //private Fondo fondo;
        //private int ciclosPantalla;

        public enum MODO { BIENVENIDA, JUEGO, JUEGO1, FINPARTIDA};
        public MODO modoActual { get; set; }

        public GestorDePantallas()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 720;
            graphics.PreferredBackBufferHeight = 1020;

            graphics.ApplyChanges();

            bienvenida = new PantallaDeBienvenida(this);
            juego = new PantallaDeJuego();
            //juego1 = new PantallaDeJuego2();
            creditos = new PantallaCreditos(this);
            //fondo = new Fondo(Content);


            IsMouseVisible = false;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bienvenida.CargarContenidos(Content);
            //juego1.CargarContenidos(Content);
            juego.CargarContenidos(Content);
            creditos.CargarContenidos(Content,juego.GetPuntuacion());
        }

        protected override void Update(GameTime gameTime)
        {
            switch (modoActual)
            {
                case MODO.JUEGO: 
                    juego.Actualizar(gameTime);                   
                    break;
                case MODO.JUEGO1:
                    juego.Actualizar(gameTime);
                    break;
                case MODO.BIENVENIDA : 
                    bienvenida.Actualizar(gameTime);
                    break;
                case MODO.FINPARTIDA : 
                    creditos.Actualizar(gameTime);                                    
                    break;
            }


            if (juego.Terminado == true)
            {

                modoActual = MODO.FINPARTIDA;
                juego.Terminado = false;
                creditos.CargarContenidos(Content, juego.GetPuntuacion());
                juego.Reset(Content);
            }

            if (juego.Pasado == true)
            {
                //juego.Terminado = false;
                //juego1.Reset();
                //modoActual = MODO.JUEGO1;

                //juego.CargarContenidos(Content);
                juego.Reset(Content);
            }

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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (modoActual)
            {
                case MODO.JUEGO :
                    juego.Dibujar(gameTime, spriteBatch);
                    break;
                case MODO.JUEGO1:
                    juego.Dibujar(gameTime, spriteBatch);
                    break;

                case MODO.BIENVENIDA: 
                    bienvenida.Dibujar(gameTime, spriteBatch);
                    break;

                case MODO.FINPARTIDA :
                    creditos.Dibujar(gameTime, spriteBatch);
                    //juego.Reset();
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //public virtual void CargarMusica(ContentManager Content)
        //{
        //    MediaPlayer.Stop();
        //}

        public void Terminar()
        {
            Exit();
        }
    }
}

