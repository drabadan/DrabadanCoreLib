﻿using System;
using System.Text.RegularExpressions;

namespace DrabadanCoreLib.Data
{
    /// <summary>
    /// Object expose graphic index ID of gump control in pressed or released state.
    /// </summary>
    public struct Graphic
    {
        /// <summary>
        /// Stores pressed control graphic index ID.
        /// </summary>
        public int Pressed { get; private set; }
        /// <summary>
        /// Stores released control graphic index ID.
        /// </summary>
        public int Released { get; private set; }

        internal Graphic(int p, int r) : this()
        {
            Pressed = p;
            Released = r;
        }
    }

    /// <summary>
    /// Object expose values to determine size of control.
    /// </summary>
    public struct Size
    {
        /// <summary>
        /// Stores width of control.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Stores heigth of control.
        /// </summary>
        public int Heigth { get; private set; }

        internal Size(int h, int w) : this()
        {
            Heigth = h;
            Width = w;
        }
    }

    /// <summary>
    /// Object expose values to determine range between two values.
    /// </summary>
    public struct Range
    {
        /// <summary>
        /// Stores Min Value of control.
        /// </summary>
        public int Min { get; private set; }
        /// <summary>
        /// Stores Max Value of control.
        /// </summary>
        public int Max { get; private set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public Range(int min, int max) : this()
        {
            if (min <= max)
            {
                Min = min;
                Max = max;
            }
            else
            {
                Min = max;
                Max = min;
            }
        }
    }

    /// <summary>
    /// Structure expose the model of a 2 dimensional location.
    /// </summary>
    public struct Point2D : IPoint2D
    {
        /// <summary>
        /// Describes a dummy Point2D
        /// </summary>
        public static readonly Point2D Zero = new Point2D(0, 0);
        internal int m_X;
        internal int m_Y;

        /// <summary>
        /// Default Constructor for instancing Point2D
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point2D(int x, int y)
        {
            m_X = x;
            m_Y = y;
        }

        /// <summary>
        /// Constructor for objects interfaced by IPoint2D
        /// </summary>
        /// <param name="p"></param>
        public Point2D(IPoint2D p)
            : this(p.X, p.Y)
        {
        }

        /// <summary>
        /// Returns x-axis.
        /// </summary>
        public int X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        /// <summary>
        /// Returns y-axis
        /// </summary>
        public int Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }

        /// <summary>
        /// Converts Point2D into proper string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("({0}, {1})", m_X, m_Y);
        }

        /// <summary>
        /// Function tries to parse a string like "123,456" to Point2D.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Point2D Parse(string value)
        {
            var start = value.IndexOf('(');
            var end = value.IndexOf(',', start + 1);

            var param1 = value.Substring(start + 1, end - (start + 1)).Trim();

            start = end;
            end = value.IndexOf(')', start + 1);

            var param2 = value.Substring(start + 1, end - (start + 1)).Trim();

            return new Point2D(Convert.ToInt32(param1), Convert.ToInt32(param2));
        }

        /// <summary>
        /// Allows to compare this with passed point2D. Returns false if not possible.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public override bool Equals(object o)
        {
            if (!(o is IPoint2D))
                return false;

            var p = (IPoint2D) o;

            return m_X == p.X && m_Y == p.Y;
        }

