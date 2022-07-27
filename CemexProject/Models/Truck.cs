using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CemexProject.Models
{
    public class Truck
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        public int number { get; set; }

        [StringLength(30)]
        public string? driver_name { get; set; }

        [StringLength(15)]
        public string? Type { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public  DateTime? LastUpdateDate { get; set; }

    }
}
