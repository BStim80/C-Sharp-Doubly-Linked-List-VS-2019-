using System;
using System.Collections.Generic;

namespace C_Sharp_Doubly_Linked_List__VS_2019_
{
    //this class will hold the data that we want to store in the list and a reference to the
    //previous and next nodes in the list
    public class Node
    {
        //private fields 
        private object data;//contains the data stored in the node
        private Node next;//the reference to the next node. if no next node, next = null;
        private Node previous;//reference to the previous node. if no previous node, previous = null;

        //constructor
        public Node(object data, Node next, Node previous)
        {
            this.data = data;
            this.next = next;
            this.previous = previous;
        }

        //Public Properties: we access our private fields with our public properties in order to
        //make changed. Properties act as gatekeepers for private fields (they work together)
        //value is what is actually being stored in the node
        public object Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        public Node Next
        {
            get { return this.next; }
            set { this.next = value; }
        }

        public Node Previous
        {
            get { return this.previous; }
            set { this.previous = value; }
        }
    }

    public class LinkedList
    {
        //Private Fields
        private Node head;//reference to head node
        private Node tail;//reference to tail node
        private int count;//total count of all node in the list

        //Constructor 
        public LinkedList()
        {
            this.head = null;
            this.tail = null;
            this.count = 0;
        }

        //Public Properties
        public bool Empty
        {
            get { return this.count == 0; }
        }

        //I did not need to define count. It is built into the C# library
        public int Count
        {
            get { return this.count -1; }
        }

        //indexer(just another way to use the find method)

        //I wanted an indexer so that I could access the nodes in the list 
        //as if it were an array (by index)
        public object this[int index]
        {
            get { return this.FindByIndex(index); }
        }

        //Methods
        public void InsertAtHead(object itemToInsert)
        {
            Node temp = new Node(itemToInsert, null, null);

            if (Empty)
            {
                temp.Previous = temp;
                temp.Next = temp;
                head = temp;
                tail = temp;
            }
            else
            {
                temp.Next = head;
                head.Previous = temp;
                temp.Previous = null;
                head = temp;
            }
            count++;
        }

        public void InsertByIndex(int index, object itemToInsert)
        {
/*            Node temp = new Node(itemToInsert, null, null);
*/
            //tell Dima that I changed the line below to count + 1 in case I was trying to
            //make new tail. I can change it back to (index < 0 || index > count)
            if (index < 0 || index > count + 1)
                throw new ArgumentOutOfRangeException("Index " + index);

            //adds to beginning. Previous pointer should = null
            else if (this.Empty || index == 0)
            {
                InsertAtHead(itemToInsert);
            }

            //loops to node right before the one we want to insert into
            else
            {
                Node current = this.head;

                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Previous.Next;//adjusts pointers
                }
                current.Next = new Node(itemToInsert, null, current.Previous);
/*                current.Next = new Node(itemToInsert, current.Next, current.Previous);//creates new node, adjusts pointers
*/
            }
            count++;
        }

        public void InsertAtTail(object itemToInsert)
        {
            this.InsertByIndex(Count, itemToInsert);//this may need to be changed to 
                                                    //this.InsertByIndex(Count + 1, itemToInsert)
        }

        public object FindByIndex(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index " + index);

            else if (this.Empty)
                return null;

            else if (index >= this.count)
                index = this.count;

            Node current = this.head;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current.Data;
        }


        //to do: make a for loop to iterate through the list until it finds the reference 
        //that user entered
        //return index where data can be found 
        //return -1 if data is not in the list

        /*
         * [] Initialize a variable, say position, to store the position of the node containing 
         *      data value X in the doubly linked list
         * 
         * [x] Initialize pointer, say temp, to store the head node of the doubly linked list
         * 
         * [] Iterate over the linked list and, for every node, check if data value of that node
         *      is equal to X or not. If found to be true, print position
         * 
         * [x] Otherwise, print -1        
         */


        /*
         * In it's current state, the FindByData method is not working to find integers.
         * It will find string references but does not always return the right index position
         * 5/8 5:00pm
         */
        public int FindByData(object dataToFind)
        {
            Node current = this.head;//gets head node
            //this.count = count - 1;

            int indexPosition = 0;

            for (var i = 0; i < this.count; i++)
            {
                
                if (current.Data.Equals(dataToFind))
                {
                    return indexPosition;
                }
                current = current.Next;
                indexPosition++;
            }
            return -1;
        }

        public object Remove(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index: " + index);

            else if (index > this.count)
                throw new ArgumentOutOfRangeException("index: " + index);

            else if (this.Empty)
                return null;

            Node current = this.head;
            object result = null;

            //removes the head node
            if (index == 0)
            {
                result = current.Data;//get the data from the head node
                this.head = current.Next;//removes reference from head and set it to next node
                                         //garbage collection takes care of removing the former 
                                         //head node for us
            }

            count--;
            return result;
        }

        public void Clear()
        {
            this.head = null;
            this.tail = null;
            this.count = 0;
        }

        public void Print()
        {
            Node current = this.head;

            if (head == null)
            {
                Console.WriteLine("List is empty");
            }

            Console.WriteLine("Nodes of doubly linked list are: ");

            for (int i = 0; i < this.count - 1; i++)
            {
                Console.WriteLine(current.Data + " ");
                current = current.Next;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LinkedList list = new LinkedList();



            list.InsertByIndex(0, "Test1");//1
            list.InsertByIndex(1, "Test3");//2
            list.InsertByIndex(2, "Test4");//3

            list.InsertAtHead("Test5");//0

            list.InsertAtHead("Test6");//4

            list.InsertAtHead("Test7");

            list.InsertAtHead("Test8");

            list.InsertAtTail("Test9");

            Console.WriteLine("All elements in the list: ");
            list.Print();


            Console.WriteLine("Is it empty? " + list.Empty);

            Console.WriteLine("Count: " + list.Count);

            Console.WriteLine("Head Node is: " + list.FindByIndex(0));

            Console.WriteLine("Tail node is: " + list.FindByIndex(6));

            Console.WriteLine("The data you searched for is in index position: " 
                + list.FindByData("Test3"));

            Console.ReadKey();
        }

    }
}
