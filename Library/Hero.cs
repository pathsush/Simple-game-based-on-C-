/*
Sushil Pathak 
4/19/2018
Description -It is the Hero which move around the map to conqured the game.
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
    public class Hero : Actor
    {
        private bool _IsRunningAway;
        private Weapon _Weapon;
        private DoorKey _DoorKey;
        /// <summary>
        /// Overloaded Constructor for a hero
        /// </summary>
        /// <param name="newName">Hero's Name</param>
        /// <param name="newTitle">Hero's Title</param>
        /// <param name="atkSpeed">Hero's AttackSpeed</param>
        /// <param name="hitPoints">Hero's Starting HP</param>
        /// <param name="startingPositionX">Hero's Starting X Position</param>
        /// <param name="startingPositionY">Hero's Starting Y Position</param>
        public Hero(String newName, String newTitle, double atkSpeed, int hitPoints, int startingPositionX, int startingPositionY)
            : base(newName, newTitle, atkSpeed, hitPoints, startingPositionX, startingPositionY)
        {
            Weapon = null;
        }

        /// <summary>
        /// Get Hero's attack speed
        /// </summary>
        public override double AttackSpeed
        {
            get
            {
                if (HasWeapon)
                {
                    return base.AttackSpeed + Weapon.SpeedModifier;
                }
                else
                {
                    return base.AttackSpeed;
                }
            }
        }
        /// <summary>
        /// Get and set the Hero's weapon
        /// </summary>
        public Weapon Weapon
        {
            get
            {
                return _Weapon;
            }

            set
            {
                _Weapon = value;
            }
        }
        /// <summary>
        /// Check to see if the hero has a weapon equipped.
        /// </summary>
        public bool HasWeapon
        {
            get
            {
                return _Weapon != null;
            }
        }

        /// <summary>
        /// Move the hero.
        /// </summary>
        /// <param name="dirToMove">Direction the hero will move.</param>
        public override void Move(Actor.Direction dirToMove)
        {
            base.Move(dirToMove);
        }
        /// <summary>
        /// Whether or not the hero is running away.
        /// </summary>
        public bool IsRunningAway
        {
            get { return _IsRunningAway; }
            set { _IsRunningAway = value; }
        }

        /// <summary>
        /// Apply Item to Hero
        /// </summary>
        /// <param name="i">Item to Apply to Hero</param>
        /// <returns>Item that hero needs to discard. Returns weapon if hero replaces weapon.</returns>
        public Item Apply(Item i)
        {
            Item retItem = null;
            if (i.GetType() == typeof(Potion))
            {
                healMe(i.AffectValue);
                
            }
            else if (i.GetType() == typeof(Weapon))
            {
                retItem = _Weapon;
                _Weapon = (Weapon)i;
            }
            else if (i.GetType() == typeof(DoorKey))
            {
                // checking wether hero has a key or not and to pick up the key to open the door. 
                if (_DoorKey != null)
                {
                    retItem = _DoorKey;
                    _DoorKey = (DoorKey)i;
                }
                else
                {
                    _DoorKey= (DoorKey)i;
                    return null;
                }
            }
            else
            {
                retItem = i;
            }
            return retItem;
        }
        /// <summary>
        /// How much damage that the Hero can do.
        /// </summary>
        public int AttackValue
        {
            get
            {
                if (HasWeapon) return _Weapon.AffectValue;
                return 1;
            }
        }
        /// <summary>
        /// Key that the Hero holds
        /// </summary>
        public DoorKey DoorKey
        {
            get
            {
                return _DoorKey;
            }
        }
        /// <summary>
        /// Operator for adding a hero to a monster.
        /// </summary>
        /// <param name="h">Hero to add</param>
        /// <param name="m">Monster to add</param>
        /// <returns>true if the hero is still alive</returns>
        public static bool operator +(Hero h, Monster m)
        {
            if (!h.IsRunningAway)
            {
                if (h.AttackSpeed > m.AttackSpeed)
                {
                    m.damageMe(h.AttackValue);
                    if (m.isAlive())
                    {
                        h.damageMe(m.AttackValue);
                    }
                }
                else if (h.AttackSpeed < m.AttackSpeed)
                {
                    h.damageMe(m.AttackValue);
                    if (h.isAlive())
                    {
                        m.damageMe(h.AttackValue);
                    }
                }
                else
                { // Attack Speeds are the same.
                    m.damageMe(h.AttackValue);
                    h.damageMe(m.AttackValue);
                }
            }
            else
            {
                if (h.AttackSpeed <= m.AttackSpeed)
                    h.damageMe(m.AttackValue);
            }
            h.IsRunningAway = false;
            return h.isAlive();
        }
        /// <summary>
        /// Operator for adding a hero to a monster.
        /// </summary>
        /// <param name="h">Hero to add</param>
        /// <param name="m">Monster to add</param>
        /// <returns>true if the hero is still alive</returns>
        public static bool operator +(Monster m, Hero h)
        {
            // call the other operator overload.
            return h + m;
        }
    }
}
