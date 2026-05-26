using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Interfaces.Pages
{
    /// <summary>
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : Page
    {
        public Accueil()
        {
            InitializeComponent();
        }

        public static void FadeButtonColor(object sender, string mode)
        {

            if (sender == null) return;

            Border border = (Border)sender;

            if (border == null) return;

            ColorAnimation fade = new ColorAnimation();

            fade.From = (Color) ColorConverter.ConvertFromString(mode == "In" ? border.Tag.ToString() : "#808080");
            fade.To = (Color) ColorConverter.ConvertFromString(mode == "In" ? "#808080" : border.Tag.ToString());
            fade.Duration = TimeSpan.FromSeconds(0.1);

            SolidColorBrush brush = new SolidColorBrush();
            border.Background = brush;

            brush.BeginAnimation(SolidColorBrush.ColorProperty, fade);
        }

        private void OnMouseEnterButton(object sender, MouseEventArgs e)
        {
            FadeButtonColor(sender, "In");
        }

        private void OnMouseLeaveButton(object sender, MouseEventArgs e)
        {
            FadeButtonColor(sender, "Out");

        }
    }
}
