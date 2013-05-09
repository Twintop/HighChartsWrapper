using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HighCharts
{
    class PlotPoint
    {        
        private /*sealed*/ Decimal x;
        private /*sealed*/ Decimal y;

        public PlotPoint(Decimal xPoint, Decimal yPoint)
        {
            x = xPoint;
            y = yPoint;
        }

        public override Boolean Equals(Object other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is PlotPoint))
            {
                return false;
            }

            return (this.x.Equals(((PlotPoint) other).getX()) && this.y.Equals(((PlotPoint) other).getY()));
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() + this.y.GetHashCode();
        }

        public int getX()
        {
            return Convert.ToInt32(x);
        }

        public int getY()
        {
            return Convert.ToInt32(y);
        }
    }
}
