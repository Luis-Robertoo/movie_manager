﻿namespace MovieManager.BLL.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}