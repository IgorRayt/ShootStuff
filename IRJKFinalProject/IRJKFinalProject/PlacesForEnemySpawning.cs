using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRJKFinalProject
{
    class PlacesForEnemySpawning
    {
        PlayerShip playerShip;
        public struct gameFieldGrid
        {
            public int X;
            public int Y;
        }
        gameFieldGrid[,] gameField;

        internal gameFieldGrid[,] GameField
        {
            get { return gameField; }
            set { gameField = value; }
        }
        int defaultSize = 15;
        int X = 0;
        int Y = 0;
        public void gameFieldSet()
        {
            gameField = new gameFieldGrid[15, 15];
            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int k = 0; k < gameField.GetLength(1); k++)
                {
                    if (k == 0 || k == gameField.GetLength(1) - 1)
                    {
                        gameField[i, k].X = X + playerShip.Tex.Width;
                        gameField[i, k].Y = Y + playerShip.Tex.Height;
                    }
                    else
                    {
                        gameField[0, k].X = X + playerShip.Tex.Width;
                        gameField[0, k].Y = Y + playerShip.Tex.Height;

                        gameField[gameField.GetLength(0) - 1, k].X = X + playerShip.Tex.Width;
                        gameField[gameField.GetLength(0) - 1, k].Y = Y + playerShip.Tex.Height;
                        break;
                    }
                }
            }
        }
    }
}
