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
    public class ActionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        PlayerShip playerShip;
        PlayerShipGun playerShipGun;


        Lives livesInfo;
        LivesString livesString;
        ScoreInfo scoreInfo;
        ScoreString scoreString;

        Level level;
        Game game;
        Vector2 stage;
        SpriteFont regularFont;
        GameTime gt;
        public ActionScene(Game game, SpriteBatch spriteBatch, Vector2 stage)
            : base(game)
        {
            // TODO: Construct any child components here
            this.stage = stage;
            regularFont = game.Content.Load<SpriteFont>("Fonts/regularFont");
            Texture2D playerShipTex = game.Content.Load<Texture2D>("Images/PlayerShip");
            Vector2 playerShipSpeed = new Vector2(0, 0);
            Vector2 playerShipPos = new Vector2(stage.X / 2 - playerShipTex.Width / 2,
                stage.Y / 2 - playerShipTex.Height / 2);
            playerShip = new PlayerShip(game, spriteBatch, playerShipTex, playerShipPos, playerShipSpeed, stage);
            this.Components.Add(playerShip);

            Texture2D playerShipGunTex = game.Content.Load<Texture2D>("Images/shipTurret");
            Vector2 playerShipGunSpeed = new Vector2(5, 5);
            Vector2 playerShipGunPos = new Vector2(playerShipGunTex.Width / 2, playerShipGunTex.Height / 2);
            playerShipGun = new PlayerShipGun(game, spriteBatch, playerShipGunTex, playerShipGunPos, stage, playerShip, this);
            this.Components.Add(playerShipGun);

            Vector2 scoreStringpos = new Vector2(10, 30);
            scoreString = new ScoreString(game, spriteBatch, regularFont, scoreStringpos);
            this.Components.Add(scoreString);

            Vector2 scoreStringDimension = regularFont.MeasureString(scoreString.ScoreString1);
            Vector2 scoreInfoPos = new Vector2(scoreStringDimension.X + 10, 30);
            scoreInfo = new ScoreInfo(game, spriteBatch, regularFont, scoreInfoPos);
            this.Components.Add(scoreInfo);

            Vector2 livesStringPos = new Vector2(10, 10);
            livesString = new LivesString(game, spriteBatch, regularFont, livesStringPos);
            this.Components.Add(livesString);

            Vector2 livesStringDimension = regularFont.MeasureString(livesString.LivesString1);
            Vector2 livesInfoPos = new Vector2(livesStringDimension.X + 10, 10);
            livesInfo = new Lives(game, spriteBatch, regularFont, livesInfoPos, scoreInfo, playerShip, playerShipGun, level, this);
            this.Components.Add(livesInfo);

            level = new Level(game, spriteBatch, playerShipGun, playerShip, scoreInfo, stage, livesInfo, this);
            this.Components.Add(level);
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
            this.gt = gameTime;

            base.Update(gameTime);
        }
        public void Pause(bool pause)
        {
            level.Pause = pause;
            level.Update(gt);
            playerShipGun.Pause = pause;
            playerShipGun.Update(gt);
        }

      
    }
}
