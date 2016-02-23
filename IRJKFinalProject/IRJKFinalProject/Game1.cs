/* 9.11.2015
 * 10:17
 * Fuckin Amazing Game created
 * Hell Yeah!
 */
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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        PlayerShip playerShip;
        PlayerShipGun playerShipGun;
        

        Lives livesInfo;
        LivesString livesString;
        ScoreInfo scoreInfo;
        ScoreString scoreString;

        Level level;

        StartScene startScene;
        HelpScene helpScene;
        ActionScene actionScene;
         TitleText titleText;
         CreditsScreen creditsScreen;
         Song song;
        
         //SoundEffect sound;
        bool Pause = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 750;
            graphics.PreferredBackBufferHeight = 750;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Vector2 stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            Texture2D texOne = Content.Load<Texture2D>("Images/debrisTrans");
            Rectangle r = new Rectangle(0, 0, texOne.Width, texOne.Height);

            Texture2D texTwo = Content.Load<Texture2D>("Images/backgroundSpace");
            Rectangle g = new Rectangle(0, 0, texTwo.Width, texTwo.Height);

            Vector2 pos = new Vector2(0, stage.Y - r.Height);
            ScrollingBackground sb = new ScrollingBackground(this,
                spriteBatch, texOne, r, pos, new Vector2(0, (float)-1.5));


            Vector2 pos1 = new Vector2(0, stage.Y - r.Height - 50);
            ScrollingBackground sb1 = new ScrollingBackground(this,
                spriteBatch, texOne, r, pos1, new Vector2(0, (float)-0.5));

            Vector2 pos2 = new Vector2(0, stage.Y - r.Height - 50);
            ScrollingBackground sb2 = new ScrollingBackground(this,
                spriteBatch, texTwo, g, pos2, new Vector2(0, (float)-0.05));

            this.Components.Add(sb2);
            this.Components.Add(sb1);

            titleText = new TitleText(this, spriteBatch);
            this.Components.Add(titleText);

            creditsScreen = new CreditsScreen(this, spriteBatch);
            this.Components.Add(creditsScreen);

            startScene = new StartScene(this, spriteBatch);
            this.Components.Add(startScene);

            helpScene = new HelpScene(this, spriteBatch);
            this.Components.Add(helpScene);

            actionScene = new ActionScene(this, spriteBatch, stage);
            this.Components.Add(actionScene);
            song = Content.Load<Song>("Sounds/SpaceShooter");
            MediaPlayer.Play(song);
            //sound = Content.Load<SoundEffect>("Sounds/SpaceShooter");
            //SoundEffectInstance inst = sound.CreateInstance();
            //inst.IsLooped = true;
            //sound.Play();

            startScene.show();
             //TODO: use this.Content to load your game content here
            //Texture2D playerShipTex = Content.Load<Texture2D>("Images/PlayerShip");
            //Vector2 playerShipSpeed = new Vector2(0, 0);
            //Vector2 playerShipPos = new Vector2(stage.X / 2 - playerShipTex.Width / 2,
            //    stage.Y/2 - playerShipTex.Height / 2);
            //playerShip = new PlayerShip(this, spriteBatch, playerShipTex, playerShipPos, playerShipSpeed, stage);
            //this.Components.Add(playerShip);

            //Texture2D playerShipGunTex = Content.Load<Texture2D>("Images/shipTurret");
            //Vector2 playerShipGunSpeed = new Vector2(5, 5);
            //Vector2 playerShipGunPos = new Vector2(playerShipGunTex.Width / 2, playerShipGunTex.Height / 2);
            //playerShipGun = new PlayerShipGun(this, spriteBatch, playerShipGunTex, playerShipGunPos, stage, playerShip);
            //this.Components.Add(playerShipGun);

            
            //Vector2 scoreStringpos = new Vector2(10, 30);
            //scoreString = new ScoreString(this, spriteBatch, inGameFont, scoreStringpos);
            //this.Components.Add(scoreString);

            //Vector2 scoreStringDimension = inGameFont.MeasureString(scoreString.ScoreString1);
            //Vector2 scoreInfoPos = new Vector2(scoreStringDimension.X + 10, 30);
            //scoreInfo = new ScoreInfo(this, spriteBatch, inGameFont, scoreInfoPos);
            //this.Components.Add(scoreInfo);

            //Vector2 livesStringPos = new Vector2(10 , 10);
            //livesString = new LivesString(this, spriteBatch, inGameFont, livesStringPos);
            //this.Components.Add(livesString);

            //Vector2 livesStringDimension = inGameFont.MeasureString(livesString.LivesString1);
            //Vector2 livesInfoPos = new Vector2(livesStringDimension.X + 10, 10);
            //livesInfo = new Lives(this, spriteBatch, inGameFont, livesInfoPos, scoreInfo, playerShip, playerShipGun, level);
            //this.Components.Add(livesInfo);
            
            //level = new Level(this, spriteBatch, playerShipGun, playerShip, scoreInfo, stage, livesInfo);
            //this.Components.Add(level);
            //cme = new CollitionManagerWithEnemy(this, level.EnemyShips, playerShipGun.Missle, level.FirstSoawn);



            //Texture2D playerShipGunTex = Content.Load<Texture2D>("Images/PlayerShipGun");
            //Vector2 playerShipGunPos = new Vector2(18, 21);
            //playerShipGun = new PlayerShip(this, spriteBatch, playerShipTex, playerShipPos, playerShipSpeed, stage);
            //this.Components.Add(playerShip);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //cme = new CollitionManagerWithEnemy(this, level.EnemyShips, playerShipGun.Missle, playerShip, scoreInfo,level.FirstSoawn);
            //cme.Update(gameTime);

            // TODO: Add your update logic here

            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAll();
                    actionScene.show();
                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAll();
                    helpScene.show();
                }
                else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAll();
                    creditsScreen.show();
                }
                else if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }

            if (helpScene.Enabled || actionScene.Enabled || creditsScreen.Enabled) // || add others
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    if (actionScene.Enabled)
                    {
                        actionScene.Pause(true);
                    }
                    
                    hideAll();
                    titleText.show();
                    startScene.show();

                }

            }
            if (helpScene.Enabled ==false  && actionScene.Enabled ==false&& creditsScreen.Enabled==false)
            {
                titleText.show();
            }

            

            base.Update(gameTime);
        }
        private void hideAll()
        {
            GameScene gs = null;
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    gs = (GameScene)item;
                    gs.hide();
                }
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        //public Missle(ContentManager content)
        //{
        //    this.missleTex = content.Load<Texture2D>("Images/bullet");
        //}
    }
}
