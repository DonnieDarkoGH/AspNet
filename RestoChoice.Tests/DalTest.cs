using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Survey.Models;

namespace Survey.Tests
{
	[TestClass]
	public class DalTests
	{
		private IDal dal;

		[TestInitialize]
		public void Init_AvantChaqueTest()
		{
			IDatabaseInitializer<SurveyDbContext> init = new DropCreateDatabaseAlways<SurveyDbContext>();
			Database.SetInitializer(init);
			init.InitializeDatabase(new SurveyDbContext());

			dal = new Dal();
		}

		[TestCleanup]
		public void ApresChaqueTest()
		{
			dal.Dispose();
		}

		[TestMethod]
		public void CreerRestaurant_AvecUnNouveauRestaurant_ObtientTousLesRestaurantsRenvoitBienLeRestaurant()
		{
			dal.CreateRestaurant("La bonne fourchette", "0102030405");
			List<Restaurant> restos = dal.GetAllRestaurants();

			Assert.IsNotNull(restos);
			Assert.AreEqual(1, restos.Count);
			Assert.AreEqual("La bonne fourchette", restos[0].Name);
			Assert.AreEqual("0102030405", restos[0].PhoneNumber);
		}

		[TestMethod]
		public void ModifierRestaurant_CreationDUnNouveauRestaurantEtChangementNomEtTelephone_LaModificationEstCorrecteApresRechargement()
		{
			dal.CreateRestaurant("La bonne fourchette", "0102030405");
			dal.ModifyRestaurant(1, "La bonne cuillère", null);

			List<Restaurant> restos = dal.GetAllRestaurants();
			Assert.IsNotNull(restos);
			Assert.AreEqual(1, restos.Count);
			Assert.AreEqual("La bonne cuillère", restos[0].Name);
			Assert.IsNull(restos[0].PhoneNumber);
		}

		[TestMethod]
		public void RestaurantExiste_AvecCreationDunRestauraunt_RenvoiQuilExiste()
		{
			dal.CreateRestaurant("La bonne fourchette", "0102030405");

			bool existe = dal.IsExistingRestaurant("La bonne fourchette");

			Assert.IsTrue(existe);
		}

		[TestMethod]
		public void RestaurantExiste_AvecRestaurauntInexistant_RenvoiQuilExiste()
		{
			bool existe = dal.IsExistingRestaurant("La bonne fourchette");

			Assert.IsFalse(existe);
		}

		[TestMethod]
		public void ObtenirUtilisateur_UtilisateurInexistant_RetourneNull()
		{
			User utilisateur = dal.GetUser(1);
			Assert.IsNull(utilisateur);
		}

		[TestMethod]
		public void ObtenirUtilisateur_IdNonNumerique_RetourneNull()
		{
			User utilisateur = dal.GetUser("abc");
			Assert.IsNull(utilisateur);
		}

		[TestMethod]
		public void AjouterUtilisateur_NouvelUtilisateurEtRecuperation_LutilisateurEstBienRecupere()
		{
			dal.AddUser("Nouvel utilisateur", "12345");

			User utilisateur = dal.GetUser(1);

			Assert.IsNotNull(utilisateur);
			Assert.AreEqual("Nouvel utilisateur", utilisateur.Name);

			utilisateur = dal.GetUser("1");

			Assert.IsNotNull(utilisateur);
			Assert.AreEqual("Nouvel utilisateur", utilisateur.Name);
		}

		[TestMethod]
		public void Authentifier_LoginMdpOk_AuthentificationOK()
		{
			dal.AddUser("Nouvel utilisateur", "12345");

			User utilisateur = dal.Authenticate("Nouvel utilisateur", "12345");

			Assert.IsNotNull(utilisateur);
			Assert.AreEqual("Nouvel utilisateur", utilisateur.Name);
		}

		[TestMethod]
		public void Authentifier_LoginOkMdpKo_AuthentificationKO()
		{
			dal.AddUser("Nouvel utilisateur", "12345");
			User utilisateur = dal.Authenticate("Nouvel utilisateur", "0");

			Assert.IsNull(utilisateur);
		}

		[TestMethod]
		public void Authentifier_LoginKoMdpOk_AuthentificationKO()
		{
			dal.AddUser("Nouvel utilisateur", "12345");
			User utilisateur = dal.Authenticate("Nouvel", "12345");

			Assert.IsNull(utilisateur);
		}

		[TestMethod]
		public void Authentifier_LoginMdpKo_AuthentificationKO()
		{
			User utilisateur = dal.Authenticate("Nouvel utilisateur", "12345");

			Assert.IsNull(utilisateur);
		}

		[TestMethod]
		public void ADejaVote_AvecIdNonNumerique_RetourneFalse()
		{
			bool pasVote = dal.HasAlreadyVoted(1, "abc");

			Assert.IsFalse(pasVote);
		}

		[TestMethod]
		public void ADejaVote_UtilisateurNAPasVote_RetourneFalse()
		{
			int idSondage = dal.CreateSurvey();
			int idUtilisateur = dal.AddUser("Nouvel utilisateur", "12345");

			bool pasVote = dal.HasAlreadyVoted(idSondage, idUtilisateur.ToString());

			Assert.IsFalse(pasVote);
		}

