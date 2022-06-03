using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Tests.Services.AuthenticationServices
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private Mock<IAccountService> _mockAccountService = new();
        private Mock<IPasswordHasher> _mockPasswordHasher = new();
        private AuthenticationService _authenticationService;

        [SetUp]
        public void SetUp()
        {
            _mockAccountService = new();
            _mockPasswordHasher = new();
            _authenticationService = new(_mockAccountService.Object, _mockPasswordHasher.Object);
        }

        [Test]
        public async Task Login_WithCorrectPasswordForExistingUsername_ReturnsAccountForCorrectUsername()
        {
            string expectedUsername = "testUsername";
            string password = "testPassword";


            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(
                new Account()
                {
                    AccountHolder = new User()
                    {
                        Username = expectedUsername
                    }
                });


            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password))
                .Returns(PasswordVerificationResult.Success);


            Account account = await _authenticationService.Login(expectedUsername, password);

            string actualUsername = account.AccountHolder.Username;

            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithIncorrectPasswordForExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            string expectedUsername = "testUsername";
            string password = "testPassword";

            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(
                new Account()
                {
                    AccountHolder = new User()
                    {
                        Username = expectedUsername
                    }
                });

            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password))
                .Returns(PasswordVerificationResult.Failed);

            if (Assert.ThrowsAsync<InvalidPasswordException>
                (() => _authenticationService.Login(expectedUsername, password))
                is InvalidPasswordException exception)
            {
                string actualUsername = exception.Username;
                Assert.AreEqual(expectedUsername, actualUsername);
            }
        }

        [Test]
        public void Login_WithNonExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            string expectedUsername = "testUsername";
            string password = "testPassword";

            if (Assert.ThrowsAsync<UserNotFoundException>
                (() => _authenticationService.Login(expectedUsername, password))
                is UserNotFoundException exception)
            {
                string actualUsername = exception.Username;
                Assert.AreEqual(expectedUsername, actualUsername);
            }
        }

        [Test]
        public async Task Register_WithPasswordsNotMatching_ReturnsPasswordsDoNotMatch()
        {

            string password = "password";
            string confirmPassword = "confirmPassword";

            RegistrationResult expected = RegistrationResult.PasswordsDoNotMatch;
            RegistrationResult actual = await _authenticationService
                .Register(It.IsAny<string>(), It.IsAny<string>(), password, confirmPassword);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingEmail_ReturnsEmailAlreadyExists()
        {
            string email = "example@you.com";

            _mockAccountService.Setup(s => s.GetByEmail(email)).ReturnsAsync(new Account());

            RegistrationResult expected = RegistrationResult.EmailAlreadyExists;
            RegistrationResult actual = await _authenticationService
                .Register(email, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingUsername_ReturnsUsernameAlreadyExists()
        {
            string username = "UserName";

            _mockAccountService.Setup(s => s.GetByUsername(username)).ReturnsAsync(new Account());

            RegistrationResult expected = RegistrationResult.UsernameAlreadyExists;
            RegistrationResult actual = await _authenticationService
                .Register(It.IsAny<string>(), username, It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithNonExistingUsernameAndMatchingPasswords_ReturnsSuccess()
        {
            RegistrationResult expected = RegistrationResult.Success;
            RegistrationResult actual = await _authenticationService
                .Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expected, actual);
        }
    }
}
