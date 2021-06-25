using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace SnakeWPF
{
    public partial class MainWindow : Window
    {
        public DispatcherTimer timer = null;

        public ScoreList scoreList = new ScoreList();

        private int c = 0;

        public MainWindow()
        {
            Game.GM = this;

            InitializeComponent();

            this.fileManager();

            timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 500), DispatcherPriority.Normal, new EventHandler(Game.Play), Application.Current.Dispatcher);
            timer.Stop();

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    CheckBox CB = new CheckBox();

                    GraphicModeSnake.Children.Add(CB);

                    CB.SetValue(Grid.ColumnProperty, i);
                    CB.SetValue(Grid.RowProperty, j);
                }
            }

            Game.Draw(Mode.txtMenu);
        }

        private void fileManager()
        {
            string path = "../../../Scores.txt";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(new Result());
                    sw.WriteLine(new Result());
                    sw.WriteLine(new Result());
                    sw.WriteLine(new Result());
                    sw.WriteLine(new Result());
                }
            }
            else if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string s, nick;

                    int score, i = 0;

                    while ((s = sr.ReadLine()) != null)
                    {
                        i++;

                        if (i == 6)
                        {
                            sr.Close();

                            using (StreamWriter sw = File.CreateText(path))
                            {
                                foreach (var r in scoreList)
                                {
                                    sw.WriteLine(r);
                                }
                            }

                            break;
                        }

                        string[] data = s.Split(' ');
                        nick = data[0];
                        score = int.Parse(data[1]);
                        scoreList.Add(new Result(nick, score));
                    }
                }
            }
        }

        #region Basic Buttons

        private void TextModeButton_Click(object sender, RoutedEventArgs e)
        {
            Game.txtMode = 0;

            Mode mode = Game.mode;

            if (mode == Mode.graphicMenu)
            {
                mode = Mode.txtMenu;
            }
            else if (mode == Mode.graphicGame)
            {
                mode = Mode.txtGame;
            }
            else if (mode == Mode.graphicInstruction)
            {
                mode = Mode.txtInstruction;
            }
            else if (mode == Mode.graphicScore)
            {
                mode = Mode.txtScore;
            }

            Game.mode = mode;

            Game.Draw(Game.mode);
        }

        private void GraphicModeButton_Click(object sender, RoutedEventArgs e)
        {
            Mode mode = Game.mode;

            if (mode == Mode.txtMenu)
            {
                mode = Mode.graphicMenu;
            }
            else if (mode == Mode.txtGame)
            {
                mode = Mode.graphicGame;
            }
            else if (mode == Mode.txtInstruction)
            {
                mode = Mode.graphicInstruction;
            }
            else if (mode == Mode.txtScore)
            {
                mode = Mode.graphicScore;
            }

            Game.mode = mode;

            Game.Draw(Game.mode);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuPlayButton_Click(object sender, RoutedEventArgs e)
        {
            Game.mode = Mode.graphicGame;

            Game.Reset();

            Game.NewFruit();

            timer.Start();
            
            Game.Draw(Game.mode);
        }

        private void MenuInstructionsButton_Click(object sender, RoutedEventArgs e)
        {
            Game.mode = Mode.graphicInstruction;
            Game.Draw(Game.mode);
        }

        private void MenuScoressButton_Click(object sender, RoutedEventArgs e)
        {
            Game.mode = Mode.graphicScore;
            Game.Draw(Game.mode);
        }

        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Game.mode = Mode.graphicMenu;
            Game.Draw(Game.mode);
        }

        #endregion

        private void KeyPressed(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                #region WSAD

                case Key.W:

                    if (Game.mode == Mode.txtMenu)
                    {
                        Game.txtMode++;
                    }
                    else if (Game.mode == Mode.txtGame || Game.mode == Mode.graphicGame)
                    {
                        if (Game.lastDirection == Direction.Down)
                        {
                            break;
                        }

                        Game.SetVector(0, -1);

                        Game.newDirection = Direction.Up;
                    }
                    else if(Game.mode == Mode.txtDead)
                    {
                        if (Game.txtMode % 3 == 2)
                        {
                            Game.o1++;
                            
                            if(Game.o1 > 'Z')
                            {
                                Game.o1 = 'A';
                            }
                        }
                        if (Game.txtMode % 3 == 1)
                        {
                            Game.o2++;

                            if (Game.o2 > 'Z')
                            {
                                Game.o2 = 'A';
                            }
                        }
                        if (Game.txtMode % 3 == 0)
                        {
                            Game.o3++;

                            if (Game.o3 > 'Z')
                            {
                                Game.o3 = 'A';
                            }
                        }
                    }

                    break;

                case Key.S:

                    if (Game.mode == Mode.txtMenu)
                    {
                        Game.txtMode--;
                    }
                    if (Game.mode == Mode.txtGame || Game.mode == Mode.graphicGame)
                    {
                        if (Game.lastDirection == Direction.Up)
                        {
                            break;
                        }

                        Game.SetVector(0, 1);

                        Game.newDirection = Direction.Down;
                    }
                    if (Game.mode == Mode.txtDead)
                    {
                        if (Game.txtMode % 3 == 2)
                        {
                            Game.o1--;

                            if (Game.o1 < 'A')
                            {
                                Game.o1 = 'Z';
                            }
                        }
                        if (Game.txtMode % 3 == 1)
                        {
                            Game.o2--;

                            if (Game.o2 < 'A')
                            {
                                Game.o2 = 'Z';
                            }
                        }
                        if (Game.txtMode % 3 == 0)
                        {
                            Game.o3--;

                            if (Game.o3 < 'A')
                            {
                                Game.o3 = 'Z';
                            }
                        }
                    }

                    break;

                case Key.D:

                    if (Game.mode == Mode.txtGame || Game.mode == Mode.graphicGame)
                    {
                        if (Game.lastDirection == Direction.Left)
                        {
                            break;
                        }

                        Game.SetVector(1, 0);

                        Game.newDirection = Direction.Right;
                    }
                    if (Game.mode == Mode.txtDead)
                    {
                        Game.txtMode--;
                    }

                    break;

                case Key.A:

                    if (Game.mode == Mode.txtGame || Game.mode == Mode.graphicGame)
                    {
                        if (Game.lastDirection == Direction.Right)
                        {
                            break;
                        }

                        Game.SetVector(-1, 0);

                        Game.newDirection = Direction.Left;
                    }
                    if (Game.mode == Mode.txtDead)
                    {
                        Game.txtMode++;
                    }

                    break;

                #endregion

                case Key.Escape:

                    if (Game.mode == Mode.txtInstruction || Game.mode == Mode.txtScore)
                    {
                        Game.mode = Mode.txtMenu;
                    }
                    else if (Game.mode == Mode.graphicInstruction || Game.mode == Mode.graphicScore)
                    {
                        Game.mode = Mode.graphicMenu;
                    }

                    break;

                case Key.E:

                    if (Game.mode == Mode.txtMenu)
                    {
                        if (Game.txtMode % 3 == 2)
                        {
                            Game.mode = Mode.txtGame;

                            Game.Reset();

                            Game.NewFruit();

                            timer.Start();
                        }
                        if (Game.txtMode % 3 == 1)
                        {
                            Game.mode = Mode.txtInstruction;
                        }
                        if (Game.txtMode % 3 == 0)
                        {
                            Game.mode = Mode.txtScore;
                        }
                    }
                    else if (Game.mode == Mode.txtInstruction || Game.mode == Mode.txtScore)
                    {
                        Game.mode = Mode.txtMenu;
                    }
                    else if (Game.mode == Mode.graphicInstruction || Game.mode == Mode.graphicScore)
                    {
                        Game.mode = Mode.graphicMenu;
                    }
                    else if(Game.mode == Mode.txtDead)
                    {
                        Result result = new Result($"{Game.o1}{Game.o2}{Game.o3}", Game.score);

                        if (scoreList.IsTopFive(Game.score))
                        {
                            scoreList.AddResult(result);
                        }

                        Game.mode = Mode.txtMenu;

                        Game.txtMode = 0;
                    }

                    break;

                case Key.C:

                    ChangeColor(++c);

                    break;

                case Key.I:

                    if(TextModeScreen.Visibility == Visibility.Visible)
                    {
                        GraphicModeButton_Click(sender, e);
                        break;
                    }
                    TextModeButton_Click(sender, e);
                    break;

            }

            Game.Draw(Game.mode);
        }

        private void ChangeColor(int c)
        {
            if( c > 13)
            {
                this.c = 0;
            }

            switch (c)
            {
                case 0:
                    TextModeScreen.Foreground = Brushes.White;
                    break;
                case 1:
                    TextModeScreen.Foreground = Brushes.LightGray;
                    break;
                case 2:
                    TextModeScreen.Foreground = Brushes.LightBlue;
                    break;
                case 3:
                    TextModeScreen.Foreground = Brushes.SkyBlue;
                    break;
                case 4:
                    TextModeScreen.Foreground = Brushes.Blue;
                    break;
                case 5:
                    TextModeScreen.Foreground = Brushes.Purple;
                    break;
                case 6:
                    TextModeScreen.Foreground = Brushes.DeepPink;
                    break;
                case 7:
                    TextModeScreen.Foreground = Brushes.Red;
                    break;
                case 8:
                    TextModeScreen.Foreground = Brushes.Brown;
                    break;
                case 9:
                    TextModeScreen.Foreground = Brushes.Orange;
                    break;
                case 10:
                    TextModeScreen.Foreground = Brushes.Yellow;
                    break;
                case 11:
                    TextModeScreen.Foreground = Brushes.LightGreen;
                    break;
                case 12:
                    TextModeScreen.Foreground = Brushes.Green;
                    break;
                case 13:
                    TextModeScreen.Foreground = Brushes.Gold;
                    break;
            }

        }
    }
}
