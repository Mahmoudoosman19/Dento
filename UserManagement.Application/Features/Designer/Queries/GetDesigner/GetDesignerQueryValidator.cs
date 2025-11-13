using Common.Domain.Repositories;
using UserManagement.Application.Features.Designer.Queries.GetDesignerById;

namespace UserManagement.Application.Features.Designer.Queries;

internal class GetDesignerQueryValidator : AbstractValidator<GetDesignerQuery>
{
    public GetDesignerQueryValidator(IGenericRepository<Domain.Entities.Designer> vendorRepo)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(Messages.EmptyField);
            //.EntityExist(vendorRepo).WithMessage(Messages.NotFound);
    }
}
