using System;
using System.Data;
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

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement element in MainRoot.Children)
            {
                if (element is Button)
                {
                    ((Button)element).Click += Button_Click;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string new_symb = (string)b.Content;
            char last_symb = '0';
            foreach (char symbol in TextBlock.Text)
                last_symb = symbol;

            if (TextBlock.Text == "0")
            {
                TextBlock.Text = TextBlock.Text.Remove(0, 1);
            }
            if (new_symb == "CE")
            {
                TextBlock.Text = "0";
            }
            else if (new_symb == "C")
            {
                if (TextBlock.Text.Length > 0)
                    TextBlock.Text = TextBlock.Text.Remove(TextBlock.Text.Length - 1, 1);
                if (TextBlock.Text.Length == 0)
                    TextBlock.Text = "0";
            }
            else if ((last_symb == '/' && new_symb == "0") || TextBlock.Text.Length > 24)
            {
                Console.WriteLine("ERROR");
            }
            else if (new_symb == "=")
            {
                string result;
                if (last_symb != '-' && last_symb != '+' && last_symb != '*' && last_symb != '/')
                {
                    try
                    {
                        result = new DataTable().Compute(TextBlock.Text, null).ToString();
                        TextBlock.Text = result;
                    }
                    catch (System.OverflowException)
                    {

                    }
                }
            }
            else
            {
                int error = 0, temp = 0;
                foreach (char symbol in TextBlock.Text)
                {
                    if (symbol == '-' || symbol == '+' || symbol == '*' || symbol == '/')
                        temp = 0;
                    else if ((symbol == '.' || symbol == ','))
                        temp++;
                }
                if (new_symb == "." && temp != 0)
                    error = 1;

                if (last_symb == '-' || last_symb == '+' || last_symb == '*' || last_symb == '/')
                {
                    if (new_symb == "-" || new_symb == "+" || new_symb == "*" || new_symb == "/")
                    {
                        TextBlock.Text = TextBlock.Text.Remove(TextBlock.Text.Length - 1, 1);
                    }
                }
                else if (last_symb == '.' && new_symb == ".")
                    error = 1;
                if (error == 0)
                    TextBlock.Text += new_symb;
            }
        }
    }
}
