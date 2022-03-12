using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Clock c = new Clock(0,1);
            c.setAlarm(0, 45);

            ClockEvent clockEvent = new ClockEvent();

            c.ClockEvent1 += clockEvent.tick;
            c.ClockEvent2 += clockEvent.alarm;

            c.start();
        }
    }

    class Clock
    {
        private int min = 0;
        private int hour = 0;

        private int alarmMin = 0;
        private int alarmHour = 0;

        public int Min { get => min; }
        public int Hour { get => hour; }

        public delegate void ClockHandler(int h,int m);
        public event ClockHandler ClockEvent1;
        public event ClockHandler ClockEvent2;
        public Clock(int h,int m)
        {
            min = m;
            hour = h;
        }

        public void setAlarm(int h, int m)
        {
            alarmMin = m;
            alarmHour = h;
        }

        public void start()
        {
            for(int i=0; i<12; i++)
            {
                hour = i;

                for(int j = 0; j < 60; j++)
                {
                    System.Threading.Thread.Sleep(500);
                    min = j;
                    if (ClockEvent1 != null) ClockEvent1(i, j);
                    if (i == alarmHour && j == alarmMin && ClockEvent2 != null) ClockEvent2(i,j);
                }

            }
        }


    }

    class ClockEvent
    {
        public void tick(int i,int j)
        {
            Console.WriteLine($"Tick! Time is {i} : {j}", i, j);
        }

        public void alarm(int i,int j)
        {
            Console.WriteLine($"Alarm! Time is {i} : {j}", i, j);
        }
    }

   
}
