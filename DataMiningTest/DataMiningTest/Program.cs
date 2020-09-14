using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DataMiningTest
{
    public static class Program
    {
        private static string goodToAnalyze;
        private static string settingsPath = "settings.txt";
        
        [STAThread]
        public static void Main()
        {
            DataAnalyzer.Analyze();
            if (!File.Exists(settingsPath))
                goodToAnalyze = DataAnalyzer.GoodsData.ElementAt(DataAnalyzer.GoodsData.Count / 2).Key;
            else
                goodToAnalyze = File.ReadLines(settingsPath).First();
            var form = CreateChartForm();
            Application.Run(form);
        }

        private static Form CreateChartForm()
        {
            var form = new Form {WindowState = FormWindowState.Maximized};
            var chart = ChartBuilder.CreateChart("Статистика по товарам", "Товар", "Раз куплен", Color.Blue,
                DataAnalyzer.GoodsData);
            var chart2 = ChartBuilder.CreateChart(String.Format("Сопутствующие c {0} товары", goodToAnalyze), "Товар", "В каком проценте случаев был куплен с товаром", Color.Blue,
                DataAnalyzer.GetCompanionGoods(goodToAnalyze));
            chart.Dock = DockStyle.Top;
            chart2.Dock = DockStyle.Bottom;
            form.Controls.Add(chart);
            form.Controls.Add(chart2);
            form.Resize += (sender, args) => ResizeCharts(form, chart, chart2);
            form.Shown += (sender, args) => ResizeCharts(form, chart, chart2);
            return form;
        }

        private static void ResizeCharts(Form form, Control chart, Control chart2)
        {
            chart.Height = form.ClientSize.Height / 2;
            chart2.Height = form.ClientSize.Height / 2;
        }
    }
}