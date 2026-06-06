using System;
using System.Collections.Generic;
using System.Text;
using Systeme.User;

namespace Systeme.Game
{
    public class Partie
    {
        private Grille plateau;
        private DateTime debut;
        private string etatPartie;
        private int nbJetonAAligner;
        private TimeSpan temps;
        private Joueur[] listeParticipant;
        private Joueur participantActuel;

        public Partie(Joueur j1, Joueur j2, int nbAAligner = 4, int lignes = 6, int colonnes = 7)
        {
            this.plateau = new Grille(lignes, colonnes);
            this.debut = DateTime.Now;
            this.etatPartie = "Configuration";
            this.nbJetonAAligner = nbAAligner;
            this.listeParticipant = new Joueur[2];
            this.listeParticipant[0] = j1;
            this.listeParticipant[1] = j2;
            this.participantActuel = j1;
        }

        public void InitialiserGrille()
        {
            this.plateau.ReinitialiserGrille();
        }

        public void ChangerTour()
        {
            if (this.participantActuel == this.listeParticipant[0])
            {
                this.participantActuel = this.listeParticipant[1];
            }
            else
            {
                this.participantActuel = this.listeParticipant[0];
            }
        }

        public bool VerifierFin()
        {
            if (this.plateau.VerifierAlignement(
                    this.participantActuel.GetCouleurJeton(),
                    this.nbJetonAAligner))
            {
                this.etatPartie = "Termine";
                this.participantActuel.IncrementerVictoire();
                return true;
            }

            if (this.plateau.GrilleEstPleine())
            {
                this.etatPartie = "Termine";
                return true;
            }

            return false;
        }

        public void DemarrerPartie()
        {
            this.etatPartie = "EnCours";
            this.debut = DateTime.Now;
        }

        public Grille GetPlateau()
        {
            return this.plateau;
        }

        public Joueur[] GetListeParticipant()
        {
            return this.listeParticipant;
        }

        public Joueur GetParticipantActuel()
        {
            return this.participantActuel;
        }
    }
}
