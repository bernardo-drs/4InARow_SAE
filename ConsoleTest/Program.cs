using System;
using Systeme.Game;
using Systeme.User;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Grille plateau = new Grille();
            Joueur humain = new Humain(0, "Joueur", "Rouge", "", "");
            IntelligenceArtificielle ia = new IntelligenceArtificielle(1, "IA", "Jaune", 5);
            Partie partie = new Partie(humain, ia);

            Console.WriteLine("=== Test IA Puissance 4 ===");
            Console.WriteLine("Grille vide — premier coup de l'IA :");
            ia.ChoisirCoup(plateau, partie);

            Console.ReadLine();
        }
    }
}