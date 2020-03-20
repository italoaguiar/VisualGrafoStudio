using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Visual_Grafo_Studio.Algoritmos
{
    class Dijkstra
    {
        public int[] d { get; set; }
        public int[] r { get; private set; }
        public List<Vertice> q { get; private set; }

        public void dijkstra(List<Vertice> grafo, int s)
        {
            d = new int[grafo.Count];
            r = new int[grafo.Count];
            q = new List<Vertice>();

            for (int i = 0; i < grafo.Count; i++)
            {
                d[i] = 99999;
                r[i] = -1;//represeta null

                q.Add(grafo[i]);
            }
            d[s-1] = 0;
            grafo[s-1].Peso = 0;
            while (q.Count != 0)
            {
                Vertice u = q.OrderBy(p => p.Peso).ToList()[0];//Ordenação muito melhor que java
                q.Remove(u);

                for (int i = 0; i < u.tAdjascencias.Count; i++)
                {
#if Debug
                   MessageBox.Show("Vertice " + u.tAdjascencias[i].vertice.Valor + ": " + d[u.tAdjascencias[i].vertice.Valor - 1] + " > Vertice" + u.Valor + ": " + d[u.Valor - 1] + " + " + u.tAdjascencias[i].peso.ToString());
#endif           
                    if (d[u.tAdjascencias[i].vertice.Valor -1] > d[u.Valor -1] + u.tAdjascencias[i].peso)
                    {
                        d[u.tAdjascencias[i].vertice.Valor - 1] = d[u.Valor - 1] + u.tAdjascencias[i].peso;
                        r[u.tAdjascencias[i].vertice.Valor - 1] = u.Valor - 1;

                        q.Add(u.tAdjascencias[i].vertice);
                    }
                }
            }
        }
        public List<int> RecCaminho(int s, int t)
        {
            List<int> c = new List<int>();
            c.Add(t-1);

            int aux = t-1;

            while (aux != s-1)
            {
                aux = r[aux];
                c.Insert(0, aux);
            }

            return c;
        }

    }
}
