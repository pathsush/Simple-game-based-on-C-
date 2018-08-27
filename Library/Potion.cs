/*
Sushil Pathak 
4/19/2018
Description - Potion which is used to heal the Hero
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
    public class Potion : Item, IRepeatable<Potion>
    {
        private Colors _Color;

        public enum Colors
        {
            Red,
            Green,
            Blue,
            Purple
        }

        #region "Constructors"
        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="newName">Potion's Name</param>
        /// <param name="value">Value or Potency of the Potion</param>
        /// <param name="clr">Color of the Potion</param>
        public Potion(String newName, int value, Colors clr)
            : base(newName, value)
        {
            _Color = clr;
        }
        #endregion

        /// <summary>
        /// Get and Set the Potion's Color
        /// </summary>
        public Colors Color
        {
            get
            {
                return _Color;
            }

            set
            {
                _Color = value;
            }
        }
        /// <summary>
        /// Create a deep copy of this potion.
        /// </summary>
        /// <returns></returns>
        public Potion CreateCopy()
        {
            return new Potion(this._Name, this._AffectValue, this._Color);
        }
    }
}
