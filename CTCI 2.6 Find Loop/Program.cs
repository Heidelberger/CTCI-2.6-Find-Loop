using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTCI_2._6_Find_Loop
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeaderMsg(2, 1, "Remove Duplicates");

            Node head = CreateSinglyLinkedList(1000);
            Console.WriteLine("List created");            

            CreateLoop(head, 500);

            FindLoopStart(head);            

            Console.ReadLine();
        }

        /// <summary>
        /// 
        /// 1. Advance two runners through the list until they collide:
        ///    runner_slow advances 1 step per iteration
        ///    runner_fast advances 2 steps per iteration
        /// 2. If end-of-list found, report no cycle
        /// 3. After collision, start new runner from head:
        ///    runner_slow advances from current position 1 step per iteration
        ///    runner_findstart advances from head 1 step per iteration
        /// 4. Above runners will collide at the starting node of the cycle
        /// 
        /// Complexity:     Algorithm requires O(N) time
        ///                 Time grows as input list grows:
        ///                     - runner_slow touches every node once.
        ///                     - runner_fast touches every other node once.
        ///                     - runner_findstart touches every node from head to 
        ///                     - cycle start once.
        ///                     
        ///                 Algorithm requires O(1) memory
        ///                 Memory requirement is constant regardless of input 
        ///                 list size.
        /// 
        /// </summary>
        /// <param name="head"></param>
        private static void FindLoopStart(Node head)
        {
            Stopwatch sw = Stopwatch.StartNew();

            Node runner_slow = head;
            Node runner_fast = head;
            Node runner_findstart = head;
            int count = 0;

            do
            {
                if ((runner_slow.next == null) || (runner_fast.next == null) || (runner_fast.next.next == null))
                {
                    Console.WriteLine("No loop found.");
                    return;
                }

                runner_slow = runner_slow.next;
                runner_fast = runner_fast.next.next;
            } while (runner_slow != runner_fast);

            while (runner_slow != runner_findstart)
            {
                runner_slow = runner_slow.next;
                runner_findstart = runner_findstart.next;
                ++count;
            }

            sw.Stop();

            Console.WriteLine("Cycle detected starting at node " + count + " (Node data: " + runner_slow.Data + ")");
            Console.WriteLine("Ellapsed time: " + sw.ElapsedTicks + " ticks");
        }

        private static void CreateLoop(Node head, int start)
        {
            // find node to start loop
            Node loop_start = head;
            for (int i = 0; i < start; ++i)
            {
                loop_start = loop_start.next;
            }

            // find the last node in the list
            Node loop_end = head;
            while (loop_end.next != null)
            {
                loop_end = loop_end.next;
            }

            // create a loop from the last node to the "start" node
            loop_end.next = loop_start;

            Console.WriteLine("Cycle created starting at node " + start + " (Node data: " + loop_start.Data + ")");
            Console.WriteLine();
        }

        private static Node CreateSinglyLinkedList(int count)
        {
            if (count < 1)
                return null;

            Random rnd = new Random();

            Node head = new Node(rnd.Next(0, 1000));

            Node n = head;

            for (int i = 0; i < count - 1; ++i)
            {
                n.next = new Node(rnd.Next(0, 1000));
                n = n.next;
            }

            return head;
        }

        private static void PrintNodes(Node passed_n)
        {
            while (passed_n != null)
            {
                Console.Write(passed_n.Data + ", ");

                passed_n = passed_n.next;
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void PrintHeaderMsg(int chapter, int problem, string title)
        {
            Console.WriteLine("Cracking the Coding Interview");
            Console.WriteLine("Chapter " + chapter + ", Problem " + chapter + "." + problem + ": " + title);
            Console.WriteLine();
        }
    }

    class Node
    {
        public Node next = null;

        public int Data;

        public Node(int d) => Data = d;

        public void ApppendToTail(int d)
        {
            Node n = this;

            while (n.next != null)
            {
                n = n.next;
            }

            n.next = new Node(d);
        }
    }
    }
