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
                invalidUser.GetValidationErrors().Should().Contain(e => e.ErrorCode == "USERNAME_EMPTY" && e.PropertyName == "Username");
                invalidUser.GetValidationErrors().Should().Contain(e => e.ErrorCode == "PASSWORD_EMPTY" && e.PropertyName == "Password");
            }
            else if (string.IsNullOrEmpty(invalidUser.Username))
            {
                invalidUser.GetValidationErrors().Should().HaveCount(1);
                invalidUser.GetValidationErrors().First().Should().Match<IError>(e => e.ErrorCode == "USERNAME_EMPTY" && e.PropertyName == "Username");                           
            }
            else if (string.IsNullOrEmpty(invalidUser.Password))
            {
                invalidUser.GetValidationErrors().Should().HaveCount(1);
                invalidUser.GetValidationErrors().First().Should().Match<IError>(e => e.ErrorCode == "PASSWORD_EMPTY" && e.PropertyName == "Password");
            }
        }

        private class User: ValidatableModel<User>
        {
            public string Username { get; private set; }
            public string Password { get; private set; }

            public User(string username, string password)
            {
                Username = username;
                Password = password;

                UseRuleSpecification(this)
                    .SetRules(u => u.Username, RuleSpecifications.StringNotEmpty("USERNAME_EMPTY", "Username is invalid"))
                    .SetRules(u => u.Password, RuleSpecifications.StringNotEmpty("PASSWORD_EMPTY", "Password is invalid"));
            }

            public void ChangeUsername(string username)
            {
                Username = username;
                ReforceRevalidation();
            }

            public void ChangePassword(string password)
            {
                Password = password;
                ReforceRevalidation();
            }

            struct Error : IError
            {
                public string ErrorCode { get; }
                public string PropertyName { get; }
                public string ErrorMessage { get; }

                public Error(string propertyName, string errorCode, string errorMessage)
                {
                    ErrorCode    = errorCode;
                    ErrorMessage = errorMessage;
                    PropertyName = propertyName;
                }
            }
        }
    }
}