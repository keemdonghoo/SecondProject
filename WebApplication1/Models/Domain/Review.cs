﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.Domain
{
    [Table("Review")]
    public class Review
    {
        public long ReviewUID { get; set; } 

        public float Rate { get; set; }

        public string Content { get; set; } 

        public DateTime Date { get; set; }

        public User User { get; set; }
        public long UserUID { get; set; }

        public Movie Movie { get; set; }    
        public long MovieUID { get; set; }  


    }
}
