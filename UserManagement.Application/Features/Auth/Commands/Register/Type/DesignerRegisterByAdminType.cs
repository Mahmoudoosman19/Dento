//using Common.Domain.Repositories;
//using Common.Domain.Shared;
//using ImageKitFileManager.Abstractions;
//using ImageKitFileManager.Enums;
//using MapsterMapper;
//using MediatR;
//using UserManagement.Application.Features.Auth.Commands.Register.Abstract;
//using UserManagement.Application.Features.Auth.Commands.Register.DTOs;
//using UserManagement.Application.Identity;
//using UserManagement.Application.Specifications.Role;
//using UserManagement.Application.Specifications.Vendor;
//using UserManagement.Domain.Entities;
//using UserManagement.Domain.Enums;
//using UserManagement.Domain.Resources;

//namespace UserManagement.Application.Features.Auth.Commands.Register.Type
//{
//    internal class DesignerRegisterByAdminType : BaseRegister
//    {
//        private readonly IGenericRepository<Designer> _designerRepo;
//        private readonly IImageKitService _imageKitService;
//        private readonly ISender _sender;
//        public DesignerRegisterByAdminType(
//            IMapper mapper,
//            CustomUserManager userManager,
//            IGenericRepository<Role> roleRepo,
//            IGenericRepository<Designer> designerRepo,
//            IImageKitService imageKitService,
//            ISender sender)
//            : base(
//                  mapper,
//                  userManager,
//                  roleRepo)
//        {
//            _designerRepo = designerRepo;
//            _imageKitService = imageKitService;
//            _sender = sender;
//        }

//        public override RegisterType Type { get; set; } = RegisterType.DesignerByAdmin;

//        public async override Task<ResponseModel> Register(RegisterCommand registerDto)
//        {
//            var user = await _userManager.FindByEmailAsync(registerDto.Vendor!.Email);
//            if (user != null)
//                return ResponseModel.Failure(Messages.thisemailisalreadyexist);

//            var userExist = await _designerRepo.IsExistAsync(x => x.User.PhoneNumber == registerDto.Vendor!.PhoneNumber);
//            if (userExist)
//                return ResponseModel.Failure(Messages.PhoneNumberAlreadyUsed);

//            var vendor = _designerRepo.GetEntityWithSpec(new GetVendorByEmailAndPhoneNumberAndCompanyNameSpecification(registerDto));
//            if (vendor != null)
//                return ResponseModel.Failure(Messages.RedundantData);

//            user = _mapper.Map<Domain.Entities.User>(registerDto.Vendor!);
//            user.SetStatus(UserStatus.Active);
//            var vendorRole = _roleRepo.GetEntityWithSpec(new GetRoleByNameEnSpecification(Roles.Vendor.ToString()));
//            user.AssignRole(vendorRole!.Id);
//            user.ConfirmEmail();
//            user.ConfirmPhoneNumber();
//            var result = await _userManager.CreateAsync(user, registerDto.Vendor!.Password);
//            if (!result.Succeeded)
//            {
//                var errors = string.Join(Environment.NewLine,
//                    result.Errors.Select(e => e.Description));
//                return ResponseModel.Failure(errors);
//            }

//            vendor = _mapper.Map<Designer>(registerDto.Vendor);
//            vendor.SetUserId(user.Id);
//            await HandleVendorImages(vendor, registerDto.Vendor, user);

//            await _designerRepo.AddAsync(vendor);
//            await _designerRepo.SaveChangesAsync();

//            CreateWalletCommand command = new CreateWalletCommand();
//            command.UserId = user.Id;
//            var response = await _sender.Send(command);

//            return ResponseModel.Success(Messages.SuccessfulOperation);
//        }

//        private async Task HandleVendorImages(
//            Designer vendor,
//            DesignerRegisterDto vendorRegisterDto,
//            Domain.Entities.User user)
//        {
//            if (vendorRegisterDto.LogoImage != null)
//            {
//                var uploadLogoResult = await _imageKitService.UploadFileAsync(vendorRegisterDto.LogoImage, FileType.Vendor, user.Id);
//                vendor.SetLogoImage(uploadLogoResult.Name, uploadLogoResult.FileId);
//            }
//            var uploadFrontIdResult = await _imageKitService.UploadFileAsync(vendorRegisterDto.BackIdImage, FileType.Vendor, user.Id);
//            var uploadBackIdResult = await _imageKitService.UploadFileAsync(vendorRegisterDto.FrontIdImage, FileType.Vendor, user.Id);

//            vendor.SetBackIdImage(uploadBackIdResult.Name, uploadBackIdResult.FileId);
//            vendor.SetFrontIdImage(uploadFrontIdResult.Name, uploadFrontIdResult.FileId);
//        }
//    }
//}

