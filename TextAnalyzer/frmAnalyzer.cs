using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Windows.Forms.DataVisualization.Charting;

namespace TextAnalyzer
{
    public partial class frmAnalyzer : Form
    {
        public frmAnalyzer()
        {
            InitializeComponent();
        }

        List<string> series_names = new List<string>();
        List<int> series_points = new List<int>();

        private void btnGetDistinctValues_Click(object sender, EventArgs e)
        {
            if (txtInput.Text == string.Empty)
            {
                return;
            }

            AnalyzeData();
            FillChart();

        }//btnGetDistinctValues_Click

        private void ClearDataPoints()
        {
            //remove any items from the chart data lists since they perist outside of this method between calls
            series_names.Clear();
            series_points.Clear();

            if (chtReport.Series.Count != 0)
            {
                chtReport.Series["Frequency"].Points.Clear();
            }

        }//ClearDataPoints()

        private string CleanText()
        {
            //ignore punctuation and ignore case when computing frequency of words
            var text = Regex.Replace(txtInput.Text, @"[^\w\n' -]", "", RegexOptions.IgnoreCase).ToLower();

            //text may have excess internal spaces and/or leading/trailing spaces
            text = text.Trim();
            text = Regex.Replace(text, @"\s{2,}", " ");

            return text;
        }//CleanText()

        
        private IOrderedEnumerable<IGrouping<string, string>> GetSortedDistinctWords(string text)
        {
            var all_words = text.Split(new char[] { ' ', '\n' });
            var distinct_words = all_words.GroupBy(word => word);
            var sorted_distinct_words = from word in distinct_words orderby word.Count() select word;
            return sorted_distinct_words;

        }//GetSortedDistinctWords()

        private void PopulateChartData(IOrderedEnumerable<IGrouping<string, string>> sorted_distinct_words)
        {
            foreach (var distinct_word in sorted_distinct_words)
            {
                var word_count = distinct_word.Count();
                if (word_count >= (int)minCount.Value)
                {
                    //populate Lists for chart data
                    series_names.Add(distinct_word.Key.ToString());
                    series_points.Add(word_count);
                }//(word_count >= 5)

            }
        }// PopulateChartData()

        private void AnalyzeData()
        {
            ClearDataPoints();
            var text = CleanText();
            var sorted_distinct_words = GetSortedDistinctWords(text);
            PopulateChartData(sorted_distinct_words);

        }//AnalyzeData()

        private void FillChart()
        {
            if (series_points.Count == 0)
            {
                return;
            }

            Series series;

            if (chtReport.Series.Count == 0)
            {
                series = chtReport.Series.Add("Frequency");
                series.ChartType = SeriesChartType.Bar;
            }

            else

            {
                series = chtReport.Series["Frequency"];
                series.Points.Clear();
            }

            for (int i=0;i < series_points.Count;i++)
            { 
                series.Points.Add(series_points[i]);
                series.Points[i].AxisLabel = series_names[i];
            }

        }//FillChart()


        private void frmAnalyzer_Load(object sender, EventArgs e)
        {
            chtReport.Series.Clear();
        }//frmAnalyzer_Load()

        private void minCount_ValueChanged(object sender, EventArgs e)
        {
            
            if (chtReport.Series.Count == 0)
            {
                return;
            }

            AnalyzeData();
            FillChart();
        }//minCount_ValueChanged()

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            if (txtInput.Text == string.Empty)
            {
                ClearDataPoints();
            }
        }//txtInput_TextChanged()
    }//frmAnalyzer
}//TextAnalyzer
