using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;

namespace DataMiningTest
{
    public static class ChartBuilder
    {
        public static Control CreateChart(string title, string xTitle, string yTitle, Color color, Dictionary<string, double> data)
        {
            var chart = new ZedGraphControl();
            chart.GraphPane.YAxis.Title.Text = yTitle;
            chart.GraphPane.XAxis.Title.Text = xTitle;
            chart.GraphPane.Title.Text = title;
            chart.GraphPane.AddBar("1", null, data.Values.ToArray(), color);
            chart.GraphPane.XAxis.Scale.MinAuto = true;
            chart.GraphPane.XAxis.Scale.MaxAuto = true;
            chart.GraphPane.YAxis.Scale.MinAuto = true;
            chart.GraphPane.YAxis.Scale.MaxAuto = true;
            chart.GraphPane.XAxis.Type = AxisType.Text;
            chart.GraphPane.XAxis.Scale.TextLabels = data.Keys.ToArray();
            chart.AxisChange();
            chart.Invalidate();
            return chart;
        }
        
        /*
        public Control CreateGoodsChart(string title)
        {
            var chart = new ZedGraphControl();
            chart.GraphPane.YAxis.Title.Text = "Раз куплен";
            chart.GraphPane.XAxis.Title.Text = "Товар";
            chart.GraphPane.Title.Text = title;
            chart.GraphPane.AddBar("1", null, DataAnalyzer.GoodsData.Values.Select(x => (double) x).ToArray(), Color.Blue);
            chart.GraphPane.XAxis.Scale.MinAuto = true;
            chart.GraphPane.XAxis.Scale.MaxAuto = true;
            chart.GraphPane.YAxis.Scale.MinAuto = true;
            chart.GraphPane.YAxis.Scale.MaxAuto = true;
            chart.GraphPane.XAxis.Type = AxisType.Text;
            chart.GraphPane.XAxis.Scale.TextLabels = DataAnalyzer.GoodsData.Keys.ToArray();
            chart.AxisChange();
            chart.Invalidate();
            return chart;
        }
        
        public Control CreateCompanionGoodsChart(string title, string good)
        {
            var data = DataAnalyzer.GetCompanionGoods(good);
            var chart = new ZedGraphControl();
            chart.GraphPane.YAxis.Title.Text = "Раз куплен";
            chart.GraphPane.XAxis.Title.Text = "Товар";
            chart.GraphPane.Title.Text = title;
            chart.GraphPane.AddBar("1", null, data.Values.ToArray(), Color.Blue);
            chart.GraphPane.XAxis.Scale.MinAuto = true;
            chart.GraphPane.XAxis.Scale.MaxAuto = true;
            chart.GraphPane.YAxis.Scale.MinAuto = true;
            chart.GraphPane.YAxis.Scale.MaxAuto = true;
            chart.GraphPane.XAxis.Type = AxisType.Text;
            chart.GraphPane.XAxis.Scale.TextLabels = data.Keys.ToArray();
            chart.AxisChange();
            chart.Invalidate();
            return chart;
        }

    */
    }
}