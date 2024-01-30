using System.ComponentModel.DataAnnotations;

namespace VehicleTracking.Core.Web.API.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string? PlateNumber { get; set; } //plaka
        [StringLength(100)] 
        public string? RawMaterial { get; set; } //Hammadde
        public double Amount { get; set; } //Miktar
        public int Approval { get; set; } //durum
       // public int SecondApproval { get; set; } //ikinci onay
        //public int WeighingProcess { get; set; } //Tartım işlemi
        public bool IsDeleted { get; set; }
        public enum EnStatus
        {
           İlkOnayBekliyor = 0,
                 Onaylandi = 1,
            TartimBekliyor = 2,
        İkinciOnayBekliyor = 3
        }
    }
}
