using System.Web.UI;
using System.Web.UI.HtmlControls;
using NMock;
using NUnit.Framework;
using ThoughtWorks.CruiseControl.Core;
using ThoughtWorks.CruiseControl.WebDashboard.MVC;
using ThoughtWorks.CruiseControl.WebDashboard.MVC.Cruise;

namespace ThoughtWorks.CruiseControl.UnitTests.WebDashboard.MVC.Cruise
{
	[TestFixture]
	public class ExceptionCatchingActionProxyTest : Assertion
	{
		private DynamicMock actionMock;
		private ExceptionCatchingActionProxy exceptionCatchingAction;
		private Control view;
		private IRequest request;

		[SetUp]
		public void Setup()
		{
			actionMock = new DynamicMock(typeof(IAction));
			exceptionCatchingAction = new ExceptionCatchingActionProxy((IAction) actionMock.MockInstance);
			view = new Control();
			request = new NameValueCollectionRequest(null);
		}

		private void VerifyAll()
		{
			actionMock.Verify();
		}

		[Test]
		public void ShouldReturnProxiedViewIfProxiedActionDoesntThrowException()
		{
			// Setup
			actionMock.ExpectAndReturn("Execute", view, request);

			// Execute
			Control returnedControl = exceptionCatchingAction.Execute(request);

			// Verify
			AssertEquals(view, returnedControl);
			VerifyAll();
		}

		[Test]
		public void ShouldGiveViewOfExceptionIfProxiedActionThowsException()
		{
			// Setup
			CruiseControlException e = new CruiseControlException("A nasty exception");
			actionMock.ExpectAndThrow("Execute", e, request);

			// Execute
			Control returnedControl = exceptionCatchingAction.Execute(request);

			// Verify
			Assert(((HtmlGenericControl) returnedControl).InnerHtml.IndexOf("A nasty exception") > -1);
			VerifyAll();
		}
	}
}
