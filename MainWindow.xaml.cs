using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Linq;
using System.Globalization;

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

        private double ToDecimal(string str)
        {
            string[] number = new string[2];
            if (str.Contains('.'))
            {
                number = str.Split('.');
            }
            else
            {
                number[0] = str;
                number[1] = "0";
            }
            int sign;
            if (number[0].Contains('-'))
            {
                sign = -1;
                number[0] = number[0].Replace("-", "");
            }
            else
            {
                sign = 1;
            }
            double decimalNum = 0;
            double beforeDot = Convert.ToDouble(number[0]);
            double afterDot = Convert.ToDouble(number[1]);
            for (int i = 0; i < number[0].Length; i++)
            {
                double last = beforeDot % 10;
                decimalNum += Math.Pow(7, i) * last;
                beforeDot /= 10;
            }
            if (number[1].Length != 0 && number[1] != "0")
            {
                for (int i = number[1].Length; i > 0; i--)
                {
                    double last = afterDot % 10;
                    decimalNum += Math.Pow(7, -i) * last;
                    afterDot /= 10;
                }
            }
            return (sign == -1) ? -decimalNum : decimalNum;
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
                zel /= 7;
            } while (zel != 0);

            if (FracVal != 0)
            {
                string FracPart = "";
                int tmp;
                while (FracVal > 0 && FracPart.Length <= 5)
                {
                    FracVal *= 7;
                    tmp = (int)Math.Truncate(FracVal);
                    FracPart += tmp;
                    FracVal -= tmp;
                }
                StrInt = StrInt + "." + Convert.ToString(FracPart);
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
                    string currentExpression = "(" + expression + ")";
                    string finalExpression = "";
                    string number = "";
                    for (int i = 0; i < currentExpression.Length; i++)
                    {
                        double symbol;
                        bool success = double.TryParse(currentExpression[i].ToString(), out symbol);
                        if (success || currentExpression[i].ToString() == ".")
                        {
                            number += currentExpression[i].ToString();
                        }
                        else
                        {
                            if (number != "")
                            {
                                finalExpression += ToDecimal(number).ToString("F", CultureInfo.CreateSpecificCulture("en-CA")) + currentExpression[i].ToString();
                            }
                            else
                            {
                                finalExpression += currentExpression[i].ToString();
                            }
                            number = "";
                        }
                    }
                    var result = new DataTable().Compute(finalExpression, null);
                    Text.Text = "";
                    Text.Text += input + '\n';
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
    }
}
