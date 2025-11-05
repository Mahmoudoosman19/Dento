using Common.Domain.Repositories;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UserManagement.Domain.Enums;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Identity
{
using UserManagement.Domain.Enums;
    public class CustomUserManager : UserManager<User>
    {
        private readonly IGenericRepository<Role> _roleRepo;
        private readonly IMapper _mapper;

        public CustomUserManager(
            IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger,
            IGenericRepository<Role> roleRepo,
            IMapper mapper)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _roleRepo = roleRepo;
            _mapper = mapper;
        }

        public async Task<bool> IsUserExistByEmailAsync(string email)
        {
            var user = await FindByEmailAsync(email);
            return user != null;
        }

        public async Task<bool> IsUserExistByIdAsync(Guid id)
        {
            var user = await FindByIdAsync(id.ToString());
            return user != null;
        }

        public bool IsUserActive(User user)
        {
            return user != null && user.Status == UserStatus.Active;
        }

        public bool IsUserBlocked(User user)
        {
            return user != null && user.Status == UserStatus.Blocked;
        }

        public bool IsUserDeleted(User user)
        {
            return user != null && user.Status == UserStatus.Deleted;
        }

        public async Task<User?> GetUserWithRoleAsync(Guid id)
        {
            var user = await FindByIdAsync(id.ToString());
            if (user != null)
            {
                long roleId = (long)user.RoleId!;
                var role = await _roleRepo.GetByIdAsync(roleId);
                user.AssignRole(role!);
            }
            return user;
        }

        public async Task<User?> GetUserWithRoleAsync(string email)
        {
            var user = await FindByEmailAsync(email);
            if (user != null)
            {
                long roleId = (long)user.RoleId!;
                var role = await _roleRepo.GetByIdAsync(roleId);
                user.AssignRole(role!);
            }
            return user;
        }

        public override Task<bool> CheckPasswordAsync(User user, string password)
        {
            bool validPassword = BCrypt.Net.BCrypt.EnhancedVerify(password, user.PasswordHash);
            return Task.FromResult(validPassword);
        }

        public override async Task<IdentityResult> CreateAsync(User user, string password)
        {
            user.SecurityStamp = await GenerateConcurrencyStampAsync(user);

            var userValidatorResults = await ValidateUserAsync(user);
            if (!userValidatorResults.Succeeded)
            {
                return userValidatorResults;
            }

            var passwordValidatorResults = await ValidatePasswordAsync(user, password);
            if (!passwordValidatorResults.Succeeded)
            {
                return passwordValidatorResults;
            }

            user.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            user.NormalizedUserName = user.UserName!.ToUpper();
            user.NormalizedEmail = user.Email!.ToUpper();
            user.DisableLockout();

            var result = await Store.CreateAsync(user, CancellationToken.None);
            return result;
        }

        public async override Task<IdentityResult> UpdateAsync(User user)
        {
            var storedUser = await FindByIdAsync(user.Id.ToString());

            if (storedUser == null)
                return IdentityResult.Failed(new IdentityError { Description = $"User with ID {user.Id} not found." });

            _mapper.Map(user, storedUser);

            return await base.UpdateAsync(storedUser);
        }
        public async Task<IdentityResult> validatePassword(User user, string password)
        {
            return await ValidatePasswordAsync(user, password);
        }
        
        public async Task<IdentityResult> CreateExternalLoginUser(User user)
        {
            user.SecurityStamp = await GenerateConcurrencyStampAsync(user);

            user.NormalizedEmail = user.Email!.ToUpper();
            user.EmailConfirmed = true;
            
            user.DisableLockout();

            var result = await Store.CreateAsync(user, CancellationToken.None);
            return result;
        }

        public void ChangePassword(User user, string password)
        {
            user!.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }
    }
}
