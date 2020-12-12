using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Monthler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Fields

        /// <summary>
        /// Whether the window always stays on top.
        /// </summary>
        private bool isWindowPinned = false;

        /// <summary>
        /// The list of calendars displayed in the wrap layout.
        /// </summary>
        public List<Calendar> Calendars = new List<Calendar>();

        #endregion

        #region Keybinds

        RoutedCommand newCmd = new RoutedCommand();

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            newCmd.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(newCmd, MiAdvanceYear_Click));

            // Create starting calendars
            const int CALENDARS_TO_CREATE = 12;
            for (int i = 0; i < CALENDARS_TO_CREATE; i++)
            {
                Calendars.Add(new Calendar()
                {
                    DisplayDate = new DateTime(DateTime.Now.Year, i+1, DateTime.Now.Day)
                });
                WpCalendars.Children.Add(Calendars[i]);
            }
        }

        #region Pinning window

        /// <summary>
        /// Pins the calendar window above all other windows.
        /// </summary>
        private void BtnPinWindow_Click(object sender, RoutedEventArgs e)
        {
            isWindowPinned = !isWindowPinned;
            this.Topmost = isWindowPinned;
            this.ChangePinText();
        }

        /// <summary>
        /// Changes the pin's btn text, based on if the window is pinned or not.
        /// </summary>
        private void ChangePinText()
        {
            if (isWindowPinned)
            {
                BtnPinWindow.Content = "Unpin Window";
            }
            else
            {
                BtnPinWindow.Content = "Pin Window";
            }
        }

        #endregion

        #region Application Menus

        /// <summary>
        /// Advance year from menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiAdvanceYear_Click(object sender, RoutedEventArgs e)
        {
            AdvanceYear();
        }

        public void AdvanceYear()
        {
            WpCalendars.Children.RemoveRange(0, WpCalendars.Children.Count);
            foreach (var calendar in Calendars)
            {
                calendar.DisplayDate = calendar.DisplayDate.AddYears(1);
                WpCalendars.Children.Add(calendar);
            }
        }

        #endregion
    }
}
