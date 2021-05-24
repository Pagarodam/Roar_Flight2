using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roar_Flight2
{
    class PantallaCreditos
    {
        private SpriteFont fuente;
        private GestorDePantallas gestor;
        private Song musicaDeFondo;//Musica
        private int puntuacion;

        public PantallaCreditos(GestorDePantallas gestor)
        {
            this.gestor = gestor;
        }

        public void CargarContenidos(ContentManager Content, int puntuacion)
        {
            fuente = Content.Load<SpriteFont>("Arial1");
            this.puntuacion = puntuacion;
        }

        public void Actualizar(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gestor.ChangeMode(GestorDePantallas.MODO.BIENVENIDA);
            }
        }

        public void Dibujar(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(fuente, "GAME OVER", new Vector2(
                400, 100), Color.White);
            spriteBatch.DrawString(fuente, "MEJORES PUNTUACIONES", new Vector2(
                200, 150), Color.White);
            spriteBatch.DrawString(fuente, puntuacion.ToString() , new Vector2(
                200, 200), Color.White);
        }
    }
}

