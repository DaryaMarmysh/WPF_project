using System.Collections.Generic;
using System.Windows.Controls;
using System;
using kurs.Models;

using System.Data.Entity;
using System.Linq;
using System.Windows;
using kurs.Context;
using Point = kurs.Models.Point;
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
        public int i=0;
        
        public ResultOfTest()
        {
           
            InitializeComponent();
            GetPoints();
            DrowDiagram(_pointList[0]);
            buttonLeft.IsEnabled=false;
            


        }
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
        {      if (i <1)
                {
                    buttonLeft.IsEnabled = false;
                }
                if (i == count-1)
                {
                    buttonRight.IsEnabled = false;
                }
                if (i > 0)
                {
                    buttonLeft.IsEnabled = true;
                }
                if (i < count-1)
                {
                    buttonRight.IsEnabled = true;
                }
            using (var db = new DatabaseContext())
            {
                _answerList = db.Answers.Where(a => a.PointId == cur.Id).ToList();
                List<KeyValuePair<string, int>> valueList = new List<KeyValuePair<string, int>>();
                foreach (Answer answer in _answerList)
                {
                    valueList.Add(new KeyValuePair<string, int>(answer.Content, answer.Counter));

                }
                pieChart.Title = cur.Title.ToString();
                pieChart.DataContext = valueList;
                

               
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {


            Point temp = _pointList[0];
              
            Button b = (Button)sender;
            if (b.Name == "buttonLeft")
            {
                    i--;
                    temp= _pointList[i];
                    
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
    



