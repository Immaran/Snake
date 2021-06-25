using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SnakeWPF
{
    public static class Game
    {
        #region Game fields

        public static MainWindow GM = null;

        public static int[,] array = new int[11, 11];

        public static Mode mode = Mode.txtMenu;

        public static Direction lastDirection = Direction.None;

        public static Direction newDirection = Direction.None;

        public static int txtMode = 0;

        public static char o1, o2, o3, u1, u2, u3, d1, d2, d3;

        public static int score = 0;

        private static Point head = new Point(5, 5);

        private static Point fruit = new Point();

        private static Point vector = new Point();

        private static bool dead = false;

        #endregion

        private static void PirntHead()
        {
            GM.TextModeScreen.Text = "\n\n\t\t\t\t\t\t ____  __ _   __   __ _  ____" +
                                             "\n\t\t\t\t\t\t/ ___)(  ( \\ / _\\ (  / )(  __)" +
                                             "\n\t\t\t\t\t\t\\___ \\/    //    \\ )  (  ) _) " +
                                             "\n\t\t\t\t\t\t(____/\\_)__)\\_/\\_/(__\\_)(____)";
        }

        private static string FormatScore(int score)
        {
            string format = "";

            string temp = score.ToString();

            if (score < 10)
            {
                format += "00" + temp;
            }
            else if (score < 100)
            {
                format += "0" + temp;
            }
            else
            {
                format += temp;
            }

            return format;
        }

        public static void Draw(Mode mode)
        {
            GM.TextModeScreen.Visibility = Visibility.Collapsed;
            GM.GraphicModeMenuScreen.Visibility = Visibility.Collapsed;
            GM.GraphicModeGameScreen.Visibility = Visibility.Collapsed;
            GM.GraphicModeInstructionScreen.Visibility = Visibility.Collapsed;
            GM.GraphicModeScoresScreen.Visibility = Visibility.Collapsed;

            switch (mode)
            {
                case Mode.txtMenu:

                    #region

                    if (txtMode < 9)
                    {
                        txtMode = 101;
                    }
                    if (txtMode % 3 == 2)
                    {
                        o1 = 'X';
                        o2 = ' ';
                        o3 = ' ';

                    }
                    else if (txtMode % 3 == 1)
                    {
                        o1 = ' ';
                        o2 = 'X';
                        o3 = ' ';
                    }
                    else if (txtMode % 3 == 0)
                    {
                        o1 = ' ';
                        o2 = ' ';
                        o3 = 'X';
                    }

                    GM.TextModeScreen.Visibility = Visibility.Visible;

                    Game.PirntHead();

                    GM.TextModeScreen.Text += $"\n\n\n\n\t\t\t\t\t\t[{o1}] Play the game about snake" +
                                              $"\n\n\t\t\t\t\t\t[{o2}] Some hints 4 ya" +
                                              $"\n\n\t\t\t\t\t\t[{o3}] Scores" +
                                              $"\n\n\n\n\t\t\t\t\t\t     E is the key.";

                    break;

                #endregion

                case Mode.txtGame:

                    #region

                    GM.TextModeScreen.Visibility = Visibility.Visible;

                    Game.PirntHead();

                    GM.TextModeScreen.Text += "\n\n\n\t\t\t\t\t\t  \u2554";

                    for (int i = 0; i < 23; i++)
                    {
                        GM.TextModeScreen.Text += "\u2550";
                    }

                    GM.TextModeScreen.Text += "\u2557\n";

                    for (int i = 0; i < 11; i++)
                    {
                        GM.TextModeScreen.Text += "\t\t\t\t\t\t  \u2551 ";

                        for (int j = 0; j < 11; j++)
                        {
                            if (i == head.y && j == head.x)
                            {
                                if(dead)
                                {
                                    GM.TextModeScreen.Text += "X ";
                                }
                                else
                                {
                                    GM.TextModeScreen.Text += "O ";
                                }
                            }
                            else
                            {
                                if (i == fruit.y && j == fruit.x)
                                {
                                    GM.TextModeScreen.Text += "# ";
                                }
                                else if (array[i, j] > 0)
                                {
                                    GM.TextModeScreen.Text += "o ";
                                }
                                else
                                {
                                    GM.TextModeScreen.Text += "  ";
                                }
                            }
                        }

                        GM.TextModeScreen.Text += "\u2551\n";
                    }

                    GM.TextModeScreen.Text += "\t\t\t\t\t\t  \u255A";

                    for (int i = 0; i < 23; i++)
                    {
                        GM.TextModeScreen.Text += "\u2550";
                    }

                    GM.TextModeScreen.Text += "\u255D\n\n";

                    GM.TextModeScreen.Text += "\t\t\t\t\t\t  SCORE : ";

                    GM.TextModeScreen.Text += FormatScore(score);

                    break;

                #endregion

                case Mode.txtInstruction:

                    #region

                    GM.TextModeScreen.Visibility = Visibility.Visible;

                    Game.PirntHead();

                    GM.TextModeScreen.Text += "\n\n\n\n\t\t\t\t\t\t\t  WATCH OUT !" +
                                              "\n\n\t\t\t\t\t\t\t cheat == ban" +
                                              "\n\n\t\t\t\t    use WSAD, E and C to control everything and everyone" +
                                              "\n\n\t\t\t\t\t       it's online. U can't pause it :\\" +
                                              "\n\n\n\t\t\t\t\t\t    press E to back to menu";

                    break;

                #endregion

                case Mode.txtScore:

                    #region

                    GM.TextModeScreen.Visibility = Visibility.Visible;

                    Game.PirntHead();

                    GM.TextModeScreen.Text += "\n\n\n\t\t\t\t\t\t  \u2554";

                    for (int i = 0; i < 23; i++)
                    {
                        GM.TextModeScreen.Text += "\u2550";
                    }

                    GM.TextModeScreen.Text += "\u2557\n";

                    for (int i = 0; i < 11; i++)
                    {
                        GM.TextModeScreen.Text += "\t\t\t\t\t\t  \u2551 ";

                        if (i % 2 == 1 && i > 0)
                        {
                            GM.TextModeScreen.Text += "      ";

                            GM.TextModeScreen.Text += GM.scoreList[(i - 1) / 2].nick;

                            GM.TextModeScreen.Text += "   ";

                            score = GM.scoreList[(i - 1) / 2].score;

                            GM.TextModeScreen.Text += FormatScore(score);

                            GM.TextModeScreen.Text += "       ";
                        }
                        else
                        {
                            for (int j = 0; j < 11; j++)
                            {
                                GM.TextModeScreen.Text += "  ";
                            }
                        }

                        GM.TextModeScreen.Text += "\u2551\n";
                    }

                    GM.TextModeScreen.Text += "\t\t\t\t\t\t  \u255A";

                    for (int i = 0; i < 23; i++)
                    {
                        GM.TextModeScreen.Text += "\u2550";
                    }

                    GM.TextModeScreen.Text += "\u255D";

                    GM.TextModeScreen.Text += "\n\n\t\t\t\t\t\t    hit E to back to menu";

                    break;

                #endregion

                case Mode.txtDead:

                    #region

                    #region ugly stuff

                    if (txtMode < 9)
                    {
                        txtMode = 101;
                    }
                    if (txtMode % 3 == 2)
                    {
                        u1 = '\u25B2';
                        u2 = ' ';
                        u3 = ' ';
                        d1 = '\u25BC';
                        d2 = ' ';
                        d3 = ' ';
                    }
                    if (txtMode % 3 == 1)
                    {
                        u1 = ' ';
                        u2 = '\u25B2';
                        u3 = ' ';
                        d1 = ' ';
                        d2 = '\u25BC';
                        d3 = ' ';
                    }
                    if (txtMode % 3 == 0)
                    {
                        u1 = ' ';
                        u2 = ' ';
                        u3 = '\u25B2';
                        d1 = ' ';
                        d2 = ' ';
                        d3 = '\u25BC';
                    }

                    #endregion

                    GM.TextModeScreen.Visibility = Visibility.Visible;

                    Game.PirntHead();

                    GM.TextModeScreen.Text += "\n\n\n\t\t\t\t\t\t  \u2554";

                    for (int i = 0; i < 23; i++)
                    {
                        GM.TextModeScreen.Text += "\u2550";
                    }

                    GM.TextModeScreen.Text += "\u2557\n";

                    GM.TextModeScreen.Text += "\t\t\t\t\t\t  \u2551                       \u2551\n";
                    GM.TextModeScreen.Text += "\t\t\t\t\t\t  \u2551                       \u2551\n";
                    GM.TextModeScreen.Text += "\t\t\t\t\t\t  \u2551      SCORE:  ";

                    GM.TextModeScreen.Text += FormatScore(score) + "      \u2551\n";

                    GM.TextModeScreen.Text += "\t\t\t\t\t\t  \u2551                       \u2551\n";
                    GM.TextModeScreen.Text += $"\t\t\t\t\t\t  \u2551              {u1}{u2}{u3}      \u2551\n";
                    GM.TextModeScreen.Text += $"\t\t\t\t\t\t  \u2551      NICK:   {o1}{o2}{o3}      \u2551\n";
                    GM.TextModeScreen.Text += $"\t\t\t\t\t\t  \u2551              {d1}{d2}{d3}      \u2551\n";
                    GM.TextModeScreen.Text += "\t\t\t\t\t\t  \u2551                       \u2551\n";
                    GM.TextModeScreen.Text += "\t\t\t\t\t\t  \u2551     PRESS E TO SAVE   \u2551\n";
                    GM.TextModeScreen.Text += "\t\t\t\t\t\t  \u2551                       \u2551\n";
                    GM.TextModeScreen.Text += "\t\t\t\t\t\t  \u2551                       \u2551\n";

                    GM.TextModeScreen.Text += "\t\t\t\t\t\t  \u255A";

                    for (int i = 0; i < 23; i++)
                    {
                        GM.TextModeScreen.Text += "\u2550";
                    }

                    GM.TextModeScreen.Text += "\u255D\n\n";

                    break;

                #endregion

                case Mode.graphicMenu:

                    GM.GraphicModeMenuScreen.Visibility = Visibility.Visible;
                    break;

                case Mode.graphicGame:

                    #region

                    GM.GraphicModeGameScreen.Visibility = Visibility.Visible;

                    var list = GM.GraphicModeSnake.Children.OfType<CheckBox>();

                    for (int i = 0; i < 11; i++)
                    {
                        for (int j = 0; j < 11; j++)
                        {
                            CheckBox CB = list.FirstOrDefault(cb =>
                            {
                                int x = (int)cb.GetValue(Grid.ColumnProperty);
                                int y = (int)cb.GetValue(Grid.RowProperty);

                                if (x == j && y == i)
                                {
                                    return true;
                                }
                                return false;
                            });

                            if (array[i, j] > 0)
                            {
                                if (CB != null)
                                {
                                    CB.IsChecked = null;
                                }
                            }
                            else if (i == head.y && j == head.x)
                            {
                                CB.IsChecked = null;
                            }
                            else if (i == fruit.y && j == fruit.x)
                            {
                                CB.IsChecked = true;
                            }
                            else
                            {
                                if (CB != null)
                                {
                                    CB.IsChecked = false;
                                }
                            }
                        }
                    }

                    GM.GraphicScore.Content = FormatScore(score);

                    break;

                #endregion

                case Mode.graphicInstruction:

                    GM.GraphicModeInstructionScreen.Visibility = Visibility.Visible;

                    break;

                case Mode.graphicScore:

                    #region

                    GM.TextBlock.Text = "";

                    for (int i = 0; i < 5; i++)
                    {
                        GM.TextBlock.Text += GM.scoreList[i].nick + "\t";

                        score = GM.scoreList[i].score;

                        GM.TextBlock.Text += FormatScore(score) + "\n";
                    }
                    GM.GraphicModeScoresScreen.Visibility = Visibility.Visible;

                    break;

                    #endregion
            }
        }

        public static void Reset()
        {
            for(int i = 0; i < 11; i++)
            {
                for(int j = 0; j < 11; j++)
                {
                    array[i, j] = 0;
                }
            }

            score = 0;

            head.x = 5;
            head.y = 5;

            vector.x = 0;
            vector.y = 0;

            dead = false;

            lastDirection = Direction.None;
            newDirection = Direction.None;
        }

        public static void SetVector(int x, int y)
        {
            if(vector.x * x != -1)
            {
                vector.x = x;
            }
            if(vector.y * y != -1)
            {
                vector.y = y;
            }

        }

        private static bool IsFruitTasty()
        {
            if (array[fruit.y, fruit.x] > 0)
            {
                return false;
            }
            else if (fruit.Equal(head))
            {
                return false;
            }
            return true;
        }

        public static void NewFruit()
        {
            Random rnd = new Random();

            do
            {
                fruit.x = rnd.Next(0, 11);
                fruit.y = rnd.Next(0, 11);
            } while (!IsFruitTasty());
        }

        private static void ArrayUpdate()
        {
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (array[i, j] > 0)
                    {
                        array[i, j]--;
                    }
                }
            }
        }

        private static bool WillHit()
        {
            if (head.x + vector.x == -1 || head.x + vector.x == 11 || head.y + vector.y == -1 || head.y + vector.y == 11)
            {
                return true;
            }
            return false;
        }

        private static bool WillBite()
        {
            if (!(head.x + vector.x == -1 || head.x + vector.x == 11 || head.y + vector.y == -1 || head.y + vector.y == 11))
            {
                if (array[head.y + vector.y, head.x + vector.x] > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static void Play(object sender, EventArgs e)
        {
            lastDirection = newDirection;

            if (!WillHit())
            {
                ArrayUpdate();
            }

            array[head.y, head.x] += score;

            if (WillHit() || WillBite())
            {
                GM.timer.Stop();
                dead = true;

                if (WillBite())
                {
                    head.Add(vector);
                }

                o1 = 'A'; o2 = 'A'; o3 = 'A';

                if (GM.scoreList.IsTopFive(score))
                {
                    if(mode == Mode.txtGame)
                    {
                        mode = Mode.txtDead;
                    }
                    else
                    {
                        AddScore addScore = new AddScore(FormatScore(score));
                        addScore.ShowDialog();
                    }
                }
                else
                {
                    if (mode == Mode.txtGame)
                    {
                        mode = Mode.txtMenu;
                    }
                    else
                    {
                        mode = Mode.graphicMenu;
                    }
                }
            }
            else
            {
                head.Add(vector);
            }

            if (head.Equal(fruit))
            {
                score++;
                NewFruit();
            }

            Draw(mode);
        }
    }
}