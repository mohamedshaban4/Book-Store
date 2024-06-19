﻿namespace BookStore.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
