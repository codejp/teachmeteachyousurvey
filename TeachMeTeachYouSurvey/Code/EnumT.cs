using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeachMeTeachYouSurvey
{
    public class Enum<T>
    {
        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}