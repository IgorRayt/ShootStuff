using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace IRJKFinalProject
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class PlayerShip : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;

        public Texture2D Tex
        {
            get { return tex; }
            set { tex = value; }
        }
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 oldSpeed;
        private Vector2 speed;
        
        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private float scaleFactor = 1.0f;
        private Vector2 stage;
        private Vector2 origin;
        private Vector2 oldDirection = new Vector2(0, 0);
        private Vector2 newDirection;
        private Rectangle srcRect;
        private float rotationFactor = 0f;

        public float RotationFactor
        {
            get { return rotationFactor; }
            set { rotationFactor = value; }
        }
        private float rotationChange = 0.11f;
        double x = 1;
        private float acceleration = 0.5f;
        private float accelerationIncrease = 0.1f;
        private float mass = 1f;
        private float weight = 0f;
        private float thrust = 2f;
        int frames = 0;
        float elapsed;
        float delay = 50f;
        //private float inertiaIncrease = 0.02f;

        public PlayerShip(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 speed,
            Vector2 stage)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            this.stage = stage;
            srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(tex.Width / 20, tex.Height / 2);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            //MouseState ms = Mouse.GetState();
            KeyboardState ks = Keyboard.GetState();
            //Vector2 target = new Vector2(ms.X, ms.Y);
            //float xDiff = target.X - position.X;
            //float yDiff = target.Y - position.Y;
            //rotationFactor = (float)Math.Atan2(yDiff, xDiff);
            if (ks.IsKeyDown(Keys.Up))
            {
                vec();
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                rotationFactor += rotationChange;
                if (rotationFactor > Math.PI * 2)
                {
                    rotationFactor = 0f;
                }
            }
            if (ks.IsKeyDown(Keys.Left))
            {
                rotationFactor -= rotationChange;
                if (rotationFactor > Math.PI *2)
                {
                    rotationFactor = 0f;
                }
            }
            //rotationFactor =(float)Math.PI;
            if (ks.IsKeyUp(Keys.Up))
            {
                speed = new Vector2(speed.X - speed.X/80, speed.Y - speed.Y/80);
                oldDirection = newDirection;
                oldSpeed = speed;
                //acceleration = 0;

            }
            position += speed;
            if (position.X < tex.Width / 20)
            {
                position.X = tex.Width/20;
            }
            if (position.X > stage.X - tex.Width/20)
            {
                position.X = stage.X - tex.Width/20;
            }
            if (position.Y < tex.Height / 2)
            {
                position.Y = tex.Height/2;
            }
            if (position.Y > stage.Y - tex.Height/2)
            {
                position.Y = stage.X - tex.Height/2;
            }
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (frames >= 9)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
            }
            srcRect = new Rectangle(50 * frames, 0, 50, 50);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRect, Color.White,
                rotationFactor, origin, scaleFactor, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void vec()
        {
            newDirection = new Vector2((float)Math.Cos(rotationFactor), (float)Math.Sin(rotationFactor));
            acceleration += accelerationIncrease;
            if (acceleration > 8)
            {
                acceleration = 6;
            }
            speed = new Vector2(newDirection.X * acceleration, newDirection.Y * acceleration);
            //acceleration += accelerationIncrease;
            //if (acceleration > 6)
            //{
            //    acceleration = 6;
            //}

            //if (Math.Abs(oldSpeed.X) - acceleration > 0)
            //{
            //    if (oldSpeed.X > 0)
            //    {
            //        oldSpeed.X -= acceleration;
            //    }
            //    if (oldSpeed.X < 0)
            //    {
            //        oldSpeed.X += acceleration;
            //    }
            //}
            //else
            //{
            //    oldSpeed.X = 0;
            //}
            //if (Math.Abs(oldSpeed.Y) - acceleration > 0)
            //{
            //    if (oldSpeed.Y > 0)
            //    {
            //        oldSpeed.Y -= acceleration;
            //    }
            //    if (oldSpeed.Y < 0)
            //    {
            //        oldSpeed.Y += acceleration;
            //    }
            //}
            //else
            //{
            //    oldSpeed.Y = 0;
            //}
            //speed = new Vector2(newDirection.X * acceleration + oldSpeed.X, newDirection.Y * acceleration + oldSpeed.Y);
        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X + 1/4*tex.Width/10, (int)position.Y+1/4*tex.Height, tex.Width/20, tex.Height/2);
        }
    }
}
