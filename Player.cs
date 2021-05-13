using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace Roar_Flight2
{
    class Player : Sprite
    {
        public int Vidas { get; set; }
        public Player(ContentManager content)
            : base(350, 920, new string[] { "player1_1", "player1_2", "player1_3" }, content)
        {
            VelocX = 320;
        }


        public void MoverIzquierda(GameTime gameTime)
        {
            base.Mover(gameTime);
            X -= VelocX *
                (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void MoverDerecha(GameTime gameTime)
        {
            base.Mover(gameTime);
            X += VelocX *
                (float)gameTime.ElapsedGameTime.TotalSeconds;            
        }

        public int PosicionJugador()
        {
            return Convert.ToInt32(X);
        }
        //public void PlayerHit()
        //{            
        //    for (int i = (int)X; i < new System.Random().Next((int)X + 50); i++)
        //    {
        //        X = X + i;
        //    }            
        //} 
        //TODO
    }
}
