namespace RPi_backend.Model
{
    public class Temperature
    {
        public int Id { get; set; }
        public float? Value { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public string? Device { get; set; }
    }
}
