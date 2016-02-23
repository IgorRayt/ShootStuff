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
    
    public class Level : Microsoft.Xna.Framework.GameComponent
    {
        private float level;
        private float levelTime = 0f;
        private int x;
        private int spawnTime;
        private int enemiesAmount;
        private float timeSinceLastUpdate = 0f;
        private float updateFrequency = 1f;
        private float timeSinceLastSpawn;
        private Vector2 stage;
        private Vector2[,] spawnPositions = new Vector2[15, 15];
        List<Vector2> enemyPositions;

        public List<Vector2> EnemyPositions
        {
            get { return enemyPositions; }
            set { enemyPositions = value; }
        }
        List<int> classPos;

        public List<int> ClassPos
        {
            get { return classPos; }
            set { classPos = value; }
        }
        List<int> classRotation;

        public List<int> ClassRotation
        {
            get { return classRotation; }
            set { classRotation = value; }
        }
        List<EnemyShip> enemyShips = new List<EnemyShip>();
        PlayerShipGun playerShipGun;
        PlayerShip playerShip;
        private List<Missle> missle;
        ScoreInfo score;
        string name;
        bool pause = false;

        public bool Pause
        {
            get { return pause; }
            set { pause = value; }
        }
        ActionScene actionScene;
        public List<EnemyShip> EnemyShips
        {
            get { return enemyShips; }
            set { enemyShips = value; }
        }
        float rotation = 0f;
        private int[] randomPlace;
        Game game;
        SpriteBatch spriteBatch;
        Vector2 speed;
        private bool firstSoawn = true;
        List<Vector2> oldSpeed = new List<Vector2>();
        bool pauseUsed = false;
        public bool FirstSoawn
        {
            get { return firstSoawn; }
            set { firstSoawn = value; }
        }
        Lives lives;
        public Level(Game game,
            SpriteBatch spriteBatch,
            PlayerShipGun playerShipGun,
            PlayerShip playerShip,
            ScoreInfo score,
            Vector2 stage,
            Lives lives,
            ActionScene actionScene)
            : base(game)
        {
            this.stage = stage;
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.playerShipGun = playerShipGun;
            this.playerShip = playerShip;
            this.score = score;
            this.lives = lives;
            this.actionScene = actionScene;
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
            KeyboardState ks = Keyboard.GetState();
            if (pause == true)
            {
                pause = false;
                foreach (EnemyShip e in enemyShips)
                {
                    e.Speed = new Vector2(0, 0);
                    e.Visible = false;
                }
                pauseUsed = true;
                actionScene.Visible = false;
            }
            if (actionScene.Visible && pauseUsed == true)
            {
                pauseUsed = false;
                int count = 0;
                pause = false;
                foreach (EnemyShip e in enemyShips)
                {
                    e.Speed = oldSpeed[count];
                    e.Enabled = true;
                    e.Visible = true;

                    count++;
                }
            }
            if (pause == false)
	        {
		        this.playerShipGun = playerShipGun;
            this.playerShip = playerShip;
            this.score = score;
            this.lives = lives;
            missle = playerShipGun.Missle;
            timeSinceLastUpdate += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastUpdate > updateFrequency)
            {
                x++;
                level = (float)Math.Log(0.4 * x) + 2;
                timeSinceLastUpdate = 0f;
                firstSoawn = false;
            }
            timeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (firstSoawn == false)
            {
                if (timeSinceLastSpawn > levelTime)
                {
                    levelTime = (float)Math.Pow(1.1, (float)-0.8 * (float)level) * 8;
                    enemyPositions = new List<Vector2>();
                    classPos = new List<int>();
                    classRotation = new List<int>();
                    enemiesAmount = (int)level * 9;
                    if (enemiesAmount > 49)
                    {
                        enemiesAmount = 49;
                    }
                    timeSinceLastSpawn = 0f;
                    EnemySpawn();

                } 
            }
	        }
            
            // TODO: Add your update code here

            base.Update(gameTime);
        }
        public void EnemySpawn()
        {
            randomPlace = new int[enemiesAmount];
            GeneratePlace();
            for (int i = 0; i < randomPlace.Length; i++)
            {
                if (randomPlace[i] < 15 )
	            {
                    enemyPositions.Add(new Vector2(-50, randomPlace[i]*50+25));
		            classPos.Add(0);
                    classRotation.Add(0);
	            }
                else if (randomPlace[i] < 30)
	            {
		            enemyPositions.Add(new Vector2((randomPlace[i]-15)*50+25, 15*50));
                    classPos.Add(1);
                    classRotation.Add(1);
	            }
                else if (randomPlace[i]<45)
                {
                    enemyPositions.Add(new Vector2(15*50, (randomPlace[i]-30)*50+25));
                    classPos.Add(2);
                    classRotation.Add(2);
                }
                else if(randomPlace[i]<60)
	            {
		            enemyPositions.Add(new Vector2((randomPlace[i]-45)*50+25, -50));
                    classPos.Add(3);
                    classRotation.Add(3);
	            }
            }
            enemyShips = new List<EnemyShip>();
            bool createNew = true;
            bool added = false;
            for (int i = 0; i < enemyPositions.Count; i++)
            {
                switch(classPos[i])
                {
                    case 0:
                        speed = new Vector2(4, 0);
                        rotation = 0f;
                        break;
                    case 1:
                        speed = new Vector2(0, -4);
                        rotation = -1* (float)Math.PI / 2;
                        break;
                    case 2:
                        speed = new Vector2(-4, 0);
                        rotation = (float)Math.PI;
                        break;
                    case 3:
                        speed = new Vector2(0, 4);
                        rotation = (float)Math.PI / 2;
                        break;
                }
                added = false;
                foreach (EnemyShip e in enemyShips)
                {
                    if (e.Visible ==false)
                    {
                        
                        added = true;
                        e.Spawn(enemyPositions[i], speed, rotation);
                        oldSpeed.Add(e.Speed);
                        break;
                    }
                }
                if (added == false)
                {
                    name = "cme" + enemyShips.Count;
                    EnemyShip e = new EnemyShip(game, spriteBatch, enemyPositions[i], speed, stage, rotation, playerShipGun.Missle, playerShip, score, firstSoawn, name, lives);
                    e.Spawn(enemyPositions[i], speed, rotation);
                    oldSpeed.Add(e.Speed);
                    //cme = new CollitionManagerWithEnemy(game, e, playerShipGun.Missle, playerShip, score, firstSoawn);
                    //game.Components.Add(cme);
                    enemyShips.Add(e);
                    game.Components.Add(e);
                    //Thread t = new Thread(new CollitionManagerWithEnemy(game, e, playerShipGun.Missle, playerShip, score, firstSoawn).CheckCollide);
                    //t.Start();
                }
            }
        }
        public void GeneratePlace()
        {
            Random r = new Random();
            int counter = 0;
            do
            {
                int num = r.Next(60);
                if(!exists(num)){
                    randomPlace[counter] = num;
                    counter++;
                    if (counter == enemiesAmount)
                    {
                        break;
                    }
                }
            } while (true);
        }
        bool exists(int num)
        {
            for (int i = 0; i < randomPlace.Length - 1; i++)
            {
                if (num == randomPlace[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}