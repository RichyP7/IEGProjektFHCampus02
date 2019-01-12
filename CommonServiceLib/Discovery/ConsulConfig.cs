namespace CommonServiceLib.Discovery
{
    public class ConsulConfig
    {
        public static string Address { get; set; } = "http://localhost:8500";
        public string ServiceName { get; set; }
        public string ServiceID { get; set; }
    }
}