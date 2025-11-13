using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using MediatR;
using UserManagement.Application.Features.Auth.Commands.Register.Abstract;
using UserManagement.Application.Features.Auth.Commands.Register.DTOs;
using UserManagement.Application.Identity;
using UserManagement.Application.Specifications.Role;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Auth.Commands.Register.Type
{
    internal class DesignerRegisterType : BaseRegister
    {
        private readonly IGenericRepository<Domain.Entities.Designer> _designerRepo;
        private readonly ISender _sender;
        public DesignerRegisterType(
            IMapper mapper,
            CustomUserManager userManager,
            IGenericRepository<Role> roleRepo,
            IGenericRepository<Domain.Entities.Designer> designerRepo,
            ISender sender)
            : base(
                  mapper,
                  userManager,
                  roleRepo)
        {
            _designerRepo = designerRepo;
            _sender = sender;
        }

        public override RegisterType Type { get; set; } = RegisterType.Designer;

        public async override Task<ResponseModel> Register(RegisterCommand registerDto)
        {
            var user = _mapper.Map<Domain.Entities.User>(registerDto.Designer!);
            user.SetStatus(UserStatus.NotActive);
            var designerRole = _roleRepo.GetEntityWithSpec(new GetRoleByNameEnSpecification(Roles.Designer.ToString()));
            user.AssignRole(designerRole!.Id);

            var result = await _userManager.CreateAsync(user, registerDto.Designer!.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine,
                    result.Errors.Select(e => e.Description));
                return ResponseModel.Failure(errors);
            }

            var vendor = _mapper.Map<Domain.Entities.Designer>(registerDto.Designer);
            vendor.SetUserId(user.Id);
            //await HandleVendorImages(vendor, registerDto.Vendor, user);

            await _designerRepo.AddAsync(vendor);
            await _designerRepo.SaveChangesAsync();

          
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
