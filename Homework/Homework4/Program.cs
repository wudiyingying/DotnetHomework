using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //初始化链表
            GenericList<int> genericList = new GenericList<int>();
            for(int i = 0; i < 10; i++)
            {
                genericList.Add(i * 2);
            }

            //依次打印值
            Console.WriteLine("Print num");
            GenericList<int>.ForEach(genericList, m => Console.WriteLine(m));

            Console.WriteLine("Max num");
            int n=genericList.Head.Data;
            GenericList<int>.ForEach(genericList, m =>
            {
                if (m > n) n = m;
            });
            Console.WriteLine(n);

            Console.WriteLine("Min num");
            n = genericList.Head.Data;
            GenericList<int>.ForEach(genericList, m =>
            {
                if (m < n) n = m;
            });
            Console.WriteLine(n);

            Console.WriteLine("everage num");
            double d =0;
            GenericList<int>.ForEach(genericList, m => d += m);
            Console.WriteLine(d/genericList.Length);

        }
    }

    //节点类
    public class Node<T>
    {
        public Node<T> Next { set; get; }
        public T Data { set; get; }

        
        public Node(T t)
        {
            Next = null;
            Data = t;
        }

    }

    //泛型链表类
    public class GenericList<T>
    {
        private Node<T> head;
        private Node<T> tail;
        //链表节点数
        private int length;

        public GenericList()
        {
            tail = head = null;
            length = 0;
        }

        public Node<T> Head {get => head; }
        public int Length { get => length; }

        public void Add(T t){
            Node<T> n = new Node<T>(t);
            length++;
            if (tail == null)
            {
                head = tail = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
        }


        //遍历方法
        public static void ForEach(GenericList<T> g, Action<T> action)
        {
            if (g == null)
            {
                throw new ArgumentNullException("GenericList");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }


            Node<T> node = g.Head; ;
            action(node.Data);
            while (node.Next != null)
            {
                action(node.Next.Data);
                node = node.Next;
            }

        }



    }




}
