using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathtastic_Voyage {
    class MathProblem {
        private string  _infix;
        private string  _postfix;
        private string  _correct_answer;
        private string  _user_answer;
        private bool    _is_correct;

        //CONSTRUCTOR
        public MathProblem(string data) {
            _infix = data;
            _postfix = "";
        }//end constructor

        public MathProblem() {
            _infix = "";
            _postfix = "";
        }//end constructor
        
        //PROPERTY
        public string CorrectAnswer {
            get { return _correct_answer; }
            set { _correct_answer = value; }
        }//end property

        public string UserAnswer {
            get { return _user_answer; }
            set { _user_answer = value; }
        }//end property

        public bool IsCorrect {
            get { return _is_correct; }
            set { _is_correct = value; }
        }//end property

        public string Infix {
            get { return _infix; }
            set { _infix = value; }
        }//end property

        public string Postfix {
            get { return _postfix; }
            set { _postfix = value; }
        }//end property

        //METHODS
        public string[] FixInfixLayout(string infix) {
            char[] infix_char_array = new char[infix.Length];
            infix_char_array = infix.ToCharArray();
            string final_infix = "";
            int count = 0;

            foreach(char item in infix_char_array) {//CYCLE THROUGH THE CHARS TO ADD SPACES WHERE NEEDED
                if(item == '-' && infix_char_array[count + 1] >= 0 || item == '-' && infix_char_array[count + 1] <= 9) {
                    final_infix += $" {item}";
                } else if (item == '+' || item == '-' || item == '/' || item == '*' || item == '^' || item == '(' || item == ')') {
                    final_infix += $" {item} ";
                } else {
                    final_infix += $"{item}";
                }//end if
                count++;
            }//end foreach

            //SET INFIX
            Infix = final_infix;
            //SPLIT AND REMOVE UNNECESSARY SPACES, TABS, OR RETURNS
            char[] seperators = { ' ', '\t', '\n', };
            string[] infix_array = Infix.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

            return infix_array;
        }//end method

        public void ConvertToPostfix(string[] infix) {            
            Stack<string> Stack = new Stack<string>();
            Postfix = "";

            for(int index = 0; index < infix.Length; index++) {
                if(IsOperator(infix[index])) {//THEN
                    //LOOP UNTIL STACK IS EMPTY, OR LEFT PARENTHESIS, AND IF HAS HIGHER PRECEDENCE
                    while(!Stack.Empty && Stack.Peek != "(" && HasHigherPrecedence(Stack.Peek, infix[index])) {
                        //ADD TO STRING
                        Postfix += Stack.Pop();
                    }//end while
                    //PUSH CHARACTER
                    Stack.Push(infix[index]);
                } else if (IsNumeric(infix[index])) {//THEN
                    //ADD TO STRING
                    Postfix += $" {infix[index]} ";
                } else if (IsLeftParenthesis(infix[index])) {//THEN
                    //PUSH ON STACK
                    Stack.Push(infix[index]);
                } else if (IsRightParenthesis(infix[index])) {//THEN
                    //LOOP UNTIL STACK IS EMPTY OR MEETS LEFT PARENTHESIS
                    while (Stack.Peek != null && Stack.Peek != "(") {
                        //ADD TO STRING
                        Postfix += $" {Stack.Pop()} ";
                    }//end while
                    //THIS POPS OFF LEFT PARENTHESIS
                    Stack.Pop();
                }//end if
            }//end for

            while(!Stack.Empty) {//EMPTY STACK
                Postfix += $" {Stack.Pop()} ";
            }//end while
        }//end method                

        private bool HasHigherPrecedence(string op1, string op2) {
            int op1Weight = GetOperatorWeight(op1);
            int op2Weight = GetOperatorWeight(op2);
            
            //IF OPERATORS HAVE EQUAL PRECEDENCE RETURNS TRUE IF LEFT ASSOCIATIVE AND RETURNS FALSE IF RIGHT ASSOCIATIVE.
            //IF OPERATOR IS LEFT ASSOCIATIVE THEN LEFT OPERATOR IS GIVEN PRIORITY.
            if(op1Weight == op2Weight) {
                if(IsRightAssociative(op1)) return false;
                else return true;
            }//end if
            return op1Weight > op2Weight ? true : false;
        }//end method

        private bool IsRightAssociative(string op) {
            if(op == "^") {
                return true;
            }//end if
            return false;
        }//end method

        private int GetOperatorWeight(string op) {
            int weight = -1;

            //GET WEIGHT OF OPERATORS
            if(op == "+" || op == "-") {
                weight = 1;
            } else if (op == "*" || op == "/") {
                weight = 2;
            } else if (op == "^") {
                weight = 3;
            }//end if

            return weight;
        }//end method        

        public bool IsNumeric(string data) {
            double temp;
            return double.TryParse(data, out temp);
        }//end method

        private bool IsOperator(string data) {
            if(data == "+" || data == "-" || data == "*" || data == "/" || data == "^") { return true; }
            return false;
        }//end method

        private bool IsLeftParenthesis(string data) {
            if(data == "(") {
                return true;
            }//end if
            return false;
        }//end method

        private bool IsRightParenthesis(string data) {
            if(data == ")") {
                return true;
            }//end if
            return false;
        }//end method

        public bool CheckUserCorrect() {
            if(UserAnswer == CorrectAnswer) {
                return true;
            } else {
                return false;
            }//end if
        }//end method

        public bool StringCheck(string[] data) {
            bool is_valid = false;

            if(data.Length < 3) {
                is_valid = false;
            } else {
                is_valid = CheckIfValid(data);
            }//end if
            return is_valid;
        }//end method

        private bool CheckIfValid(string[] data) {
            int num_of_nums = 0;
            int num_of_opers = 0;
            int count = 1;

            foreach(string oper in data) {
                if(count == 1 && !IsNumeric(oper) || count == 2 && !IsNumeric(oper)) {
                    return false;
                }//end if
                count++;
            }//end foreach

            foreach(string oper in data) {
                if(IsNumeric(oper)) {
                    num_of_nums++;
                } else if(oper == "+" || oper == "-" || oper == "/" || oper == "*") {
                    num_of_opers++;
                } else {
                    return false;
                }//end if
            }//end foreach
            return (num_of_nums - num_of_opers == 1);
        }//end method


    }//end class
}//end namespace
