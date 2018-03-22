using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace ThoughtsAndPrayersThree.Models
{
    //LOCAL-SQLLITE
    public class PrayerRequest 
    {
		public int Id { get; set; }
		public string CreatedDateTimeString { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { get; set; }
        public string FullNameAndDate { get; set; }
		public string FBProfileUrl { get; set; }
		public string PrayerRequestText { get; set; }
        public int NumberOfThoughts { get; set; }
        public int NumberOfPrayers { get; set; }
        public string CombinedNumberOfThoughtsAndPrayers 
        { get 
            {
                //if (NumberOfPrayers != null & NumberOfThoughts != null)
                //{
                //    return " ";
                //}
                return String.Format("{0} Thoughts and {1} Prayers", NumberOfThoughts.ToString(), NumberOfPrayers.ToString()); //Concat(NumberOfThoughts.ToString(), NumberOfPrayers.ToString()); 
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