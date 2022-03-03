﻿using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatform.Models.Articles
{
    public class ArticleViewModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        public string ImageURL { get; set; }
    }
}
