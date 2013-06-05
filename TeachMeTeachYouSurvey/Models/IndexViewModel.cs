using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeachMeTeachYouSurvey.Models
{
    public class IndexViewModel
    {
        public Dictionary<string, IOrderedEnumerable<Theme>> ThemesPerType { get; set; }
    }
}