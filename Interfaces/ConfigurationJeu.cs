using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public static class ConfigurationJeu
    {
        // Paramètre du jeu
        public static int LargeurGrille { get; set; }
        public static int HauteurGrille { get; set; }
        public static int JetonsPourGagner { get; set; }
        public static string LimiteTemps { get; set; } = "Aucune";

        public static string ModeDeJeu { get; set; } = "Classique";

        // Option du jeu
        public static double TailleTexte { get; set; } = 2.0;    // De 1 à 3
        public static double Contraste { get; set; } = 1.0;      // De 1 à 3
        public static string CouleurFond { get; set; } = "CouleurBleuFonce"; // Stocke l'hexa de la couleur
        public static string FormeJeton { get; set; } = "Rond"; // Rond, Carré, Etoile, Triangle
    }
}

