using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visual_Grafo_Studio.Util;

namespace Visual_Grafo_Studio.Algoritmos
{
    class Busca
    {
        public List<Vertice> grafo
        {
            get;
            set;
        }
        private int[] visitado;
        public List<int> solucao
        {
            get;
            set;
        }
        public Busca(List<Vertice> grafo)
        {
            this.grafo = grafo;
            visitado = new int[grafo.Count];
            solucao = new List<int>();
        }
        public void buscaProf()
        {
            for (int i = 0; i < grafo.Count; i++)
            {
                visitado[i] = 0;
            }
            for (int i = 0; i < grafo.Count; i++)
            {
                if (visitado[i] == 0)
                {
                    prof(i);
                }
            }
        }
        public void prof(int v)
        {
            visitado[v] = 1;
            solucao.Add(v + 1);
            for (int i = 0; i < grafo[v].tAdjascencias.Count; ++i)
            {
                int adjascente = grafo[v].tAdjascencias[i].vertice.Valor;
                if (visitado[adjascente - 1] == 0)
                {
                    prof(adjascente - 1);
                }
            }
        }
        public void ordTopologica()
        {
            for (int i = 0; i < grafo.Count; i++)
            {
                visitado[i] = 0;
            }
            for (int i = 0; i < grafo.Count; i++)
            {
                if (visitado[i] == 0)
                {
                    profOrd(i);
                }
            }
        }
        private void profOrd(int v)
        {
            visitado[v] = 1;
            for (int i = 0; i < grafo[v].tAdjascencias.Count; ++i)
            {
                int adjascente = grafo[v].tAdjascencias[i].vertice.Valor;
                if (visitado[adjascente - 1] == 0)
                {
                    profOrd(adjascente - 1);
                }
            }
            solucao.Insert(0, v + 1);
        }
        public void buscaLargura(int v)
        {
            for (int i = 0; i < grafo.Count; i++)
            {
                visitado[i] = 0;
            }
            visitado[v - 1] = 1;
            solucao.Add(v);
            List<int> fila = new List<int>();
            fila.Add(v);
            while (fila.Count != 0)
            {
                int w = fila[0];
                fila.RemoveAt(0);
                for (int i = 0; i < grafo[w - 1].tAdjascencias.Count; ++i)
                {
                    int adjascente = grafo[w - 1].tAdjascencias[i].vertice.Valor;
                    if (visitado[adjascente - 1] == 0)
                    {
                        visitado[adjascente - 1] = 1;
                        solucao.Add(adjascente);
                        fila.Add(adjascente);
                    }

                }
            }
        }

    }
}
