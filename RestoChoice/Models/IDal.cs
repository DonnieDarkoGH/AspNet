using System;
using System.Collections.Generic;

namespace Survey.Models
{
	public interface IDal : IDisposable
	{
		List<Restaurant> GetAllRestaurants();
		User GetUser(string name);
		User GetUser(int id);

		void CreateRestaurant(string name, string phoneNumber);
		void ModifyRestaurant(int? id, string name, string phoneNumber);
		bool IsExistingRestaurant(string name);

		int AddUser(string name, string password);
		User Authenticate(string name, string password);
		bool HasAlreadyVoted(int surveyId, string userId);

		int CreateSurvey();
		void AddVote(int surveyId, int restaurantId, int userId);
		List<Result> GetResults(int surveyId);
		 
	}
}
