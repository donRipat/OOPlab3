using System;

namespace OOPlab3
{
    abstract class AbsShape
    {
        public abstract void Draw();
    }
    
    class Point: AbsShape
    {
        public int x;
        public int y;

        //  constructors
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
        public Point(Point p)
        {
            x = p.x;
            y = p.y;
        }

        //  "Draw"
        public override void Draw()
        {
            Console.WriteLine("   Point::Draw: P({0},{1}).", x, y);
        }
    }
        
    class Circle: AbsShape
    {
        private Point center;
        private int radius;

        //  constructors
        public Circle()
        {
            center = new Point();
            radius = 0;
        }
        public Circle(Point center, int radius)
        {
            this.center = new Point(center);
            this.radius = Math.Abs(radius);
        }
        public Circle(int x, int y, int radius)
        {
            center = new Point(x, y);
            this.radius = Math.Abs(radius);
        }
        public Circle(Circle c)
        {
            center = new Point(c.center);
            radius = c.radius;
        }

        //  "Draw"
        public override void Draw()
        {
            Console.WriteLine("  Circle::Draw: O({0},{1}); radius = {2}.",
                center.x, center.y, radius);
        }
    }

    class Triangle:AbsShape
    {
        private Point a;
        private Point b;
        private Point c;

        //  constuctors
        public Triangle()
        {
            a = new Point();
            b = new Point();
            c = new Point();
        }
        public Triangle(Point a, Point b, Point c)
        {
            this.a = new Point(a);
            this.b = new Point(b);
            this.c = new Point(c);
        }
        public Triangle(ref Triangle t)
        {
            a = new Point(t.a);
            b = new Point(t.b);
            c = new Point(t.c);
        }
        public Triangle(int ax, int ay, int bx, int by, int cx, int cy)
        {
            a = new Point(ax, ay);
            b = new Point(bx, by);
            c = new Point(cx, cy);
        }

        //  "Draw"
        public override void Draw()
        {
            Console.WriteLine(
                "Triangle::Draw: A({0},{1}); B({2},{3}); C({4},{5}).",
                a.x, a.y, b.x, b.y, c.x, c.y
                );
        }
    }

    class DoublyNode
    {
        private AbsShape shape;
        public DoublyNode prev;
        public DoublyNode next;

        //  constructor
        public DoublyNode(AbsShape shape)
        {
            this.shape = shape;
            prev = null;
            next = null;
        }

        public AbsShape Shape { get => shape; }
    }

    class DoublyLinkedList
    {
        private int count;
        private DoublyNode head;
        private DoublyNode current;
        private DoublyNode tail;

        //  construsctor
        public DoublyLinkedList()
        {
            head = null;
            tail = null;
            current = null;
            count = 0;
        }

        //  Add new shape to the back of the list
        public bool Push_back(AbsShape shape)
        {
            if (shape == null)
                return false;
            DoublyNode fresh = new DoublyNode(shape);
            if (count > 0)
            {
                fresh.prev = tail;
                tail.next = fresh;
                tail = fresh;
            }
            else
            {
                tail = fresh;
                head = fresh;
            }
            current = fresh;
            ++count;
            return true;
        }

        //  Add new shape to the front of the list
        public bool Push_front(AbsShape shape)
        {
            if (shape == null)
                return false;
            DoublyNode fresh = new DoublyNode(shape);
            if (count > 0)
            {
                head.prev = fresh;
                fresh.next = head;
                head = fresh;
            }
            else
            {
                tail = fresh;
                head = fresh;
            }
            current = fresh;
            ++count;
            return true;
        }

        //  Delete last shape from list
        public bool Delete_last()
        {
            if (tail == null)
                return false;
            if (count > 1)
            {
                tail.prev.next = null;
                tail = tail.prev;
                if (current == null)
                    current = tail;
            }
            else
            {
                tail = null;
                head = null;
                current = null;
            }
            --count;
            return true;
        }

        //  Delete first shape from list
        public bool Delete_first()
        {
            if (head == null)
                return false;
            if (count > 1)
            {
                head.next.prev = null;
                head = head.next;
                if (current == null)
                    current = head;
            }
            else
            {
                tail = null;
                head = null;
                current = null;
            }
            --count;
            return true;
        }

        //  Move current to the next shape
        public bool Step_forward()
        {
            if (current.next == null)
                return false;
            current = current.next;
            return true;
        }

