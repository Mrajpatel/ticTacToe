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

namespace ticTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Member
        private MarkType[] mResult;     //Holds the current result of the cells in active game
        private bool mPlayer1Turn;      //True if the player 1's turn X or 2nd turn O
        private bool mGameEnd;          //True if the game has ended
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }
        #endregion

        #region New Game
        private void NewGame()
        {
            mResult = new MarkType[9];
            for (var a = 0; a < mResult.Length; a++)
            {
                mResult[a] = MarkType.Free;
            }

            mPlayer1Turn = true;

            //interect each and every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //set the default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            //Make sure the game hasn't finished
            mGameEnd = false;
        }
        #endregion

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //start the new game
            if (mGameEnd)
            {
                NewGame();
                return;
            }
            //cast the sender button
            var button = (Button)sender;
            //find the button on the postion of an array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //don't do anything if the button has already a value in it
            if(mResult[index] != MarkType.Free)
            {
                return;
            }

            //Set the cell value on the base of player's turn
            if (mPlayer1Turn) {
                mResult[index] = MarkType.Cross;
            }
            else
            {
                mResult[index] = MarkType.Nought;
            }
            //set button text   
            button.Content = mPlayer1Turn ? "X" : "O";

            //changing the turn color
            if (!mPlayer1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            //change the player's trun
            mPlayer1Turn ^= true;

            //calling the winner method
            Winner();
        }

        private void Winner()
        {
            //check Horizontal winner

            #region Rows
            if (mResult[0] != MarkType.Free && (mResult[0] & mResult[1] & mResult[2]) == mResult[0])
            {
                mGameEnd = true;
                button0_0.Background = button1_0.Background = button2_0.Background = Brushes.LightBlue;
            }
            if (mResult[3] != MarkType.Free && (mResult[3] & mResult[4] & mResult[5]) == mResult[3])
            {
                mGameEnd = true;
                button0_1.Background = button1_1.Background = button2_1.Background = Brushes.LightBlue;
            }
            if (mResult[6] != MarkType.Free && (mResult[6] & mResult[7] & mResult[8]) == mResult[6])
            {
                mGameEnd = true;
                button0_2.Background = button1_2.Background = button2_2.Background = Brushes.LightBlue;
            }
            #endregion

            #region Colums
            if (mResult[0] != MarkType.Free && (mResult[0] & mResult[3] & mResult[6]) == mResult[0])
            {
                mGameEnd = true;
                button0_0.Background = button0_1.Background = button0_2.Background = Brushes.LightBlue;
            }
            if (mResult[1] != MarkType.Free && (mResult[1] & mResult[4] & mResult[7]) == mResult[1])
            {
                mGameEnd = true;
                button1_0.Background = button1_1.Background = button1_2.Background = Brushes.LightBlue;
            }
            if (mResult[2] != MarkType.Free && (mResult[2] & mResult[5] & mResult[8]) == mResult[2])
            {
                mGameEnd = true;
                button2_0.Background = button2_1.Background = button2_2.Background = Brushes.LightBlue;
            }
            #endregion
            
            #region Diagonal
            if (mResult[0] != MarkType.Free && (mResult[0] & mResult[4] & mResult[8]) == mResult[0])
            {
                mGameEnd = true;
                button0_0.Background = button1_1.Background = button2_2.Background = Brushes.LightBlue;
            }
            if (mResult[2] != MarkType.Free && (mResult[2] & mResult[4] & mResult[6]) == mResult[2])
            {
                mGameEnd = true;
                button0_2.Background = button2_0.Background = button1_1.Background = Brushes.LightBlue;
            }
            #endregion
            
            #region Tie
            if (!mResult.Any(result => result == MarkType.Free))
            {
                mGameEnd = true;
                Container.Children.Cast<Button>().ToList().ForEach(Button => {
                    Button.Background = Brushes.LightBlue;
                });
            }
            #endregion

            
        }
    }
}
