using Xunit;
using ViaCepClient.Validators;
using FluentAssertions;
using System.Linq;

namespace ViaCepClient.Testing.Validations
{
    public class ValidationUnitTesting
    {
        [Fact]
        public void TestValidEntity()
        {
            User user = new User("username", "password");
            AssertValidUser(user);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("username", "")]
        [InlineData("", "password")]
        public void TestInvalidEntities(string username, string password)
        {
            User user = new User(username, password);
            AssertInvalidUser(user);
        }

        [Fact]
        public void TestRevalidation()
        {
            User user = new User("username", "password");
            AssertValidUser(user);

            user.ChangeUsername("");
            user.ChangePassword("password");
            AssertInvalidUser(user);

            user.ChangeUsername("username");
            user.ChangePassword("");
            AssertInvalidUser(user);

            user.ChangeUsername("");
            user.ChangePassword("");
            AssertInvalidUser(user);
        }

        private void AssertValidUser(User validUser)
        {
            validUser.IsValid().Should().BeTrue();
            validUser.IsInvalid().Should().BeFalse();
            validUser.GetValidationErrors().Should().NotBeNull();
            validUser.GetValidationErrors().Should().BeEmpty();
        }

        private void AssertInvalidUser(User invalidUser)
        {
            invalidUser.IsValid  ().Should().BeFalse();
            invalidUser.IsInvalid().Should().BeTrue();
            invalidUser.GetValidationErrors().Should().NotBeNull();
            invalidUser.GetValidationErrors().Should().NotBeEmpty();

            if (string.IsNullOrEmpty(invalidUser.Username) && string.IsNullOrEmpty(invalidUser.Password))
            {
                invalidUser.GetValidationErrors().Should().HaveCount(2);
                invalidUser.GetValidationErrors().Should().Contain(e => e.ErrorCode == "INVALID_USERNAME");
                invalidUser.GetValidationErrors().Should().Contain(e => e.ErrorCode == "INVALID_PASSWORD");
            }
            else if (string.IsNullOrEmpty(invalidUser.Username))
            {
                invalidUser.GetValidationErrors().Should().HaveCount(1);
                invalidUser.GetValidationErrors().First().Should().Match<IError>(e => e.ErrorCode == "INVALID_USERNAME");                           
            }
            else if (string.IsNullOrEmpty(invalidUser.Password))
            {
                invalidUser.GetValidationErrors().Should().HaveCount(1);
                invalidUser.GetValidationErrors().First().Should().Match<IError>(e => e.ErrorCode == "INVALID_PASSWORD");
            }
        }

        private class User: ValidatableModel<User>
        {
            private string _userName;
            public string Username 
            {
                get => _userName;
                private set
                {
                    _userName = value;
                    ReforceRevalidation();
                }
            }

            private string _password;
            public string Password 
            {
                get => _password;
                private set
                {
                    _password = value;
                    ReforceRevalidation();
                }
            }

            public User(string username, string password)
            {
                Username = username;
                Password = password;
            }

            public void ChangeUsername(string username)
            {
                Username = username;
            }

            public void ChangePassword(string password)
            {
                Password = password;
            }

            protected override void PerformValidation()
            {
                if (string.IsNullOrEmpty(Username))
                    AddError("UserName", "INVALID_USERNAME", "Username is invalid");

                if (string.IsNullOrEmpty(Password))
                    AddError("Password", "INVALID_PASSWORD", "Password is invalid");
            }
        }
    }
}