/* 
Sushil Pathak 
4/19/2018
Description -It is the class which represent the door in game
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
    public class Door : Item
    {
        string _Code;
        public Door(String name, int value, String code) : base(name, value)
        {
            _Code = code;
        }

        public bool isMatch(DoorKey key)
        {
            return key.Code == _Code;
        }


    }
}
