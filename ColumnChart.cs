using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace HighCharts
{
    public class ColumnChart : HighCharts
    {
        public ColumnChart(IWebDriver driver, IWebElement chart)
            : base(driver, chart)
        {
        }

        public void hoverOverPrimarySeriesAtXAxisLabel(String xAxisLabel)
        {
            hoverOverColumnOrBarChartSeriesAtXAxisPosition(1, xAxisLabel);
        }

        public void hoverOverSecondarySeriesAtXAxisLabel(String xAxisLabel)
        {
            hoverOverColumnOrBarChartSeriesAtXAxisPosition(2, xAxisLabel);
        }

        public String getPrimarySeriesColourForXAxisLabel(String xAxisLabelValue)
        {
            return getSeriesColorAtXAxisPosition(1, xAxisLabelValue);
        }

        public String getSecondarySeriesColourForXAxisLabel(String xAxisLabelValue)
        {
            return getSeriesColorAtXAxisPosition(2, xAxisLabelValue);
        }
    }
}
