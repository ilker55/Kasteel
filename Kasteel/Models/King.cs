namespace Kasteel.Models
{
    public class King
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string DeathDate { get; set; }
        public string Location { get; set; }
        public int Year { get; set; }
        public string FamousFor { get; set; }
        public string Remarks { get; set; }

        public int CastleId { get; set; }
        public Castle Castle { get; set; } = null!;

        public King Clone()
         => new()
         {
             Id = Id,
             Name = Name,
             BirthDate = BirthDate,
             DeathDate = DeathDate,
             Location = Location,
             Year = Year,
             FamousFor = FamousFor,
             Remarks = Remarks,
             CastleId = CastleId
         };
    }
}
