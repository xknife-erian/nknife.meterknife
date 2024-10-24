using Moq;
using RAY.Common.Authentication;
using RAY.Common.Plugin.Modules;
using System.Security.Claims;

namespace NKnife.Module.Manager.SurroundingManager
{
    public class MockAuthenticationManagerModule : BasePicoModule<IAuthenticationManager>
    {
        private readonly Mock<IAuthenticationManager> _mockAuth = new ();

        /// <inheritdoc />
        public override Task<bool> StartServiceAsync()
        {
            var mockUser = new Mock<IUser>();
            _mockAuth.Setup(auth => auth.GetCurrentUser()).Returns(mockUser.Object);

            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, "administrator"),
                new (ClaimTypes.Role, "管理员"),
                new ("UserId", "1"),
                new ("RoleId", "1"),
                new ("DisplayName", "admin")
            };

            var identity = new ClaimsIdentity(claims, "LeiaoAuth");
            UserSession.Instance.CurrentUser = new ClaimsPrincipal(identity);

            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public override Task<bool> StopServiceAsync()
        {
            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public override Task<bool> ResetServiceAsync()
        {
            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public override Lazy<IAuthenticationManager> Build(params object[] args)
        {
            return new Lazy<IAuthenticationManager>(() => _mockAuth.Object);
        }

        /// <inheritdoc />
        public override void Dispose() { }
    }
}