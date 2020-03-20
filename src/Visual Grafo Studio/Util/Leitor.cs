using QuickGraph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Visual_Grafo_Studio.Util
{
    class Leitor
    {
        public List<Vertice> Customgrafo
        {
            get;
            set;
        }

        public Leitor()
        {
            Customgrafo = new List<Vertice>();
        }
        public void ReadFile(string filename)
        {
            FileStream fs = File.OpenRead(filename);
            StreamReader sr = new StreamReader(fs);


            String[] line;

            line = sr.ReadLine().Split(new char[]{' '});
            int numVertices = int.Parse(line[0]);
            int numArestas = int.Parse(line[1]);

            for (int i = 0; i < numVertices; ++i)
            {
                Vertice v = new Vertice();
                Customgrafo.Add(v);
            } 
            //Ler arestas
            for (int i = 0; i < numArestas; ++i)
            {
                line = sr.ReadLine().Split(new char[] { ' ' });

                Vertice v = Customgrafo[int.Parse(line[1]) -1];
                v.Valor = int.Parse(line[1]);
                v.Peso = int.Parse(line[2]);
                tAresta a = new tAresta(v,int.Parse(line[2]));
                Customgrafo[int.Parse(line[0]) -1].tAdjascencias.Add(a);                    
            }
            setGrafoData();

        }

        private void setGrafoData()
        {
            gr = new BidirectionalGraph<object, IEdge<object>>();
            foreach (Vertice tv in Customgrafo)
            {
                if (tv.Valor == 0)
                {
                    tv.Valor = 1;
                }
                gr.AddVertex(tv);
            }
            foreach (Vertice tv in Customgrafo)
            {
                foreach (tAresta ta in tv.tAdjascencias)
                {
                    gr.AddEdge(new TaggedEdge<object,object>(tv, ta.vertice,ta.peso.ToString()));
                }
            }
        }
        public string LayoutType = "EfficientSugiyama";
        private BidirectionalGraph<object, IEdge<object>> gr;
        public BidirectionalGraph<object, IEdge<object>> Grafo
        {
            get
            {                
                return gr;
            }
        }
    }
    public class tAresta
    {
        public tAresta(Vertice v, int p)
        {
            vertice = v;
            peso = p;
        }
        public Vertice vertice{ get; set;}
        public int peso{ get; set;}
    }
}
