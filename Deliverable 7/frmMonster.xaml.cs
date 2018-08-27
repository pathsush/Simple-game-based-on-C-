/*
CS/INFO 1182 
Sushil Pathak 
4/19/2018
Description -  It is the subform which  pops up when the monster is found in map.
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

namespace Deliverable_7
{
    /// <summary>
    /// Interaction logic for frmMonster.xaml
    /// </summary>
    public partial class frmMonster : Window
    {
        public frmMonster()
        {
            InitializeComponent();
            btnClose.Visibility = Visibility.Hidden;
            displayInfo();

        }
        /// <summary>
        /// displaying the info when something is found in map
        /// </summary>
        private void displayInfo()
        {

            txtFound.Text = "What do You want to do? ";
            lblHero.Content = Game.Map.Adventurer.Name();

            lblMonster.Content = Game.Map.CurrentLocation.Monster.Name();
            lblMaximum.Content = String.Format("{0}/{1}", Game.Map.CurrentLocation.Monster.CurrentHitPoints, Game.Map.CurrentLocation.Monster.MaximumHitPoints);


            lblCurrent.Content = String.Format("{0}/{1}", Game.Map.Adventurer.CurrentHitPoints, Game.Map.Adventurer.MaximumHitPoints);
            Game.State = Game.GameState.Lost;



        }
        /// <summary>
        /// attacking the hero 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnAttack_Click(object sender, RoutedEventArgs e)
        {
            bool action = (Game.Map.Adventurer + Game.Map.CurrentLocation.Monster);
            Window wnd = null;
            if (Game.Map.Adventurer.isAlive() == false)
            {
                Game.State = Game.GameState.Lost;
                wnd = new frmGameOver();
                wnd.Show();

                this.Close();

            }
            else if (Game.Map.CurrentLocation.Monster.isAlive() == false)
            {
                Game.Map.Cells[Game.Map.Adventurer.PositionY, Game.Map.Adventurer.PositionX].Monster = null;
                this.Close();
            }
            else
            {


                displayInfo();
            }
        }
        /// <summary>
        /// making runaway event which doen't fight with the hero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRunAway_Click(object sender, RoutedEventArgs e)
        {
            Game.Map.Adventurer.IsRunningAway = true;
            bool alive = (Game.Map.Adventurer + Game.Map.CurrentLocation.Monster);
            if (alive == false)
            {
                Game.State = Game.GameState.Lost;
            }


            
            this.Close();

        }

        /// <summary>
        /// closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
