/*
Sushil Pathak 
4/19/2018
Description - This is the item which is displayed in the Map and used by Hero. 
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
    public abstract class Item
    {
        protected String _Name;
        protected int _AffectValue;
        /// <summary>
        /// Overloaded contstructor to create an Item.
        /// </summary>
        /// <param name="myName">Name of the item</param>
        /// <param name="value"></param>
        public Item(String myName, int value)
        {
            Name = myName;
            AffectValue = value;
        }

        /// <summary>
        /// Get and Set Item Name
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        }

        /// <summary>
        /// Get and Set Item Affect Value; this is the value of how
        ///  much can cause as an effect.
        /// </summary>
        public int AffectValue
        {
            get
            {
                return _AffectValue;
            }

            set
            {
                _AffectValue = value;
            }
        }
    }
}
