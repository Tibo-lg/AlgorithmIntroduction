using System;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace AlgorithmIntroduction.Dijkstra
{
    public class Map
    {
        public Map()
        {
        }

        public Point[,] Points { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Point CurrentPosition { get; set; }

        public void Load(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                //Ignore first line
                streamReader.ReadLine();
                // Read Height
                Height = int.Parse(streamReader.ReadLine().Split(' ')[1]);
                // Read Width
                Width = int.Parse(streamReader.ReadLine().Split(' ')[1]);
                //Ignore 'map'
                streamReader.ReadLine();

                Points = new Point[Height,Width];

                int y = 0;
                while (!streamReader.EndOfStream)
                {
                    int x = 0;
                    var line = streamReader.ReadLine();
                    foreach (char c in line)
                    {
                        Points[y, x] = new Point(x, y, Point.CharToType(c));
                        x++;
                    }
                    y++;
                }
            }

            SetCurrentPosition();
        }

        public void SetCurrentPosition()
        {
            var rand = new Random();
            do
            {
                int x = rand.Next(Width - 1);
                int y = rand.Next(Height - 1);
                CurrentPosition = Points[y, x];
            } while (!Point.IsPassable(CurrentPosition));
        }
    }
}
