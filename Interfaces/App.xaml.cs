using System.Configuration;
using System.Data;
using System.Windows;

namespace Interfaces
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var manager = new OptionManager();
            manager.AppliquerTailleTexte(ConfigurationJeu.TailleTexte);
            manager.AppliquerContraste(ConfigurationJeu.Contraste);
            manager.AppliquerCouleurFond(ConfigurationJeu.CouleurFond);
        }
    }

}
