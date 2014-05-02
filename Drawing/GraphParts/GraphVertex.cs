using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph;
using System.Diagnostics;
using GraphSharp.Controls;

namespace BeehiveSelection.Drawing.GraphParts {
    
    class GraphVertex {

        public Bee.State state;
        public int numberOfBees { get; set; }

        public GraphVertex(Bee.State s) {
            state = s;
        }

        public override string ToString() {
            return string.Format("State: {0} Number of Bees: {1}", state, numberOfBees);
        }
    }
}
