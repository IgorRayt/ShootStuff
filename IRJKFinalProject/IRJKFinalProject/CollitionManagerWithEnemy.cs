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
    public class CollitionManagerWithEnemy : Microsoft.Xna.Framework.GameComponent
    {
        private List<EnemyShip> enemyShips = new List<EnemyShip>();
        private List<Missle> missle = new List<Missle>();
        Game game;
        bool first;
        ScoreInfo score;
        PlayerShip player;
        Vector2 pos;
        Rectangle eRect;
        Rectangle pRect;
        EnemyShip enemy;
        Lives lives;
        public CollitionManagerWithEnemy(Game game,
            EnemyShip enemy,
            List<Missle> missle,
            PlayerShip player,
            ScoreInfo score,
            bool first,
            Lives lives)
            : base(game)
        {
            // TODO: Construct any child components here
            this.game = game;
            this.missle = missle;
            this.first = first;
            this.player = player;
            this.score = score;
            this.enemy = enemy;
            this.lives = lives;
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
            eRect = enemy.getBounds();
            pRect = player.getBounds();
            if (first == false)
            {
                foreach (Missle m in missle)
                {
                    Rectangle mRect = m.getBounds();
                    if (eRect.Intersects(mRect))
                    {
                        enemy.Visible = false;
                        enemy.Enabled = false;
                        m.Visible = false;
                        enemy.Pos = new Vector2(20300, -70568);
                        enemy.Speed = new Vector2(0, 0);
                        m.Pos = new Vector2(-200, -300);
                        m.Speed = new Vector2(0, 0);
                        score.ScorePoints++;
                    }
                }
                if (eRect.Intersects(pRect))
                {

                    enemy.Pos = new Vector2(20300, -70568);
                    enemy.Speed = new Vector2(0, 0);
                    enemy.Visible = false;
                    lives.LivesAmount--;
                }
            }
            

            base.Update(gameTime);
        }
    }
}
