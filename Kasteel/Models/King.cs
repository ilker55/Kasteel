namespace Kasteel.Models
{
    public class King(int id, int castleId, string name, string birthDate, string deathDate, string location, int year, string famousFor, string remarks)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string BirthDate { get; set; } = birthDate;
        public string DeathDate { get; set; } = deathDate;
        public string Location { get; set; } = location;
        public int Year { get; set; } = year;
        public string FamousFor { get; set; } = famousFor;
        public string Remarks { get; set; } = remarks;

        public int CastleId { get; set; } = castleId;
        public Castle Castle { get; set; } = null!;
    }
}
