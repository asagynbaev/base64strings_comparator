namespace RadioFreeEuropeClient
{
    internal class Diff
    {
        public Diff(int id) => Id = id;

        public Diff(int id, string base64Value) => (Id, Base64Value) = (id, base64Value);

        public Diff(int id, string base64value, DiffType type) => (Id, Base64Value, Type) = (id, base64value, type);

        public int Id { get; set; }

        public string? Base64Value { get; set; }

        public DiffType? Type { get; set; }
    }
}
