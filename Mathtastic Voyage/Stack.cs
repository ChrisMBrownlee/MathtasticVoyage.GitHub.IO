using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathtastic_Voyage {
    class Stack<T> {
        #region FIELDS
        private Node<T> _tail;
        private int _size = 0;
        #endregion

        #region CONSTRUCTORS
        public Stack() {
            _tail = null;
        }//end constructor

        public Stack(T new_data) {
            _tail = new Node<T>(new_data);
            _size++;
        }//end constructor
        #endregion

        #region PROPERTY
        public int Size {
            get { return _size; }
        }//end property

        public bool Empty {
            get { return _size == 0; }
        }//end property

        public T Peek {
            get { return _tail.Data; }
        }//end property
        #endregion

        #region METHODS
        //PUSH - ADD NEW VALUE TO STACK
        public void Push(T value) {
            Node<T> new_node = new Node<T>(value);
            new_node.Next = _tail;
            _tail = new_node;
            _size++;
        }//end method

        //POP - REMOVE TOP VALUE FROM STACK
        public T Pop() {
            if(!Empty) {
                //STORE TAILS DATA
                T return_data = _tail.Data;

                //REMOVE NODE
                _tail = _tail.Next;
                _size--;

                return return_data;
            } else {
                throw new IndexOutOfRangeException("Can not remove if stack is EMPTY");
            }//end if            
        }//end method
        #endregion        
    }//end class
}
