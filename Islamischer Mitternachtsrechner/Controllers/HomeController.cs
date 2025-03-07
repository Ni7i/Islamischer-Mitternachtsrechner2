using Microsoft.AspNetCore.Mvc;
using System;

namespace IslamicMidnightCalculator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(TimeSpan fajr, TimeSpan maghrib)
        {
            // Easter Egg: Bei genau 03:00 (Fajr) und 23:14 (Maghrib) wird eine spezielle Nachricht angezeigt
            if (fajr == new TimeSpan(3, 0, 0) && maghrib == new TimeSpan(23, 14, 0))
            {
                ViewBag.Midnight = "<h1 class='display-1 text-center '>Te du shume akhi!</h1>";
                return View("Index");
            }

            // Falls Fajr kleiner oder gleich Maghrib ist, wird Fajr auf den nächsten Tag gesetzt
            if (fajr <= maghrib)
                fajr = fajr.Add(TimeSpan.FromHours(24));

            // Berechnung der Nacht: Nacht = Fajr - Maghrib
            TimeSpan nightDuration = fajr - maghrib;
            // Halbe Nachtdauer
            TimeSpan halfNight = TimeSpan.FromMinutes(nightDuration.TotalMinutes / 2);
            // Islamische Mitternacht = Maghrib + halbe Nachtdauer
            TimeSpan midnight = maghrib.Add(halfNight);

            // Falls die berechnete Zeit >= 24h ist, wird sie um 24 Stunden reduziert
            if (midnight.TotalHours >= 24)
                midnight = midnight.Subtract(TimeSpan.FromHours(24));

            ViewBag.Midnight = $"<h3>Islamische Mitternacht: {midnight:hh\\:mm}</h3>";
            return View("Index");
        }
    }
}
