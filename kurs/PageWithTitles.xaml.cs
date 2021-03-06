﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using kurs.Models;
using kurs.Context;
using System.Windows.Media;

namespace kurs
{
    /// <summary>
    /// Логика взаимодействия для PageWithTitles.xaml
    /// </summary>
    public partial class PageWithTitles : Page
    {
        private List<TestTitle> _titleList;
        private List<Button> _buttons;

        public Test Test { get; set; }
        public static int MyCurrentTag;
        public PageWithTitles()
        {
            
            Test = new Test();
            DataContext = Test;
            using (var db = new DatabaseContext())
            {
                _titleList = db.TestTitles.Where(p=>p.CategoryId==MyCurrentTag).ToList();
            }

            InitializeComponent();
            ShowTitles(null, new RoutedEventArgs());
        }

        // private int _curI;
        

        

        private void ShowTitles(object sender, RoutedEventArgs args)
        {
            _buttons = new List<Button>();
            BringButtonsToFront();

        }

        private void BringButtonsToFront()
        {
            using (var db = new DatabaseContext())
            {
                var titles = db.TestTitles.Where(p => p.CategoryId == MyCurrentTag).ToList();
                CreateButtonsList(titles);
                Test.Buttons = _buttons;
            }
        }

        private void CreateButtonsList(ICollection<TestTitle> titles)
        {
            if (titles == null) return;
            
            foreach (var title in titles)
            {
                var button = new Button()
                {
                    Content = title.Title,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10, 14, 0, 0),
                    MinWidth = 300,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Tag= title.Id
                };
                //////добавить в юзер список тестов
                button.Click += SelectCategory;
                if (title.DoUsers!= null)
                {
                    foreach (User u in title.DoUsers)
                    {
                        if (u.Id == CurrentUser.User.Id)

                        {
                            button.Foreground = Brushes.Gray;
                        }
                    }
                }
                _buttons.Add(button);
            }
        }
        private void SelectCategory(object sender, RoutedEventArgs args)
        {
            Button b = (Button)sender;
            using (var db = new DatabaseContext())
            {
                TestTitle title = db.TestTitles.FirstOrDefault(t => t.Id == (int)b.Tag);
                title.DoUsers.Add(db.Users.Find(CurrentUser.User.Id));
                db.SaveChanges();
            }
            Testing.MyCurrentTest = int.Parse(b.Tag.ToString());
            this.NavigationService.Navigate(new Uri("Testing.xaml", UriKind.RelativeOrAbsolute));
            // this.NavigationService.Navigate(new Uri("Testing.xaml", UriKind.RelativeOrAbsolute));
            
        }
    }
}
