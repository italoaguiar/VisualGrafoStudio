using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Visual_Grafo_Studio.Util;
using GraphSharp.Controls;
using QuickGraph;

namespace Visual_Grafo_Studio
{
    /// <summary>
    /// A simple identifiable vertex.
    /// </summary>
    [DebuggerDisplay("{ID}-{IsMale}")]
    public class Vertice
    {
        public Vertice()
        {
        }
        public Vertice(int valor)
        {
            this.Valor = valor;
        }
        public Vertice(int valor, int peso)
        {
            Valor = valor;
            Peso = peso;
        }
        public int Valor { get; set; }
        public int Peso { get; set; }
        private List<tAresta> adj = new List<tAresta>();
        public List<tAresta> tAdjascencias
        {
            get
            {
                return adj;
            }
            set
            {
                adj = value;
            }
        }
    }
    public class TaggedGraphLayout : GraphLayout<object, TaggedEdge<object, object>, IBidirectionalGraph<object, TaggedEdge<object, object>>>
    {
    }
}
