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

        #endregion // Constructors

        #region Fields

        /// <summary>
        /// Defines the height, width of a calendar in pixles
        /// </summary>
        public readonly List<(int width, int height)> CalendarDimensions = new List<(int width, int height)>
        {
            (172, 150), // Compact
            (179, 165), // Normal
            (185, 168)  // Extended
        };

        /// <summary>
        /// Creates a reference to the different calendar sizes
        /// </summary>
        public enum CalendarSize
        {
            Compact,
            Normal,
            Extended
        }

        #endregion // Fields

        #region Properties

        public List<Calendar> Calendars { get; set; } = new List<Calendar>();

        #endregion // Properties

        #region Methods

        // PUBLIC

        /// <summary>
        /// Adds a set amount of year to each calendar
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">If new date is invalid</exception>
        /// <param name="yearsToAdd"></param>
        public void AddYears(int yearsToAdd)
            => this.Calendars.ForEach(cal => cal.DisplayDate = cal.DisplayDate.AddYears(yearsToAdd));

        /// <summary>
        /// Sets the year for all the calendars
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Date out of range</exception>
        /// <param name="year">The year to set all the calendars to</param>
        public void SetYear(int year)
            => this.Calendars.ForEach(cal => cal.DisplayDate = new DateTime(year, cal.DisplayDate.Month, cal.DisplayDate.Day));

        /// <summary>
        /// Clears out the all the calendars. Creates new ones in their place.
        /// Sets the starting calendar to two months behind todays date
        /// </summary>
        /// <param name="numCalendarsToCreate">How many calendars to create, one per month.</param>
        public void CreateNewCalendarList(int numCalendarsToCreate)
        {
            this.Calendars.Clear();
            for (int i = 0; i < numCalendarsToCreate; i++)
            {
                this.Calendars.Add(new Calendar()
                {
                    DisplayDate = DateTime.Now.AddMonths(-2 + i)
                });
            }
        }

        /// <summary>
        /// Sets the starting calendar to two months behind todays date
        /// each calendar, increment by one month. jan, feb....
        /// </summary>
        public void ResetCalendarDates()
        {
            for (int i = 0; i < this.Calendars.Count; i++)
            {
                this.Calendars[i].DisplayDate = DateTime.Now.AddMonths(-2 + i);
            }
        }

        /// <summary>
        /// Gets the dimensions of a calendar of width, height in pixles
        /// </summary>
        /// <param name="calendarSize">Specifies what size the calendar should be</param>
        /// <returns></returns>
        public (int width, int height) GetCalendarDimension(CalendarSize calendarSize)
            => this.CalendarDimensions[(int)calendarSize];

        /// <summary>
        /// Resizes the pixel size of each calendar.
        /// </summary>
        /// <param name="calendarSize">Specifiy what size should the calendars be</param>
        public void ResizeCalendars(CalendarSize calendarSize)
        {
            (int width, int height) = GetCalendarDimension(calendarSize);
            foreach (Calendar calendar in this.Calendars)
            {
                calendar.Height = height;
                calendar.Width = width;
            }
        }

        #endregion // Methods
    }
}