        //  Move current to the previous shape
        public bool Step_back()
        {
            if (current.prev == null)
                return false;
            current = current.prev;
            return true;
        }

        //  Draw all shapes in list
        public bool Draw_whole_list()
        {
            if (count == 0)
                return false;
            current = head;
            for (bool cond = true; cond; cond = Step_forward())
                current.Shape.Draw();
            return true;
        }

        //  Set current to the head
        public bool Set_current_first()
        {
            if (head == null)
            return false;
            current = head;
            return true;
        }

        //  Set current to the tail
        public bool Set_current_last()
        {
            if (tail == null)
                return false;
            current = tail;
            return true;
        }
        
        //  no need to describe
        public AbsShape Get_current_shape()
        {
            if (current == null)
                return null;
            return current.Shape;
        }

        //  Check if list is empty
        public bool Is_empty()
        {
            if (count == 0)
                return true;
            return false;
        }

        //  Search given shape and set current to it if found
        public bool Search(AbsShape s)
        {
            DoublyNode t = current;
            for (bool cond = (!Is_empty()); cond; Step_forward())
                if (Current.Shape == s)
                    return true;
            current = t;
            return false;
        }

        public int Count { get => count; }
        public DoublyNode Current { get => current; }
        public DoublyNode Head { get => head; }
        public DoublyNode Tail { get => tail; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            DoublyLinkedList shapes = new DoublyLinkedList();
            Random r = new Random();
            AbsShape sh = null;
            const int n = 100;
            const int h = 11;
            const int l = -9;

            //  Creating new objects and adding them in list
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();  
            for (int i = 0; i < n; ++i)
            {
                int type = r.Next(0, 3);
                if (type == 0)
                {
                    int x = r.Next(l, h);
                    int y = r.Next(l, h);
                    sh = new Point(x, y);
                }
                else if (type == 1)
                {
                    int x = r.Next(l, h);
                    int y = r.Next(l, h);
                    int radius = r.Next(l, h);
                    sh = new Circle(x, y, radius);
                }
                else if (type == 2)
                {
                    int ax = r.Next(l, h);
                    int ay = r.Next(l, h);
                    int bx = r.Next(l, h);
                    int by = r.Next(l, h);
                    int cx = r.Next(l, h);
                    int cy = r.Next(l, h);
                    sh = new Triangle(ax, ay, bx, by, cx, cy);
                }
                if (i < n / 2)
                    shapes.Push_back(sh);
                else
                    shapes.Push_front(sh);
                shapes.Current.Shape.Draw();
            }
            Console.WriteLine();
            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            Console.WriteLine("Time spent to create and initialize the list: " +
                "{0:00}.{1:000} seconds\n", ts.Seconds, ts.Milliseconds);

            //  walking through the whole  list twice
            timer = new System.Diagnostics.Stopwatch();
            timer.Start();  

            //  walking through whole list from head to tail
            shapes.Set_current_first();
            for (bool cond = (!shapes.Is_empty()); cond; cond = shapes.Step_forward())
                shapes.Current.Shape.Draw();

            Console.WriteLine();

            //  walking through whole list from tail to head
            shapes.Set_current_last();
            for (bool cond = (!shapes.Is_empty()); cond; cond = shapes.Step_back())
                shapes.Current.Shape.Draw();

            timer.Stop();
            ts = timer.Elapsed;
            Console.WriteLine("\nTime spent to walk through and draw the whole list: " +
                "{0:00}.{1:000} seconds\n", ts.Seconds, ts.Milliseconds);

            //  Calling method for all objects
            timer = new System.Diagnostics.Stopwatch();
            timer.Start();  

            shapes.Draw_whole_list();

            timer.Stop();
            ts = timer.Elapsed;
            Console.WriteLine("\nTime spent to draw the whole list: " +
                "{0:00}.{1:000} seconds", ts.Seconds, ts.Milliseconds);
            
            //  Deleting the list node by node
            timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            for (; !shapes.Is_empty(); )
            {
                shapes.Delete_first();
                shapes.Delete_last();
                Console.WriteLine();
                shapes.Draw_whole_list();
            }

            timer.Stop();
            ts = timer.Elapsed;
            Console.WriteLine("\nTime spent to delete the list node by node: " +
                "{0:00}.{1:000} seconds", ts.Seconds, ts.Milliseconds);
            Console.WriteLine("\nList contained {0} nodes\nThe program was finished.", n);
            Console.Read();
        }
    }
}
