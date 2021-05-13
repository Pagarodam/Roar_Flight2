using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Roar_Flight2
{
    class Disparo : Sprite
    {
        public Disparo(ContentManager content)
            : base(350, 100, "fuego", content)
        {
            SetVelocidad(400, 400);
            Activo = false;
        }

        public override void Mover(GameTime gameTime)
        {
            if (Activo)
            {
                Y -= VelocY *
                    (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Y < 0)
                {
                    Activo = false;
                }
            }
        }
    }
}
