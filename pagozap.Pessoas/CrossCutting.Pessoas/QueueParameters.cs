using System;

namespace CrossCutting.Pessoas
{
    public class QueueParameters
    {
        public static string Host { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static string QueuelName { get; set; }
        public static string QueuelNameRequest { get; set; }
        public static string KeyChannel { get; set; }
        public static string KeyChannelRequest { get; set; }
        public static Object Message { get; set; }
        public static string CorrelationID { get; set; }
    }
}
