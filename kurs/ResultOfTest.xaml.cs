using System.Collections.Generic;
using System.Windows.Controls;
using System;
using kurs.Models;
using LiveCharts;
using LiveCharts.Wpf;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using kurs.Context;
using Point = kurs.Models.Point;
using System.Windows.Data;

namespace kurs
{
    /// <summary>
    /// Логика взаимодействия для ResultOfTest.xaml
    /// </summary>
    public partial class ResultOfTest : Page
    {
        public static int MyCurrentTest;
        public int CurrentPointId;
        private List<Answer> _answerList;
        private List<Point> _pointList;
        public int count;
        public int i = 0;
        SeriesCollection t = new SeriesCollection();
        public ResultOfTest()
        {
           
            InitializeComponent();
           
            GetPoints();
            

            buttonLeft.IsEnabled = false;
            DrowDiagram(_pointList[0]);
            




        }
     Func<ChartPoint, string> labelPoint = chartPoint =>
     string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);


        private void GetPoints()
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    _pointList = db.Points.Where(p => p.TestTitleId == MyCurrentTest).ToList();
                    count = _pointList.Count;
                    CurrentPointId = _pointList[0].Id;
                }
                catch { }

            }
        }



        private void DrowDiagram(Point cur)
        {

           
            if (pieChart.Series != null)
            { pieChart.Series.Clear(); }
            if (i < 1)
            {
                buttonLeft.IsEnabled = false;
            }
            if (i == count - 1)
            {
                buttonRight.IsEnabled = false;
            }
            if (i > 0)
            {
                buttonLeft.IsEnabled = true;
            }
            if (i < count - 1)
            {
                buttonRight.IsEnabled = true;
            }
            var db = new DatabaseContext();
            _answerList = db.Answers.Where(a => a.PointId == cur.Id).ToList();
            Binding bind = new Binding("LabelPoint");
            foreach (Answer answer in _answerList)
            {
            PieSeries temp = new PieSeries();
            temp.Title = answer.Content ;
            temp.Values = new ChartValues<int> { answer.Counter };
             temp.LabelPoint = labelPoint;
            t.Add(temp);
            
             }
            pieChart.Series = t;
            TitleofTest.Text = cur.Title.ToString();
           
        }
            

        
        private void Button_Click(object sender, RoutedEventArgs e)
            {

            
                Point temp = _pointList[0];

                Button b = (Button)sender;
                if (b.Name == "buttonLeft")
                {
                    i--;
                    temp = _pointList[i];

                }

                else if (b.Name == "buttonRight")
                {
                    i++;
                    temp = _pointList[i];

                }
                DrowDiagram(temp);
            
            }
        }


        }

    



