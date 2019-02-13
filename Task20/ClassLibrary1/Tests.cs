using NUnit.Framework;
using OpenQA.Selenium;
using Excel = Microsoft.Office.Interop.Excel;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        public const string url = "https://www.tut.by/";
        public const string username = "seleniumtests@tut.by";
        public const string password = "123456789zxcvbn";

        public const string frameUrl = "https://the-internet.herokuapp.com/iframe";
        public const string alertUrl = "https://the-internet.herokuapp.com/javascript_alerts";

        [Test]
        public void Login()
        {
            var driver = Drivers.Methods.NavigateTo(url);
            var actualUrl = Drivers.Methods.GetUrl(driver);
            Assert.IsTrue(actualUrl.StartsWith(url), "invalid url)");

            Drivers.Methods.PopulateLoginForm(driver, username, password);

            var submitButtonState = Drivers.Methods.IsLoginButtonEnabled(driver);
            Assert.IsTrue(submitButtonState);

            Drivers.Methods.LoginUser(driver);

            var loginState = Drivers.Methods.IsUserLogin(driver);
            
            Assert.IsTrue(loginState);
        }

        [Test]
        public void TimeOut()
        {
            var driver = Drivers.Methods.NavigateTo(url);
            Drivers.Methods.PopulateLoginForm(driver, username, password);
            Drivers.Methods.LoginUser(driver);
            Drivers.Methods.WaitUntilElementDisplayed(driver, By.XPath(Drivers.Methods.authorizedUserLocator));
        }

        [Test]
        public void FromExcel()
        {
            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(@"C:\Users\katyaastakhova\source\repos\TC_WebDriver\Task20\DDT4.xlsx");
            Excel.Worksheet worksheet = (Excel.Worksheet)app.Worksheets["DataSet"];
            Excel.Range range = worksheet.UsedRange;

            for (int i = 1; i <= 2; i++)
            {
                var driver = Drivers.Methods.NavigateTo(url);
                string username;
                string password;
                username = range.Cells[1][i].value2;
                password = range.Cells[2][i].value2;

                Drivers.Methods.LoginSeveralUsers(driver, username, password);
                var loginState = Drivers.Methods.IsUserLogin(driver);
                Assert.IsTrue(loginState);
                driver.Close();
            }
        }

        [Test]
            public void IframeTests()
        {
            var driver = Drivers.Methods.NavigateTo(frameUrl);
            var actual = Drivers.Methods.PrintText(driver);
            var expected = "Hello \uFEFFworld!";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void JsAlertTest()
        {
            var driver = Drivers.Methods.NavigateTo(alertUrl);
            var actualAcceptResultMessage = Drivers.Methods.ClickOnJsAlert(driver);
            var expectedAcceptResultMessage = "You successfuly clicked an alert";
            Assert.AreEqual(expectedAcceptResultMessage, actualAcceptResultMessage);
        }

        [Test]
        public void JsConfirmAcceptTest()
        {
            var driver = Drivers.Methods.NavigateTo(alertUrl);
            var actualConfirmResultMessage = Drivers.Methods.ClickOKJsConfirm(driver);
            var expectedConfirmResultMessage = "You clicked: Ok";
            Assert.AreEqual(expectedConfirmResultMessage, actualConfirmResultMessage);
            var actualCancelResultMessage = Drivers.Methods.ClickCancelOnJsConfirm(driver);
            var expectedCancelResultMessage = "You clicked: Cancel";
            Assert.AreEqual(expectedCancelResultMessage, actualCancelResultMessage);
        }
        [Test]
        public void JsPromptTest()
        {
            var driver = Drivers.Methods.NavigateTo(alertUrl);
            var actualConfirmResultMessage = Drivers.Methods.ClickJsPromptConfirm(driver);
            var expectedConfirmResultMessage = "You entered:";
            Assert.AreEqual(expectedConfirmResultMessage, actualConfirmResultMessage);
            var actualCancelResultMessage = Drivers.Methods.ClickJsPromptCancel(driver);
            var expectedCancelResultMessage = "You entered: null";
            Assert.AreEqual(expectedCancelResultMessage, actualCancelResultMessage);
            var actualResultText = Drivers.Methods.ClickJsPromptText(driver);
            var expectedResultText = driver.FindElement(By.XPath("//p[@id='result']")).Text;
            Assert.AreEqual(expectedResultText, actualResultText);
        }
    }
}
