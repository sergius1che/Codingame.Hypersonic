using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber
{
    public class Map
    {
        private int _width;
        private int _height;
        private Point[,] _points;
        private int _maxValue;
        private Dictionary<int, List<Point>> _pointGroups = new Dictionary<int, List<Point>>();

        public Point[,] Points { get { return _points; } }

        public Map(int width, int height)
        {
            this._width = width;
            this._height = height;
            this._points = new Point[width, height];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    this._points[x, y] = new Point(x, y);
        }

        public void UpdateLine(string line, int y)
        {
            char[] pnts = line.ToCharArray();
            for (int x = 0; x < pnts.Length; x++)
                this._points[x, y].BoxPlace = pnts[x] == '0';
        }

        public void KoefRecalc(int radius)
        {
            int max = 1;
            this._pointGroups = new Dictionary<int, List<Point>>();

            for (int y = 0; y < this._height; y++)
                for (int x = 0; x < this._width; x++)
                    if (!this._points[x, y].BoxPlace)
                    {
                        int boxes = GetBoxes(this._points[x, y], radius).Length;
                        this._points[x, y].BoxCount = boxes;
                        if (boxes > max)
                            max = boxes;
                        if (this._pointGroups.ContainsKey(boxes))
                            this._pointGroups[boxes].Add(this._points[x, y]);
                        else
                        {
                            this._pointGroups.Add(boxes, new List<Point>());
                            this._pointGroups[boxes].Add(this._points[x, y]);
                        }
                    }
            this._maxValue = max;
        }

        public Point[] GetBoxes(Point point, int radius)
        {
            int x = point.X;
            int y = point.Y;
            bool u = true, r = true, d = true, l = true;
            List<Point> boxes = new List<Point>();
            for(int i = 1; i < radius; i++)
            {
                if (u && y - i >= 0 && this._points[x, y - i].BoxPlace)
                {
                    boxes.Add(this._points[x, y - i]);
                    u = false;
                }
                if (r && x + i < this._width && this._points[x + i, y].BoxPlace)
                {
                    boxes.Add(this._points[x + i, y]);
                    r = false;
                }
                if (d && y + i < this._height && this._points[x, y + i].BoxPlace)
                {
                    boxes.Add(this._points[x, y + i]);
                    d = false;
                }
                if (l && x - i >= 0 && this._points[x - i, y].BoxPlace)
                {
                    boxes.Add(this._points[x - i, y]);
                    l = false;
                }
            }
            return boxes.ToArray();
        }

        public Point GetNearPoint(Point point, params Point[] withoutPoints)
        {
            return _getNearMaxPoint(point, this._maxValue, withoutPoints);
        }

        public Point GetNearPoint(Point point)
        {
            return GetNearPoint(point, new Point[0]);
        }

        public static double GetLength(Point A, Point B)
        {
            double Xa = (double)A.X;
            double Ya = (double)A.Y;
            double Xb = (double)B.X;
            double Yb = (double)B.Y;

            //return Math.Sqrt(Math.Pow(Math.Abs(Xb - Xa), 2D) + Math.Pow(Math.Abs(Yb - Ya), 2D));
            return Math.Abs(Xb - Xa) + Math.Abs(Yb - Ya);
        }

        private Point _getNearMaxPoint(Point p, int v, Point[] wp)
        {
            List<Point> l = _pointGroups[v];
            Point mp = null;
            double min = double.MaxValue;
            for (int i = 0; i < l.Count; i++)
            {
                if (wp.Any(x => x.Equals(l[i])))
                    continue;
                double ab = GetLength(p, l[i]);
                if (min > ab)
                {
                    min = ab;
                    mp = l[i];
                }
            }
            if (mp != null)
                return mp;
            else if (v > 0)
                return _getNearMaxPoint(p, v - 1, wp);
            else
                return p;

        }
    }

    public class Point : IComparable
    {
        private int _x;
        private int _y;
        private int _boxCount;
        private bool _boxPlace;

        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public int BoxCount
        {
            get
            {
                return _boxCount;
            }
            set
            {
                if (value > 4 || value < 0)
                    throw new Exception("Invalid BoxCount number. Value must between 0 to 4.");
                _boxPlace = false;
                _boxCount = value;
            }
        }
        public bool BoxPlace
        {
            get
            {
                return _boxPlace;
            }
            set
            {
                _boxPlace = value;
                if (value)
                    _boxCount = -1;
                else
                    _boxCount = 0;
            }
        }

        public Point(int x, int y)
        {
            this._x = x;
            this._y = y;
            this._boxCount = 0;
            this._boxPlace = false;
        }

        public int CompareTo(object obj)
        {
            return _boxCount.CompareTo(obj);
        }

        public override bool Equals(object obj)
        {
            if (this == null)
                throw new NullReferenceException();
            Point p = obj as Point;
            if (p != null)
                return this.X == p.X && this.Y == p.Y;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return _x + _y - _boxCount;
        }

        public override string ToString()
        {
            return $"{_x} {_y}";
        }
    }
}
