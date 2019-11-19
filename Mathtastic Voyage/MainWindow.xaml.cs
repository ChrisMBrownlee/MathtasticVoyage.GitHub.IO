using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Mathtastic_Voyage {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private Queue<string> MathQueue = new Queue<string>();
        private MathProblem maths = new MathProblem();
        static int Correct = 0;
        static int Incorrect = 0;

        public MainWindow() {
            InitializeComponent();
            PopulateQuestionList(ImportFile());
            LblShowProblem.Content = $"{MathQueue.Peek()} =";
        }//end constructor

        private void BtnSubmit_Click(object sender, RoutedEventArgs e) {
            TxtAnswer.Background = Brushes.White;
            maths.UserAnswer = TxtAnswer.Text;
            
            maths.ConvertToPostfix(maths.FixInfixLayout(MathQueue.Peek()));
            char[] split_args = { ' ', '\t', '\n' };
            maths.CorrectAnswer = Operation(maths.Postfix.Split(split_args, StringSplitOptions.RemoveEmptyEntries));

            UpdaterEvent();

            CheckEmpty();
        }//end event

        private string[] ImportFile() {
            string path = "C:\\temp\\mathlog.txt";
            StreamReader stream = new StreamReader(path);
            char[] splitters = {'\n', '\r'};
            string[] stream_array = stream.ReadToEnd().Split(splitters, StringSplitOptions.RemoveEmptyEntries);

            return stream_array;            
        }//end method

        private void PopulateQuestionList(string[] questions) {            
            //GO THROUGH EACH STRING AND ADD TO THE MATH PROBLEMS QUEUE
            foreach(string item in questions) {
                MathQueue.Enqueue(item);
                LsbMathProblems.Items.Add(item);
            }//end foreach
        }//end method        

        private void UpdaterEvent() {
            if(maths.CheckUserCorrect()) {
                LsbCorrect.Items.Add($"{MathQueue.Peek()} = {maths.UserAnswer}");
                LsbMathProblems.Items.Remove(MathQueue.Dequeue());
                Correct++;
            } else {
                LsbIncorrect.Items.Add($"{MathQueue.Peek()} = {maths.UserAnswer}");
                LsbMathProblems.Items.Remove(MathQueue.Dequeue());
                Incorrect++;
            }//end if
            if(LsbMathProblems.Items.Count == 0) {
                double total = Correct + Incorrect;
                TxtScore.Text = $"{(((total - Incorrect) / total) * 100)}%";
            }//end if
        }//end 

        private void CheckEmpty() {
            if(!MathQueue.Empty) {
                LblShowProblem.Content = $"{MathQueue.Peek()} =";
            } else {
                BtnSubmit.IsEnabled = false;
                LblShowProblem.Foreground = Brushes.Green;
                LblShowProblem.Content = $"Test Complete";
            }//end if
        }//end method

        private string Operation(string[] data) {
            bool is_valid = maths.StringCheck(data);
            Stack<double> stack_ref = new Stack<double>();

            if(is_valid) {
                foreach(string oper in data) {
                    if(maths.IsNumeric(oper)) {//PUSH OPERAND ONTO STACK 
                        stack_ref.Push(double.Parse(oper));
                    } else if(oper == "+" && stack_ref.Size > 1) {//SEND TO ADD
                        stack_ref.Push(Add(stack_ref.Pop(), stack_ref.Pop()));
                    } else if(oper == "-" && stack_ref.Size > 1) {//SEND TO SUBSTRACT
                        stack_ref.Push(Subtract(stack_ref.Pop(), stack_ref.Pop()));
                    } else if(oper == "/" && stack_ref.Size > 1) {//SEND TO DIVIDE
                        stack_ref.Push(Divide(stack_ref.Pop(), stack_ref.Pop()));                        
                    } else if(oper == "*" && stack_ref.Size > 1) {//SEND TO MULTIPLY
                        stack_ref.Push(Multiply(stack_ref.Pop(), stack_ref.Pop()));                        
                    } else {
                        TxtAnswer.Background = Brushes.Red;
                        return "WRONG INPUT";
                    }//end if
                }//end foreach

                return stack_ref.Peek.ToString();
            } else {
                TxtAnswer.Background = Brushes.Red;
                return "WRONG INPUT";
            }//end if
        }//end method

        private double Add(double operand_1, double operand_2) {
            return operand_2 + operand_1;
        }//end method

        private double Subtract(double operand_1, double operand_2) {
            return operand_2 - operand_1;
        }//end method

        private double Divide(double operand_1, double operand_2) {
            return operand_2 / operand_1;
        }//end method

        private double Multiply(double operand_1, double operand_2) {
            return operand_2 * operand_1;
        }//end method

        
    }//end class
}//end namespace
