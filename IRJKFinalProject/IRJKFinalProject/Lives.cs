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
    public class Lives : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private int livesAmount = 5;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private string livesLeftMessage;
        private Vector2 position;
        HighScoreList scoreList;
        ActionScene actionScene;
        public string LivesLeftMessage
        {
            get { return livesLeftMessage; }
            set { livesLeftMessage = value; }
        }

        public int LivesAmount
        {
            get { return livesAmount; }
            set { livesAmount = value; }
        }
        ScoreInfo scoreInfo;
        PlayerShip playerShip;
        PlayerShipGun playerShipGun;
        Level level;
        Game game;
        int score;
        GameOver gameOver;
        bool over = false;
        public Lives(Game game,
            SpriteBatch spriteBatch,
            SpriteFont font, 
            Vector2 position,
            ScoreInfo score,
            PlayerShip playerShip,
            PlayerShipGun playerShipGun,
            Level level,
            ActionScene actionScene)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.position = position;
            this.game = game;
            this.level = level;
            this.playerShipGun = playerShipGun;
            this.playerShip = playerShip;
            this.scoreInfo = score;
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
            // TODO: Add your update code here
            livesLeftMessage = livesAmount.ToString();
            if (livesAmount == 0)
            {
                over = true;
                livesAmount = 5;
                score = scoreInfo.ScorePoints;
                game.Components.Add(gameOver);
                gameOver = new GameOver(game, spriteBatch, score, actionScene);
            }
            KeyboardState ks = Keyboard.GetState();
            if (over)
            {
                
            if (ks.IsKeyDown(Keys.Enter))
            {
                
            }
            }
            base.Update(gameTime);
        }
        public void NewGame()
        {
            
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, livesLeftMessage, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
