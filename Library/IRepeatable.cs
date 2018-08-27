/*
Sushil Pathak 
4/19/2018
Description - It is the interface of the game
Used the code from Profeesor Holmes of deliverable 6,and updated as necessary for Deliverable 7
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
  
    interface IRepeatable<T>
    {
        /// <summary>
        /// Create a copy of this object
        /// </summary>
        /// <returns>copy of the object</returns>
        T CreateCopy();
    }
}
