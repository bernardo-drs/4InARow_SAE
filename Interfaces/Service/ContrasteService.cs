using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Interfaces.Service
{
    public static class ContrasteService
    {
        public static void AppliquerContraste(DependencyObject page)
        {
            double contraste = ConfigurationJeu.Contraste;

            // Textes en jaune si contraste >= 2
            foreach (var tb in FindVisualChildren<TextBlock>(page))
            {
                if (tb.Tag?.ToString() == "Error")
                    continue;

                if (tb.Tag?.ToString() == "NoContrast")
                    continue;

                if (contraste >= 2.0)
                    tb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD700"));
                else
                    tb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FAFAFA"));
            }

            // Boutons (Border avec Tag) en rouge si contraste >= 3
            if (contraste >= 3.0)
            {
                foreach (var border in FindVisualChildren<Border>(page))
                {
                    if (border.Tag != null)
                        border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
                }
            }
            else
            {
                foreach (var border in FindVisualChildren<Border>(page))
                {
                    if (border.Tag != null)
                        border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(border.Tag.ToString()));
                }
            }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) yield break;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T t) yield return t;
                foreach (var descendant in FindVisualChildren<T>(child))
                    yield return descendant;
            }
        }
    }
}

