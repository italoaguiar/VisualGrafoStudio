using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Contac
{
    /// <summary>
    /// Interaction logic for Dialog.xaml
    /// </summary>
    public partial class Dialog : Window
    {
        public Dialog()
        {
            InitializeComponent();
        }
        public string setDialog(string Title,string Caption, string Value)
        {
            this.Value = Value;
            this.Caption = Caption;
            this.Titulo = Title;
            this.textBox1.SelectAll();
            this.textBox1.Focus();
            return this.Value;
        }
        public string Titulo
        {
            get
            {
                return this.Title;
            }
            set
            {
                this.Title = value;
            }
        }
        public string Caption
        {
            get
            {
                return this.textBlock1.Text;
            }
            set
            {
                this.textBlock1.Text = value;
            }
        }
        public string Value
        {
            get
            {
                return this.textBox1.Text;
            }
            set
            {
                this.textBox1.Text = value;
            }
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            this.DialogResult = true;
        }
    }
}
