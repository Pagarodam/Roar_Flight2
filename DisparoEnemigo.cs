using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Roar_Flight2
{
    class DisparoEnemigo :Sprite
    {
        public DisparoEnemigo(ContentManager content)
             : base(350, 100, "fuegoEnemigo", content)
        {
            SetVelocidad(600, 600);
            Activo = false;
        }

        public override void Mover(GameTime gameTime)
        {
            Y += VelocY *
                (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
