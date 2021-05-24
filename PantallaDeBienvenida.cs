using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Roar_Flight2
{
    class PantallaDeBienvenida
    {
        private SpriteFont fuente;
        private GestorDePantallas gestor;        

        public PantallaDeBienvenida(GestorDePantallas gestor)
        {
            this.gestor = gestor;
        }

        public void CargarContenidos(ContentManager Content)
        {
            fuente = Content.Load<SpriteFont>("Arial1");           
        }

        public void Actualizar(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                gestor.ChangeMode(GestorDePantallas.MODO.JUEGO);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                gestor.ChangeMode(GestorDePantallas.MODO.INTRODUCCION);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                gestor.Terminar(); 
            }

        }

        public void Dibujar(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(fuente, "1. Jugar", new Vector2(
                400, 100), Color.White);
            spriteBatch.DrawString(fuente, "2. Puntuaciones", new Vector2(
                400, 150), Color.White);
            spriteBatch.DrawString(fuente, "3. Volver a flipar", new Vector2(
                400, 200), Color.White);
            spriteBatch.DrawString(fuente, "S. Salir", new Vector2(
                400, 250), Color.White);
        }
    }
}
