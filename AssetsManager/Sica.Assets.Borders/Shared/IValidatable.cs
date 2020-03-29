using FluentValidation;

namespace Sica.Assets.Borders.Shared
{
    public interface IValidatable<T>
    {
        void SetValidator(AbstractValidator<T> validator);
    }
}
