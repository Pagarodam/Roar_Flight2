using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Roar_Flight2
{
    class PantallaIntroduccion
    {
        //private SpriteBatch SpriteBatch;
        private Fondo fondoIntroduccion;
        private Sprite dragon;
        private SpriteFont fuente;
        private GestorDePantallas gestor;
        private Song musicaDeFondo;//Musica

        public PantallaIntroduccion(GestorDePantallas gestor)
        {
            this.gestor = gestor;
        }

        public void CargarContenidos(ContentManager Content)
        {
            fuente = Content.Load<SpriteFont>("Arial1");
            fondoIntroduccion = new Fondo(Content, "Fondo Introduccion-mini-min",-720 ,0);
            dragon = new Sprite(200, 200, "Dragon Bienvenida", Content);
            //CargarMusica(Content);

        }

        //public void CargarMusica(ContentManager Content)
        //{
        //    MediaPlayer.Stop();
        //    musicaDeFondo = Content.Load<Song>("PantallaBienvenida");
        //    MediaPlayer.Play(musicaDeFondo);
        //    MediaPlayer.Volume = 0.5f;
        //}
        public void Actualizar(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                gestor.ChangeMode(GestorDePantallas.MODO.BIENVENIDA);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gestor.Terminar();
            }
            MoverElementos(gameTime);
        }

        public void Dibujar(GameTime gameTime, SpriteBatch spriteBatch)
        {
            fondoIntroduccion.RedimensionayDibuja(spriteBatch);
            dragon.Dibujar(spriteBatch, Color.White);
            spriteBatch.DrawString(fuente, "Pulsa enter para continuar", new Vector2(
                200, 100), Color.DarkOrange);
            spriteBatch.DrawString(fuente, "En una tierra lejana vivia un principe de nombre", new Vector2(
                50, 850), Color.Black);
            spriteBatch.DrawString(fuente, "ya olvidado. Un dia decidio plantar cara a su padre", new Vector2(
                50, 880), Color.Black);
            spriteBatch.DrawString(fuente, "que luchaba contra los dragones, para aliarse", new Vector2(
                50, 910), Color.Black);
            spriteBatch.DrawString(fuente, "con uno de ellos. Y esta es su historia.", new Vector2(
                50, 940), Color.Black);


        }

        public void MoverElementos(GameTime gameTime)
        {
            fondoIntroduccion.Mover2(gameTime);
        }
    }
}
