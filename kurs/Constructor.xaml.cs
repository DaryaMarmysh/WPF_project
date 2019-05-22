using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using kurs.Models;
using Point = kurs.Models.Point;

namespace kurs
{
    public partial class Constructor
    {
        public Constructor()
        {
            InitializeComponent();
            List<string> categories;
            using (var db = new DatabaseContext())
            {
                categories = db.Categories.Select(c => c.Title).ToList();
            }
            CategoriesListBox.ItemsSource = categories;
        }

        private void AddPoint(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Answer1.Text) || string.IsNullOrEmpty(Answer2.Text) ||
                string.IsNullOrEmpty(Answer3.Text) || string.IsNullOrEmpty(Answer4.Text) ||
                string.IsNullOrEmpty(Title.Text) || string.IsNullOrEmpty(CategoriesListBox.SelectedValue.ToString()) ||
                string.IsNullOrEmpty(PointBox.Text))
            {
                ErrorBlock.Text = "Все поля обязательны для заполнения!";
                return;
            }

            using (var db = new DatabaseContext())
            {
                CreateTest(db);
            }

            ErrorBlock.Text = "Вопрос успешно добавлен!";
            ClearFields();
        }

        private void ClearFields()
        {
            Answer1.Text = string.Empty;
            Answer2.Text = string.Empty;
            Answer3.Text = string.Empty;
            Answer4.Text = string.Empty;
            PointBox.Text = string.Empty;
        }

        private void CreateTest(DatabaseContext db)
        {
            var category =
                db.Categories.FirstOrDefault(c => c.Title.Equals(CategoriesListBox.SelectedValue.ToString()));
            var test = db.TestTitles.FirstOrDefault(t => t.Title.Equals(Title.Text)) ?? new TestTitle() { Title = Title.Text };
            var point = new Point() { Title = PointBox.Text };
            var answer1 = new Answer() { Content = Answer1.Text, Counter = 0 };
            var answer2 = new Answer() { Content = Answer2.Text, Counter = 0 };
            var answer3 = new Answer() { Content = Answer3.Text, Counter = 0 };
            var answer4 = new Answer() { Content = Answer4.Text, Counter = 0 };
            point.Answers.Add(answer1);
            point.Answers.Add(answer2);
            point.Answers.Add(answer3);
            point.Answers.Add(answer4);
            test.Points.Add(point);
            category?.TestTitles.Add(test);
            db.SaveChanges();
        }
    }
}
