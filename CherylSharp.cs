using System;
using System.Collections.Generic;
using System.Linq;

namespace cheryl
{
    /// <summary>
    /// A silly little program that solves the Cheryl's birthday problem. 
    /// Inspired by Peter Norvig's Python solution at: http://nbviewer.ipython.org/url/norvig.com/ipython/Cheryl.ipynb
    /// </summary>
    class CherylSharp
    {
        static readonly string[] Dates =
        {
            "May 15",
            "May 16",
            "May 19",
            "June 17",
            "June 18",
            "July 14",
            "July 16",
            "August 14",
            "August 15",
            "August 17"
        };

        static void Main()
        {
            Console.WriteLine(CherylsBirthday());
        }

        static string Month(string str)
        {
            return str.Split(' ')[0];
        }

        static int Day(string str)
        {
            return int.Parse(str.Split(' ')[1]);
        }

        /// <summary>
        /// Cheryl tells a part of her birthdate to someone; return a new list of possible dates that match the part.
        /// </summary>
        static IEnumerable<string> Tell(string month)
        {
            return Dates.Where(d => Month(d) == month);
        }

        /// <summary>
        /// Cheryl tells a part of her birthdate to someone; return a new list of possible dates that match the part.
        /// </summary>
        static IEnumerable<string> Tell(int day)
        {
            return Dates.Where(d => Day(d) == day);
        }

        /// <summary>
        /// A person knows the birthdate if they have exactly one possible date.
        /// </summary>
        static bool Know(IEnumerable<string> dates)
        {
            return dates.Count() == 1;
        }

        static string CherylsBirthday()
        {
            return Dates.Single(Statements3To5);
        }

        static bool Statements3To5(string date)
        {
            return Statement3(date) && Statement4(date) && Statement5(date);
        }

        /// <summary>
        /// "Albert: I don't know when Cheryl's birthday is, but I know that Bernard does not know too."
        /// </summary>
        static bool Statement3(string date)
        {
            var possibleDates = Tell(Month(date));
            return !Know(possibleDates) && possibleDates.All(d => !Know(Tell(Day(d))));
        }

        /// <summary>
        /// Bernard: At first I don't know when Cheryl's birthday is, but I know now.
        /// </summary>
        static bool Statement4(string date)
        {
            var atFirst = Tell(Day(date));
            return !Know(atFirst) && Know(atFirst.Where(Statement3));
        }

        /// <summary>
        /// Albert: Then I also know when Cheryl's birthday is.
        /// </summary>
        static bool Statement5(string date)
        {
            return Know(Tell(Month(date)).Where(Statement4));
        }
    }
}
