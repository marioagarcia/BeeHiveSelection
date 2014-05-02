using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeehiveSelection
{
    class TaskDistribution : HumanIntervention 
    {
        public TaskDistribution(Random rand) : base(rand)
        {
        }

        internal override void ClickEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        internal override void MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        internal override void MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        internal override void MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        internal override List<Shape> GetShapes()
        {
            throw new NotImplementedException();
        }
    }

}
