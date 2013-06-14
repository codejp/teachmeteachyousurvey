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

        [Required(ErrorMessage="入力してください。"), AllowHtml]
        [StringLength(maximumLength: 125, ErrorMessage="{1}文字までで入力してください。")]
        public string Description { get; set; }
    }
}