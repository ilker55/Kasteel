namespace Kasteel.Models
{
    public class Castle
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<King> Kings { get; set; } = new List<King>();
    }
}
