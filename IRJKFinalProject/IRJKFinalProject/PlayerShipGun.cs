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
    public class PlayerShipGun : Microsoft.Xna.Framework.DrawableGameComponent
    {
        GameTime GT;
        Game game;
        PlayerShip playerShip;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        bool pause = false;
        List<Vector2> oldSpeed = new List<Vector2>();

        public bool Pause
        {
            get { return pause; }
            set { pause = value; }
        }
        bool pauseUsed = false;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private float scaleFactor = 1.0f;
        private Vector2 stage;
        private float rotationFactor = 0f;
        int missleCount = 0;
        private List<Missle> missle = new List<Missle>();

        public List<Missle> Missle
        {
            get { return missle; }
            set { missle = value; }
        }
        ContentManager cM;
        float timeSinceLastShot = 0f;
        float shotsFrequency = 80f;
        int counter = 1;
        int limit = 10;

        public float RotationFactor
        {
            get { return rotationFactor; }
            set { rotationFactor = value; }
        }
        private Rectangle srcRect;
        private Vector2 origin;
        ActionScene actionScene;
        int frames = 0;
        float elapsed;
        float delay = 50f;
        public PlayerShipGun(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 stage,
            PlayerShip playerShip,
            ActionScene actionScene)
            : base(game)
        {
            // TODO: Construct any child components here
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.stage = stage;
            srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(5 ,4);
            this.playerShip = playerShip;
            this.actionScene = actionScene;
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
            if (pause == true)
            {
                foreach (Missle m in missle)
                {
                    m.Speed = new Vector2(0, 0);
                    m.Visible = false;
                }
                pauseUsed = true;
            }
            if (pauseUsed == true && actionScene.Visible)
            {
                pauseUsed = false;
                int count = 0;
                pause = false;
                foreach (Missle e in missle)
                {
                    e.Speed = oldSpeed[count];
                    e.Enabled = true;
                    e.Visible = true;

                    count++;
                }
            }
            // TODO: Add your update code here
            if (pause == false)
            {
                MouseState ms = Mouse.GetState();
                Vector2 target = new Vector2(ms.X, ms.Y);
                float xDiff = target.X - position.X;
                float yDiff = target.Y - position.Y;
                position.X = playerShip.Position.X - 5;
                position.Y = playerShip.Position.Y - 1;
                rotationFactor = (float)Math.Atan2(yDiff, xDiff);
                base.Update(gameTime);
                timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timeSinceLastShot > shotsFrequency)
                {
                    MissileShoot();
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
                srcRect = new Rectangle(10 * frames, 0, 8, 8);
            }
           
            //if (ms.LeftButton == ButtonState.Pressed)
            //{
            //    //Vector2 missleSpeed = Vector2.Zero;
            //    //Vector2 misslePos = new Vector2(tex.Width / 2, tex.Height / 2);
            //    //missle = new List<Missle>();
            //    MissileShoot();

            //}
            //currentTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //if (currentTime >= timer)
            //{
            //    counter++;
            //    currentTime -= timer;
            //}
            //if (counter >= limit)
            //{
            //    counter = 0;
            //    MissileShoot();
            //}
            
        }
        public void MissileShoot()
        {
            timeSinceLastShot = 0f;
            bool createNew = true;
            foreach (Missle m in missle)
            {
                if (m.Visible == false)
                {
                    createNew = false;
                    
                    m.Shoot(position, new Vector2((float)Math.Cos(rotationFactor), (float)Math.Sin(rotationFactor)), rotationFactor);
                    oldSpeed.Add(m.Speed);
                    break;
                }
            }
            if (createNew == true)
            {
                Missle m = new Missle(game, spriteBatch, position, rotationFactor, this);
                m.Shoot(position, new Vector2((float)Math.Cos(rotationFactor), (float)Math.Sin(rotationFactor)), rotationFactor);
                oldSpeed.Add(m.Speed);
                missle.Add(m);
                game.Components.Add(m);
                m.Draw(GT);
            }
            //missleCount++;
            //Missle m = new Missle(game1, position, this);
            //m.LoadContent(cM);
            //foreach (Missle m in missle)
            //{
            //    m.Shoot(position, new Vector2((float)Math.Cos(rotationFactor), (float)Math.Sin(rotationFactor)));
            //}
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRect, Color.White, rotationFactor, origin, scaleFactor, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
