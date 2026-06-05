using Interfaces.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Service
{
    public class PageService
    {
        private static MainWindow? _window;

        public PageService(MainWindow w)
        {
            _window = w;
        }

        public PageService(string pageName)
        {

            if (Window == null) return;

            Navigate(pageName);

        }

        public MainWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static void Navigate(string? PageName)
        {

            /*
             string PageName nom de la page que vous voulez afficher

            Change la page afficher sur la fenêtre
             */

            switch(PageName)
            {
                case "Accueil":
                    _window.mainFrame.Content = new Accueil(_window);
                    break;
                case "Options":
                    _window.mainFrame.Content = new Options();
                    break;
                case "Historique":
                    _window.mainFrame.Content = new HistoriquePartie();
                    break;
                case "Game":
                    _window.mainFrame.Content = new Game();
                    break;
                case "Leaderboard":
                    _window.mainFrame.Content = new LeaderBoard();
                    break;
                case "Parametres":
                    _window.mainFrame.Content = new Parametres();
                    break;
                case "ParametreJeu":
                    _window.mainFrame.Content = new ParametreJeu();
                    break;
                default:
                    break;
            }
        }

        public static void PopUp(string? nomPage)
        {
            switch (nomPage)
            {
                case "Quitter":
                    _window.popUpFrame.Content = new Quitter(_window);
                    break;
                case "OptionsQuitter":
                    _window.popUpFrame.Content = new ConfirmationSortieOption();
                    break;
                case "ChoixModeJeu":
                    _window.popUpFrame.Content = new ChoixModeJeu();
                    break;
                default:
                    _window.popUpFrame.Content = null;
                    break;
            }
        }
    }
}
