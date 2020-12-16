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
        /// Whether the current theme is the default one.
        /// </summary>
        private bool isDefaultTheme = false;

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

            // Set starting view
            this.CalendarGroup.ResizeCalendars(CalendarGroup.CalendarSize.Compact);
            this.ApplySeasonalTheme();
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

        // Change Themes

        /// <summary>
        /// Change to default theme
        /// </summary>
        private void MiSeasonTheme_Click(object sender, RoutedEventArgs e)
            => ApplySeasonalTheme();

        /// <summary>
        /// Change to default theme
        /// </summary>
        private void MiDefaultTheme_Click(object sender, RoutedEventArgs e)
            => ApplyDefaultTheme();

        /// <summary>
        /// Applies a seasonal theme to each month
        /// </summary>
        private void ApplySeasonalTheme()
            => this.CalendarGroup.Calendars.ForEach(cal => cal.Style = GetStyleOnMonth(cal.DisplayDate));

        /// <summary>
        /// Changes the calendar themes to default colors
        /// </summary>
        private void ApplyDefaultTheme()
            => this.CalendarGroup.Calendars.ForEach(cal => cal.Style = GetCalendarStyle(CalendarStyles.Default));


        /// <summary>
        /// Different Calendar Styles
        /// </summary>
        public enum CalendarStyles
        {
            Winter,
            Spring,
            Summer,
            Fall,
            Default
        }

        /// <summary>
        /// Get a pertical style froms the styles enum
        /// </summary>
        /// <param name="styles">A style to choose from</param>
        /// <returns>T</returns>
        private Style GetCalendarStyle(CalendarStyles styles)
        {
            return styles switch
            {
                CalendarStyles.Winter => (Style)FindResource("CalendarWinterStyle"),
                CalendarStyles.Spring => (Style)FindResource("CalendarSpringStyle"),
                CalendarStyles.Summer => (Style)FindResource("CalendarSummerStyle"),
                CalendarStyles.Fall => (Style)FindResource("CalendarFallStyle"),
                CalendarStyles.Default => (Style)FindResource("CalendarDefaultStyle"),
                _ => (Style)FindResource("CalendarDefaultStyle"),
            };
        }

        /// <summary>
        /// Gets a style based on what month it is. Winter, summer...
        /// </summary>
        /// <param name="dateTime">The datetime to pick the style from</param>
        /// <returns>A new style based on the month</returns>
        private Style GetStyleOnMonth(DateTime dateTime)
        {
            CalendarStyles currentStyle = CalendarStyles.Default;
            // Winter
            if (dateTime.Month == 12 || dateTime.Month < 3)
                currentStyle = CalendarStyles.Winter;
            // Spring
            else if (dateTime.Month >= 3 && dateTime.Month <= 5)
                currentStyle = CalendarStyles.Spring;
            // Summer
            else if (dateTime.Month > 5 && dateTime.Month < 9)
                currentStyle = CalendarStyles.Summer;
            // Fall
            else if (dateTime.Month >= 9 && dateTime.Month < 12)
                currentStyle = CalendarStyles.Fall;
            return GetCalendarStyle(currentStyle);
        }

        /// <summary>
        /// Toggles the between default and seasonal theme
        /// </summary>
        private void ToggleTheme(object sender, RoutedEventArgs e)
        {
            isDefaultTheme = !isDefaultTheme;
            if (isDefaultTheme)
                ApplyDefaultTheme();
            else
                ApplySeasonalTheme();
        }

        // Change Calendar Spacing 

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
                #region Application Menus

                #region Edit

                RoutedCommand ResetDates = new RoutedCommand();
                ResetDates.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(ResetDates, MiResetDates_Click));

                RoutedCommand AddYear = new RoutedCommand();
                AddYear.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(AddYear, MiAdvanceYear_Click));

                RoutedCommand SubtractYear = new RoutedCommand();
                SubtractYear.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(SubtractYear, MiSubtractYear_Click));

                RoutedCommand Add10Years = new RoutedCommand();
                Add10Years.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(Add10Years, MiAdd10Years_Click));

                RoutedCommand Subtract10Years = new RoutedCommand();
                Subtract10Years.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(Subtract10Years, MiSubtract10Years_Click));

                #endregion // Edit

                #region View

                // Themes

                RoutedCommand TogTheme = new RoutedCommand();
                TogTheme.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(TogTheme, ToggleTheme));

                // Calendar sizes

                RoutedCommand CompactView = new RoutedCommand();
                CompactView.InputGestures.Add(new KeyGesture(Key.Z, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(CompactView, MiCompactView_Click));

                RoutedCommand NormalView = new RoutedCommand();
                NormalView.InputGestures.Add(new KeyGesture(Key.X, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(NormalView, MiNormalView_Click));

                RoutedCommand ExtendedView = new RoutedCommand();
                ExtendedView.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(ExtendedView, MiExtendedView_Click));

                #endregion // View

                #endregion // Application menu
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
