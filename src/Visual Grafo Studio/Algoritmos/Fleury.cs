using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visual_Grafo_Studio.Util;

namespace Visual_Grafo_Studio.Algoritmos
{
    class Fleury
    {
        public List<tAresta> c {get;set;}
        public List<Vertice> grafo { get; set; }

        public Fleury(List<Vertice> grafo)
        {
            this.grafo = grafo;
        }
        public bool fleury()
        {
            c = new List<tAresta>();
            List<Vertice> grafoCopy = new List<Vertice>(grafo);
            foreach (Vertice va in grafo)
            {
                grafoCopy.Add(va);
            }
            int origem = 1;
            int u = origem;
            tAresta uv = grafoCopy[u - 1].tAdjascencias[0];
            grafoCopy[u - 1].tAdjascencias.RemoveAt(0);
            int v = uv.vertice.Valor;
            removeArestaReversa(grafoCopy, v, u);
            c.Add(uv);

            while (origem != v || grafo[origem - 1].tAdjascencias.Count != 0)
            {
                bool arestaPonte = true;
                int indiceArestaPonte = -1;

                for (int i = 0; i < grafoCopy[v - 1].tAdjascencias.Count; i++)
                {
                    int adjacente = grafoCopy[v - 1].tAdjascencias[i].vertice.Valor;
                    if (grafoCopy[v - 1].tAdjascencias.Count < 2 || grafo[adjacente - 1].tAdjascencias.Count < 2)
                    {
                        u = v;
                        uv = grafoCopy[v - 1].tAdjascencias[0];
                        grafoCopy[v - 1].tAdjascencias.RemoveAt(i);//////
                        c.Add(uv);
                        v = uv.vertice.Valor;
                        removeArestaReversa(grafoCopy, v, u);
                        arestaPonte = false;
                        continue;
                    }
                    else
                    {
                        indiceArestaPonte = i;
                    }
                }
                if (arestaPonte)
                {
                    if (indiceArestaPonte == -1)
                        return false;
                    u = v;
                    v = uv.vertice.Valor;
                    removeArestaReversa(grafoCopy, v, u);
                }
            }
            return true;
        }
        private void removeArestaReversa(List<Vertice> grafocopy, int origem, int destino)
        {
            for (int i = 0; i < grafocopy[origem - 1].tAdjascencias.Count; i++)
            {
                if (grafocopy[origem - 1].tAdjascencias[i].vertice.Valor == destino)
                {
                    grafocopy[origem - 1].tAdjascencias.RemoveAt(i);
                }
            }
        }
    }
}
