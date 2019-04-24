/*
 * Conner Warboys
 * April 18th
 * Hangman Summative #3
 * ICS3U
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

namespace _185338Hangman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();
        string word = "";
        int randNumber = 0;
        string LetterGuess = "";
        bool GameOver = false;
        int RemainingGuesses = 4;
        public MainWindow()
        {
            
            InitializeComponent();
            ImageBrush Image = new ImageBrush();
            Image.ImageSource = new BitmapImage(new Uri("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQHlDnW4znI1TrwxpmxqMUTJpQSYialXFx1euPaiCyKt7kN43I0"));
            myCanvas.Background = Image;
            //Setting background image 
            
            startGame(true);
            //telling game to run start insturtions
            randNumber = random.Next(1, 11);
            //picks a random word between 1 and 10
            //MessageBox.Show(randNumber.ToString());

            System.IO.StreamReader sr = new System.IO.StreamReader("Words.txt");
            //Stream reader for reading and choosing a random word from the text file

            int counter = 0;
            string temp = "";
            while (counter != randNumber)
            {
                temp = sr.ReadLine();
                counter++;
                word = temp;

            }//end while
            //MessageBox.Show(temp);
            //Random Number/word 

            string hiddenword = "";
            //MessageBox.Show(word.Length.ToString());
            for (int i = 0; i < word.Length; i++)
            {
                hiddenword += "_ ";
                Console.WriteLine(i.ToString());
            }//end loop
            txbWord.Text = hiddenword;
            //adding an underscore and space to each charactor place from the rando word choosen
        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            LetterGuess = txtInput.Text.ToLower();

            string NewOutput = "";

            bool incorect = true;


            for (int i = 0; i < word.Length; i++)
            {
                string oldLetter = txbWord.Text.ToString();
                string newLetter = word.Substring(i, 1);
                if (newLetter == LetterGuess)
                {
                    NewOutput += LetterGuess + " ";
                    incorect = false;
                }
                else
                {
                    NewOutput += oldLetter.Substring(i * 2, 2);
                }
            }//end for loop
            //Telling program to output the letters guessed 
            

            txbWord.Text = NewOutput;
            lblLetters.Content += LetterGuess;

            if (incorect == true)
            {
                string temp = lblLives.Content.ToString();
                RemainingGuesses--;
                lblLives.Content = "Lives left: " + RemainingGuesses;

                if (RemainingGuesses == 0)
                {
                    MessageBox.Show("You lose" + "\n" + "Play Again");
                    GameOver = true;
                    startGame(GameOver);
                }
            }//end if
            //Telling the program to end game when 0 lives are remaining 
            //where to start lives at 

            bool test = false;
            CheckForWin(test);

        }

        private bool CheckForWin (bool win)
        {
            if (!txbWord.Text.ToString().Contains("_"))
            {
                MessageBox.Show("You Win!!!");
                GameOver = true;
                startGame(GameOver);
                return win = true;
            }
            else
            {
                return win = false;
            }//end if
            //Telling program what to do if all the letters in the word are guessed which is output "You Win!!!

        }

        private bool startGame (bool test)
        {
            if (txbWord.Text.ToString() == word || GameOver == true)
            {
                randNumber = random.Next(1, 11);

                System.IO.StreamReader streamReader = new System.IO.StreamReader("Words.txt");

                int counter = 0;
                while (counter != randNumber)
                {
                    string temp = streamReader.ReadLine();
                    counter++;
                    word = temp;
                }//end while

                string hiddenWord = "";
                for (int i = 0; i < word.Length; i++)
                {
                    hiddenWord += "_ ";
                    Console.WriteLine(i.ToString());
                }
                //Telling the program how to start a game 
                //First select random word from text file with stream reader and then convert it to underscores
                

                txbWord.Text = hiddenWord;
                lblLives.Content = "Lives left : 4";
                RemainingGuesses = 4;
                lblLetters.Content = "Letter's Used: ";
                GameOver = false;
                return test = true;
                //Assinging variables and text for lables
            }
            else
            {
                return test = false;
            }//end if
        }
    }
}
