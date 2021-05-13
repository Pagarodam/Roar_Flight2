using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roar_Flight2
{
    class Sprite
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float VelocX { get; set; }
        public float VelocY { get; set; }
        public bool Activo { get; set; }

        private Texture2D imagen;
        private int cantidadFotogramas;
        private Texture2D[] secuencia;
        int fotogramaActual;
        int tiempoCadaFotograma;
        int tiempoHastaSiguienteFotograma;
        bool haySecuencia;
       


        public Sprite(int x, int y, string nombreImagen, ContentManager Content)
        {
            X = x;
            Y = y;
            imagen = Content.Load<Texture2D>(nombreImagen);
            Activo = true;
            haySecuencia = false;
        }

        //public Sprite(int x, int y, ContentManager Content)
        //{
        //    X = x;
        //    Y = y;
        //    Activo = true;
        //    haySecuencia = false;
        //}


        public Sprite(int x, int y, string[] imagenes, ContentManager Content)
        {
            X = x;
            Y = y;
            Activo = true;
            cantidadFotogramas = imagenes.Length;
            secuencia = new Texture2D[cantidadFotogramas];
            for (int i = 0; i < cantidadFotogramas; i++)
            {
                secuencia[i] = Content.Load<Texture2D>(imagenes[i]);
            }
            imagen = secuencia[0];

            fotogramaActual = 0;
            tiempoCadaFotograma = 130;
            tiempoHastaSiguienteFotograma = tiempoCadaFotograma;
            haySecuencia = true;

        }
        public void SetVelocidad(float vx, float vy)
        {
            VelocX = vx;
            VelocY = vy;
        }
        public void Dibujar1(SpriteBatch spriteBatch)
        {
            if (Activo)
            {
                spriteBatch.Draw(imagen,
                    new Rectangle(
                        (int)X, (int)Y,
                        imagen.Width, imagen.Height),
                        Color.White);
            }
        }
        public void Dibujar2(SpriteBatch spriteBatch)
        {
            if (Activo)
            {
                spriteBatch.Draw(imagen,
                    new Rectangle(
                        (int)X, (int)Y,
                        imagen.Width, imagen.Height),
                        Color.Cyan);
            }
        }

        public void Dibujar3(SpriteBatch spriteBatch)
        {
            if (Activo)
            {
                spriteBatch.Draw(imagen,
                    new Rectangle(
                        (int)X, (int)Y,
                        imagen.Width, imagen.Height),
                        Color.Red);
            }
        }



        public bool ColisionaCon(Sprite otro)
        {
            if (!Activo) return false;
            if (!otro.Activo) return false;
            Rectangle r1 = new Rectangle(
                    (int)X, (int)Y,
                    imagen.Width - 30, imagen.Height-30);


            Rectangle r2 = new Rectangle(
                    (int)otro.X, (int)otro.Y,
                    otro.imagen.Width -30, otro.imagen.Height- 30);

            return r1.Intersects(r2);
        }

        public virtual void Mover(GameTime gameTime)
        {
            if (haySecuencia)
            {
                tiempoHastaSiguienteFotograma -= gameTime.ElapsedGameTime.Milliseconds;
                if (tiempoHastaSiguienteFotograma <= 0 )
                {
                    fotogramaActual++;
                    if (fotogramaActual >= cantidadFotogramas)
                    {
                        fotogramaActual = 0;
                    }
                    tiempoHastaSiguienteFotograma = tiempoCadaFotograma;
                    imagen = secuencia[fotogramaActual];
                }
            }
        }
    }
}
