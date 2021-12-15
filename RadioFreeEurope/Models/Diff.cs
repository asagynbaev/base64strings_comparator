using System.ComponentModel.DataAnnotations;

namespace RadioFreeEurope.Models
{
    public class Diff
    {
        public Diff() { }

        public Diff(int id, string base64value, DiffType type)
        {
            (Id, Base64Value, Type) = (id, base64value, type);
        }

        [Key]
        public int InternalId { get; set; }

        public int Id { get; set; }

        public string Base64Value { get; set; }

        public DiffType? Type { get; set; }
    }
}
