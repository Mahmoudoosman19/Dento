using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using MediatR;
using UserManagement.Application.Features.Auth.Commands.Register.Abstract;
using UserManagement.Application.Features.Auth.Commands.Register.DTOs;
using UserManagement.Application.Identity;
using UserManagement.Application.Specifications.Designer;
using UserManagement.Application.Specifications.Role;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.Auth.Commands.Register.Type
{
    internal class DesignerRegisterByAdminType : BaseRegister
    {
    
        public DesignerRegisterByAdminType(
            IMapper mapper,
            CustomUserManager userManager,
            IGenericRepository<Role> roleRepo)
            : base(
                  mapper,
                  userManager,
                  roleRepo)
        {
            
        }

        public override RegisterType Type { get; set; } = RegisterType.DesignerByAdmin;

        public async override Task<ResponseModel> Register(RegisterCommand registerDto)
        {
            // Map to User entity
            var user = _mapper.Map<Domain.Entities.User>(registerDto.Designer!);
            user.SetStatus(UserStatus.Active);
            user.ConfirmEmail();
            user.ConfirmPhoneNumber();

            // Assign Role
            var designerRole = _roleRepo.GetEntityWithSpec(new GetRoleByNameEnSpecification(Roles.Designer.ToString()));
            user.AssignRole(designerRole!.Id);
            user.ConfirmEmail();
            user.ConfirmPhoneNumber();

            // Create user in Identity
            var result = await _userManager.CreateAsync(user, registerDto.Designer!.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine,
                    result.Errors.Select(e => e.Description));

                return ResponseModel.Failure(errors);
            }


            return ResponseModel.Success(Messages.SuccessfulOperation);
        }

        //private async Task HandleVendorImages(
        //    Designer vendor,
        //    DesignerRegisterDto vendorRegisterDto,
        //    Domain.Entities.User user)
        //{
        //    if (vendorRegisterDto.LogoImage != null)
        //    {
        //        var uploadLogoResult = await _imageKitService.UploadFileAsync(vendorRegisterDto.LogoImage, FileType.Vendor, user.Id);
        //        vendor.SetLogoImage(uploadLogoResult.Name, uploadLogoResult.FileId);
        //    }
        //    var uploadFrontIdResult = await _imageKitService.UploadFileAsync(vendorRegisterDto.BackIdImage, FileType.Vendor, user.Id);
        //    var uploadBackIdResult = await _imageKitService.UploadFileAsync(vendorRegisterDto.FrontIdImage, FileType.Vendor, user.Id);

        //    vendor.SetBackIdImage(uploadBackIdResult.Name, uploadBackIdResult.FileId);
        //    vendor.SetFrontIdImage(uploadFrontIdResult.Name, uploadFrontIdResult.FileId);
        //}
    }
}

