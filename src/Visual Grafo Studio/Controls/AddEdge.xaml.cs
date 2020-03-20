using Contac;
using QuickGraph;
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

namespace Visual_Grafo_Studio.Controls
{
    /// <summary>
    /// Interaction logic for AddEdge.xaml
    /// </summary>
    public partial class AddEdge : Window
    {
        public AddEdge()
        {
            InitializeComponent();
        }
        public void setVetices(BidirectionalGraph<object, IEdge<object>> grafo)
        {
            foreach (Vertice v in grafo.Vertices)
            {
                List1.Items.Add(v);
                List2.Items.Add(v);
            }
        }
        public Vertice Origem { get; set; }
        public Vertice Destino { get; set; }
        public int Peso { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (List1.SelectedItems.Count != 1 || List2.SelectedItems.Count != 1)
            {
                MessageBox.Show("É necessário selecionar um vértice de origem e um vértice de destino.");
            }
            else
            {
                var d = new Dialog();
                d.setDialog("Busca em Largura", "Informe o peso:", "");
                if (d.ShowDialog() == true)
                {
                    try
                    {
                        Origem = List1.SelectedItem as Vertice;
                        Destino = List2.SelectedItem as Vertice;
                        Peso = int.Parse(d.Value);
                        this.DialogResult = true;
                    }
                    catch { MessageBox.Show("Ocorreu um erro. Verifique se o valor digitado é válido."); }
                }
            }
        
        }
    }
}
