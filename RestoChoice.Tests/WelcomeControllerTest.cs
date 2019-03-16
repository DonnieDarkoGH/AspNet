using System;
using Survey.Controllers;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Survey.Tests
{
	[TestClass]
	public class WelcomeControllerTest
	{
		[TestMethod]
		public void WelcomeController_Index_ReturnDefaultView()
		{
			WelcomeController wc = new WelcomeController();
			ViewResult vr = (ViewResult)wc.Index();

			Assert.AreEqual(string.Empty, vr.ViewName);
		}

		[TestMethod]
		public void WelcomeController_DisplayMessageWithDate_ReturnIndexView()
		{
			WelcomeController wc = new WelcomeController();
			ViewResult vr = (ViewResult)wc.DisplayMessageWithDate("Yoyo");

			Assert.AreEqual("Index", vr.ViewName);
			Assert.AreEqual(DateTime.Now.ToLongDateString(), ((DateTime)vr.ViewData["Date"]).ToLongDateString());
			Assert.AreEqual("Hello Yoyo !", vr.ViewBag.Message);
		}
	}
}
