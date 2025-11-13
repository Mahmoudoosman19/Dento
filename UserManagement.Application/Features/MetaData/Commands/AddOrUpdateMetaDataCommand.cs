using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.MetaData.Commands
{
    public class AddOrUpdateMetaDataCommand : ICommand
    {
        public decimal ApplicationRate { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
    }
}
