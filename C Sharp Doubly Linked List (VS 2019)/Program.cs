using System;
using System.Collections.Generic;

namespace C_Sharp_Doubly_Linked_List__VS_2019_
{
    //This is the final build before any refactoring. 

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
        //make changes. Properties act as gatekeepers for private fields (they work together)
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


    //This class is where I will make the list and all of the methods
    public class LinkedList
    {
        //Private Fields
        private Node head;//reference to head node
        private Node tail;//reference to tail node
        private int count;//total count of all node in the list

        //Constructor 
        //This initializes the private fields
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
            get { return this.count - 1; }//look into this
        }

        //indexer(just another way to use the find method)

        //I wanted an indexer so that I could access the nodes in the list 
        //as if it were an array (by index)
        public object this[int index]
        {
            get { return this.FindByIndex(index); }
        }

        //Methods

        //This method creates a new head node. Parameters require you to specify the data that
        //you would like to store in the new head node
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

            //Adds one to the count every time we add a node to the list
            count++;
        }

        //This method will insert a node anywhere in the list. Parameters require you to 
        //specify which index you would like to insert into and the data that you would like 
        //the node to store
        public void InsertByIndex(int index, object itemToInsert)
        {
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
                    current = current.Next;
                }
                current.Next = new Node(itemToInsert, current, current);
            }

            //Adds one to the count every time we add a node to the list
            count++;
        }

        //This method creates a new tail node. Parameters require you to specify the data that
        //you would like to store in the new tail node
        public void InsertAtTail(object itemToInsert)
        {
            this.InsertByIndex(Count, itemToInsert);
        }

        //This method will search the list and return the data stored in whichever node you
        //specify (by index)
        public object FindByIndex(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index " + index);

            else if (this.Empty)
                return null;

            //this finds the tail
            else if (index >= this.count)
                index = this.count;

            //The for loop below will iterate through the list until it finds the index you
            //entered in the parameters. It will return the data that is stored in the 
            //specified node

            Node current = this.head;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current.Data;
        }


        //This method will search through the nodes in the list until it finds the data 
        //(String reference) that you specified in the parameters. It will return the index 
        //position of the node that the dataToFind is stored in.
        public int FindByData(object dataToFind)
        {
            Node current = this.head;

            int indexPosition = 0;

            for (var i = 0; i < this.count; i++)
            {
                
                if (current.Data.Equals(dataToFind))
                {
                    return indexPosition;
                }
                current = current.Next;
                
                //every time we loop through the list, this will increment indexPostion
                indexPosition++;
            }

            //If dataToFind is not found in the list, this method will return - 1
            return -1;
        }

        //This method will remove a node anywhere in the list. It requires you to specify the
        //index of the node that you would like to remove in the parameters. 
        public object RemoveByIndex(int indexToRemove)
        {
            Node current = this.head;
            object result = null;

            if (indexToRemove < 0)
                throw new ArgumentOutOfRangeException("index: " + indexToRemove);

            else if (indexToRemove > this.count)
                throw new ArgumentOutOfRangeException("index: " + indexToRemove);

            else if (this.Empty)
                throw new ArgumentOutOfRangeException("index: " + indexToRemove);

            //removes the head node
            else if (indexToRemove == 0)
            {
                //get the data from the head node
                //I set result to equal null
                result = current.Data;

                //removes reference from head and set it to next node
                this.head = current.Next;

                //garbage collection takes care of removing the former 
                //head node for us
                current.Previous = null; 
            }

            //removes tail node  
            else if (indexToRemove + 1 == count)
            {
                result = current.Data;
                this.tail = current.Previous;
                current.Next = null;
            }

            //removes node in the middle
            else
            {
                for (int i = 0; i <= indexToRemove - 1; i++)
                {
                    current = current.Next;
                }
                result = current.Data;
                current.Next.Previous = current.Previous;
                current.Previous.Next = current.Next;
            }

            //removes one from count every time a node is removed
            count--;

            return result;
        }

        //This method removes all nodes from the list. We set head and tail to = null and set 
        //the count to zero. Garbage collection handles the rest
        public void Clear()
        {
            this.head = null;
            this.tail = null;
            this.count = 0;
        }

        //This method prints all of the nodes in the list in order
        public void Print()
        {
            //this finds the head node for when we loop through the list
            Node current = this.head;

            //If list is empty:
            if (head == null)
            {
                Console.WriteLine("List is empty");
            }

            //If list is not empty:
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



            list.InsertByIndex(0, "Test1");
            list.InsertByIndex(1, "Test2");
            list.InsertByIndex(2, "Test3");


            list.InsertByIndex(3, "Test4");

            list.InsertByIndex(4, "Test5");
            list.InsertByIndex(5, "Test6");
            list.InsertByIndex(6, "Test7");

            list.InsertAtHead("Test0");

            list.InsertAtTail("Test8");

            //list.RemoveByIndex(3);

            //list.Clear();

            list.Print();

            Console.WriteLine("Is it empty? " + list.Empty);

            Console.WriteLine("Count: " + list.Count);

            Console.WriteLine("Head Node is: " + list.FindByIndex(0));

            Console.WriteLine("Tail node is: " + list.FindByIndex(99));

            Console.WriteLine("The data you searched for is in index position: " 
                + list.FindByData("Test3"));

            Console.ReadKey();
        }

    }
}
