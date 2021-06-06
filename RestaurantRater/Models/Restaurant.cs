﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRater.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
        public double Rating 
        {
            get
            {
                double totalAverageRating = 0;

                foreach (Rating rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }

                return Ratings.Count > 0 ?
                    Math.Round(totalAverageRating / Ratings.Count, 2)
                    : 0;
            }
        }

        public bool IsRecommended
        {
            get
            {
                return Rating > 7;
            }
        }

        // Average food rating
        // Average cleanliness rating
        // Average environment rating
    }
}