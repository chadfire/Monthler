using Monthler.Calendars;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        /// The calendars to display to the user.
        /// </summary>
        public CalendarGroup CalendarGroup { get; set; } = new CalendarGroup();

        #endregion // Fields

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;
            this.AddHotKeys();
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

        #endregion // Pinning window

        #region Application Menus

        #region Edit
        /// <summary>
        /// Advance year from menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiAdvanceYear_Click(object sender, RoutedEventArgs e)
            => this.CalendarGroup.AddYears(1);

        private void MiSubtractYear_Click(object sender, RoutedEventArgs e)
            => this.CalendarGroup.AddYears(-1);

        private void MiAdd10Years_Click(object sender, RoutedEventArgs e)
            => this.CalendarGroup.AddYears(10);

        private void MiSubtract10Years_Click(object sender, RoutedEventArgs e)
            => this.CalendarGroup.AddYears(-10);

        #endregion // Edit

        #endregion // Context menus

        #region Hotkeys

        private void AddHotKeys()
        {
            try
            {
                RoutedCommand firstSettings = new RoutedCommand();
                firstSettings.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Alt));
                CommandBindings.Add(new CommandBinding(firstSettings, My_first_event_handler));

                RoutedCommand secondSettings = new RoutedCommand();
                secondSettings.InputGestures.Add(new KeyGesture(Key.B, ModifierKeys.Alt));
                CommandBindings.Add(new CommandBinding(secondSettings, My_first_event_handler));
            }
            catch (Exception)
            {
                //handle exception error
                MessageBox.Show("Error: Could not load hotkeys", "Error");
            }
        }

        private void My_first_event_handler(object sender, ExecutedRoutedEventArgs e)
        {
            //handler code goes here.
            MessageBox.Show("Alt+A key pressed");
        }

        #endregion //Hotkeys
    }
}
