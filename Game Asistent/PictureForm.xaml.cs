﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy PictureForm.xaml
    /// </summary>
    public partial class PictureForm : Window
    {
        public string ImagePath = "Default";

        public bool IsAllowDrag = false;

        public PictureForm()
        {
            InitializeComponent();
            rec1.Visibility = Visibility.Hidden;
        }

        private void ImageBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsAllowDrag && e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

    }
}
