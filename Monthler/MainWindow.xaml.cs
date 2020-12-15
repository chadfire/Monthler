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

        #region Header Buttons

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

        #region ResetDates

        /// <summary>
        /// Button to reset all the dates to the current year
        /// </summary>
        private void BtnResetCalendars_Click(object sender, RoutedEventArgs e)
            => this.CalendarGroup.ResetCalendarDates();

        #endregion // Reset Dates

        #endregion // Header Buttons

        #region Application Menus

        #region Edit

        /// <summary>
        /// Resets all the calendars to the current year
        /// </summary>
        private void MiResetDates_Click(object sender, RoutedEventArgs e)
            => this.CalendarGroup.ResetCalendarDates();

        /// <summary>
        /// Advances a year from all the calendars
        /// </summary>
        private void MiAdvanceYear_Click(object sender, RoutedEventArgs e)
            => this.CalendarGroup.AddYears(1);

        /// <summary>
        /// Subtracts a year from all the calendars
        /// </summary>
        private void MiSubtractYear_Click(object sender, RoutedEventArgs e)
            => this.CalendarGroup.AddYears(-1);

        /// <summary>
        /// Adds 10 years to all the calendars
        /// </summary>
        private void MiAdd10Years_Click(object sender, RoutedEventArgs e)
            => this.CalendarGroup.AddYears(10);

        /// <summary>
        /// Subtracts 10 years from all the calendars
        /// </summary>
        private void MiSubtract10Years_Click(object sender, RoutedEventArgs e)
            => this.CalendarGroup.AddYears(-10);

        #endregion // Edit

        #region View

        /// <summary>
        /// Resizes the calendar to be in a compact view (smaller)
        /// </summary>
        private void MiCompactView_Click(object sender, RoutedEventArgs e)
            => this.CalendarGroup.ResizeCalendars(CalendarGroup.CalendarSize.Compact);

        /// <summary>
        /// Resizes the calendar to be a normal size
        /// </summary>
        private void MiNormalView_Click(object sender, RoutedEventArgs e)
            => this.CalendarGroup.ResizeCalendars(CalendarGroup.CalendarSize.Normal);

        /// <summary>
        /// Resizes the calendar to have more padding around it
        /// </summary>
        private void MiExtendedView_Click(object sender, RoutedEventArgs e)
            => this.CalendarGroup.ResizeCalendars(CalendarGroup.CalendarSize.Extended);


        #endregion // View

        #endregion // Context menus

        #region Hotkeys

        private void AddHotKeys()
        {
            try
            {
                // Edit App Context
                RoutedCommand ResetDates = new RoutedCommand();
                ResetDates.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(ResetDates, MiResetDates_Click));

                RoutedCommand AddYear = new RoutedCommand();
                AddYear.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(AddYear, MiAdvanceYear_Click));

                RoutedCommand SubtractYear = new RoutedCommand();
                SubtractYear.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(SubtractYear, MiSubtractYear_Click));

                RoutedCommand Add10Years = new RoutedCommand();
                Add10Years.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(Add10Years, MiAdd10Years_Click));

                RoutedCommand Subtract10Years = new RoutedCommand();
                Subtract10Years.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(Subtract10Years, MiSubtract10Years_Click));
            }
            catch (Exception)
            {
                //handle exception error
                string message = "Error: Some hotkeys could not load.";
                string caption = "Error!";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(message, caption, buttons, icon);
            }
        }

        #endregion //Hotkeys
    }
}
