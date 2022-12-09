namespace RPi_backend.Model
{
    public class Humidity
    {
        public int Id { get; set; }
        public float? Value { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public string? Device { get; set; }
    }
}
