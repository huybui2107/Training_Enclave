namespace BE.Databases.Entities
{
    public class File
    {
        public int Id { get; set; }
        public string Version { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
