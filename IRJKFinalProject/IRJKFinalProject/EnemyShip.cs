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
using System.Threading;


namespace IRJKFinalProject
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class EnemyShip : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 pos;

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        private Vector2 speed;

        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private Vector2 stage;
        private Rectangle scrRect;
        private float rotationFactor;

        public float RotationFactor
        {
            get { return rotationFactor; }
            set { rotationFactor = value; }
        }
        private Vector2 origin;
        SpriteEffects spriteEffects;
        private List<Missle> missle = new List<Missle>();
        PlayerShip playerShip;
        ScoreInfo score;
        bool first;
        CollitionManagerWithEnemy cme;
        Game game;
        int num;
        string name;
        Lives lives;
        public EnemyShip(Game game,
            SpriteBatch spriteBatch,
            Vector2 pos,
            Vector2 speed,
            Vector2 stage,
            float rotationFactor,
            List<Missle> missle,
            PlayerShip playerShip,
            ScoreInfo score,
            bool first,
            string name,
            Lives lives)
            : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.pos = pos;
            this.speed = speed;
            this.rotationFactor = rotationFactor;
            this.stage = stage;
            this.tex = game.Content.Load<Texture2D>("Images/enemy");
            this.missle = missle;
            this.playerShip = playerShip;
            this.score = score;
            this.first = first;
            this.name = name;
            this.lives = lives;
            scrRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            cme = new CollitionManagerWithEnemy(game, this, missle, playerShip, score, first, lives);
            game.Components.Add(cme);
            // TODO: Construct any child components here
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
            cme = new CollitionManagerWithEnemy(game, this, missle, playerShip, score, first, lives);
            pos += speed;
            if (pos.X < -100 || pos.X > 850 || pos.Y < -100 || pos.Y > 850)
            {
                Visible = false;
            }
            if (rotationFactor == 0f)
            {
                if (pos.X > 750)
                {
                    speed.X = speed.X * -1;
                    rotationFactor = (float)Math.PI;
                }
            }
            if (rotationFactor > 3 && rotationFactor<3.15)
            {
                if (pos.X < 5 && pos.X > -10)
                {
                    speed.X = speed.X * -1;
                    rotationFactor = 0f;
                }
            }
            if (rotationFactor > 1.5 && rotationFactor < 1.6)
            {
                if (pos.Y > 745 && pos.Y <755)
                {
                    speed.Y = speed.Y * -1;
                    rotationFactor = -1 * (float)Math.PI / 2; 
                }
            }
            if (rotationFactor < -1.5 && rotationFactor >-1.6)
	        {
                if (pos.Y >-5 && pos.Y <5)
                {
		            speed.Y = speed.Y * -1;
                    rotationFactor = (float)Math.PI / 2;
                }
	        }

            base.Update(gameTime);
        }
        public void Spawn(Vector2 theStartPos, Vector2 theSpeed, float theRotation)
        {
            rotationFactor = theRotation;
            pos = theStartPos;
            speed = theSpeed;
            Visible = true;
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, pos, scrRect, Color.White, rotationFactor, origin, 1f, SpriteEffects.None, 1f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }
    }
}
