using Interfaces.Service;
using System;
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

namespace Interfaces.Pages
{
    /// <summary>
    /// Logique d'interaction pour Quitter.xaml
    /// </summary>
    public partial class Quitter : Page
    {
        MainWindow window;

        public Quitter(MainWindow w)
        {
            InitializeComponent();
            window = w;
            this.Loaded += (s, e) => ContrasteService.AppliquerContraste(this);
            this.IsVisibleChanged += (s, e) => ContrasteService.AppliquerContraste(this);
        }

      

        // Utilitaire pour trouver tous les enfants d'un type donné
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) yield break;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T t) yield return t;
                foreach (var descendant in FindVisualChildren<T>(child))
                    yield return descendant;
            }
        }

        private void NonQuitterButton_Click(object sender, RoutedEventArgs e)
        {
            PageService.PopUp(null);
        }

        private void OuiQuitterButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnMouseEnterButton(object sender, MouseEventArgs e)
        {
            AnimationService.FadeColor(sender, 0.2, "In", null, null);
        }

        private void OnMouseLeaveButton(object sender, MouseEventArgs e)
        {
            AnimationService.FadeColor(sender, 0.2, "Out", null, null);
        }
    }
}