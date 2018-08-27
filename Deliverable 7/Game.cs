/*
Sushil Pathak 
4/19/2018
Description -  static game class which can respresnt the game 
Used the code from Profeesor Holmes of deliverable 6,and updated as necessary for Deliverable 7
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Deliverable_7
{
    static class Game
    {
        private static GameState _State;
        private static Map _Map;
        private static int _height;
        private static int _width;
        
      
        /// <summary>
        /// Status of the game 
        /// </summary>
        public enum GameState
        {
            Running,
            Won,
            Lost
        }
        /// <summary>
        /// Get or set the state of the game.
        /// </summary>
        public static GameState State
        {
            get
            {
                return _State;
            }

            set
            {
                _State = value;
            }
        }
        /// <summary>
        /// Get the Map for this game
        /// </summary>
        public static Map Map
        {
            get
            {
                return _Map;
            }

            set
            {
                _Map = value;
            }
        }
       
       

       
        /// <summary>
        /// Reset this game to the starting settings.
        /// </summary>
        /// <param name="height">Number of rows that the game board will have</param>
        /// <param name="width">Number of columns that the game board will have</param>
        public static void ResetGame(int height, int width)
        {
            _height = height;
            _width = width;
            _State = GameState.Running;
            _Map = new Map(width, height);
            Random rnd = new Random();
            int row, col;
            MapCell mc;
            do
            {
                row = rnd.Next(width);
                col = rnd.Next(height);
                mc = _Map.Cells[row, col];
            } while (mc.HasItem || mc.HasMonster);
            _Map.Adventurer = new Hero("Bob", "the Bestest", 10, 200, col, row);
            _Map.CurrentLocation.HasBeenSeen = true;
        }

        public static void Reset()
        {
            ResetGame( _height, _width);
        }
    }
}
