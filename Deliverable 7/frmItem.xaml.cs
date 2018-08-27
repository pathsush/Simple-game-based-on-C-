/*
CS/INFO 1182 
Sushil Pathak 
4/19/2018
Description -  It is the subform which is displayed when item is found in map 
Used the code from Profeesor Holmes of deliverable 6,and updated as necessary for Deliverable 7
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Library;

namespace Deliverable_7
{
    /// <summary>
    /// Interaction logic for frmItem.xaml
    /// </summary>
    public partial class frmItem : Window
    {
        public frmItem()
        {
            InitializeComponent();
            type();
        }

        /// <summary>
        /// creating a button event which can accept the item 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            Game.Map.CurrentLocation.Item = Game.Map.Adventurer.Apply(Game.Map.CurrentLocation.Item);

            this.Close();

        }

        /// <summary>
        /// creating a button event which close window without choosing the item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// creating a method to  to show the message in textbox on the basis of the item we found
        /// </summary>
        private void type()
        {
            if (Game.Map.CurrentLocation.Item.GetType() == typeof(Potion))
            {
                txtFound.Text = String.Format(" You found a {0}", Game.Map.CurrentLocation.Item.Name + "\r\n" + "Will You drink it");
            }
            if (Game.Map.CurrentLocation.Item.GetType() == typeof(Weapon))
            {
                txtFound.Text = String.Format(" You found a {0}", Game.Map.CurrentLocation.Item.Name + "\r\n" + "Will You Equip it");
            }
            if (Game.Map.CurrentLocation.Item.GetType() == typeof(DoorKey))
            {
                txtFound.Text = String.Format(" You found a {0}", Game.Map.CurrentLocation.Item.Name + "\r\n" + "Now, you can unlock the door");
            }
        }
    }
}
