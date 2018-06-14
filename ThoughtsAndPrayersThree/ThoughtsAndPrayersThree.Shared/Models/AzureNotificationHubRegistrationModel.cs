using System;
namespace ThoughtsAndPrayersThree.Shared.Models
{
    public class Aps
    {
        public string alert { get; set; }
    }

    public class RootObject
    {
        public Aps aps { get; set; }
    }

    public class DeviceRegistration
    {
        public string Platform { get; set; }
        public string Handle { get; set; } //NSData
        public string[] Tags { get; set; } //NSSet
    }

    public class DeviceRegistrationWithTemplate
    {
        public string Platform { get; set; }
        public string Handle { get; set; } //NSData
        public string[] Tags { get; set; } //NSSet
        public string name { get; set; }
        public string jsonBodyTemplates { get; set; }
        public string expiryTemplate { get; set; }
    }
}