using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bomber
{
    class Program
    {
        static void Main(string[] args)
        {
            Srch();
            Console.Read();
        }


        static void Test()
        {
            Stopwatch watch = new Stopwatch();
            Stopwatch swm = new Stopwatch();
            Stopwatch swall = new Stopwatch();

            swall.Start();

            swm.Start();
            Map map = new Map(13, 11);
            swm.Stop();

            watch.Start();
            for (int i = 0; i < 11; i++)
                map.UpdateLine(InitComponents.Map[i], i);
            Point p = new Point(0, 0);
            map.KoefRecalc(3);
            Point near = map.GetNearPoint(p);
            watch.Stop();
            swall.Stop();

            Console.WriteLine($"Init Map:   {swm.Elapsed:c}");
            Console.WriteLine($"Near point: {watch.Elapsed:c}");
            Console.WriteLine($"All time:   {swall.Elapsed:c}");
        }
        static void DrawMap()
        {
            Map map = new Map(13, 11);
            for (int i = 0; i < 11; i++)
                map.UpdateLine(InitComponents.Map[i], i);
            map.KoefRecalc(3);
            for (int y = 0; y < 11; y++)
            {
                for (int x = 0; x < 13; x++)
                {
                    if (map.Points[x, y].BoxPlace)
                        Console.Write($"# ");
                    else
                        Console.Write($"{map.Points[x, y].BoxCount} ");
                }
                Console.WriteLine();
            }
        }
        static void Srch()
        {
            /*Random rnd = new Random(DateTime.Now.Millisecond);
            int x = rnd.Next(0, 13);
            int y = rnd.Next(0, 11);*/
            Point A = new Point(0, 0);
            List<Point> l = new List<Point>();
            Map map = new Map(13, 11);
            for (int i = 0; i < 11; i++)
                map.UpdateLine(InitComponents.Map[i], i);
            for (int i = 0; i < 10; i++)
            {
                map.KoefRecalc(3);
                A = SearchTest(A, l, map);
                l.Add(A);
            }
        }
        static Point SearchTest(Point p, List<Point> l, Map m)
        {
            Point p2 = m.GetNearPoint(p, l.ToArray());
            Point[] boxes = m.GetBoxes(p2, 3);
            for (int i = 0; i < boxes.Length; i++)
                boxes[i].BoxPlace = false;
            Console.WriteLine($"Point A({p}) to Point B({p2}) length: {Map.GetLength(p, p2):0.000}");
            return p2;
        }
    }
}
