using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public static class ConfigurationJeu
    {
        public static int LargeurGrille { get; set; }
        public static int HauteurGrille { get; set; }
        public static int JetonsPourGagner { get; set; }
        public static string LimiteTemps { get; set; } = "Aucune";

        public static string ModeDeJeu { get; set; } = "Classique";
    }
}
