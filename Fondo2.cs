using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Roar_Flight2
{
    class Fondo2 : Sprite
    {
        public int CiclosPantalla { get; set; }


        public Fondo2(ContentManager content)
            : base(0, -1029, "Stage2 continua", content)
        {
            VelocY = 300;

        }


        public override void Mover(GameTime gameTime)
        {
            if (Activo)
            {
                Y += VelocY *
                    (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Y >= 0)
                {
                    Y = -1029;
                    CiclosPantalla++;
                }
            }
        }
    }
}