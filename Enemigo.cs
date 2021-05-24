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
            SetVelocidad(60,240 );
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

        public void Mover2(GameTime gameTime , float XJugador)
        {
            base.Mover(gameTime);
            X += VelocX *
               (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (X > XJugador && VelocX > 0)
            {
                VelocX = VelocX * -1;
            }
            if (X < XJugador && VelocX < 0)
            {
                VelocX = VelocX * -1;
            }
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
                VelocX = new System.Random().Next(-120, 120);
                VelocY = new System.Random().Next(900, 1000);
                Activo = true;                
            }
        }
    }
}
