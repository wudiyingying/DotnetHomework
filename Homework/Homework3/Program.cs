using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework3
{
    internal class Program
    {  
        static void Main(string[] args)
        {
            Shape shape1 = new rectangle(2, 3);
            Console.WriteLine(shape1.getArea());

            Shape shape2 = new Square(2);
            Console.WriteLine(shape2.getArea());

            Shape shape3 = new Triangle(2, 3,4);
            Console.WriteLine(shape3.getArea());

            Shape shape4 = new Triangle(1, 1, 2);

            float total = 0;
            for(int i = 0; i <= 9; i++)
            {
                Shape s=new ShapeFactory(shape1);
                total += s.getArea();
                
            }

            Console.WriteLine(total);
        }
    }

    public interface Shape
    {
        float getArea();
    }

    public class rectangle :Shape
    {
        float width;
        float height;

        public rectangle(float a,float b)
        {
            if (a <= 0 | b <= 0) throw new Exception("illegal param"); 
            width = a;
            height = b;
        }

        float Shape.getArea()
        {
            return width*height;
        }

        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        public float Height
        {
            get => height;
            set => height = value;
        }
    } 

    class Square : rectangle,Shape
    {

        public Square(float a): base(a, a)
        {

        }

        public float edge
        {
            get { return base.Width; }
        }
        float Shape.getArea()
        {
            return Width * Height;
        }
    }

    class Triangle : Shape
    {
        float B;
        float A;
        float C;
        public Triangle(float A,float B,float C)
        {
           
            try
            {
                if (A + B <= C | A + C <= B | B + C <= A) throw new Exception("illegal triangle input");
                if (A <= 0 | B <= 0|C<=0) throw new Exception("illegal param");
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            this.A = A;
            this.B = B;
            this.C = C;
        }

        public float Va
        {
            get { return A; }
        }

        public float Vb
        {
            get
            {
                return B;
            }
        }

        public float Vc
        {
            get
            {
                return C;
            }
        }

        float Shape.getArea()
        {
            double p = (A + B + C) / 2;
            return (float)Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }
    }

    //工厂类
    class ShapeFactory : Shape
    {
        Shape shapeN;

        public ShapeFactory(Shape shape)
        {
            try
            {
                if (shape is rectangle) shapeN = new rectangle(((rectangle)shape).Height, ((rectangle)shape).Width);
                else if (shape is Square) shapeN = new Square(((Square)shape).edge);
                else if (shape is Triangle) shapeN = new Triangle(((Triangle)shape).Va, ((Triangle)shape).Vb, ((Triangle)shape).Vc);
                else throw new Exception("illegal shape");
            }catch(Exception e)
            {
             Console.WriteLine(e.Message);
            }
        }
        float Shape.getArea()
        {
            return shapeN.getArea();
        }
    }
}
