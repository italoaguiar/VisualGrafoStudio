using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visual_Grafo_Studio.Util;

namespace Visual_Grafo_Studio
{
    public class PocGraph : QuickGraph.BidirectionalGraph<Vertice, Aresta>
    {
        public PocGraph() { }

        public PocGraph(bool allowParallelEdges)
            : base(allowParallelEdges) { }

        public PocGraph(bool allowParallelEdges, int vertexCapacity)
            : base(allowParallelEdges, vertexCapacity) { }
    }
}
