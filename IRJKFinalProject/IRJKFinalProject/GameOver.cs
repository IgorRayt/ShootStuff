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
    public class GameOver : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Vector2 stage;
        SpriteFont spriteFont;
        SpriteBatch spriteBatch;
        string end;
        int score;
        Vector2 pos;
        ActionScene actionScene;
        public GameOver(Game game, SpriteBatch spriteBatch, int score, ActionScene actionScene)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.score = score;
            this.pos = pos;
            spriteFont = game.Content.Load<SpriteFont>("Fonts/hilightFont");
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
            

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            end = "Game Over! You Died!\n Your Score is " + score.ToString();
            Vector2 dim = spriteFont.MeasureString(end);
            pos = new Vector2(352 - dim.X / 2, 350 - dim.Y / 2);
            actionScene.Pause(true);
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, end, pos, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