		[TestMethod]
		public void ADejaVote_UtilisateurAVote_RetourneTrue()
		{
			int idSondage = dal.CreateSurvey();
			int idUtilisateur = dal.AddUser("Nouvel utilisateur", "12345");
			dal.CreateRestaurant("La bonne fourchette", "0102030405");
			dal.AddVote(idSondage, 1, idUtilisateur);

			bool aVote = dal.HasAlreadyVoted(idSondage, idUtilisateur.ToString());

			Assert.IsTrue(aVote);
		}

		[TestMethod]
		public void ObtenirLesResultats_AvecQuelquesChoix_RetourneBienLesResultats()
		{
			int idSondage = dal.CreateSurvey();
			int idUtilisateur1 = dal.AddUser("Utilisateur1", "12345");
			int idUtilisateur2 = dal.AddUser("Utilisateur2", "12345");
			int idUtilisateur3 = dal.AddUser("Utilisateur3", "12345");

			dal.CreateRestaurant("Resto pinière", "0102030405");
			dal.CreateRestaurant("Resto pinambour", "0102030405");
			dal.CreateRestaurant("Resto mate", "0102030405");
			dal.CreateRestaurant("Resto ride", "0102030405");

			dal.AddVote(idSondage, 1, idUtilisateur1);
			dal.AddVote(idSondage, 3, idUtilisateur1);
			dal.AddVote(idSondage, 4, idUtilisateur1);
			dal.AddVote(idSondage, 1, idUtilisateur2);
			dal.AddVote(idSondage, 1, idUtilisateur3);
			dal.AddVote(idSondage, 3, idUtilisateur3);

			List<Result> results = dal.GetResults(idSondage);

			Assert.AreEqual(3, results[0].VotesCount);
			Assert.AreEqual("Resto pinière", results[0].Name);
			Assert.AreEqual("0102030405", results[0].PhoneNumber);
			Assert.AreEqual(2, results[1].VotesCount);
			Assert.AreEqual("Resto mate", results[1].Name);
			Assert.AreEqual("0102030405", results[1].PhoneNumber);
			Assert.AreEqual(1, results[2].VotesCount);
			Assert.AreEqual("Resto ride", results[2].Name);
			Assert.AreEqual("0102030405", results[2].PhoneNumber);
		}

		[TestMethod]
		public void ObtenirLesResultats_AvecDeuxSondages_RetourneBienLesBonsResultats()
		{
			int idSondage1 = dal.CreateSurvey();
			int idUtilisateur1 = dal.AddUser("Utilisateur1", "12345");
			int idUtilisateur2 = dal.AddUser("Utilisateur2", "12345");
			int idUtilisateur3 = dal.AddUser("Utilisateur3", "12345");
			dal.CreateRestaurant("Resto pinière", "0102030405");
			dal.CreateRestaurant("Resto pinambour", "0102030405");
			dal.CreateRestaurant("Resto mate", "0102030405");
			dal.CreateRestaurant("Resto ride", "0102030405");
			dal.AddVote(idSondage1, 1, idUtilisateur1);
			dal.AddVote(idSondage1, 3, idUtilisateur1);
			dal.AddVote(idSondage1, 4, idUtilisateur1);
			dal.AddVote(idSondage1, 1, idUtilisateur2);
			dal.AddVote(idSondage1, 1, idUtilisateur3);
			dal.AddVote(idSondage1, 3, idUtilisateur3);

			int idSondage2 = dal.CreateSurvey();
			dal.AddVote(idSondage2, 2, idUtilisateur1);
			dal.AddVote(idSondage2, 3, idUtilisateur1);
			dal.AddVote(idSondage2, 1, idUtilisateur2);
			dal.AddVote(idSondage2, 4, idUtilisateur3);
			dal.AddVote(idSondage2, 3, idUtilisateur3);

			List<Result> results1 = dal.GetResults(idSondage1);
			List<Result> results2 = dal.GetResults(idSondage2);

			Assert.AreEqual(3, results1[0].VotesCount);
			Assert.AreEqual("Resto pinière", results1[0].Name);
			Assert.AreEqual("0102030405", results1[0].PhoneNumber);
			Assert.AreEqual(2, results1[1].VotesCount);
			Assert.AreEqual("Resto mate", results1[1].Name);
			Assert.AreEqual("0102030405", results1[1].PhoneNumber);
			Assert.AreEqual(1, results1[2].VotesCount);
			Assert.AreEqual("Resto ride", results1[2].Name);
			Assert.AreEqual("0102030405", results1[2].PhoneNumber);

			Assert.AreEqual(1, results2[0].VotesCount);
			Assert.AreEqual("Resto pinambour", results2[0].Name);
			Assert.AreEqual("0102030405", results2[0].PhoneNumber);
			Assert.AreEqual(2, results2[1].VotesCount);
			Assert.AreEqual("Resto mate", results2[1].Name);
			Assert.AreEqual("0102030405", results2[1].PhoneNumber);
			Assert.AreEqual(1, results2[2].VotesCount);
			Assert.AreEqual("Resto pinière", results2[2].Name);
			Assert.AreEqual("0102030405", results2[2].PhoneNumber);
		}
	}
}
