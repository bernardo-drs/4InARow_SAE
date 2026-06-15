using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public static class ConfigurationJeu
    {
        public static int LargeurGrille { get; set; } = 7;
        public static int HauteurGrille { get; set; } = 6;
        public static int JetonsPourGagner { get; set; } = 4;
        public static string LimiteTemps { get; set; } = "Aucune";

        public static int JoueurQuiCommence { get; set; } = 1;

        public static string ModeDeJeu { get; set; } = "Classique";

        public static int VictoiresRequises { get; set; } = 3;

        public static string NomJoueur1 { get; set; } = "Joueur 1";
        public static string NomJoueur2 { get; set; } = "Joueur 2";
        public static string CouleurJoueur1 { get; set; } = "#E53935";
        public static string CouleurJoueur2 { get; set; } = "#FDD835";
        public static bool Joueur2EstBot { get; set; } = false;

        public static int NiveauIA { get; set; } = 5;

        public static int ScoreJoueur1 { get; set; } = 0;
        public static int ScoreJoueur2 { get; set; } = 0;


        public static double TailleTexte { get; set; } = 2.0;
        public static double Contraste { get; set; } = 1.0;
        public static string CouleurFond { get; set; } = "CouleurBleuFonce";
        public static string FormeJeton { get; set; } = "Rond";
    }
}

