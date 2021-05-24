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

        private PantallaIntroduccion introduccion;
        private PantallaDeBienvenida bienvenida;
        private PantallaDeJuego juego;
        private PantallaCreditos creditos;
        private MODO modoActual;

        public enum MODO { INTRODUCCION, BIENVENIDA, JUEGO, CREDITOS};

        public GestorDePantallas()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 720;
            graphics.PreferredBackBufferHeight = 1020;

            graphics.ApplyChanges();

            introduccion = new PantallaIntroduccion(this);
            bienvenida = new PantallaDeBienvenida(this);
            juego = new PantallaDeJuego(this);
            creditos = new PantallaCreditos(this);

            IsMouseVisible = false;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            switch (modoActual)
            {
                case MODO.INTRODUCCION:
                    introduccion.CargarContenidos(Content);
                    break;
                case MODO.BIENVENIDA:
                    bienvenida.CargarContenidos(Content);
                    break;
                case MODO.JUEGO:
                    juego.CargarContenidos(Content);
                    break;
                case MODO.CREDITOS:
                    creditos.CargarContenidos(Content, juego.GetPuntuacion());
                    break;
                default:
                    break;
            }
        }

        public void ChangeMode(MODO modo)
        {
            modoActual = modo;
            LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            switch (modoActual)
            {
                case MODO.INTRODUCCION:
                    introduccion.Actualizar(gameTime);
                    break;
                case MODO.BIENVENIDA:
                    bienvenida.Actualizar(gameTime);
                    break;
                case MODO.JUEGO: 
                    juego.Actualizar(gameTime,Content);                   
                    break;            
                case MODO.CREDITOS : 
                    creditos.Actualizar(gameTime);                                    
                    break;
            }


            if (juego.Terminado == true)
            {

                ChangeMode(MODO.CREDITOS);
                juego.Terminado = false;
                creditos.CargarContenidos(Content, juego.GetPuntuacion());
            }

            if (juego.Pasado == true)
            {
                juego.Pasado = false;
                //juego1.Reset();
                ChangeMode(MODO.CREDITOS);

                //juego.PasarNivel(Content);

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
                case MODO.INTRODUCCION:
                    introduccion.Dibujar(gameTime, spriteBatch);
                    break;
                case MODO.BIENVENIDA:
                    bienvenida.Dibujar(gameTime, spriteBatch);
                    break;
                case MODO.JUEGO :
                    juego.Dibujar(gameTime, spriteBatch);
                    break;
                case MODO.CREDITOS :
                    creditos.Dibujar(gameTime, spriteBatch);
                    //juego.Reset();
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Terminar()
        {
            Exit();
        }
    }
}

