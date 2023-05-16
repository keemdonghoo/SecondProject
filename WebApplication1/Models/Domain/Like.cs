﻿namespace WebApplication1.Models.Domain
{
    public class Like
    {
        public long UserUID {get; set;}
        public long PostUID { get; set;}

        public User User{ get; set; }   

        public Post Post { get; set; }   
    }
}
