using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathtastic_Voyage {
    class Queue<T> {
        //FIELDS
        private Node<T> _head;
        private Node<T> _tail;
        private int _size = 0;

        //CONSTRUCTORS
        public Queue() {
            _head = null;
            _tail = null;
            _size = 0;
        }//end constructor

        public Queue(T new_data) {
            _head = new Node<T>(new_data);
            _tail = _head;
            _size++;
        }//end constructor

        //PROPERTIES
        public bool Empty {
            get { return _head == null; }
        }//end property

        public int Size {
            get { return _size; }
        }//end property

        //METHODS
        public void Enqueue(T data) {
            if(_head == null) {//then 
                _head = new Node<T>(data);
                _tail = _head;
                _size++;
            } else {
                _tail.Next = new Node<T>(data);
                _tail = _tail.Next;
                _size++;
            }//end if
        }//end method

        public T Dequeue() {
            T return_data = _head.Data;

            _head = _head.Next;

            _size--;

            return return_data;
        }//end method

        public T Peek() {
            if(!Empty) {
                return _head.Data;
            } else {
                throw new Exception("Queue is empty");
            }//end if
        }//end method

        public void Clear() {
            _head = null;
            _size = 0;
            _tail = null;
        }//end method

        private T Get(int data_location) {
            int count = 0;

            //START AT HEAD
            Node<T> current_node = _head;

            if(data_location < 0) {
                throw new IndexOutOfRangeException($"Index can not be a negative number");
            } else {
                while(current_node != null) {
                    if(count == data_location) {
                        return current_node.Data;
                    }//end if

                    current_node = current_node.Next;
                    count++;
                }//end while
                throw new IndexOutOfRangeException($"Index [{data_location}] was not found in the Linked Lists. Index holds index 0 through {_size}.");
            }//end if
        }//end method

        //OVERRIDES
        public override string ToString() {
            string list_all = "";
            string word;

            for(int i = _size - 1; i >= 0; i--) {
                word = Convert.ToString(Get(i));
                if(i >= 1) {
                    list_all += $"{word}, ";
                } else {
                    list_all += $"{word}";
                }//end if
            }//end for

            return $"{list_all}";
        }//end method

    }//end class
}
