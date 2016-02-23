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
    public class ScoreInfo : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private int scorePoints;

        public int ScorePoints
        {
            get { return scorePoints; }
            set { scorePoints = value; }
        }
        private string scorePointsMessage;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private Vector2 position;
        public ScoreInfo(Game game,
            SpriteBatch spriteBatch,
            SpriteFont font,
            Vector2 position)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.position = position;
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
            scorePointsMessage = scorePoints.ToString();

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, scorePointsMessage, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
