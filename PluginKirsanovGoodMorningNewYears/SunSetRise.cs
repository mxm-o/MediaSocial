// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using SunSetRiseLib;

namespace PluginKirsanovGoodMorningNewYears
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
        /// <summary>
        /// Переход на летнее время
        /// </summary>
        public bool dst = false;


        // Восход
        public string SunRise()
        {
            double JD = Util.calcJD(dt);
            double sunRise = Util.calcSunRiseUTC(JD, latitude, longitude);
            return Util.getDateTime(sunRise, timeZone, dt, dst).Value.ToString("HH:mm");
        }
        // Закат
        public string SunSet()
        {
            double JD = Util.calcJD(dt);
            double sunSet = Util.calcSunSetUTC(JD, latitude, longitude);
            return Util.getDateTime(sunSet, timeZone, dt, dst).Value.ToString("HH:mm");
        }
        // Долгота дня
        public string DayDuration()
        {
            double JD = Util.calcJD(dt);
            double sunRise = Util.calcSunRiseUTC(JD, latitude, longitude);
            double sunSet = Util.calcSunSetUTC(JD, latitude, longitude);

            double mydate = sunSet - sunRise;
            return Util.getDateTime(mydate, 0, dt, dst).Value.ToString("HH:mm");
        }
    }
}
