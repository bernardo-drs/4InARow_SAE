using Interfaces.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Service
{
    public class PageService
    {
        private static MainWindow _window;

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

        public void Navigate(string PageName)
        {

            /*
             string PageName nom de la page que vous voulez afficher

            Change la page afficher sur la fenêtre
             */

            switch(PageName)
            {
                case "Accueil":
                    Window.mainFrame.Content = new Accueil(Window);
                    break;
                case "Options":
                    Window.mainFrame.Content = new Options();
                    break;
                case "Historique":
                    Window.mainFrame.Content = new HistoriquePartie();
                    break;
                default:
                    break;
            }
        }

    }
}
