using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Interfaces.Service;

namespace Interfaces.Pages
{
    /// <summary>
    /// Logique d'interaction pour Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        private DispatcherTimer _timer;
        private int _secondesRestantes = 30; //faire la référence avec ce que l'on choisit dans la page ParametreJeu
        public Game()
        {
            InitializeComponent();
            StartTimer();
        }

        private void StartTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) =>
            {
                _secondesRestantes--;
                int min = _secondesRestantes / 60;
                int sec = _secondesRestantes % 60;
                TimerText.Text = $"{min} : {sec:D2}";

                if (_secondesRestantes <= 0)
                {
                    _timer.Stop();
                }
            };
            _timer.Start();
        }

        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            PageService.PopUp("MenuPause");
        }

    }
}
