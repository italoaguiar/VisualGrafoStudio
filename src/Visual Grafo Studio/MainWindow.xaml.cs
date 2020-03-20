using Contac;
using Microsoft.Win32;
using Microsoft.Windows.Controls.Ribbon;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.IO;
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
using Visual_Grafo_Studio.Algoritmos;
using Visual_Grafo_Studio.Controls;
using Visual_Grafo_Studio.Util;

namespace Visual_Grafo_Studio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Leitor leitor;
        string fileAdress;
        private void openfile()
        {
            leitor = new Leitor();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Arquivo DIMACS (*.gr)|*.gr|Arquivo de Texto (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                fileAdress = dialog.FileName;
                try
                {
                    leitor.ReadFile(dialog.FileName);

                    DataContext = leitor;
                    GraphControl.Graph = leitor.Grafo;
                    UpdateStatusBar();               

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Formato Inválido" + ex.Message + ex.Source + ex.StackTrace);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            

        }

        private void RibbonApplicationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            openfile();
        }

        private void RibbonApplicationMenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Imagem PNG (*.png)|*.png";
            if (dialog.ShowDialog() == true)
            {
                Helpers.GraphToImage.ExportToPng(GraphControl, new Uri(dialog.FileName));
            }
        }
        //Ordenação Topológica
        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            Busca b = new Busca(leitor.Customgrafo);
            b.ordTopologica();

            string v = "";
            foreach (int i in b.solucao)
            {
                v += string.Format("({0}) ",i);
            }
            MessageBox.Show(string.Format("Uma possível Ordenação Topológica para este Grafo é:\n{0}",v));
        }
        private void UpdateStatusBar()
        {
            BidirectionalGraph<object, IEdge<object>> grafo = (BidirectionalGraph<object, IEdge<object>>) GraphControl.Graph;
            StatusBar.Text = string.Format("Vértices: {1}  Arestas: {0}",grafo.EdgeCount,grafo.VertexCount);
        }
        //Novo vertice
        private void RibbonButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (GraphControl.Graph == null)
            {
                var d = new Dialog();
                d.setDialog("Novo Vértice", "Informe o valor do vértice:", "");
                if (d.ShowDialog() == true)
                {
                    try
                    {
                        BidirectionalGraph<object, IEdge<object>> grafo = new BidirectionalGraph<object, IEdge<object>>();
                        grafo.AddVertex(new Vertice(int.Parse(d.Value)));
                        GraphControl.Graph = grafo;
                    }
                    catch { }
                }
            }
            else
            {
                var d = new Dialog();
                d.setDialog("Novo Vértice", "Informe o peso do vértice:", "");
                if (d.ShowDialog() == true)
                {
                    try
                    {
                        BidirectionalGraph<object, IEdge<object>> grafo = ((BidirectionalGraph<object, IEdge<object>>)GraphControl.Graph);
                        var l = Helpers.CopyGraphSharp(grafo);
                        l.AddVertex(new Vertice(int.Parse(d.Value)));
                        GraphControl.Graph = l;
                    }
                    catch { }
                }
            }
        }
        //Busca Largura
        private void RibbonButton_Click_2(object sender, RoutedEventArgs e)
        {
            var d = new Dialog();
            d.setDialog("Busca em Largura", "Informe o vértice de partida:", "");
            if (d.ShowDialog() == true) 
            {
                try
                {
                    Busca b = new Busca(leitor.Customgrafo);
                    b.buscaLargura(int.Parse(d.Value));


                    string v = "";
                    foreach (int i in b.solucao)
                    {
                        v += string.Format("({0}) ", i);
                    }
                    MessageBox.Show(v);
                }
                catch { MessageBox.Show("Ocorreu um erro. Verifique se o valor digitado está correto."); }
            }
        }
        //busca profundidade
        private void RibbonButton_Click_3(object sender, RoutedEventArgs e)
        {
            Busca b = new Busca(leitor.Customgrafo);
            b.buscaProf();


            string v = "";
            foreach (int i in b.solucao)
            {
                v += string.Format("({0}) ", i);
            }
            MessageBox.Show(v);
        }
        //Kruskal
        private void RibbonButton_Click_4(object sender, RoutedEventArgs e)
        {
            Kruskal k = new Kruskal(leitor.Customgrafo);
            k.Kruskal_();

            GraphControl.Graph = k.toGraphSharp();
            UpdateStatusBar();
        }

        private void RibbonButton_Click_5(object sender, RoutedEventArgs e)
        {
            AddEdge ed = new AddEdge();
            ed.setVetices((BidirectionalGraph<object, IEdge<object>>)GraphControl.Graph);
            if (ed.ShowDialog() == true)
            {
                BidirectionalGraph<object, IEdge<object>> grafo = ((BidirectionalGraph<object, IEdge<object>>)GraphControl.Graph);
                var l = Helpers.CopyGraphSharp(grafo);
                l.AddEdge(new TaggedEdge<object, object>(ed.Origem, ed.Destino, ed.Peso));
                GraphControl.Graph = l;
            }
        }

        private void RibbonButton_Click_6(object sender, RoutedEventArgs e)
        {
            Fleury f = new Fleury(leitor.Customgrafo);
            f.fleury();

            string v = "";
            foreach (tAresta i in f.c)
            {
                v += string.Format("({0} {1}) ", i.vertice.Valor,i.peso);
            }
            MessageBox.Show(v);
        }

        private void RibbonButton_Click_7(object sender, RoutedEventArgs e)
        {
            var d1 = new Dialog();
            d1.setDialog("Dijkstra", "Informe o vértice de partida:", "");
            if (d1.ShowDialog() == true)
            {
                var d2 = new Dialog();
                d2.setDialog("Dijkstra","Informe o vértice de destino:","");
                if(d2.ShowDialog() == true)
                {
                    try
                    {
                        int o = int.Parse(d1.Value);
                        int d = int.Parse(d2.Value);
                        Dijkstra dij = new Dijkstra();
                        dij.dijkstra(leitor.Customgrafo, o);
                        List<int> dist = dij.RecCaminho(o, d);
                        //MessageBox.Show("Executou");

                        string v = "";
                        foreach (int i in dij.d)
                        {
                            v += string.Format("({0}) ", i + 1);
                        }
                        MessageBox.Show(v);
                        foreach (int i in dij.r)
                        {
                            v += string.Format("({0}) ", i + 1);
                        }
                        MessageBox.Show(v);
                        foreach (int i in dist)
                        {
                            v += string.Format("({0}) ", i+1);
                        }
                        MessageBox.Show(v);
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void RibbonApplicationMenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RibbonApplicationMenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                BidirectionalGraph<object, IEdge<object>> grafo = ((BidirectionalGraph<object, IEdge<object>>)GraphControl.Graph);

                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("{0} {1}{2}", grafo.VertexCount, grafo.EdgeCount,Environment.NewLine));

                foreach (TaggedEdge<object, object> o in grafo.Edges)
                {
                    sb.Append(string.Format("{0} {1} {2}{3}", ((Vertice)o.Source).Valor, ((Vertice)o.Target).Valor, o.Tag,Environment.NewLine));
                }
                SaveFileDialog d = new SaveFileDialog();
                d.Filter = "Arquivo DIMACS (*.gr)|*.gr";
                if (d.ShowDialog() == true)
                {
                    FileStream fs = File.Create(d.FileName);
                    StreamWriter sr = new StreamWriter(fs);

                    sr.Write(sb.ToString());
                    sr.Flush();
                    fs.Close();
                    MessageBox.Show("Arquivo salvo com sucesso!");
                }
            }
            catch(Exception ex) { MessageBox.Show("Ocorreu um erro. Tente novamente." + ex.Message + ex.Source + ex.StackTrace); }
        }
    }
}
