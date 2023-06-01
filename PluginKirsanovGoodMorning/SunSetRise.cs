// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoordinateSharp;

namespace PluginKirsanovGoodMorning
{
    public class SunSetRise
    {
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime dt = DateTime.Now;
        /// <summary>
        /// Часовой пояс
        /// </summary>
        public int timeZone = 3;
        // Координаты
        /// <summary>
        /// Широта
        /// </summary>
        public double latitude = 52.650652;
        /// <summary>
        /// Долгота 
        /// </summary>
        public double longitude = 42.728663;

        // Восход
        public string SunRise()
        {
            Coordinate cSegodnya = new Coordinate(latitude, longitude, dt);
            string timeRise = convertTime(cSegodnya.CelestialInfo.SunRise.ToString(), timeZone);
            return timeRise;
        }
        // Закат
        public string SunSet()
        {
            Coordinate cSegodnya = new Coordinate(latitude, longitude, dt);
            string timeSet = convertTime(cSegodnya.CelestialInfo.SunSet.ToString(), timeZone);
            return timeSet;
        }
        // Долгота дня
        public string DayDuration()
        {
            Coordinate cSegodnya = new Coordinate(latitude, longitude, dt);
            DateTime myRise = DateTime.ParseExact(cSegodnya.CelestialInfo.SunRise.ToString(), "dd.MM.yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            DateTime mySet = DateTime.ParseExact(cSegodnya.CelestialInfo.SunSet.ToString(), "dd.MM.yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            TimeSpan mydate = mySet - myRise;
            return mydate.Hours.ToString() + ":" + mydate.Minutes.ToString("D2");
        }

        // Конвертация даты
        private string convertTime(string date, int timeZone)
        {
            DateTime myDate = DateTime.ParseExact(date, "dd.MM.yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            myDate = myDate.AddHours(timeZone);

            return myDate.ToString("HH:mm", System.Globalization.CultureInfo.GetCultureInfo("RU-ru"));
        }
    }
}
