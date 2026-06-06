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

        // Info des joueurs ou du bot Joueur 1 = information de la gauche ET Joueur 2 = information de la droite
        public static string NomJoueur1 { get; set; } = "Joueur 1";
        public static string NomJoueur2 { get; set; } = "Joueur 2";
        public static string CouleurJoueur1 { get; set; } = "#E53935"; // Rouge par défaut
        public static string CouleurJoueur2 { get; set; } = "#FDD835"; // Jaune par défaut
        public static bool Joueur2EstBot { get; set; } = false;


        // Option du jeu
        public static double TailleTexte { get; set; } = 2.0;    // De 1 à 3
        public static double Contraste { get; set; } = 1.0;      // De 1 à 3
        public static string CouleurFond { get; set; } = "CouleurBleuFonce"; // Stocke l'hexa de la couleur
        public static string FormeJeton { get; set; } = "Rond"; // Rond, Carré, Etoile, Triangle
    }
}

