using System.Text.RegularExpressions;

namespace Undefinable.Tests.ReadmeExamples;

public class PracticalExample
{
    public class SetPasswordTests
    {
        [Fact]
        public void PasswordIsNull() => Test(password: null, expectedError: "password is required");
        [Fact]
        public void PasswordIsEmpty() => Test(password: string.Empty, expectedError: "password is invalid");
        [Fact]
        public void PasswordIsTooShort() => Test(password: "sh0rt.", expectedError: "password is too short");
        [Fact]
        public void PasswordIsTooWeak() => Test(password: "weakpassword", expectedError: "password is too weak");
        [Fact]
        public void PasswordIsStrong() => Test(password: RandomStrongPassword);
        [Fact]
        public void UserIdIsNull() => Test(userId: null, expectedError: "userId is required");
        [Fact]
        public void UserIdIsEmpty() => Test(userId: string.Empty, expectedError: "userId is invalid");
        [Fact]
        public void UserIdDoesNotExist() => Test(userId: Guid.NewGuid().ToString("n"), expectedError: "userId not found");

        private IApi _api = new Api();
        private string RandomUsername => "user" + DateTime.Now.Ticks;
        private string RandomStrongPassword => Path.GetRandomFileName() + "A1a";

        private void Test(
            Undefinable<string> userId = default, 
            Undefinable<string> password = default,
            Undefinable<string> expectedError = default)
        {
            /* ARRANGE */

            // if userId is not defined, create a new user
            if (!userId.IsDefined)
                userId = _api.CreateUser(RandomUsername, RandomStrongPassword).UserId;

            // if password is not defined, define a strong one
            if (!password.IsDefined)
                password = RandomStrongPassword;

            /* ACT */
            var setPasswordResult = _api.SetPassword(userId.Value, password.Value);

            /* ASSERT */
            if (expectedError.IsDefined)
            {
                // check for error
                Assert.False(setPasswordResult.Success);
                Assert.Equal(expectedError, setPasswordResult.Error);
            }
            else
            {
                // check for success
                Assert.True(setPasswordResult.Success);
            }
        }
    }

    public interface IApi
    {
        CreateUserResult CreateUser(string username, string password);

        SetPasswordResult SetPassword(string userId, string password);
    }

    public sealed class CreateUserResult
    {
        public string Error { get; set; }
        public bool Success { get; set; }
        public string UserId { get; set; }
    }

    public sealed class SetPasswordResult
    {
        public string Error { get; set; }
        public bool Success { get; set; }
    }

    public class Api : IApi
    {
        private Dictionary<string, (string username, string password)> _users = new()
        {
            [Guid.Empty.ToString()] = ("administrator", "Adm1n1str@t0r")
        };

        public CreateUserResult CreateUser(string username, string password)
        {
            var userId = Guid.NewGuid().ToString();
            _users.Add(userId, (username, password));
            return new CreateUserResult { Success = true, UserId = userId };
        }

        public SetPasswordResult SetPassword(string userId, string password)
        {
            // validate userId
            if (userId == null)
                return new SetPasswordResult { Error = "userId is required" };

            if (string.IsNullOrWhiteSpace(userId))
                return new SetPasswordResult { Error = "userId is invalid" };

            if (!_users.ContainsKey(userId))
                return new SetPasswordResult { Error = "userId not found" };

            // validate password
            if (password == null)
                return new SetPasswordResult { Error = "password is required" };

            if (string.IsNullOrWhiteSpace(password))
                return new SetPasswordResult { Error = "password is invalid" };

            if (password.Length < 8)
                return new SetPasswordResult { Error = "password is too short" };

            if (!Regex.IsMatch(password, "[A-Z]") ||
                !Regex.IsMatch(password, "[a-z]") ||
                !Regex.IsMatch(password, "[0-9]") ||
                !Regex.IsMatch(password, "[!@#$%^&*().]"))
                return new SetPasswordResult { Error = "password is too weak" };

            // update username
            _users[userId] = (_users[userId].username, password);

            return new SetPasswordResult { Success = true };
        }
    }
}