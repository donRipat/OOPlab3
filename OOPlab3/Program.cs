using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Point(ref Point p)
        {
            x = p.x;
            y = p.y;
        }

        //  "Draw"
        public override void Draw()
        {
            Console.WriteLine(string.Format("Point::Draw P({0},{1})", x, y));
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
        public Circle(ref Point center, int radius)
        {
            this.center = new Point(ref center);
            this.radius = Math.Abs(radius);
        }
        public Circle(int x, int y, int radius)
        {
            center = new Point(x, y);
            this.radius = Math.Abs(radius);
        }
        public Circle(ref Circle c)
        {
            center = new Point(ref c.center);
            radius = c.radius;
        }

        //  "Draw"
        public override void Draw()
        {
            Console.WriteLine(string.Format("Circle::Draw O({0},{1}); radius = {2}", 
                center.x, center.y, radius));
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
        public Triangle(ref Point a, ref Point b, ref Point c)
        {
            this.a = new Point(ref a);
            this.b = new Point(ref b);
            this.c = new Point(ref c);
        }
        public Triangle(ref Triangle t)
        {
            a = new Point(ref t.a);
            b = new Point(ref t.b);
            c = new Point(ref t.c);
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
            Console.WriteLine(string.Format(
                "Triangle::Draw: A({0},{1}); B({2},{3}); C({4},{5});",
                a.x, a.y, b.x, b.y, c.x, c.y)
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

        public AbsShape Shape => shape;
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
                tail.next = fresh;
                fresh.prev = tail;
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
            }
            else
            {
                tail = fresh;
                head = fresh;
            }
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
            for (current = head; current != null; current = current.next)
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



        public DoublyNode Current => current;
    }


    class Program
    {
        static void Main(string[] args)
        {
            DoublyLinkedList shapes = new DoublyLinkedList();
            Random r = new Random();
            AbsShape sh = null;
            const int n = 10;
            for (int i = 0; i < n; ++i)
            {
                int type = r.Next(0, 3);
                if (type == 0)
                {
                    int x = r.Next(-9, 11);
                    int y = r.Next(-9, 11);
                    sh = new Point(x, y);
                }
                else if (type == 1)
                {
                    int x = r.Next(-9, 11);
                    int y = r.Next(-9, 11);
                    int radius = r.Next(-9, 11);
                    sh = new Circle(x, y, radius);
                }
                else if (type == 2)
                {
                    int ax = r.Next(-9, 11);
                    int ay = r.Next(-9, 11);
                    int bx = r.Next(-9, 11);
                    int by = r.Next(-9, 11);
                    int cx = r.Next(-9, 11);
                    int cy = r.Next(-9, 11);
                    sh = new Triangle(ax, ay, bx, by, cx, cy);
                }
                shapes.Push_back(sh);
                Console.Write("{0}) ", i);
                shapes.Current.Shape.Draw();
            }
            Console.WriteLine("\n");
            shapes.Set_current_first();
            bool t = true;
            for (; t; t = shapes.Step_forward())
            {
                shapes.Current.Shape.Draw();
            }        
            Console.Read();
        }
    }
}
