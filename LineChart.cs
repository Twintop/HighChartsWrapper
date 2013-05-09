using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
//using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.PageObjects;
//using OpenQA.Selenium.Support.UI;

namespace HighCharts
{
    public class LineChart : HighCharts
    {
        [FindsBy(How = How.CssSelector /*as = "g.highcharts-series-group > g:nth-child(1)"*/)]
        IWebElement plotContainer;
        [FindsBy(How = How.CssSelector /*using = "g.highcharts-series-group > g:nth-child(1) > path"*/)]
        IWebElement plotLine;
        [FindsBy(How = How.CssSelector /*using = "g.highcharts-tracker > g > path"*/)]
        IWebElement elementToHoverOver;
        [FindsBy(How = How.CssSelector /*using = "rect:nth-child(2)")*/)]
        IWebElement rectElement;

        public LineChart(IWebDriver driver, IWebElement chart)
            : base(driver, chart)
        {
        }

        public void hoverOverPointOfGraphAtXAxisLabel(String xAxisLabelValue)
        {
            int pointNumber = getXAxisLabelsText().IndexOf(xAxisLabelValue);
            //hoverOverGraphPointAtXAxisPosition(pointNumber);
        }            

        //public void hoverOverGraphPointAtXAxisPosition(int pointNumber)
        //{
        //    int xRect = ((ILocatable)rectElement).Coordinates.LocationInViewport.X;
        //    int yRect = ((ILocatable)rectElement).Coordinates.LocationInViewport.Y;

        //    int xHoverPoint = xRect + getPlotOffset().getX + getPlotPoint(pointNumber).getX;
        //    int yHoverPoint = yRect + getPlotOffset().getY + getPlotPoint(pointNumber).getY;

        //    //For browsers not supporting native events
        //    //JavascriptLibrary.CallEmbeddedSelenium(driver, "triggerEvent", elementToHoverOver, "mouseover");

        //    //For browsers supporting native events
             
        //    xHoverPoint = xHoverPoint - ((ILocatable)elementToHoverOver).Coordinates.LocationInViewport.X;
        //    yHoverPoint = yHoverPoint - ((ILocatable)elementToHoverOver).Coordinates.LocationInViewport.Y;
        //    performAction.MoveToElement(plotLine).MoveToElement(elementToHoverOver, xHoverPoint, yHoverPoint).Perform();
        //}            

        private PlotPoint getPlotPoint(int point)
        {
            if (point < 0)
            {
                throw new ElementNotVisibleException("Plot point ${point} not found");
            }
            return getPlotPoints()[point];
        }

        private IDictionary<int, PlotPoint> getPlotPoints()
        {
            IDictionary<int, PlotPoint> plotPoints = new Dictionary<int, PlotPoint>();
            String[] plotPointsArray = plotLine.GetAttribute("d").Replace("M", "").Split(new char[] {'L','C'});
            for (int plotPoint = 0; plotPoint < plotPointsArray.Length; plotPoint++)
            {
                String[] pointData = plotPointsArray[plotPoint].Trim().Split(' ');
                plotPoints.Add(plotPoint, new PlotPoint(Convert.ToDecimal(pointData[pointData.Length - 2]), Convert.ToDecimal(pointData[pointData.Length - 1])));
            }
            
            return plotPoints;
        }
        
        private PlotPoint getPlotOffset()
        {
            String[] points = plotContainer.GetAttribute("transform").Split(',');
            Decimal xOffset = Convert.ToDecimal(points[0].Replace("[^\\d]", ""));
            Decimal yOffset = Convert.ToDecimal(points[1].Replace("[^\\d]", "")) - 1;
            return new PlotPoint(xOffset, yOffset);
        }
    }
}
