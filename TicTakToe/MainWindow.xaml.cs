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

namespace TicTakToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Members
        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it is player 1's turn (x) or player 2's turn(O)
        /// </summary>
        private bool mPlayer1Turn;
        
        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool mGameEnded;
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        /// <summary>
        /// Starts a new Game
        /// </summary>
        private void NewGame()
        {
            //Create a new blank array of free cells
            mResults = new MarkType[9];

            for (var i=0; i < mResults.Length; i++){
                mResults[i] = MarkType.Free;
            }

            mPlayer1Turn = true;

            //Iterate every button on the grid.
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Change background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            //make sure the game hasn't finished
            mGameEnded = false;
        }


        /// <summary>
        /// Handles a button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Start a new game on the click after the game is finished.
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            //cast the sender to a button
            var button= (Button)sender;

            //Find the button in the grid
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            //Convert grid position to index base
            var index = column + (row * 3);

            //Don't do anything if the button is not free
            if(mResults[index] != MarkType.Free)
            {
                return;
            }
            //set the cell value based on which players turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            //set the content of the button to x or o
            button.Content = mPlayer1Turn ? "X" : "O";

            //change Os to green
            if (!mPlayer1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            //Toggle the players turn
            mPlayer1Turn ^= true;

            //Check for a winner
            CheckForWinner();
        }

        /// <summary>
        /// Checks if there is a winner in the grid.
        /// </summary>
        private void CheckForWinner()
        {
            #region Horizontal Wins
            // Check for Horizontal Wins
            // - Row 0
            //
            if(mResults[0]!=MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            // Check for Horizontal Wins
            // - Row 1
            //
            else if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            // Check for Horizontal Wins
            // - Row 2
            //
            else if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical Wins
            // Check for Vertical Wins
            // - Col 0
            //
            else if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            // Check for Vertical Wins
            // - Col 1
            //
            else if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;

                //Highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            // Check for Vertical Wins
            // - Col 2
            //
            else if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;

                //Highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonal Wins
            // Check for Diagonal Wins
            // - Col 0
            //
            else if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            // Check for Diagonal Wins
            // - Col 2
            //
            else if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;

                //Highlight winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }
            #endregion

            #region No Winners
            if (!mResults.Any(result => result == MarkType.Free))
            {
                //Game Ended
                mGameEnded = true;

                //Turn all cells orange
                //Iterate every button on the grid.
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
            #endregion
        }
    }
}
