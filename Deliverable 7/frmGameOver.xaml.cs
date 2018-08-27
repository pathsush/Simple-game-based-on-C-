/*
CS/INFO 1182 
Sushil Pathak 
4/19/2018
Description -  This is the sub form which is called when gameover situation matches. 
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
    /// Interaction logic for frmGameOver.xaml
    /// </summary>
    public partial class frmGameOver : Window
    {
        public frmGameOver()
        {
            InitializeComponent();
            state();
            Check();
           
        }

        /// <summary>
        /// making the close event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();


        }

        /// <summary>
        /// Button which can restart the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {

            Game.Reset();
            this.Close();
           
        }
        /// <summary>
        ///  Button which can close the whole application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Defining and making a state method to look the status of  Hero. 
        /// </summary>
        private void state()
        {
            if (Game.State == Game.GameState.Won)
            {
                txtOVer.Text = " Game Over, You have won the game! You have done the awesome Job";
                btnClose.Visibility = Visibility.Hidden;
            }
            if (Game.State == Game.GameState.Lost)
            {
                txtOVer.Text = "You lost the game. ";
                btnClose.Visibility = Visibility.Hidden;

            }
            if (Game.State == Game.GameState.Running)
            {
                btnRestart.Visibility = Visibility.Hidden;
                btnExit.Visibility = Visibility.Hidden;

            }

        }
        /// <summary>
        /// method to check wether hero has a doorkey or not to open the door. 
        /// </summary>
        public void Check()
        {
            if (Game.Map.CurrentLocation.HasItem)
            {
                if (Game.Map.CurrentLocation.Item.GetType() == typeof(Door))
                {
                    if (Game.Map.Adventurer.DoorKey != null)
                    {
                        Door door = (Door)Game.Map.CurrentLocation.Item;
                        if (door.isMatch(Game.Map.Adventurer.DoorKey))
                        {
                            Game.State = Game.GameState.Won;
                            btnRestart.Visibility = Visibility.Visible;
                            btnExit.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            txtOVer.Text = "Door key doesn't match the door. ";
                        }
                    }
                    else
                    {
                        txtOVer.Text = "You have found the door but not the key. Keep looking for key.";
                        btnExit.Visibility = Visibility.Hidden;
                        btnRestart.Visibility = Visibility.Hidden;
                        btnClose.Visibility = Visibility.Visible;
                        
                    }
                }
            }
        }
    }
}
