﻿/*
 Sushil Pathak 
4/19/2018
Description - It is the Map class, which help to track the hero.
Used the code from Profeesor Holmes of deliverable 6,and updated as necessary for Deliverable 7
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class Map
    {
        private MapCell[,] _Cells = null;
        private List<Monster> _Monsters = new List<Monster>();
        // These lists are no longer needed
        //private List<Potion> _Potions = new List<Potion>();
        //private List<Weapon> _Weapons = new List<Weapon>();
        private List<Item> _Items = new List<Item>();
        private Hero _Adventurer;

        /// <summary>
        /// Thing we will want a count of.
        /// </summary>
        private enum ThingToCount
        {
            Monster,
            Item,
            Discovered
        }

        /// <summary>
        /// Overloaded constructor that creates the map based on the given size
        /// </summary>
        /// <param name="rows">How many rows the map should have</param>
        /// <param name="cols">How many columns the map should have</param>
        public Map(int rows, int cols)
        {
            _Cells = new MapCell[rows, cols];
            fillMonsters();
            fillPotions();
            fillWeapons();
            fillMap();
        }

        /// <summary>
        /// Add all of the potions to the items collection
        /// </summary>
        private void fillPotions()
        {
            _Items.Add(new Potion("Small Healing Potion", 25, Potion.Colors.Blue));
            _Items.Add(new Potion("Medium Healing Potion", 50, Potion.Colors.Red));
            _Items.Add(new Potion("Large Healing Potion", 100, Potion.Colors.Green));
            _Items.Add(new Potion("Extreme Healing Potion", 200, Potion.Colors.Purple));
        }
        /// <summary>
        /// Add all of the weapons to the items collection
        /// </summary>
        private void fillWeapons()
        {
            _Items.Add(new Weapon("Dagger", 10, -1));
            _Items.Add(new Weapon("Club", 20, -3));
            _Items.Add(new Weapon("Sword", 30, -2));
            _Items.Add(new Weapon("Claymore", 40, -4));
        }

        /// <summary>
        /// Add all of the monsters to the monster collection
        /// </summary>
        private void fillMonsters()
        {
            _Monsters.Add(new Monster("Orc", "", 3, 100, 0, 0, 10));
            _Monsters.Add(new Monster("Goblin", "", 1, 30, 0, 0, 5));
            _Monsters.Add(new Monster("Slug", "", 5, 3, 0, 0, 2));
            _Monsters.Add(new Monster("Rat", "", 0, 5, 0, 0, 1));
            _Monsters.Add(new Monster("Skeleton", "", 4, 70, 0, 0, 7));
        }


        /// <summary>
        /// Get and set the Cells collection
        /// </summary>
        public MapCell[,] Cells
        {
            get
            {
                if (_Cells == null) fillMap();
                return _Cells;
            }

            set
            {
                _Cells = value;
            }
        }
        /// <summary>
        /// Fill the Map with items and monsters randomly
        /// </summary>
        private void fillMap()
        {
            Random rnd = new Random();
            int rows = Cells.GetLength(0);
            int cols = Cells.GetLength(1);
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    MapCell mc = new MapCell();
                    if (rnd.Next(10) % 10 == 0)
                    {
                        // add a monster
                        mc.Monster = _Monsters[rnd.Next(_Monsters.Count)].CreateCopy();
                    }
                    else if (rnd.Next(10) % 10 == 0)
                    {
                        // add an item
                        Item itm = _Items[rnd.Next(_Items.Count)];
                        if (itm.GetType() == typeof(Weapon))
                            mc.Item = ((Weapon)itm).CreateCopy();
                        else if (itm.GetType() == typeof(Potion))
                            mc.Item = ((Potion)itm).CreateCopy();
                    }
                    Cells[row, col] = mc;
                }
            }
            SetKeyAndDoorLocation(rnd, rows - 1, cols - 1, "AAAA");
        }
        /// <summary>
        /// Set the location of the Key and Door.
        /// </summary>
        /// <param name="rnd">Randomizer to use</param>
        /// <param name="rows">Max number of rows in the map</param>
        /// <param name="cols">Max number of columms in the map</param>
        /// <param name="code">Code to be shared by key and door.</param>
        private void SetKeyAndDoorLocation(Random rnd, int rows, int cols, String code)
        {
            MapCell keyLocation, doorLocation;
            keyLocation = doorLocation = Cells[rnd.Next(rows), rnd.Next(cols)];
            while (keyLocation.Monster != null || keyLocation.Item != null) // get new location
                keyLocation = Cells[rnd.Next(rows), rnd.Next(cols)];
            while (keyLocation == doorLocation || doorLocation.Monster != null || doorLocation.Item != null) // get new location
                doorLocation = Cells[rnd.Next(rows), rnd.Next(cols)];
            // set key and door.
            keyLocation.Item = new DoorKey("Key", 0, code);
            doorLocation.Item = new Door("Door", 0, code);
        }
        /// <summary>
        /// Hero that the user controls
        /// </summary>
        public Hero Adventurer
        {
            get
            {
                return _Adventurer;
            }

            set
            {
                _Adventurer = value;
            }
        }
        /// <summary>
        /// Move the adventuerer around the map. This method makes sure the user cannot
        /// go off of the map.
        /// </summary>
        /// <param name="dir">Direction the adventurer is expected to move.</param>
        /// <returns></returns>
        public bool MoveAdventurer(Actor.Direction dir)
        {
            int maxRow = Cells.GetUpperBound(0);
            int maxCol = Cells.GetUpperBound(1);
            Adventurer.Move(dir);
            // move hero back if he has left the board
            if (Adventurer.PositionX < 0) Adventurer.Move(Actor.Direction.Right);
            if (Adventurer.PositionX > maxCol) Adventurer.Move(Actor.Direction.Left);
            if (Adventurer.PositionY < 0) Adventurer.Move(Actor.Direction.Down);
            if (Adventurer.PositionY > maxRow) Adventurer.Move(Actor.Direction.Up);
            CurrentLocation.HasBeenSeen = true;
            return CurrentLocation.HasItem || CurrentLocation.HasMonster;
        }
        /// <summary>
        /// Gets the mapcell where the adventurer is on the map.
        /// </summary>
        public MapCell CurrentLocation
        {
            get
            {
                return Cells[Adventurer.PositionY, Adventurer.PositionX];
            }
        }
        /// <summary>
        /// Get the count of monster currently on our map.
        /// </summary>
        public int NumberOfMonsters
        {
            get
            {
                return countOfThing(ThingToCount.Monster);
            }
        }

        /// <summary>
        /// Get the count of monster currently on our map.
        /// </summary>
        public int NumberOfItems
        {
            get
            {
                return countOfThing(ThingToCount.Item);
            }
        }

        /// <summary>
        /// Get the count of monster currently on our map.
        /// </summary>
        public double PercentDiscovered
        {
            get
            {
                double foundCells = countOfThing(ThingToCount.Discovered);
                return foundCells / Cells.Length;
            }
        }

        /// <summary>
        /// Count things in the Map.
        /// </summary>
        /// <param name="toCount"></param>
        /// <returns></returns>
        private int countOfThing(ThingToCount toCount)
        {
            int thingCount = 0;
            for (int r = 0; r < Cells.GetLength(0); r++)
            {
                for (int c = 0; c < Cells.GetLength(1); c++)
                {
                    MapCell mc = Cells[r, c];
                    if (toCount == ThingToCount.Monster && mc.HasMonster
                        || toCount == ThingToCount.Item && mc.HasItem
                        || toCount == ThingToCount.Discovered && mc.HasBeenSeen)
                    {
                        thingCount++;
                    }
                }
            }
            return thingCount;
        }


        //public List<Monster> Monsters {
        //    get {
        //        return _Monsters;
        //    }
        //}

        //public List<Potion> Potions {
        //    get {
        //        return _Potions;
        //    }
        //}
        //public List<Weapon> Weapons {
        //    get {
        //        return _Weapons;
        //    }
        //}
    }
}
