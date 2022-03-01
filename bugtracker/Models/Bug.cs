using System.ComponentModel.DataAnnotations;
namespace bugtracker.Models
{
    public class Bug
    {
        public int Id { get; set; } 
        public string Description { get; set; }
        public int Severity { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string Details { get; set; }
        public static DateTime? TodayDate { get; set; } 
        //add user information
        public string PostedByUser { get; set; }
        public string? AssignedToUser { get; set; }

    }
}
