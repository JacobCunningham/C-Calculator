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
using System.Drawing;

namespace CSharpCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Display MainDisplay;
        private Calculator calculator;
        private string expression;

        public MainWindow()
        {
            InitializeComponent();
            TextBlock[] textBoxes = { Textbox0, Textbox1, Textbox2, Textbox3, Textbox4 };
            MainDisplay = new Display(textBoxes);
            calculator = new Calculator();
            expression = "";
         }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string number = (string)button.Content;
            expression += number;
            MainDisplay.UpdateDisplay(number);
            
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (expression.Length != 0)
            {
                double result = calculator.Caclulate(expression);
                expression = "";
                MainDisplay.UpdateDisplay(" = " + result.ToString());
                MainDisplay.MoveUp();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MainDisplay.ClearDisplay();
            expression = "";
        }

        private void OperandButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string operand = (string)button.Content;
        
            switch (operand)
            {
                case "+":
                    expression += "+";
                    break;
                case "-":
                    expression += "-";
                    break;
                case "x":
                    expression += "*";
                    break;
                case "/":
                    expression += "/";
                    break;
                case "(":
                    expression += "(";
                    break;
                case ")":
                    expression += ")";
                    break;
                case "sin":
                    expression += "sin";
                    break;
                case "cos":
                    expression += "cos";
                    break;
                case "tan":
                    expression += "tan";
                    break;
                case "x²":
                    expression += "^";
                    break;
                case "log":
                    expression += "log";
                    break;
                case "ln":
                    expression += "ln";
                    break;
                case "e":
                    expression += Math.E;
                    break;
                default:
                    break;
            }
            
            MainDisplay.UpdateDisplay(operand);
        }
        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (expression != "")
            {
                MainDisplay.BackspaceDisplay();
                expression = expression.Remove(expression.Length - 1);
            }
            
        }

        private void PiButton_Click(object sender, RoutedEventArgs e)
        {
            expression += Math.PI;
            MainDisplay.UpdateDisplay("π");
        }
    }
}
