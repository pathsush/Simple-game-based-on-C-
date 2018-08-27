/*
Sushil Pathak 
4/19/2018
Description - It is the weapon which hero can eqiup to fight with hero. 
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
    public class Weapon : Item, IRepeatable<Weapon>
    {
        private int _SpeedModifier;
        #region "Constructors"
        /// <summary>
        /// Overloaded Constructor
        /// </summary>
        /// <param name="wName">Name of the Weapon</param>
        /// <param name="wValue">Amount of damage the weapon can do</param>
        /// <param name="wSpeedModifier">How much this weapon affects the hero's attack speed.</param>
        public Weapon(String wName, int wValue, int wSpeedModifier) : base(wName, wValue)
        {
            _SpeedModifier = wSpeedModifier;
        }
        #endregion
        /// <summary>
        /// How much the hero's speed is modified when this weapon is equipped
        /// </summary>
        public int SpeedModifier
        {
            get
            {
                return _SpeedModifier;
            }
        }
        /// <summary>
        /// Create a deep copy of this weapon.
        /// </summary>
        /// <returns></returns>
        public Weapon CreateCopy()
        {
            return new Weapon(this.Name, this.AffectValue, this.SpeedModifier);
        }
    }
}
