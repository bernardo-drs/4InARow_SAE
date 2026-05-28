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
using Interfaces.Service;

namespace Interfaces.Pages
{
    /// <summary>
    /// Logique d'interaction pour ConfirmationSortieOption.xaml
    /// </summary>
    public partial class ConfirmationSortieOption : Page
    {
        public ConfirmationSortieOption()
        {
            InitializeComponent();
        }

        private void OuiOptionQuitterButton_Click(object sender, RoutedEventArgs e)
        {
            PageService.PopUp("OptionsQuitter");
            PageService.Navigate("Accueil");
            PageService.PopUp(null);


        }

        private void NonOptionQuitterButton_Click(object sender, RoutedEventArgs e)
        {
            PageService.Navigate("Options");
            PageService.PopUp(null);
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
