using Budgetify.Models;
using Budgetify.Models.DTOs;
using Budgetify.Models.Response;
using Budgetify.Repositories;
using Budgetify.Services;
using Budgetify.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RegisterRequest = Budgetify.Models.Request.RegisterRequest;

namespace Budegtify.Test
{
    [TestFixture]
    public class TestCases
    {
        private IUserService? _userService;
        private AppValidator _appValidator;
        private IJwtService _jwtService;

        [SetUp]
        public void Setup()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var result = new List<GetAllUser>()
            {
                new GetAllUser()
                {
                    UserId = 1,
                    Username = "senghong",
                    Email = "senghong@gmail.com",
                },
                new GetAllUser()
                {
                    UserId = 2,
                    Username = "senghong1",
                    Email = "senghong1@gmail.com",
                }
            };
            userRepositoryMock.Setup(repo => repo.GetAllUsers()).Returns(
                new ApiResponse<List<GetAllUser>>(result)
            );
            
            // userRepositoryMock.Setup(repo => repo.UserRegister()).Returns()
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock
                .Setup(x => x.HttpContext)
                .Returns(new DefaultHttpContext());
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Jwt:Key", "your-secret-key" },
                    { "Jwt:Issuer", "your-issuer" }
                }!)
                .Build();
            var configMock = new Mock<IConfiguration>();
            var serviceProvider = new ServiceCollection()
                .AddScoped<IUserService, UserService>()
                .AddScoped<AppValidator>()
                .AddScoped<IJwtService, JwtService>()
                .AddSingleton<IConfiguration>(configMock.Object)
                .AddSingleton<IHttpContextAccessor>(httpContextAccessorMock.Object)
                .AddScoped<IUserRepository>(_ => userRepositoryMock.Object)
                .BuildServiceProvider();
            _userService = serviceProvider.GetService<IUserService>();
        }

        [Test]
        public void Input_invalid_password_shoud_not_allow_register()
        {
            var initRequest = new RegisterRequest()
            {
                Username = "hong",
                Password = "123456", //expected invalid password
                Email = "hong@gmail.com",
                FirstName = "Hong",
                LastName = "Hong",
            };
            var result = _userService?.UserRegister(initRequest);

            Assert.NotNull(result, "Result should not be null");
            Assert.That(result.ErrorCode, Is.Not.EqualTo(0), "Expected an error code for invalid password");
            Assert.That(result.ErrorMessage, Is.EqualTo("Invalid password format"), "Expected error message did not match");
        }
        
        [Test]
        public void Input_invalid_usernamea_and_password_shoud_not_allow_register()
        {
            var initRequest = new RegisterRequest()
            {
                Username = "ho", //expected invalid username
                Password = "111", //expected invalid password
                Email = "hong@gmail.com",
                FirstName = "Hong",
                LastName = "Hong",
            };
            var result = _userService?.UserRegister(initRequest);

            Assert.NotNull(result, "Result should not be null");
            Assert.That(result.ErrorMessage, Is.EqualTo("Invalid username format, Invalid password format"), "Expected error message did not match");
        }
        [Test]
        public void verify_eisting_username_and_email_shoud_not_allow_register()
        {
            var initRequest = new RegisterRequest()
            {
                Username = "senghong3",
                Password = "111hong",
                Email = "senghong@gmail.com",
                FirstName = "Hong",
                LastName = "Hong",
            };
            var result = _userService?.UserRegister(initRequest);

            Assert.NotNull(result, "Result should not be null");
            Assert.That(result.ErrorMessage, Is.EqualTo("Username or Email is already taken"), "Expected error message did not match");
        }
        
    }
}