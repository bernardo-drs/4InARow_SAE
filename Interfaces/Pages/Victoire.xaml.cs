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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Interfaces.Pages
{
    /// <summary>
    /// Logique d'interaction pour Victoire.xaml
    /// </summary>
    /// 

    public partial class Victoire : Page
    {
        public Victoire()
        {
            InitializeComponent();
            bool ok = false;
            BrushConverter bc = new BrushConverter();

            if (ok)
            {
                RunVainqueur.Text = "rouge";
                Brush b = (Brush)bc.ConvertFromString("Red");
                RunVainqueur.Foreground = b;
            }
            else
            {
                RunVainqueur.Text = "vert";
                Brush b = (Brush)bc.ConvertFromString("Green");
                RunVainqueur.Foreground = b;
            }
            
        }
    }
}
