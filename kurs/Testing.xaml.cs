using System;
using kurs.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using kurs.Context;
using Point = kurs.Models.Point;
namespace kurs
{
    public partial class Testing:Page
    {
        private int _curI;public double max = 0;
        private List<Point> _testList;
        TestTitle TestTitle;
        private List<Button> _buttons;
        public static int MyCurrentTest;
        public Test Test { get; set; }
        public Testing()
            
        {
            _curI = 0;
            Test = new Test();
            DataContext = Test;
          

            using (var db = new DatabaseContext())
            {
                _testList = db.Points.Where(p=> p.TestTitleId==MyCurrentTest).ToList();
                try
                {   TestTitle = db.TestTitles.First(p => p.Id == MyCurrentTest);
                    ResultOfTest.MyCurrentTest = TestTitle.Id;
                   
                }
                catch
                { }

            }

            InitializeComponent();
            StartTest(null, new RoutedEventArgs());
        }

        private void StartTest(object sender, RoutedEventArgs args)
        {
            _buttons = new List<Button>();
            var btns = sender as TextBlock;
            if (_curI != 0)
            {
                SaveAnswer();
                if (btns != null) IncreaseCounter((string)btns.Text);
            }
            if (_curI < _testList.Count)
                BringButtonsToFront();
            else
            {
               
                Questions.ItemsSource = null;
                TitleLabel.Text = "Спасибо :)";

                _curI = 0;
                
               
            }
        }

        private void BringButtonsToFront()
        {
            using (var db = new DatabaseContext())
            {
                TitleofTest.Content = TestTitle.Title;
                TitleLabel.Text = _testList[_curI].Title;
                var curPoint = _testList.ElementAt(_curI).Id;
                _curI++;
                var answers = db.Points.Include(p => p.Answers).FirstOrDefault(p => p.Id == curPoint)
                    ?.Answers;
                CreateButtonsList(answers);
        
                Test.Buttons = _buttons;
            }
        }
        private void GoToResult(object sender, RoutedEventArgs args)
        {

            this.NavigationService.Navigate(new Uri("ResultOfTest.xaml", UriKind.RelativeOrAbsolute));
        }
        private void CreateButtonsList(ICollection<Answer> answers)
        {
            if (answers == null) return;
            foreach (var answer in answers)
            {
                TextBlock t = new TextBlock();
                t.TextWrapping = TextWrapping.Wrap;
                var button = new Button()
                {


                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10, 14, 0, 0),
                    Padding = new Thickness(0),
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    MinWidth=300,
                    MaxWidth=300,
                    Height=Double.NaN
                  
                };
                t.Padding = new Thickness(5);
                t.Margin = new Thickness(0);
                t.MinWidth = 300;
                t.MaxWidth=300;
                t.TextAlignment = TextAlignment.Center;
                t.Text = answer.Content;
                t.Height = Double.NaN;
                button.Content = t;
                button.Click += StartTest;
                _buttons.Add(button);
            }
        }

        private void SaveAnswer()
        {
            using (var db = new DatabaseContext())
            {
                var point = db.Points.FirstOrDefault(p => p.Title.Equals(TitleLabel.Text.ToString()));
                db.Users.FirstOrDefault(u => u.Id == CurrentUser.User.Id)?.PassedPoints.Add(point);
                db.SaveChanges();
            }
        }

        private void IncreaseCounter(string content)
        {
            var title = TitleLabel.Text == null ? string.Empty : TitleLabel.Text.ToString();
            using (var db = new DatabaseContext())
            {
                try
                {
                    db.Points.Include(p => p.Answers).FirstOrDefault(p => p.Title.Equals(title)).Answers
                        .FirstOrDefault(a => a.Content.Equals(content)).Counter++;
                    db.SaveChanges();
                }
                catch (NullReferenceException exception)
                {
                }
            }
        }
    }
}