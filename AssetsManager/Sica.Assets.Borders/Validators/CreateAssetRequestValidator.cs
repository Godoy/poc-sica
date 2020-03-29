using FluentValidation;
using Sica.Assets.Borders.Dtos.Assets;
using Sica.Assets.Borders.Entities;

namespace Sica.Assets.Borders.Validators
{
    public class CreateAssetRequestValidator : AbstractValidator<CreateAssetRequest>
    {
        public CreateAssetRequestValidator()
        {
            RuleFor(p => p).NotNull().WithErrorCode("Asset.Null");   
        }
    }
}
