using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Web;

namespace ReservationPayment.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TrainDetails> TrainDetails { get; set; }

    }
}