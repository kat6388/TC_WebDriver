using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Drivers
{
    public class Methods
    {
        public const string loginLinkLocator = "//a[.='Войти']";
        public const string usernameTextfieldLocator = "//input[@name = 'login']";
        public const string passwordTextfieldLocator = "//input[@name = 'password']";
        public const string loginButtonLocator = "//input[@value='Войти']";
        public const string authorizedUserLocator = "//span[@class='uname']";
        public const string frame = "Hello world!";
        public const string property = "//a[.='Войти']";
        public static IWebDriver NavigateTo(string url)
        {
            var chromeDriver = new ChromeDriver(@"D:\Automation");
            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            chromeDriver.Navigate().GoToUrl(url);
            return chromeDriver;
        }

        public static string GetUrl(IWebDriver chromeDriver)
        {
            return chromeDriver.Url;
        }

        public static void PopulateLoginForm(IWebDriver chromeDriver, string username, string password)
        {
            Actions builder = new Actions(chromeDriver);

            chromeDriver.FindElement(By.XPath(loginLinkLocator)).Click();
            chromeDriver.FindElement(By.XPath(usernameTextfieldLocator)).SendKeys(username);
            builder.SendKeys(Keys.Tab).Perform();
            Thread.Sleep(2000);
            chromeDriver.FindElement(By.XPath(passwordTextfieldLocator)).SendKeys(password);
        }

        public static bool IsLoginButtonEnabled(IWebDriver chromeDriver)
        {
            return chromeDriver.FindElement(By.XPath(loginButtonLocator)).Enabled;
        }

        public static void LoginUser(IWebDriver chromeDriver)
        {
            chromeDriver.FindElement(By.XPath(loginButtonLocator)).Click();
        }

        public static bool IsUserLogin(IWebDriver chromeDriver)
        {
            return chromeDriver.FindElement(By.XPath(authorizedUserLocator)).Displayed;
        }

        public static void WaitUntilElementDisplayed(IWebDriver chromeDriver, By by)
        {
            var wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(1));
            var element = wait.Until(condition =>
            {
                try
                {
                    var elementToBeDisplayed = chromeDriver.FindElement(by);
                    return elementToBeDisplayed.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }

        public static void LoginSeveralUsers(IWebDriver chromeDriver, string username, string password)
        {
            chromeDriver.FindElement(By.XPath(loginLinkLocator)).Click();
            chromeDriver.FindElement(By.XPath(usernameTextfieldLocator)).SendKeys(username.ToString());
            chromeDriver.FindElement(By.XPath(passwordTextfieldLocator)).SendKeys(password + Keys.Enter);
        }

        public static string PrintText (IWebDriver chromeDriver)
        {
            var textField = chromeDriver.FindElement(By.XPath("//iframe[@id = 'mce_0_ifr']"));
            textField.Click();
            textField.SendKeys(Keys.Control + "a" + Keys.Backspace);
            textField.SendKeys("Hello ");
            chromeDriver.FindElement(By.XPath("//i[@class = 'mce-ico mce-i-bold']")).Click();
            textField.SendKeys("world!");
            chromeDriver.SwitchTo().Frame(textField);
            var entertedText = chromeDriver.FindElement(By.XPath("html/body/p")).Text;
            return entertedText;
        }
       
        public static string ClickOnJsAlert(IWebDriver chromeDriver)
        {
            chromeDriver.FindElement(By.XPath("//button[@onclick = 'jsAlert()']")).Click();
            chromeDriver.SwitchTo().Alert().Accept();
            var result = chromeDriver.FindElement(By.XPath("//p[@id='result']")).Text;
            return result;
        }

        public static string ClickCancelOnJsConfirm(IWebDriver chromeDriver)
        {
            chromeDriver.FindElement(By.XPath("//button[@onclick = 'jsConfirm()']")).Click();
            chromeDriver.SwitchTo().Alert().Dismiss();
            var result = chromeDriver.FindElement(By.XPath("//p[@id='result']")).Text;
            return result;
        }
        public static string ClickOKJsConfirm(IWebDriver chromeDriver)
        {
            chromeDriver.FindElement(By.XPath("//button[@onclick = 'jsConfirm()']")).Click();
            chromeDriver.SwitchTo().Alert().Accept();
            var result = chromeDriver.FindElement(By.XPath("//p[@id='result']")).Text;
            return result;
        }

        public static string ClickJsPromptConfirm(IWebDriver chromeDriver)
        {
            chromeDriver.FindElement(By.XPath("//button[@onclick = 'jsPrompt()']")).Click();
            chromeDriver.SwitchTo().Alert().Accept();
            var result = chromeDriver.FindElement(By.XPath("//p[@id='result']")).Text;
            return result;
        }
        public static string ClickJsPromptCancel(IWebDriver chromeDriver)
        {
            chromeDriver.FindElement(By.XPath("//button[@onclick = 'jsPrompt()']")).Click();
            chromeDriver.SwitchTo().Alert().Dismiss();
            var result = chromeDriver.FindElement(By.XPath("//p[@id='result']")).Text;
            return result;
        }
        public static string ClickJsPromptText(IWebDriver chromeDriver)
        {
            chromeDriver.FindElement(By.XPath("//button[@onclick = 'jsPrompt()']")).Click();
            var alertText = "Text";
            chromeDriver.SwitchTo().Alert().SendKeys(alertText);
            chromeDriver.SwitchTo().Alert().Accept();
            return "You entered: " + alertText;
        }
    }
}

