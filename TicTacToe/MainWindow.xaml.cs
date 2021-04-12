using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region PrivateMembers

        private MarkType[] mResults;                     //Hold the curent symbols in cells in the active game

        private bool mPlayer1Turn;                       //Players' turn
                                                         
        private bool mGameEnded;                         //End of the game

        #endregion

        #region Constructor

        //Default constuctor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion


        //Starts The new game and clears all cells 
        private void NewGame()
        {
            mResults = new MarkType[9];     //New array of 9 elements for the symbols in cells

            for (var i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }

            mPlayer1Turn = true;            //Make sure the Player has the first move 

            //Iterate every button on the grid......
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Chenge background, text collor and cells content to default
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            //Make sure the game is not ended
            mGameEnded = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Starts the new game if clicked it after the game ended 
            if(mGameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;

            //Find the button in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //Prevents the click after cell already been clicked 
            if (mResults[index] != MarkType.Free)
                return;

            //Set the cell value based in which player turn it is 
            if (mPlayer1Turn)
                mResults[index] = MarkType.Cross;
            else
                mResults[index] = MarkType.Nought;

            //Set button text to result
            button.Content = mPlayer1Turn ? "X" : "O";

            //Change noughts to red
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            //Toggle players turns 
            mPlayer1Turn ^= true;


            //Check for the winner
            CheckForWinner();
        }

        //Check for the winner
        private void CheckForWinner()
        {
            #region Horizontal win check

            //Check for horizontal winns
            //
            // Row 0 Check
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;

                    //Highlight the winning cells in green
                    Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            //
            // Row 1 Check
            //
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;

                    //Highlight the winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            //
            // Row 2 Check
            //
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;

                    //Highlight the winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical win check
            //Check for vertical winns
            //
            // Column 0 Check
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;

                //Highlight the winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            //
            // Column 1 Check
            //
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;

                //Highlight the winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            //
            // Column 2 Check
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;

                //Highlight the winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            #endregion

            #region Diagonal win Check

            //Check fordiagonal winns
            //
            // diagonal 0 Check
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;

                //Highlight the winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            //
            // diagonal 1 Check
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;

                //Highlight the winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            #endregion

            #region No Winner
            //Check for no winner and filled board 
            if (!mResults.Any(result => result == MarkType.Free))
            {
                mGameEnded = true;

                //Turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });

            }
            #endregion
        }
    }
}
