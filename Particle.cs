using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Particle
{
    class Particle
    {
        public static List<Particle> world = new List<Particle>();

        public static Game game;
        public static SpriteBatch spriteBatch;

        public float x;
        public float y;
        public float vx = 0;
        public float vy = 0;
        Color color;
        Texture2D texture;

        public Particle(float x, float y, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color;

            texture = game.Content.Load<Texture2D>("pixel");

            world.Add(this);
        }

        public void Draw()
        {
            spriteBatch.Draw(texture, new Vector2(x,y), color);
        }
    }
}
