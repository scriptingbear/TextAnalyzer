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
        /// <summary>
        /// frmAnalyzer's function is compute frequencies of words found in the text entered/
        /// pasted into the txtInput RichTextBox control that occur at least as many times
        /// as the value of the minCount UpDown (spinner) control
        /// </summary>
        public frmAnalyzer()
        {
            InitializeComponent();
        }

        /* Declare 2 list objects that will hold the words from the text in txtInput
         * and their corresponding frequencies. Since several methods will use these
         * list object, they are declared at the top of the form class so that they
         * are available to all methods throughout the class
         */ 
        List<string> series_names = new List<string>();
        List<int> series_points = new List<int>();

        /// <summary>
        /// btnGetDistinctValues is the button control labeled "Analyze Text" on the form.
        /// This event procedure checks the contents of txtInput to make sure it is not empty
        /// before calling the AnalyzeData() method, followed by a call to FillChart().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetDistinctValues_Click(object sender, EventArgs e)
        {
        
            if (txtInput.Text == string.Empty)
            {
                return;
            }

            AnalyzeData();
            FillChart();

        }//btnGetDistinctValues_Click

        /// <summary>
        /// ClearDataPoints() empties the 2 list objects that hold data used to populate
        /// the chart's axes and horizontal bars, corresponding to the frequency of words
        /// This method is called each time the spinner control is changed or the user clicks
        /// the Analyze Text button, so that the chart doesn't accumulate data between events
        /// that cause it to be redrawn
        /// </summary>
        private void ClearDataPoints()
        {
            //remove any items from the chart data lists since they perist outside of this method between calls
            series_names.Clear();
            series_points.Clear();

            /* When the form is first opened the chart has no data, i.e. no series of labels
             * and corresponding data points. But after an event occurs that populates the 
             * chart, it will have one (1) data series named "Frequency". We don't
             * delete this data series once it has been created. We simply empty it of data so it
             * can be refreshed with a new set of data later on.
             * So check how many data series the chart has. It can be either zero or one but
             * not anything else. If there is a data series in the chart, we need to empty it
             * before continuing to analyze text and then update the chart.
             */ 
            if (chtReport.Series.Count != 0)
            {
                chtReport.Series["Frequency"].Points.Clear();
            }

        }//ClearDataPoints()

        /// <summary>
        /// CleanText() is needed to strip out leading and trailing spaces, plus various punctuation
        /// marks from the text in txtInput, because we will split the contents of that control's Text
        /// property on a single blank into an array so that we can calculate the frequency of each 
        /// word in the txtInput control. Otherwise the code will consider strings of text ending in a 
        /// comma, period, question mark etc as words and count them separately from the same strings
        /// without any punctuation marks, resulting in incorrect data analysis
        /// </summary>
        /// <returns></returns>
        private string CleanText()
        {
            /*ignore punctuation and ignore case when computing frequency of words
             * this will enable distinct words to be extracted from the RichTextBox control
             */ 
            var text = Regex.Replace(txtInput.Text, @"[^\w\n' -]", "", RegexOptions.IgnoreCase).ToLower();

            //text may have excess internal spaces and/or leading/trailing spaces
            text = text.Trim();
            text = Regex.Replace(text, @"\s{2,}", " ");

            return text;
        }//CleanText()

        /// <summary>
        /// This method returns an enumerable, grouped collection of distinct words in then
        /// the text selected for analysis.First it splits the text in txtInput into an array
        /// based on single spaces, commas and newline characters. It then uses a Linq
        /// method called GroupBy to group the items in the array so that all occurrences of 
        /// a given word are logically view as a single entity. Linq knows how many items are in 
        /// each group so we can call the Count() method on the group to obtain the frequency
        /// of the word represented by each group.
        /// Next, the grouped collection is sorted via the OrderBy() method using the 
        /// value returned by the Count() method on each group to sort the collection. 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private IOrderedEnumerable<IGrouping<string, string>> GetSortedDistinctWords(string text)
        {
            var all_words = text.Split(new char[] { ' ', '\n' });
            var distinct_words = all_words.GroupBy(word => word);
            var sorted_distinct_words = from word in distinct_words orderby word.Count() select word;
            return sorted_distinct_words;

        }//GetSortedDistinctWords()

        /// <summary>
        /// PopulateChartData() is tasked with filling in the pair of list objects declared
        /// near the top of the form class, viz. series_names (for the words) and series_points
        /// (for the frequency counts). Note that it only adds data for words whose frequency
        /// meets or exceeds the minimum threshold set by the spinner control (minCount).
        /// </summary>
        /// <param name="sorted_distinct_words"></param>
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

        /// <summary>
        /// AnalyzeData() is sort of a driver script in that it doesn't really do any processing
        /// on its own. Instead it calls a series of methods which, as group, peform the required
        /// analysis on the text in the RichTextBox control
        /// </summary>
        private void AnalyzeData()
        {
            ClearDataPoints();
            var text = CleanText();
            var sorted_distinct_words = GetSortedDistinctWords(text);
            PopulateChartData(sorted_distinct_words);

        }//AnalyzeData()


        /// <summary>
        /// FillChart() will populate the chart provdied the two list objects
        /// declared near the top of the form class have some data in them. If they
        /// don't then that means the RichTextBox control was empty at the time the Analyze Text 
        /// button was clicked or the spinner control was adjusted.
        /// If the list objects do contain data, FillChart() will either create a new Series
        /// object and populate it or it will clear out the data from an existing Series object
        /// and then repopulate it. This way data collected between events that would cause the 
        /// chart to repopulate is not accumulated. We don't want to mix data from two or more
        /// runs on the chart as that would make the data meaningless and the graphics on the 
        /// chart would quickly become too small to even read
        /// </summary>
        private void FillChart()
        {
            if (series_points.Count == 0)
            {
                return;
            }

            Series series;

            if (chtReport.Series.Count == 0)
            {
                /* here we are adding a new seires to the chart and setting the chart
                 * type so that it will display horizontal bars
                 */ 
                series = chtReport.Series.Add("Frequency");
                series.ChartType = SeriesChartType.Bar;
            }

            else

            {
                /* Since the chart already has its one and only data series, we only need to empty
                 * it of data. We don't have to destroy the series completely and then recreate
                 * it, which would be wasteful and unnecessary
                 */ 
                series = chtReport.Series["Frequency"];
                series.Points.Clear();
            }

            for (int i=0;i < series_points.Count;i++)
            { 
                series.Points.Add(series_points[i]);
                series.Points[i].AxisLabel = series_names[i];
            }

        }//FillChart()


        /// <summary>
        /// The frmAnalyzer_Load() event procedure clears the chart of any data that might
        /// have been added inadvertently during design phase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAnalyzer_Load(object sender, EventArgs e)
        {
            chtReport.Series.Clear();
        }//frmAnalyzer_Load()


        /// <summary>
        /// minCount_ValueChanged() is reponsible for clearing the chart of data
        /// and then calling AnalyzeData() and FillChart() to repopulate it each
        /// time the control's value is changed, such as when the user clicks either of the arrows at
        /// adjacent to the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minCount_ValueChanged(object sender, EventArgs e)
        {
            
            if (chtReport.Series.Count == 0)
            {
                return;
            }

            AnalyzeData();
            FillChart();
        }//minCount_ValueChanged()


        /// <summary>
        /// txtInput_TextChanged() ensures that the chart is cleared of data if the user
        /// deletes all of the text from the RichTextBox control. This enhances the user
        /// experience because the chart will reflect the current set of data, or lack thereof.
        /// If the RichTextBox control is empty, the chart should not display any information about
        /// words that are no longer present in the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            if (txtInput.Text == string.Empty)
            {
                ClearDataPoints();
            }
        }//txtInput_TextChanged()
    }//frmAnalyzer
}//TextAnalyzer
