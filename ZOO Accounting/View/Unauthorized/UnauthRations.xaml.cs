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
using ZOO_Accounting.Model.Entities;
using ZOO_Accounting.ViewModel;

namespace ZOO_Accounting.View.Unauthorized
{
    /// <summary>
    /// Логика взаимодействия для Rations.xaml
    /// </summary>
    public partial class UnauthRations : Window
    {
        private IViewModel vm;
        public UnauthRations(RationViewModel model)
        {
            vm = model;
            DataContext = vm;

            InitializeComponent();
        }
    }
}
