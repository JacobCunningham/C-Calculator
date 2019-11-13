using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;

namespace CSharpCalculator
{
    class Calculator
    {

        private List<string> operators = new List<string>(new string[] { "-", "+", "/", "*" });
        private List<string> _leftAssociatedOperators = new List<string>(new string[] { "-", "+", "/", "*" });
        private List<string> functions = new List<string>(new string[] { "sin", "cos", "tan","^","ln","log", "S" });

        public Queue<string> ParseExpression(string input)
        {
            /*
             * Implementation of the Shunting Yard Algorithm.
             * token: single symbol from input tring, i.e. number, operand, paren
             * operatorStack: stores operators until they are ready to be inserted into the infix Expresstion
             * 
             */
            Stack<string> operatorStack = new Stack<string>();
            Queue<string> infixExpression = new Queue<string>();  // Where we store the final result, to be handed to infix evaluator function
            string[] tokens = Regex.Split(input, @"([-+/*()])");  // Splits input string into array of strings, keeping operands
           

            foreach (var token in tokens)  
            {
       
                if (double.TryParse(token,out double n))
                {
                    infixExpression.Enqueue(token);
                }
                else if (functions.Contains(token))
                {
                    operatorStack.Push(token);
                }
                else if (operators.Contains(token))
                {
                        // While operator stack isn't empty, the token isn't a left paren, and the precedence of the top of the op stack is greater than the token or equal too and is left associative
                        while ((operatorStack.Count > 0) && 
                              (functions.Contains(operatorStack.Peek()) || (operators.FindIndex(op => op == operatorStack.Peek()) > operators.FindIndex(t => t == token)) ||
                               (operators.FindIndex(op => op == operatorStack.Peek()) == operators.FindIndex(t => t == token)) && _leftAssociatedOperators.Contains(token)) && (token != "("))
                        {
                            infixExpression.Enqueue(operatorStack.Pop());
                        }
                    operatorStack.Push(token);
                }

                if (token == "(")
                {
                    operatorStack.Push(token);
                }
                else if (token == ")")
                {
                    while (operatorStack.Peek() != "(")
                    {
                        infixExpression.Enqueue(operatorStack.Pop());
                    }
                    if (operatorStack.Peek() == "(")
                    {
                        operatorStack.Pop();
                        if (functions.Contains(operatorStack.Peek()))
                        {
                            infixExpression.Enqueue(operatorStack.Pop());
                        }
                    }
                }
            }
            while (operatorStack.Count != 0)
            {
                infixExpression.Enqueue(operatorStack.Pop());
            }
            return infixExpression;
        }


        private double evaluateInfix(Queue<string> inputInfix)
        {
            Stack<double> expressionStack = new Stack<double>();
            while (inputInfix.Count > 0)
            {
                if (double.TryParse(inputInfix.Peek(), out double d))
                {
                    expressionStack.Push(double.Parse(inputInfix.Dequeue()));
                }
                else if (operators.Contains(inputInfix.Peek()) || functions.Contains(inputInfix.Peek()))
                {
                    string operand = inputInfix.Dequeue();
                    double result;
                    if (functions.Contains(operand))
                    {
                        double param = expressionStack.Pop();
                        result = _evaluateFunction(operand, param);
                    }
                    else
                    {
                        double rho = expressionStack.Pop();
                        double lho = expressionStack.Pop();
                        result = _evaluateArithmeticExpression(lho, rho, operand);
                    }
                    
                    expressionStack.Push(result);
                }
            }
            return expressionStack.Pop();
        }

        private double _evaluateArithmeticExpression(double lho, double rho, string op)
        {
            switch (op)
            {

                case "+":
                    return lho + rho;
                case "-":
                    return lho - rho;
                case "*":
                    return lho * rho;
                case "/":
                    return lho / rho;
                case "^":
                    return Math.Pow(lho, rho);
                default:
                    break;
            }
            return 0;
        }

        private double _evaluateFunction(string function, double args)
        {
            switch (function)
            {
                case "sin":
                    return Math.Sin(args);
                case "cos":
                    return Math.Cos(args);
                case "tan":
                    return Math.Tan(args);
                case "log":
                    return Math.Log10(args);
                case "ln":
                    return Math.Log(args);
                case "S":
                    return Math.Sqrt(args);
                default:
                    break;
            }
            return 0;
        }

        public double Caclulate(string inputExpression)
        {
            Queue<string> infixExpression = ParseExpression(inputExpression);
            return evaluateInfix(infixExpression);
        }

    }
}
