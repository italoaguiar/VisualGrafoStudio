using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using QuickGraph;
using Visual_Grafo_Studio.Util;

namespace Visual_Grafo_Studio.Algoritmos
{
    public class Kruskal
    {
        private List<Vertice> grafo;
	    private List<ExAresta> arestas;
        public Kruskal(List<Vertice> v)
        {
            grafo = v;
        }
        private List<ExAresta> a = new List<ExAresta>();
        public List<ExAresta> Kruskal_()
        {		    
		    List<int> c = new List<int>();
            arestas = new List<ExAresta>();
		    for(int i = 0; i< grafo.Count; i++)
            {
			    c.Add(i + 1);                
                foreach (tAresta ar in grafo[i].tAdjascencias)
                {
                    arestas.Add(new ExAresta(grafo[i],ar.vertice,ar.peso));
                }
		    }
            arestas = arestas.OrderBy(p => p.peso).ToList();//Muito melhor que java

		    for(int i = 0; i < arestas.Count; ++i)
            {
			    if(c[arestas[i].Origem.Valor-1] != c[arestas[i].vertice.Valor-1])
                {
				    a.Add(arestas[i]);
				    int cOrigem = c[arestas[i].Origem.Valor-1];
				    for(int j = 0; j < c.Count; ++j)
                    {
					    if(c[j] == cOrigem)
                        {
                            c[j] = c[arestas[i].vertice.Valor - 1];
					    }
				    }
			    }
		    }		
		    return a;
	    }
        public BidirectionalGraph<object, IEdge<object>> toGraphSharp()
        {
            BidirectionalGraph<object, IEdge<object>> gr = new BidirectionalGraph<object, IEdge<object>>();
            foreach (var v in a)
            {
                if (!gr.ContainsVertex(v.Origem))
                {
                    gr.AddVertex(v.Origem);
                }
                if (!gr.ContainsVertex(v.vertice))
                {
                    gr.AddVertex(v.vertice);
                }
                gr.AddEdge(new TaggedEdge<object, object>(v.Origem, v.vertice, v.peso));
            }

            return gr;
        }
    }    
    public class ExAresta : tAresta
    {
        public Vertice Origem { get; set; }

        public ExAresta(Vertice Origem, Vertice Destino, int peso)
            :base(Destino,peso)
        {   
            this.Origem = Origem;
        }
        public override string ToString()
        {
            return string.Format("[{0} {1} {2}]",Origem.Valor,vertice.Valor,peso);
        }
    }
}
