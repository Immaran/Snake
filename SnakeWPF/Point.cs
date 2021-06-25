namespace SnakeWPF
{
    class Point
    {
        public int x;
        public int y;

        public Point()
        {
            x = 0;
            y = 0;
        }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Add(Point point)
        {
            this.x += point.x;
            this.y += point.y;
        }

        public bool Equal(Point point)
        {
            if(this.x == point.x && this.y == point.y)
            {
                return true;
            }
            return false;
        }
    }
}
