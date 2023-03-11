namespace PollsSystem.Shared.Default;

public static class DefaultConsts
{
    public class Cors
    {
        public static string SectionName => "cors";
        public static string PolicyName => "cors";
    }

    public class Security
    {
        public static string SectionName = "jwt";

        public const string DefaultRole = "user";
        public const string AdminRole = "admin";
    }

    public class Dal
    {
        public static string ConfigurationSectionPostgresName => "postgresDatabase";
        public static string ConfigurationSectionPostgresConnection => "postgresConnection";
    }

    public class Logging
    {
        public static string ConsoleOutputTemplate => "{Timestamp:HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}";
        public static string FileOutputTemplate => "{Timestamp:HH:mm:ss} [{Level:u3}] ({SourceContext}.{Method}) {Message}{NewLine}{Exception}";
        public static string AppSectionName => "app";
        public static string SerilogSectionName => "serilog";
    }
}