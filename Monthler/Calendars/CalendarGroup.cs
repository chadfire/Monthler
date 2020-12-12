using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace Monthler.Calendars
{
    /// <summary>
    /// Creates a group of modifiable calendars.
    /// </summary>
    public class CalendarGroup
    {
        #region Constructors

        /// <summary>
        /// Creates 12 calendars from the start of the current year to the end. Jan-Dec.
        /// </summary>
        /// <param name="monthsToCreate">How many calendars to create</param>
        public CalendarGroup(int monthsToCreate = 12)
        {
            this.CreateNewCalendarList(monthsToCreate);
        }

        #endregion

        #region Properties

        public ObservableCollection<Calendar> Calendars { get; set; } = new ObservableCollection<Calendar>();

        #endregion

        #region Methods

        /// <summary>
        /// Adds a set amount of year to each calendar
        /// </summary>
        /// <param name="yearsToAdd"></param>
        //public void AddYears(int yearsToAdd)
        //    => this.Calendars.ForEach(cal => cal.DisplayDate = cal.DisplayDate.AddYears(yearsToAdd));

        /// <summary>
        /// Sets the year for all the calendars
        /// </summary>
        /// <param name="year">The year to set all the calendars to</param>
        //public void SetYear(int year)
        //    => this.Calendars.ForEach(cal => cal.DisplayDate = new DateTime(year, cal.DisplayDate.Month, cal.DisplayDate.Day));

        /// <summary>
        /// Clears out the all the calendars. Creates new ones in their place.
        /// </summary>
        /// <param name="numCalendarsToCreate">How many calendars to create, one per month.</param>
        public void CreateNewCalendarList(int numCalendarsToCreate)
        {
            this.Calendars.Clear();
            for (int i = 0; i < numCalendarsToCreate; i++)
            {
                this.Calendars.Add(new Calendar()
                {
                    DisplayDate = new DateTime(DateTime.Now.Year, i + 1, DateTime.Now.Day)
                });
            }
        }

        /// <summary>
        /// Sets the starting calendar to jan of current year and for
        /// each calendar, increment by one month. jan, feb....
        /// </summary>
        public void ResetCalendarDates()
        {
            for (int i = 0; i < this.Calendars.Count; i++)
            {
                this.Calendars[i].DisplayDate = DateTime.Now.AddMonths(i);
            }
        }

        #endregion
    }
}
