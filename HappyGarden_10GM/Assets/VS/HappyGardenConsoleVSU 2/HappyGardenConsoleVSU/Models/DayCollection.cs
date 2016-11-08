using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyGardenConsoleVSU 
{
    // Using a string as an indexer value
    public class DayCollection
    {
        string[] days = { "Sun", "Mon", "Tues", "Wed", "Thurs", "Fri", "Sat" };

        // This method finds the day or returns -1
        private int GetDay(string testDay)
        {

            for (int j = 0; j < days.Length; j++)
            {
                if (days[j] == testDay)
                {
                    return j;
                }
            }

            throw new System.ArgumentOutOfRangeException(testDay, "testDay must be in the form \"Sun\", \"Mon\", etc");
        }

        // The get accessor returns an integer for a given string
        public int this[string day]
        {
            get
            {
                return (GetDay(day));
            }
        }
    }
}
