namespace Kasteel.Models
{
    public class Castle(string name)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;

        public virtual ICollection<King> Kings { get; set; } = new List<King>();
    }
}
