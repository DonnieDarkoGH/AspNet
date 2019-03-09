using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Survey.Models
{
	public class Dal : IDal
	{
		private SurveyDbContext _dbContext;

		public Dal()
		{
			_dbContext = new SurveyDbContext();
		}

		public void CreateRestaurant(string name, string phoneNumber)
		{
			_dbContext.Restaurants.Add(new Restaurant { Name = name, PhoneNumber = phoneNumber });
			_dbContext.SaveChanges();
		}

		public void ModifyRestaurant(int id, string name, string phoneNumber)
		{
			Restaurant restaurant = _dbContext.Restaurants.First(r => r.Id == id);
			if (restaurant != null)
			{
				restaurant.Name = name;
				restaurant.PhoneNumber = phoneNumber;

				_dbContext.SaveChanges();
			}
		}

		public List<Restaurant> GetAllRestaurants()
		{
			return _dbContext.Restaurants.ToList();
		}

		public bool IsExistingRestaurant(string name)
		{
			return GetAllRestaurants().Exists(r => r.Name == name);
		}

		public User GetUser(string idStr)
		{
			int id = -1;
			if (int.TryParse(idStr, out id))
			{
				return GetUser(id);
			}

			return null;
		}

		public User GetUser(int id)
		{
			return _dbContext.Users.ToList().FirstOrDefault(u => u.Id == id);
		}

		public int AddUser(string name, string password)
		{
			if (_dbContext.Users.ToList().Exists(u => u.Name == name))
			{
				return - 1;
			}

			string encodedPassword = EncodeMD5(password);
			User newUser = _dbContext.Users.Add(new User { Name = name, Password = encodedPassword });
			_dbContext.SaveChanges();

			return newUser.Id;
		}

		public User Authenticate(string name, string password)
		{
			return _dbContext.Users.ToList().FirstOrDefault(
				u => u.Name == name && u.Password == EncodeMD5(password));
		}

		public bool HasAlreadyVoted(int surveyId, string userIdtoString)
		{
			Survey survey = _dbContext.Surveys.First(s => s.Id == surveyId);
			if (survey == null || survey.Votes == null)
			{
				return false;
			}

			int userId = -1;
			if (int.TryParse(userIdtoString, out userId))
			{
				return survey.Votes.Any(v => v.User != null && v.User.Id == userId);
			}

			return false;
		}

		public int CreateSurvey()
		{
			Survey newSurvey = _dbContext.Surveys.Add(new Survey {
				Date = DateTime.Now
			});

			_dbContext.SaveChanges();

			return newSurvey.Id;
		}

		public void AddVote(int surveyId, int restaurantId, int userId)
		{
			Restaurant restaurant = GetAllRestaurants().FirstOrDefault(r => r.Id == restaurantId);
			User user = _dbContext.Users.ToList().FirstOrDefault(u => u.Id == userId);

			Survey survey = _dbContext.Surveys.ToList().FirstOrDefault(s => s.Id == surveyId);
			if (survey.Votes == null)
			{
				survey.Votes = new List<Vote>();
			}
			survey.Votes.Add(new Vote { User = user, Restaurant = restaurant });

			_dbContext.SaveChanges();
		}

		public List<Result> GetResults(int surveyId)
		{
			List<Result> results = new List<Result>();
			Survey survey = _dbContext.Surveys.ToList().FirstOrDefault(s => s.Id == surveyId);

			List<Restaurant> restaurants = GetAllRestaurants();
			foreach (IGrouping<int, Vote> group in survey.Votes.GroupBy(v => v.Restaurant.Id))
			{
				int restaurantId = group.Key;
				Restaurant restaurant = restaurants.First(r => r.Id == restaurantId);
				int voteCount = group.Count();

				results.Add(new Result {
					Name = restaurant.Name,
					PhoneNumber = restaurant.PhoneNumber,
					VotesCount = voteCount
				});
			}

			return results;
		}

		public void Dispose()
		{
			_dbContext.Dispose();
		}

		private string EncodeMD5(string motDePasse)
		{
			string motDePasseSel = "ChoixResto" + motDePasse + "ASP.NET MVC";
			return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
		}

	}
}