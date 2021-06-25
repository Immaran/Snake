using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWPF
{
    public class Result
    {
        public string nick { get; }
        public int score { get; }

        public Result()
        {
            this.nick = "AAA";
            this.score = 0;
        }

        public Result(string nick, int score)
        {
            this.nick = nick;
            this.score = score;
        }

        public override string ToString()
        {
            return nick + " " + score.ToString();
        }

    }
}
