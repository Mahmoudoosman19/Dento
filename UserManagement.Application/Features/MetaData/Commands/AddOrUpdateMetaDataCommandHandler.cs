using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;

namespace UserManagement.Application.Features.MetaData.Commands
{
    public class AddOrUpdateMetaDataCommandHandler : ICommandHandler<AddOrUpdateMetaDataCommand>
    {
        private readonly IGenericRepository<Domain.Entities.MetaData> _metaDataRepository;

        public AddOrUpdateMetaDataCommandHandler(IGenericRepository<Domain.Entities.MetaData> metaDataRepository)
        {
            _metaDataRepository = metaDataRepository;
        }
        public async Task<ResponseModel> Handle(AddOrUpdateMetaDataCommand request, CancellationToken cancellationToken)
        {
            var existingMetaData = _metaDataRepository.Get();

            if (!existingMetaData.Any())
            {
                var newMetaData = new Domain.Entities.MetaData
                {
                    Id = Guid.NewGuid(),
                    ApplicationRate = request.ApplicationRate,
                    CreatedOnUtc = DateTime.UtcNow
                };

                await _metaDataRepository.AddAsync(newMetaData);
                await _metaDataRepository.SaveChangesAsync();
                return ResponseModel.Success(Messages.SuccessfulOperation);
            }
            else
            {
                var metaDataToUpdate = existingMetaData.First();
                metaDataToUpdate.ApplicationRate = request.ApplicationRate;
                metaDataToUpdate.ModifiedOnUtc = DateTime.UtcNow;

                _metaDataRepository.Update(metaDataToUpdate);
                await _metaDataRepository.SaveChangesAsync();
                return ResponseModel.Success(Messages.SuccessfulOperation);
            }
        }
    }
}