using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachMeTeachYouSurvey.Models
{
    public class ThemeEditorViewModel
    {
        public ThemeType ThemeType { get; set; }

        [Required, AllowHtml]
        [StringLength(maximumLength: 400)]
        public string Description { get; set; }
    }
}