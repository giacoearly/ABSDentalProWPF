﻿using System;
using System.Collections.Generic;
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

namespace ABS_Dental_Pro
{
    /// <summary>
    /// Interaction logic for DespreWindow.xaml
    /// </summary>
    public partial class DespreWindow : Window
    {
        public DespreWindow()
        {
            InitializeComponent();
        }

        private void DespreWindowOKClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
