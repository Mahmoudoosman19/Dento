using Common.Domain.Primitives;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;

namespace UserManagement.Domain.Entities
{
    public class User: IdentityUser<Guid>, IAuditableEntity, ISoftDeleteEntity
    {
        #region Props
        public string? FullNameEn { get; private set; }
        public string? FullNameAr { get; private set; }
        //public string? AppleId { get; private set; }
        public UserStatus Status { get; private set; }
        public long? RoleId { get; private set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Role? Role { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public UserGender Gender { get; private set; }
        public Guid? OtpId { get; private set; }
        [ForeignKey(nameof(OtpId))]
        public virtual OTP? Otp { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? RestoredAt { get; set; }
        #endregion

        #region CTORs
        public User()
        {

        }

        public User(string userName, string email, string phoneNumber, string fullNameEn, string fullNameAr, UserStatus status, UserGender gender, DateTime? birthDate = null)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PhoneNumber = phoneNumber;
            SetFullName(fullNameEn, fullNameAr);
            SetGender(gender);
            SetStatus(status);
            SetBirthDate(birthDate);
        }
        public User(string userName, string email, string phoneNumber, string fullNameEn, string fullNameAr, UserStatus status, UserGender gender, long roleId, DateTime? birthDate = null) : this(userName, email, phoneNumber, fullNameEn, fullNameAr, status, gender, birthDate)
        {
            AssignRole(roleId);
        }

        #endregion

        public void SetFullName(string? fullNameEn = null, string? fullNameAr = null)
        {
            if (fullNameEn is null && fullNameAr is null)
            {
                throw new ArgumentException("At least one name must have a value");
            }

            if (fullNameEn == "")
            {
                throw new ArgumentException("English full name cannot be empty", nameof(fullNameEn));
            }

            if (fullNameAr == "")
            {
                throw new ArgumentException("Arabic full name cannot be empty", nameof(fullNameAr));
            }

            FullNameEn = fullNameEn;
            FullNameAr = fullNameAr;
        }

        public void SetStatus(UserStatus status)
        {
            Status = status;
        }
        public void SetGender(UserGender gender)
        {
            Gender = gender;
        }
        public void AssignRole(long roleId)
        {
            RoleId = roleId;
        }

        //public void SetAppleId(string? appleId)
        //{
        //    AppleId = appleId;
        //}

        public void AssignRole(Role role)
        {
            Role = role;
            RoleId = role.Id;
        }

        public void SetOtp(OTP? otp)
        {
            Otp = otp;
        }

        public void SetOtp(Guid otpId)
        {
            OtpId = otpId;
        }

        public void ConfirmEmail()
        {
            EmailConfirmed = true;
        }

        public void ConfirmPhoneNumber()
        {
            PhoneNumberConfirmed = true;
        }

        public void Lockout(DateTime? lockoutEnd = null)
        {
            LockoutEnabled = true;
            LockoutEnd = lockoutEnd;
        }

        public void DisableLockout()
        {
            LockoutEnabled = false;
            LockoutEnd = null;
        }

        public void SetBirthDate(DateTime? birthDate)
        {
            BirthDate = birthDate;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void ToggleActivation()
        {
            if (Status is UserStatus.Active)
                Status = UserStatus.NotActive;
            else
                Status = UserStatus.Active;
        }

        public void Restore()
        {
            IsDeleted = false;
            RestoredAt = DateTime.UtcNow;
        }
    }
}
