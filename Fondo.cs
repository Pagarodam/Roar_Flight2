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
            VelocY = 300;

        }

        public override void Mover(GameTime gameTime)
        {
            if (Activo)
            {
                Y += VelocY *
                    (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Y >= -8)
                {
                    Y = -1029;
                    CiclosPantalla++;
                }
            }
        }
    }
}