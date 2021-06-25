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

namespace SnakeWPF
{
    /// <summary>
    /// Logika interakcji dla klasy AddScore.xaml
    /// </summary>
    public partial class AddScore : Window
    {
        public AddScore()
        {
            InitializeComponent();
            NickBox.Focus();
        }
        public AddScore(string score)
        {
            InitializeComponent();
            NickBox.Focus();
            ScoreText.Content += score;
        }
        private void ClickOk(object sender, RoutedEventArgs e)
        {
            if(NickBox.Text != "")
            {
                string nick = NickBox.Text;
                
                if(nick.Length == 1)
                {
                    nick += "AA";
                }
                else if(nick.Length == 2)
                {
                    nick += "A";
                }

                Result result = new Result(nick, Game.score);
                ((MainWindow)Application.Current.MainWindow).scoreList.AddResult(result);
                DialogResult = true;

                Game.mode = Mode.graphicMenu;
            }
        }
    }
}
