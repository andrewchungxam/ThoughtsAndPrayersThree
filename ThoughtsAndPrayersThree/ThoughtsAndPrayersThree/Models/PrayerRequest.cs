using System;
using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json;
using SQLite;

namespace ThoughtsAndPrayersThree.Models
{
    //LOCAL-SQLLITE
    public class PrayerRequest 
    {
        [PrimaryKey]
		public int Id { get; set; }
		public string CreatedDateTimeString { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public string StringOnlyDateTime { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { get; set; }
        public string FullNameAndDate { get; set; }

        public string NewCombinedNameAndDate 
        { 
            get
            {
                return String.Format("{0} {1}\r\n{2}", FirstName, LastName,CreatedDateTime.ToString("MMM d h:mm tt", new CultureInfo("en-US")));
            }
        }

		public string FBProfileUrl { get; set; }
		public string PrayerRequestText { get; set; }
        public int NumberOfThoughts { get; set; }
        public int NumberOfPrayers { get; set; }

        public string CombinedNumberOfThoughtsAndPrayers 
        { get 
            {
                return String.Format("{0} thoughts and {1} prayers", NumberOfThoughts.ToString(), NumberOfPrayers.ToString()); //Concat(NumberOfThoughts.ToString(), NumberOfPrayers.ToString()); 
            }  
        }

        public string StringTheNumberOfPrayers { get; set; }
    }

	//LOCAL-COSMOSDB
	public class CosmosDBPrayerRequest
	{
		public string Id { get; set; }
		public string CreatedDateTimeString { get; set; }

		public DateTimeOffset CreatedDateTime { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { get; set; }
		public string FBProfileUrl { get; set; }
		public string PrayerRequestText { get; set; }
        public int NumberOfThoughts { get; set; }
        public int NumberOfPrayers { get; set; }
	}
}