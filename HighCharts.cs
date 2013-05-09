using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.RegularExpressions;
using System.Globalization;
using Selenium.Internal.SeleniumEmulation;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace HighCharts
{
    public class HighCharts
    {
        IWebDriver driver;
        public WebDriverWait wait;
        public IWebElement chart;
        protected Actions performAction;

        [FindsBy(How = How.CssSelector)]
        public IWebElement toolTip;

        [FindsBy(How = How.CssSelector)]
        public IWebElement legend;

        [FindsBy(How = How.CssSelector)]
        public List<IWebElement> axisLabels;


        public HighCharts(IWebDriver driver, IWebElement chart)
        {
            PageFactory.InitElements(chart, this);
            this.driver = driver;
            this.chart = chart;

            int waitTimeout = 15000;
            this.wait = new WebDriverWait(driver, new TimeSpan(waitTimeout));
            performAction = new Actions(driver);
        }

        //Check for adding wait.Until(ExpectedConditions.ElementIsVisible())
        public Boolean isChartDisplayed()
        {
            return chart.Displayed;
        }

        public Boolean isLegendDisplayed()
        {
            return legend.Displayed;
        }

        //Check for adding wait.Until(ExpectedConditions.ElementIsVisible())
        public Boolean isTooltipDisplayed()
        {
            return toolTip.Displayed;
        }

        public String getToolTipLine(int lineNo)
        {
            List<String> lines = new List<String>();
            
            foreach (IWebElement toolTipLine in toolTip.FindElements(By.CssSelector("text tspan")))
            {
                lines.Add(toolTipLine.Text);
            }
            
            if (lineNo > lines.Count)
            {
                throw new NoSuchElementException("There is no line " + lineNo + "! There are only " + lines.Count + " lines in the tool tip");
            }
            //We return line - 1 because the lines Array starts a 0 not 1
            return lines[lineNo - 1];
        }

        protected IWebElement getXAxisLabels()
        {
            return axisLabels[0];
        }

        protected IWebElement getYAxisLabels()
        {
            return axisLabels[1];
        }

        public List<String> getXAxisLabelsText()
        {
            List<String> labels = new List<String>();

            foreach (IWebElement eachLabel in getXAxisLabels().FindElements(By.CssSelector("text")))
            {
                labels.Add(eachLabel.Text);
            }

            return labels;
        }

        public List<String> getYAxisLabelsText()
        {
            List<String> labels = new List<String>();

            foreach (IWebElement eachLabel in getYAxisLabels().FindElements(By.CssSelector("text")))
            {
                labels.Add(eachLabel.Text);
            }

            return labels;
        }

        public String[] getXAxisLabelsAsArray()
        {
            List<String> xAxisLabels = getXAxisLabelsText();
            return xAxisLabels.ToArray();
        }

        public String[] getYAxisLabelsAsArray()
        {
            List<String> xAxisLabels = getYAxisLabelsText();
            return xAxisLabels.ToArray();
        }

        public int extractXAttributeAsInteger(IWebElement xAxisLabel)
        {
            Double xAttribute = Double.Parse(xAxisLabel.GetAttribute("x"));
            return Convert.ToInt32(xAttribute);
        }

        protected void hoverOverColumnOrBarChartSeriesAtXAxisPosition(int series, String xAxisLabel)
        {
            int barNumber = getXAxisLabelsText().IndexOf(xAxisLabel);
            IWebElement pointToHoverOver = chart.FindElements(By.CssSelector("g.highcharts-tracker > g:nth-of-type(" + series + ") > rect"))[barNumber];

            //For browsers not supporting native events
            JavaScriptLibrary.CallEmbeddedSelenium(driver, "triggerEvent", pointToHoverOver, "mouseover");
            //For browsers supporting native events
            performAction.MoveToElement(pointToHoverOver).Perform();
        }

        protected String getSeriesColorAtXAxisPosition(int series, String xAxisLabelValue)
        {
            //The series can vary depending on the structure of the chart, by default it is fine but if this doesn't work you may need to tweak the series
            int barNumber = getXAxisLabelsText().IndexOf(xAxisLabelValue);
            //The below varies depending on the structure of the chart, by default we need to multiply by 4
            barNumber = barNumber * 4;
            return chart.FindElement(By.CssSelector(".highcharts-series-group > g:nth-of-type(" + series + ") > rect:nth-of-type(" + barNumber + ")")).GetAttribute("fill");
        }

/* Java Code -- Not sure what it is used for? */
        //private static ExpectedCondition<Boolean> attributeIsEqualTo(final WebElement element, final String attribute, final String attributeValue) {
        //    return new ExpectedCondition<Boolean>() {
        //        @Override
        //        public Boolean apply(WebDriver driver) {
        //            return element.getAttribute(attribute).equals(attributeValue);
        //        }
        //    };
        //}
    }
}
