using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Roar_Flight2
{
    class Fondo : Sprite
    {
        public int CiclosPantalla { get; set; }


        public Fondo(ContentManager content, string nombre)
            : base(0, -1029, nombre, content)
        {
            VelocY = 500;

        }

        public Fondo(ContentManager content, string nombre, int X, int Y)
            : base(X, Y, nombre, content)
        {
            VelocX = 200;

        }


        public override void Mover(GameTime gameTime)
        {
            if (Activo)
            {
                Y += VelocY *
                    (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Y >= -8) //-8
                {
                    Y = -1029;
                    CiclosPantalla++;
                }
            }
        }

        public void Mover2(GameTime gameTime)
        {
            if (Activo)
            {
                X += VelocX *
                    (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (X >= 0) 
                {
                    X = -1435;
                }
            }
        }
    }
}