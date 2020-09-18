using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneSignal.Models
{
    public class AppViewModel
    {
        public List<App> Apps { get; set; }
    }

    public class App
    {
        [Display(Name = "App ID")]
        public string id { get; set; }

        [Required]
        [Display(Name = "App Name")]
        public string name { get; set; }
    }
}