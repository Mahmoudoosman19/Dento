//using Common.Domain.Repositories;
//using Common.Domain.Shared;
//using ImageKitFileManager.Abstractions;
//using ImageKitFileManager.Enums;
//using MapsterMapper;
//using MediatR;
//using UserManagement.Application.Features.Auth.Commands.Register.Abstract;
//using UserManagement.Application.Features.Auth.Commands.Register.DTOs;
//using UserManagement.Application.Features.wallet.Command.CreateWallet;
//using UserManagement.Application.Identity;
//using UserManagement.Application.Specifications.Role;
//using UserManagement.Domain.Entities;
//using UserManagement.Domain.Enums;

//namespace UserManagement.Application.Features.Auth.Commands.Register.Type
//{
//    internal class DesignerRegisterType : BaseRegister
//    {
//        private readonly IGenericRepository<Designer> _vendorRepo;
//        private readonly IImageKitService _imageKitService;
//        private readonly ISender _sender;
//        public DesignerRegisterType(
//            IMapper mapper,
//            CustomUserManager userManager,
//            IGenericRepository<Role> roleRepo,
//            IGenericRepository<Designer> vendorRepo,
//            IImageKitService imageKitService,
//            ISender sender)
//            : base(
//                  mapper,
//                  userManager,
//                  roleRepo)
//        {
//            _vendorRepo = vendorRepo;
//            _imageKitService = imageKitService;
//                      _sender = sender;
//        }

//        public override RegisterType Type { get; set; } = RegisterType.Vendor;

//        public async override Task<ResponseModel> Register(RegisterCommand registerDto)
//        {
//            var user = _mapper.Map<Domain.Entities.User>(registerDto.Vendor!);
//            user.SetStatus(UserStatus.NotActive);
//            var vendorRole = _roleRepo.GetEntityWithSpec(new GetRoleByNameEnSpecification(Roles.Vendor.ToString()));
//            user.AssignRole(vendorRole!.Id);

//            var result = await _userManager.CreateAsync(user, registerDto.Vendor!.Password);
//            if (!result.Succeeded)
//            {
//                var errors = string.Join(Environment.NewLine,
//                    result.Errors.Select(e => e.Description));
//                return ResponseModel.Failure(errors);
//            }

//            var vendor = _mapper.Map<Designer>(registerDto.Vendor);
//            vendor.SetUserId(user.Id);
//            await HandleVendorImages(vendor, registerDto.Vendor, user);

//            await _vendorRepo.AddAsync(vendor);
//            await _vendorRepo.SaveChangesAsync();

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
