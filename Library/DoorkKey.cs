/*
Sushil Pathak 
4/19/2018
Description - It is the doorKey which is needed to open the door.
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
    public class DoorKey : Item
    {
        string _Code;

        public DoorKey(string name, int value, string code) : base(name, value)
        {
            _Code = code;
        }

        public string Code
        {
            get
            {
                return _Code;
            }
        }
    }
}
