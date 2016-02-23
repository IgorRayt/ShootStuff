using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    public class Missle : Microsoft.Xna.Framework.DrawableGameComponent
    {
        PlayerShipGun playerShipGun;
        Missle missle;
        Game1 game1;
        private SpriteBatch spriteBatch;
        private Vector2 pos;

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        private Vector2 speedCONST = new Vector2(17, 17);
        private Vector2 speed;

        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private Texture2D tex;
        private Vector2 stage;
        private Rectangle srcRect;
        private float rotation = 1f;
        private Vector2 origin;
        const int MAX_DISTANCE = 1300;
        public Missle(Game game,
            SpriteBatch spriteBatch,
            Vector2 pos,
            float rotation, 
            PlayerShipGun playerShipGun)
            : base(game)
        {
            this.tex = game.Content.Load<Texture2D>("Images/bullet");
         //   this.tex = game1.MissleTex;
            this.pos =  pos;
            this.rotation = rotation;
            this.playerShipGun = playerShipGun;
            this.spriteBatch = spriteBatch;
            srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            
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
            //speed = new Vector2((float)Math.Cos(playerShipGun.RotationFactor * 10), (float)Math.Sin(playerShipGun.RotationFactor * 10));
            //rotation = playerShipGun.RotationFactor+(float)-1.5*(float)Math.PI;
            if (Vector2.Distance(playerShipGun.Position, pos)>MAX_DISTANCE)
            {
                Visible = false;
                speed = new Vector2(0, 0);
            }
            if (Visible == true)
            {
                pos = pos + speed;
                base.Update(gameTime);
            }


        }
        public void Shoot(Vector2 theStartPos, Vector2 direction, float rotationGun)
        {
            rotation = rotationGun + (float)-1.5 * (float)Math.PI;
            pos = theStartPos;
            speed = speedCONST  * direction;
            Visible = true;
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, pos, srcRect, Color.White, rotation, origin, 1f, SpriteEffects.None, 1f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice { get; set; }

        public Rectangle getBounds()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }
    }
}
