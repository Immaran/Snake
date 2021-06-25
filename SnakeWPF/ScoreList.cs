using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnakeWPF
{
    public class ScoreList : List<Result>
    {
        private string path = "../../../Scores.txt";

        public ScoreList()
        {
            base.Add(new Result());
            base.Add(new Result());
            base.Add(new Result());
            base.Add(new Result());
            base.Add(new Result());
        }

        public bool IsTopFive(int score)
        {
            if (score > this.Last().score)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public new void Add(Result result)
        {
            this.SortList();
            this.Remove(this.Last());
            base.Add(result);
        }
        public void AddResult(Result result)
        {
            this.Remove(this.Last());
            base.Add(result);
            this.SortList();

            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach(var item in this)
                {
                    sw.WriteLine(item);
                }
            }
        }
        private void SortList()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    if (this[j].score > this[j - 1].score)
                    {
                        Result tmp = this[j];
                        this[j] = this[j - 1];
                        this[j - 1] = tmp;
                    }
                }
            }
        }
    }
}
