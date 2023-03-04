using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models
{
    public class Driver
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DriverNumber { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public int Status { get; set; }
        public int WorldChampionship { get; set; }
    }
}
