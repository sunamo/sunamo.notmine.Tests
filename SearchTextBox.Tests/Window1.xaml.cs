using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SearchTextBox;
using static SearchTextBox.SearchTextBox;

namespace TestUI {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        List<string> items = null;

        public Window1()
        {
            InitializeComponent();

            // Supply the control with the list of sections
            //List<string> sections = new List<string> { "Author", "Title", "Comment" };
            //m_txtTest.SectionsList = sections;

            m_txtTest.ShowSectionButton = false;

            // Choose a style for displaying sections
            m_txtTest.SectionsStyle = SectionsStyles.RadioBoxStyle;

            // Add a routine handling the event OnSearch
            m_txtTest.OnSearch += m_txtTest_OnSearch;

            items = new List<string>(new[] { "ab", "ac", "bb", "bc" });

            lb.ItemsSource = items;
        }

        void m_txtTest_OnSearch(SearchEventArgs searchArgs)
        {
            //SearchEventArgs searchArgs = e as SearchEventArgs;

            // Display search data
            //string sections = "\r\nSections(s): ";
            //foreach (string section in searchArgs.Sections)
            //    sections += (section + "; ");

            StringBuilder sb = new StringBuilder();

            var s = items.Where(d => d.Contains(searchArgs.Keyword));
            foreach (var item in s)
            {
                sb.AppendLine(item);
            }
            

            m_txtSearchContent.Text = "Keyword: " + searchArgs.Keyword + Environment.NewLine + sb.ToString();
        }
    }

}
