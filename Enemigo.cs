using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace Roar_Flight2
{
    class Enemigo : Sprite
    {
        public Enemigo(ContentManager content)
            : base(new System.Random().Next(600), new System.Random().Next(-200,0), new string[] { "enemigo1_1", "enemigo1_2", "enemigo1_3" }, content)
        {
            SetVelocidad(120,240);
        }

        public override void Mover(GameTime gameTime)
        {
            base.Mover(gameTime);
            X += VelocX *
                (float)gameTime.ElapsedGameTime.TotalSeconds;

            if ((X > 600)
                || (X < 2))
                VelocX = -1*VelocX;
        }
        public void MoverAbajo(GameTime gameTime)
        {
            Y += VelocY *
                (float)gameTime.ElapsedGameTime.TotalSeconds;
            if ((Y > 1040))
            {
                X = new System.Random().Next(600);
                Y = 0;
                VelocX = new System.Random().Next(-400, 400);
                VelocY = new System.Random().Next(100, 500);
                Activo = true;
            }
        }

        public void MoverAbajo2(GameTime gameTime, float XJugador)
        {
            Y += VelocY *
                (float)gameTime.ElapsedGameTime.TotalSeconds;
            if ((Y > 1040))
            {
                X = new System.Random().Next(600);
                Y = 0;
                VelocX = new System.Random().Next(-400, 400);
                VelocY = new System.Random().Next(300, 700);
                Activo = true;

                //if (VelocX > XJugador)
                //{
                //    X--;
                //}
                //if (VelocX < XJugador)
                //{
                //    X++;
                //}
            }
        }
    }
}
