using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace HighCharts
{
    public class BarChart : HighCharts
    {
        public BarChart(IWebDriver driver, IWebElement chart)
            : base(driver, chart)
        {
        }

        public void hoverOverPrimarySeriesXAxisLabel(string xAxisLabel)
        {
            hoverOverPrimarySeriesXAxisLabel(xAxisLabel);
        }

        public void hoverOverSecondarySeriesXAxisLabel(string xAxisLabel)
        {
            hoverOverSecondarySeriesXAxisLabel(xAxisLabel);
        }

        public String getPrimarySeriesColourForXAxisLabel(String xAxisLabelValue)
        {
            return getSeriesColorAtXAxisPosition(2, xAxisLabelValue);
        }

        public String getSecondarySeriesColourForXAxisLabel(String xAxisLabelValue)
        {
            return getSeriesColorAtXAxisPosition(1, xAxisLabelValue);
        }
    }
}
