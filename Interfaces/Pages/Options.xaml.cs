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
using Interfaces;

namespace Interfaces.Pages
{
    public partial class Options : Page
    {
        private readonly OptionManager _manager = new();
        public Options()
        {
            InitializeComponent();
            ChargerOptionsActuelles();

        }

        private void BtnAppliquerOptions_Click(object sender, RoutedEventArgs e)
        {

            string forme = GetFormeCochee();
            string? couleur = GetCouleurCochee() ?? "CouleurBleuFonce";
            double tailleTexte = SliderTailleTexte.Value;
            double contraste = SliderContraste.Value;

            ConfigurationJeu.FormeJeton = forme;
            ConfigurationJeu.CouleurFond = couleur;
            ConfigurationJeu.TailleTexte = tailleTexte;
            ConfigurationJeu.Contraste = contraste;

            _manager.AppliquerFormeJeton(forme);
            _manager.AppliquerTailleTexte(tailleTexte);
            _manager.AppliquerContraste(contraste);
            _manager.AppliquerCouleurFond(couleur);

            PageService.Navigate("Accueil");
        }

        private void BtnCouleur_Click(object sender, RoutedEventArgs e)
        {
            string? nomCouleur = GetCouleurCochee();
            if (nomCouleur != null)
            {
                var brushCouleur = _manager.AppliquerCouleurFond(nomCouleur);
                if (brushCouleur != null)
                    this.Background = brushCouleur;
            }

        }

        private void BtnFermer_Click(object sender, RoutedEventArgs e)
        {
            PageService.PopUp("OptionsQuitter");
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

        private void SliderTailleTexte_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _manager.AppliquerTailleTexte(SliderTailleTexte.Value);
        }

        private void SliderContraste_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _manager.AppliquerContraste(SliderContraste.Value);
        }

        private void BtnAppliquer_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimationService.FadeColor(BtnAppliquerOptions, 0.1, "In", null, null);

        }

        private void BtnAppliquer_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationService.FadeColor(BtnAppliquerOptions, 0.1, "Out", null, null);
        }

        private void ChargerOptionsActuelles()
        {
            SliderTailleTexte.Value = ConfigurationJeu.TailleTexte;
            SliderContraste.Value = ConfigurationJeu.Contraste;

            FormeRond.IsChecked = ConfigurationJeu.FormeJeton == "Rond";
            FormeCarré.IsChecked = ConfigurationJeu.FormeJeton == "Carré";
            FormeEtoile.IsChecked = ConfigurationJeu.FormeJeton == "Etoile";
            FormeTriangle.IsChecked = ConfigurationJeu.FormeJeton == "Triangle";

            CouleurBleuFonce.IsChecked = ConfigurationJeu.CouleurFond == "#20114a";
            CouleurMauve.IsChecked = ConfigurationJeu.CouleurFond == "#81608a";
            CouleurVert.IsChecked = ConfigurationJeu.CouleurFond == "#01773e";
            CouleurOrange.IsChecked = ConfigurationJeu.CouleurFond == "#a7501a";
            CouleurJaune.IsChecked = ConfigurationJeu.CouleurFond == "#ccb147";
            CouleurBleuClair.IsChecked = ConfigurationJeu.CouleurFond == "#2579a9";
        }
    }
}
