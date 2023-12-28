using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Particle
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Random random;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferHeight = 500;
            _graphics.PreferredBackBufferWidth = 500;
        }

        protected override void Initialize()
        {
            random = new Random();

            base.Initialize();
        }

        Particle[] red;
        Particle[] yellow;
        Particle[] green;

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Particle.game = this;
            Particle.spriteBatch = _spriteBatch;

            red = CreateParticles(200, Color.Red);
            yellow = CreateParticles(200, Color.Yellow);
            green = CreateParticles(200, Color.Lime);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Rule(green, green, -0.5f);
            Rule(green, red, 1f);
            Rule(green, yellow, -0.5f);

            Rule(red, red, 1f);
            Rule(red, green, -0.5f);
            Rule(red, yellow, -0.5f);

            Rule(yellow, yellow, -0.5f);
            Rule(yellow, green, -0.5f);
            Rule(yellow, red, 1f);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            for (int i = 0; i < Particle.world.Count; i++)
                Particle.world[i].Draw();

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        Particle[] CreateParticles(int number, Color color)
        {
            Particle[] particles = new Particle[number];
            for(int i = 0; i < number; i++)
            {
                particles[i] = new Particle(random.Next(0, 500), random.Next(0, 480), color);
            }

            return particles;
        }

        void Rule(Particle[] particles1, Particle[] particles2, float g)
        {
            for (int i = 0; i < particles1.Length; i++)
            {
                var fx = 0f;
                var fy = 0f;
                Particle a = particles1[i];
                for (int j = 0; j < particles2.Length; j++)
                {
                    Particle b = particles2[j];
                    var dx = a.x - b.x;
                    var dy = a.y - b.y;
                    var d = MathF.Sqrt(dx*dx + dy*dy);
                    if(d > 0 && d < 80)
                    {
                        var F = g * 1 / d;
                        fx += F * dx;
                        fy += F * dy;
                    }
                }

                a.vx = (a.vx + fx) * 0.5f;
                a.vy = (a.vy + fy) * 0.5f;
                a.x += a.vx;
                a.y += a.vy;

                if (a.x <= 0 || a.x >= 500)
                    a.vx = -a.vx;

                if (a.y <= 0 || a.y >= 500)
                    a.vy = -a.vy;
            }

        }
    }
}
