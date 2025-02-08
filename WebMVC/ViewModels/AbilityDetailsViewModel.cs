﻿using WebMVC.Models;

namespace WebMVC.ViewModels
{
    public class AbilityDetailsViewModel
    {
        public string Name { get; set; }

        public string EffectEntries { get; set; }

        public List<string> Pokemon { get; set; }

        public string Description { get; set; }  // Description of the ability
    }
}