        /// <summary>
        /// Returns Hashcode of object.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return m_X ^ m_Y;
        }

        /// <summary>
        /// Function is for comperation 2 Point2D onto equality
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator ==(Point2D l, Point2D r)
        {
            return l.m_X == r.m_X && l.m_Y == r.m_Y;
        }

        /// <summary>
        /// Function is for comperation 2 Point2D onto unequality
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator !=(Point2D l, Point2D r)
        {
            return l.m_X != r.m_X || l.m_Y != r.m_Y;
        }

        /// <summary>
        /// Function is for comperation 1 Point2D and 1 object interfaced by IPoint2D onto equality
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator ==(Point2D l, IPoint2D r)
        {
            return r != null && l.m_X == r.X && l.m_Y == r.Y;
        }

        /// <summary>
        /// Function is for comperation 1 Point2D and 1 object interfaced by IPoint2D onto unequality
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator !=(Point2D l, IPoint2D r)
        {
            return r != null && (l.m_X != r.X || l.m_Y != r.Y);
        }

        /// <summary>
        /// Function is for comperation 2 Point2D if left Point2D is greater then right point.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator >(Point2D l, Point2D r)
        {
            return l.m_X > r.m_X && l.m_Y > r.m_Y;
        }
        /// <summary>
        /// Function is for comperation 2 Point2D if left Point2D is greater then right point.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator >(Point2D l, Point3D r)
        {
            return l.m_X > r.m_X && l.m_Y > r.m_Y;
        }

        /// <summary>
        /// Function is for comperation 2 Point2D if left Point2D is greater then right object interfaced by IPoint2D.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator >(Point2D l, IPoint2D r)
        {
            return l.m_X > r.X && l.m_Y > r.Y;
        }

        /// <summary>
        /// Function is for comperation 2 Point2D if left Point2D is smaller then right point.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator <(Point2D l, Point2D r)
        {
            return l.m_X < r.m_X && l.m_Y < r.m_Y;
        }

        /// <summary>
        /// Function is for comperation 2 Point2D if left Point2D is smaller then right point.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator <(Point2D l, Point3D r)
        {
            return l.m_X < r.m_X && l.m_Y < r.m_Y;
        }

        /// <summary>
        /// Function is for comperation 2 Point2D if left Point2D is smaller then right object interfaced by IPoint2D.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator <(Point2D l, IPoint2D r)
        {
            return l.m_X < r.X && l.m_Y < r.Y;
        }

        /// <summary>
        /// Function is for comperation 2 Point2D if left Point2D is greater or equal then right point.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator >=(Point2D l, Point2D r)
        {
            return l.m_X >= r.m_X && l.m_Y >= r.m_Y;
        }

        /// <summary>
        /// Function is for comperation 2 Point2D if left Point2D is greater or equal then right point.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator >=(Point2D l, Point3D r)
        {
            return l.m_X >= r.m_X && l.m_Y >= r.m_Y;
        }

        /// <summary>
        /// Function is for comperation 2 Point2D if left Point2D is greater or equal then right object interfaced by IPoint2D.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator >=(Point2D l, IPoint2D r)
        {
            return l.m_X >= r.X && l.m_Y >= r.Y;
        }

        /// <summary>
        /// Function is for comperation 2 Point2D if left Point2D is smaller or equal then right point.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator <=(Point2D l, Point2D r)
        {
            return l.m_X <= r.m_X && l.m_Y <= r.m_Y;
        }

        /// <summary>
        /// Function is for comperation 2 Point2D if left Point2D is smaller or equal then right point.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator <=(Point2D l, Point3D r)
        {
            return l.m_X <= r.m_X && l.m_Y <= r.m_Y;
        }

        /// <summary>
        /// Function is for comperation 2 Point2D if left Point2D is smaller or equal then right object interfaced by IPoint2D.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator <=(Point2D l, IPoint2D r)
        {
            return l.m_X <= r.X && l.m_Y <= r.Y;
        }
    }

    /// <summary>
    /// Structure expose the model of a 3 dimensional location.
    /// </summary>
    public struct Point3D : IPoint3D
    {
        /// <summary>
        /// Describes a dummy Point3D.
        /// </summary>
        public static readonly Point3D Zero = new Point3D(0, 0, 0);
        internal int m_X;
        internal int m_Y;
        internal int m_Z;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Point3D(int x, int y, int z)
        {
            m_X = x;
            m_Y = y;
            m_Z = z;
        }

        /// <summary>
        /// Alternative constructor allows to use an IPoint3D interfaced object as base.
        /// </summary>
        /// <param name="p"></param>
        public Point3D(IPoint3D p) : this(p.X, p.Y, p.Z)
        {
        }

        /// <summary>
        /// Alternative constructor allows to use an IPoint2D interfaced object as base + z-axis.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="z"></param>
        public Point3D(IPoint2D p, int z) : this(p.X, p.Y, z)
        {
        }

        /// <summary>
        /// Returns X.
        /// </summary>
        public int X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        /// <summary>
        /// Returns Y.
        /// </summary>
        public int Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }

        /// <summary>
        /// Returns Z.
        /// </summary>
        public int Z
        {
            get { return m_Z; }
            set { m_Z = value; }
        }

        /// <summary>
        /// Returns object proper parsed to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", m_X, m_Y, m_Z);
        }

        /// <summary>
        /// Allows to compare object with other Point3D.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public override bool Equals(object o)
        {
            if (!(o is IPoint3D))
                return false;

            var p = (IPoint3D) o;

            return m_X == p.X && m_Y == p.Y && m_Z == p.Z;
        }

        /// <summary>
        /// Returns Hashcode of object.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return m_X ^ m_Y ^ m_Z;
        }

        /// <summary>
        /// Try to parse a string and generate a Point3D. Sample Parse("123,456,-7").
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Point3D Parse(string value)
        {
            var start = value.IndexOf('(');
            var end = value.IndexOf(',', start + 1);

            var param1 = value.Substring(start + 1, end - (start + 1)).Trim();

            start = end;
            end = value.IndexOf(',', start + 1);

            var param2 = value.Substring(start + 1, end - (start + 1)).Trim();

            start = end;
            end = value.IndexOf(')', start + 1);

            var param3 = value.Substring(start + 1, end - (start + 1)).Trim();

            return new Point3D(Convert.ToInt32(param1), Convert.ToInt32(param2), Convert.ToInt32(param3));
        }

        /// <summary>
        /// Operand to compare if both values are equal.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator ==(Point3D l, Point3D r)
        {
            return l.m_X == r.m_X && l.m_Y == r.m_Y && l.m_Z == r.m_Z;
        }

        /// <summary>
        /// Operand to compare if both values are unequal.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator !=(Point3D l, Point3D r)
        {
            return l.m_X != r.m_X || l.m_Y != r.m_Y || l.m_Z != r.m_Z;
        }

        /// <summary>
        /// Operand to compare if both values are equal.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator ==(Point3D l, IPoint3D r)
        {
            return r != null && l.m_X == r.X && l.m_Y == r.Y && l.m_Z == r.Z;
        }
        /// <summary>
        /// Operand to compare if both values are unequal.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator !=(Point3D l, IPoint3D r)
        {
            return r != null && (l.m_X != r.X || l.m_Y != r.Y || l.m_Z != r.Z);
        }
    }

    /// <summary> 
    /// Structure expose the model of a 2 dimensional area with start and endpoint.
    /// </summary>
    public struct Rectangle2D
    {
        private Point2D m_End;
        private Point2D m_Start;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public Rectangle2D(IPoint2D start, IPoint2D end)
        {
            m_Start = new Point2D(start);
            m_End = new Point2D(end);
        }

        /// <summary>
        /// Alternate Constructor.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Rectangle2D(int x, int y, int width, int height)
        {
            m_Start = new Point2D(x, y);
            m_End = new Point2D(x + width, y + height);
        }

        /// <summary>
        /// Stores the startpoint.
        /// </summary>
        public Point2D Start
        {
            get { return m_Start; }
            set { m_Start = value; }
        }

        /// <summary>
        /// Stores the endpoint.
        /// </summary>
        public Point2D End
        {
            get { return m_End; }
            set { m_End = value; }
        }

        /// <summary>
        /// Stores X of startpoint.
        /// </summary>
        public int X
        {
            get { return m_Start.m_X; }
            set { m_Start.m_X = value; }
        }

        /// <summary>
        /// Stores Y of startpoint
        /// </summary>
        public int Y
        {
            get { return m_Start.m_Y; }
            set { m_Start.m_Y = value; }
        }

        /// <summary>
        /// Stores witdth of area.
        /// </summary>
        public int Width
        {
            get { return m_End.m_X - m_Start.m_X; }
            set { m_End.m_X = m_Start.m_X + value; }
        }

        /// <summary>
        /// Stores Heigth of area.
        /// </summary>
        public int Height
        {
            get { return m_End.m_Y - m_Start.m_Y; }
            set { m_End.m_Y = m_Start.m_Y + value; }
        }

        /// <summary>
        /// Allows to renew the setting of area.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Set(int x, int y, int width, int height)
        {
            m_Start = new Point2D(x, y);
            m_End = new Point2D(x + width, y + height);
        }

        /// <summary>
        /// Function tries to parse string on format if stored as format "x1,y1,x2,y2".
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Rectangle2D Parse(string value)
        {
            var pattern = new Regex(@"^\(([0-9]+),([0-9]+)\)\+\(([0-9]+),([0-9]+)\)$", RegexOptions.IgnoreCase);

            try
            {
                var match = pattern.Match(value.Replace(" ", ""));

                var x = Convert.ToInt32(match.Groups[1].Value);
                var y = Convert.ToInt32(match.Groups[2].Value);
                var width = Convert.ToInt32(match.Groups[3].Value);
                var height = Convert.ToInt32(match.Groups[4].Value);

                return new Rectangle2D(x, y, width, height);
            }
            catch
            {
                throw new FormatException("value string has not Rectangle2D format");
            }
        }

        /// <summary>
        /// Method allows to resize the area.
        /// </summary>
        /// <param name="r"></param>
        public void MakeHold(Rectangle2D r)
        {
            if (r.m_Start.m_X < m_Start.m_X)
                m_Start.m_X = r.m_Start.m_X;

            if (r.m_Start.m_Y < m_Start.m_Y)
                m_Start.m_Y = r.m_Start.m_Y;

            if (r.m_End.m_X > m_End.m_X)
                m_End.m_X = r.m_End.m_X;

            if (r.m_End.m_Y > m_End.m_Y)
                m_End.m_Y = r.m_End.m_Y;
        }

        /// <summary>
        /// Function returns if point is within area.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Contains(Point3D p)
        {
            return (m_Start.m_X <= p.m_X && m_Start.m_Y <= p.m_Y && m_End.m_X > p.m_X && m_End.m_Y > p.m_Y);
            //return ( m_Start <= p && m_End > p );
        }

        /// <summary>
        /// Function returns if point is within Area
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Contains(Point2D p)
        {
            return (m_Start.m_X <= p.m_X && m_Start.m_Y <= p.m_Y && m_End.m_X > p.m_X && m_End.m_Y > p.m_Y);
            //return ( m_Start <= p && m_End > p );
        }

        /// <summary>
        /// Function returns if point is within area.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Contains(IPoint2D p)
        {
            return (m_Start <= p && m_End > p);
        }

        /// <summary>
        /// Converts object to proper parsed string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("({0}, {1})+({2}, {3})", X, Y, Width, Height);
        }
    }

    /// <summary>
    /// Structure expose the model of a 3 dimensional area with start and endpoint.
    /// </summary>
    public struct Rectangle3D
    {
        private Point3D m_End;
        private Point3D m_Start;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public Rectangle3D(Point3D start, Point3D end)
        {
            m_Start = start;
            m_End = end;
        }

        /// <summary>
        /// Alternative Constructor.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        public Rectangle3D(int x, int y, int z, int width, int height, int depth)
        {
            m_Start = new Point3D(x, y, z);
            m_End = new Point3D(x + width, y + height, z + depth);
        }

        /// <summary>
        /// Stores the startpoint.
        /// </summary>
        public Point3D Start
        {
            get { return m_Start; }
            set { m_Start = value; }
        }

        /// <summary>
        /// Stores the endpoint.
        /// </summary>
        public Point3D End
        {
            get { return m_End; }
            set { m_End = value; }
        }

        /// <summary>
        /// Stores the width.
        /// </summary>
        public int Width
        {
            get { return m_End.X - m_Start.X; }
        }

        /// <summary>
        /// Stores the height.
        /// </summary>
        public int Height
        {
            get { return m_End.Y - m_Start.Y; }
        }
        
        /// <summary>
        /// Stores the depth.
        /// </summary>
        public int Depth
        {
            get { return m_End.Z - m_Start.Z; }
        }

        /// <summary>
        /// Functions checks if a certain point is within that area.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Contains(Point3D p)
        {
            return (p.m_X >= m_Start.m_X)
                   && (p.m_X < m_End.m_X)
                   && (p.m_Y >= m_Start.m_Y)
                   && (p.m_Y < m_End.m_Y)
                   && (p.m_Z >= m_Start.m_Z)
                   && (p.m_Z < m_End.m_Z);
        }

        /// <summary>
        /// Functions checks if a certain point is within that area.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Contains(IPoint3D p)
        {
            return (p.X >= m_Start.m_X)
                   && (p.X < m_End.m_X)
                   && (p.Y >= m_Start.m_Y)
                   && (p.Y < m_End.m_Y)
                   && (p.Z >= m_Start.m_Z)
                   && (p.Z < m_End.m_Z);
        }
    }
}