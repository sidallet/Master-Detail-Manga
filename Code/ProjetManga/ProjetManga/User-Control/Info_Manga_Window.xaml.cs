﻿using System;
using System.Collections.Generic;
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

namespace ProjetManga
{
    /// <summary>
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class Info_Manga_Window : UserControl
    {
        public Info_Manga_Window()
        {
            InitializeComponent();
        }

        public void Button_Modifier_Manga(object sender, RoutedEventArgs e)
        {
            var modifWindow = new Modifier_Window();
            modifWindow.ShowDialog();
        }

        private void Button_Supprimer_Manga(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Souhaitez-vous supprimer définitivement ce manga de l'application ?","Supprimer Manga", MessageBoxButton.OKCancel);
        }
    }
}
