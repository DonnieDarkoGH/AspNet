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

	}
}
