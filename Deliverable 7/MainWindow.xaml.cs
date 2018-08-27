/*
CS/INFO 1182 
Sushil Pathak 
4/19/2018
Description - It is the code of main window which user can see
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Serialization.Formatters.Binary;
using Library;
using Microsoft.Win32;
using System.IO;




namespace Deliverable_7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
         

            InitializeComponent();
           
            Game.ResetGame(12, 12);
            for (int row = 0; row <= Game.Map.Cells.GetUpperBound(0); row++)
                grdMap.RowDefinitions.Add(new RowDefinition());
            for (int col = 0; col <= Game.Map.Cells.GetUpperBound(1); col++)
                grdMap.ColumnDefinitions.Add(new ColumnDefinition());
            DrawMap();
            // define directiosn for Buttons
            btnDown.Tag = Actor.Direction.Down;
            btnUp.Tag = Actor.Direction.Up;
            btnLeft.Tag = Actor.Direction.Left;
            btnRight.Tag = Actor.Direction.Right;
            updateDetail();
        }

        public void DrawMap()
        {
            // make sure the grid is cleared first
            grdMap.Children.Clear();
            for (int row = 0; row <= Game.Map.Cells.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= Game.Map.Cells.GetUpperBound(1); col++)
                {
                    MapCell mcToCheck = Game.Map.Cells[row, col]; // get the current mapcell
                    TextBlock tbContents = new TextBlock(); // create a textblock to display contents
                    tbContents.TextAlignment = TextAlignment.Center;
                    // making the cell color black until and unless something is doscovered in cell
                    if (mcToCheck.HasBeenSeen == false)
                    {
                        tbContents.Background = new SolidColorBrush(Colors.Black);
                    }
                    else
                    if (mcToCheck.HasItem)
                    {
                        if (mcToCheck.Item.GetType() == typeof(Weapon))
                            tbContents.Background = new SolidColorBrush(Colors.Gray);
                        else if (mcToCheck.Item.GetType() == typeof(Potion))
                        {
                            Potion ptn = (Potion)mcToCheck.Item;
                            tbContents.Background = GetColorBrush(ptn);
                        }
                        else if (mcToCheck.Item.GetType() == typeof(Door))
                            tbContents.Background = new SolidColorBrush(Colors.Peru);
                        else if (mcToCheck.Item.GetType() == typeof(DoorKey))
                            tbContents.Background = new SolidColorBrush(Colors.Gold);
                        tbContents.Text = mcToCheck.Item.Name;
                    }
                    else if (mcToCheck.HasMonster)
                    {
                        tbContents.Text = mcToCheck.Monster.Name(false);
                        tbContents.Background = new SolidColorBrush(Colors.DarkRed);
                        tbContents.Foreground = new SolidColorBrush(Colors.White);
                    }
                    else
                    if (Game.Map.Adventurer.PositionX == col
                        && Game.Map.Adventurer.PositionY == row)
                    {
                        tbContents.Text = Game.Map.Adventurer.Name(true);
                        tbContents.Foreground = new SolidColorBrush(Colors.White);
                        tbContents.Background = new SolidColorBrush(Colors.DarkBlue);
                    }
                    tbContents.TextWrapping = TextWrapping.Wrap;
                    Grid.SetRow(tbContents, row);
                    Grid.SetColumn(tbContents, col);
                    grdMap.Children.Add(tbContents);
                }
            }



        }
        /// <summary>
        /// setting the color of potion
        /// </summary>
        /// <param name="ptn"></param>
        /// <returns></returns>
        private static SolidColorBrush GetColorBrush(Potion ptn)
        {
            if (ptn.Color == Potion.Colors.Blue)
                return new SolidColorBrush(Colors.LightBlue);
            else if (ptn.Color == Potion.Colors.Red)
                return new SolidColorBrush(Colors.LightPink);
            else if (ptn.Color == Potion.Colors.Green)
                return new SolidColorBrush(Colors.LightGreen);
            else if (ptn.Color == Potion.Colors.Purple)
                return new SolidColorBrush(Colors.Thistle);
            else
                return new SolidColorBrush(Colors.Black);
        }
        /// <summary>
        /// making the move button which make hero to enable in map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
           
            Actor.Direction dir = (Actor.Direction)btn.Tag;
            bool needToAct = Game.Map.MoveAdventurer(dir);
            if (needToAct)
            {
                Window wnd = null;
                
                if (Game.Map.CurrentLocation.HasItem)
                {
                    wnd = new frmItem();

                }
                // checking wether the hero current location has door
                if(Game.Map.CurrentLocation.Item is Door)
                {
                    Game.State = Game.GameState.Won;
                    wnd = new frmGameOver();
                   
                   

                }
                else if (Game.Map.CurrentLocation.HasMonster)
                {
                    wnd = new frmMonster();
                }
                else
                {
                   
                    // do nothing
                }
                if (wnd != null) wnd.ShowDialog();
            }
           
            DrawMap();
            updateDetail();
        }
        //refreshing the whole game
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Game.ResetGame(12, 12);
            DrawMap();
        }
        /// <summary>
        /// Handle the key press for the hero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                btnMove_Click(btnUp, null);
            else if (e.Key == Key.Left)
                btnMove_Click(btnLeft, null);
            else if (e.Key == Key.Right)
                btnMove_Click(btnRight, null);
            else if (e.Key == Key.Down)
                btnMove_Click(btnDown, null);
        }
        /// <summary>
        /// Saving the game 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveD = new SaveFileDialog();
            Map content = Game.Map;
            saveD.Filter = "Map File(*.map) |*.map";
            if (saveD.ShowDialog() == true)
            {
                String file = saveD.FileName;
                BinaryFormatter binary = new BinaryFormatter();
                FileStream files = File.Create(file);
                binary.Serialize(files, content);
                files.Close();
            }
        }
        /// <summary>
        /// loading the Game from the saved file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openD = new OpenFileDialog();
            openD.Filter = "Map File(*.map) |*.map";

            if (openD.ShowDialog() == true)
            {
                try
                {
                    Map content;
                    BinaryFormatter binary = new BinaryFormatter();
                    FileStream file = new FileStream(openD.FileName, FileMode.Open);

                    content = (Map)binary.Deserialize(file);

                    Game.Map = content;
                    DrawMap();
                    updateDetail();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        /// <summary>
        /// updating the details in label as per the movement
        /// </summary>

        private void updateDetail()
        {
            if (Game.Map.Adventurer.HasWeapon)
            {
                lblDisplayWeapon.Content = Game.Map.Adventurer.Weapon.Name;
            }
            else
            {
                lblDisplayWeapon.Content = "None";
            }

            if (Game.Map.Adventurer.DoorKey != null)
            {
                lblDisplayKey.Content = Game.Map.Adventurer.DoorKey.Name;
            }
            else
            {
                lblDisplayKey.Content = "None";
            }

            lblDisplayName.Content = Game.Map.Adventurer.NameWithTitle();
            lblDisplayHp.Content = String.Format("{0}/{1}",Game.Map.Adventurer.CurrentHitPoints, Game.Map. Adventurer.MaximumHitPoints); 

            
        }
        /// <summary>
        /// Creating a method tocall the subform when game is lost. 
        /// </summary>

        private void HandleGameState()
        {
            if (Game.State == Game.GameState.Lost)
            {
                Window wd = new frmGameOver();
                wd.ShowDialog();
                //DrawMap();


            }
        }
    }
}