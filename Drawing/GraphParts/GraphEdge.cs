using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph;
using System.Diagnostics;
using GraphSharp.Controls;

namespace BeehiveSelection.Drawing.GraphParts {

    class GraphEdge : Edge<GraphVertex> {

        private int thickness { get; set; }

        public GraphEdge(GraphVertex vertex1, GraphVertex vertex2)
            : base(vertex1, vertex2) {

            thickness = 1;
        }
    }
}
