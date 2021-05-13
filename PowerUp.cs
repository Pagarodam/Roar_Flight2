﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Roar_Flight2
{
    class PowerUp : Sprite
    {

        public PowerUp(ContentManager content)
            : base(350, -20, new string[] { "PocionVelocidad", "PocionMovimiento", "PocionFuego"}, content)
        {
            SetVelocidad(120, 240);
        }

        public override void Mover(GameTime gameTime)
        {
            base.Mover(gameTime);
            X += VelocX *
                (float)gameTime.ElapsedGameTime.TotalSeconds;

            if ((X > 600)
                || (X < 2))
                VelocX = -1 * VelocX;
        }
        public void MoverAbajo(GameTime gameTime)
        {
            Y += VelocY *
                (float)gameTime.ElapsedGameTime.TotalSeconds;
            if ((Y > 1040))
            {
                X = new System.Random().Next(600);
                Y = 100;
                VelocX = new System.Random().Next(-400, 400);
                VelocY = new System.Random().Next(300, 600);
                Activo = true;
            }
        }
    }
}
