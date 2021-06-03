using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;

namespace Calc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string input = string.Empty;
        string expression = string.Empty;
        private bool resultGot = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private string FromDecimal(double num)
        {
            int sign = (num < 0) ? -1 : 1;
            num = Math.Abs(num);
            int zel = (int)Math.Truncate(num);
            double FracVal = num - zel;
            string StrInt = "";
            do
            {
                StrInt = (zel % 7) + StrInt;
                zel = zel / 7;
            } while (zel != 0);

            if (FracVal != 0)
            {
                string FracPart = "";
                int tmp;
                while (FracVal > 0 && FracPart.Length <= 5)
                {
                    FracVal = FracVal * 7;
                    tmp = (int)Math.Truncate(FracVal);
                    FracPart = FracPart + tmp;
                    FracVal = FracVal - tmp;
                }
                StrInt = StrInt + "," + Convert.ToString(FracPart);
            }
            return (sign == -1) ? "-" + StrInt : StrInt;
        }

        private void BClick(object sender, RoutedEventArgs e) 
        {
            string buttonName = (string)((Button)e.OriginalSource).Content;
            if (resultGot)
            {
                Text.Text = "";
                resultGot = false;
            }
            switch (buttonName)
            {
                case "=":
                    var result = new DataTable().Compute(expression, null);
                    Text.Text = "";
                    Text.Text += expression + '\n';
                    //Text.Text += "Decimal: " + result + '\n';
                    Text.Text += FromDecimal(Convert.ToDouble(result));
                    resultGot = true;
                    break;
                case "DEL":
                    Text.Text = "";
                    input = string.Empty;
                    expression = string.Empty;
                    break;
                default:
                    Text.Text = "";
                    input += buttonName;
                    expression += buttonName;
                    Text.Text += input;
                    break;
            }
        }



        /*
                private void Text_TextChanged(object sender, TextChangedEventArgs e)
                {

                }

                private void Button1_Click(object sender, RoutedEventArgs e)
                {
                    Text.Text = "";
                    input += "1";
                    expression += "1";
                    Text.Text += input;
                }

                private void Button2_Click(object sender, RoutedEventArgs e)
                {
                    //Text.Text = "";
                    input += "2";
                    expression += "2";
                    Text.Text += input;
                }

                private void Button3_Click(object sender, RoutedEventArgs e)
                {
                    Text.Text = "";
                    input += "3";
                    Text.Text += input;
                }

                private void Button4_Click(object sender, RoutedEventArgs e)
                {
                    Text.Text = "";
                    input += "4";
                    Text.Text += input;
                }

                private void Button5_Click(object sender, RoutedEventArgs e)
                {
                    Text.Text = "";
                    input += "5";
                    Text.Text += input;
                }

                private void Button6_Click(object sender, RoutedEventArgs e)
                {
                    Text.Text = "";
                    input += "6";
                    Text.Text += input;
                }

                private void Button0_Click(object sender, RoutedEventArgs e)
                {
                    Text.Text = "";
                    input += "0";
                    Text.Text += input;
                }

                private void Dot_Click(object sender, RoutedEventArgs e)
                {
                    expression += ".";
                }

                private void Mult_Click(object sender, RoutedEventArgs e)
                {
                    operand1 = input;
                    operation = '*';
                    expression += "*";
                    input = string.Empty;
                }

                private void Div_Click(object sender, RoutedEventArgs e)
                {
                    operand1 = input;
                    operation = '/';
                    input = string.Empty;
                }

                private void Plus_Click(object sender, RoutedEventArgs e)
                {
                    operand1 = input;
                    operation = '+';
                    expression += "+";
                    Text.Text += operation;
                    input = string.Empty;
                }

                private void Minus_Click(object sender, RoutedEventArgs e)
                {
                    operand1 = input;
                    operation = '-';
                    input = string.Empty;
                }

                private void Equal_Click(object sender, RoutedEventArgs e)
                {
                    operand2 = input;
                    double num1, num2;
                    double.TryParse(operand1, out num1);
                    double.TryParse(operand2, out num2);

                    if (operation == '+')
                    {
                        result = num1 + num2;
                        Text.Text = result.ToString();
                    }
                    else if (operation == '-')
                    {
                        result = num1 - num2;
                        Text.Text = result.ToString();
                    }
                    else if (operation == '*')
                    {
                        result = num1 * num2;
                        Text.Text = result.ToString();
                    }
                    else if (operation == '/')
                    {
                        if (num2 != 0)
                        {
                            result = num1 / num2;
                            Text.Text = result.ToString();
                        }
                        else
                        {
                            Text.Text = "DIV/Zero!";
                        }

                    }
                }

                private void Del_Click(object sender, RoutedEventArgs e)
                {
                    Text.Text = "";
                    input = string.Empty;
                    operand1 = string.Empty;
                    operand2 = string.Empty;
                    expression = string.Empty;
                }

                private void Open_Click(object sender, RoutedEventArgs e)
                {
                    operand1 = input;
                    operation = '(';
                    expression += "(";
                    input = string.Empty;
                }

                private void Close_Click(object sender, RoutedEventArgs e)
                {
                    operand1 = input;
                    operation = ')';
                    expression += ")";
                    input = string.Empty;
                }
        */
    }
}
