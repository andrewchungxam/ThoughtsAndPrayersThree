using System;
using System.Collections.Generic;
using System.Globalization;
using ThoughtsAndPrayersThree.Models;

namespace ThoughtsAndPrayersThree.LocalData
{
	public static class FixedPrayerRequests
	{
		public static List<PrayerRequest> ListOfPrayerRequests { get; set; } = new List<PrayerRequest>
		{
			new PrayerRequest (){
				Id = 1,
				CreatedDateTimeString = DateTime.Now.ToString("MMM d h:mm tt", new CultureInfo("en-US")),
				CreatedDateTime = DateTimeOffset.UtcNow,
				FirstName = "Andrew",
				LastName = "Kim",
				FullName = "Andrew Kim",
                FullNameAndDate = "Andrew Kim\r\nMarch 1, 2018",
//                FBProfileUrl = "http://loremflickr.com/600/600/nature?filename=simple.jpg",
				FBProfileUrl = "http://graph.facebook.com/450/picture?type=normal",
				PrayerRequestText = "Guys - we want to make it to the World Series this year.  It's going to be an odd numbered year - so we have a chance.  Please keep us in your thoughts!",
                NumberOfThoughts = 7,
                NumberOfPrayers = 5,
                StringTheNumberOfPrayers = "first test string"
			},
			new PrayerRequest (){
				Id = 2,
				CreatedDateTimeString = DateTime.Now.ToString("MMM d h:mm tt", new CultureInfo("en-US")),
				CreatedDateTime = DateTimeOffset.UtcNow,
				FirstName = "Andrew",
				LastName = "Kim",
				FullName = "Andrew Kim",
                FullNameAndDate = "Andrew Kim\r\nMarch 1, 2018",
//				FBProfileUrl = "http://loremflickr.com/600/600/nature?filename=simple.jpg",
				FBProfileUrl = "http://graph.facebook.com/450/picture?type=normal",
				PrayerRequestText = "Guys - we want to make it to the World Series with year.  It's going to be an odd numbered year - so we have a chance.  Please keep us in your thoughts!",
				NumberOfThoughts = 7,
				NumberOfPrayers = 5,
                StringTheNumberOfPrayers = "first test string"
			},
			new PrayerRequest (){
				Id = 3,
				CreatedDateTimeString = DateTime.Now.ToString("MMM d h:mm tt", new CultureInfo("en-US")),
				CreatedDateTime = DateTimeOffset.UtcNow,
				FirstName = "Andrew",
				LastName = "Kim",
				FullName = "Andrew Kim",
                FullNameAndDate = "Andrew Kim\r\nMarch 1, 2018",
				//FBProfileUrl = "http://loremflickr.com/600/600/nature?filename=simple.jpg",
				FBProfileUrl = "http://graph.facebook.com/450/picture?type=normal",
				PrayerRequestText = "Guys - we want to make it to the World Series with year.  It's going to be an odd numbered year - so we have a chance.  Please keep us in your thoughts!",
				NumberOfThoughts = 7,
				NumberOfPrayers = 5,
                StringTheNumberOfPrayers = "first test string"
			},
			new PrayerRequest (){
				Id = 4,
				CreatedDateTimeString = DateTime.Now.ToString("MMM d h:mm tt", new CultureInfo("en-US")),
				CreatedDateTime = DateTimeOffset.UtcNow,
				FirstName = "Andrew",
				LastName = "Kim",
				FullName = "Andrew Kim",
                FullNameAndDate = "Andrew Kim\r\nMarch 1, 2018",
				FBProfileUrl = "http://graph.facebook.com/450/picture?type=normal",
				PrayerRequestText = "Guys - we want to make it to the World Series with year.  It's going to be an odd numbered year - so we have a chance.  Please keep us in your thoughts!",
				NumberOfThoughts = 7,
				NumberOfPrayers = 5,
                StringTheNumberOfPrayers = "first test string"
			}
		};
	}
}
