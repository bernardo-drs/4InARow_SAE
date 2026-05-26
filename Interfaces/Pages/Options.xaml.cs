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
using Systeme;

namespace Interfaces.Pages
{
    public partial class Options : Page
    {
        private readonly OptionManager _manager = new();

        public Options()
        {
            InitializeComponent();
        }

        private void BtnAppliquerOptions_Click(object sender, RoutedEventArgs e)
        {
            _manager.AppliquerTailleTexte(SliderTailleTexte.Value);

            var brushContraste = _manager.AppliquerContraste(SliderContraste.Value);
            this.Background = brushContraste;

            string? nomCouleur = GetCouleurCochee();
            if (nomCouleur != null)
            {
                var brushCouleur = _manager.AppliquerCouleurFond(nomCouleur);
                if (brushCouleur != null)
                    this.Background = brushCouleur;
            }

            _manager.AppliquerFormeJeton(GetFormeCochee());
        }

        private void BtnFermer_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService?.CanGoBack == true)
                NavigationService.GoBack();
        }

        private string? GetCouleurCochee()
        {
            var boutons = new[] { CouleurBleuFonce, CouleurMauve, CouleurVert,
                                  CouleurOrange, CouleurJaune, CouleurBleuClair };
            foreach (var rb in boutons)
                if (rb.IsChecked == true) return rb.Name;
            return null;
        }

        private string GetFormeCochee()
        {
            if (FormeCarré.IsChecked == true) return "Carré";
            if (FormeEtoile.IsChecked == true) return "Etoile";
            if (FormeTriangle.IsChecked == true) return "Triangle";
            return "Rond";
        }
    }
}
