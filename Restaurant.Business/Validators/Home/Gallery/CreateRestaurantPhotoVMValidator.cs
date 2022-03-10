using FluentValidation;
using Restaurant.Business.ViewModels.Home.Gallery;

namespace Restaurant.Business.Validators.Home.Gallery
{
    public class CreateRestaurantPhotoVMValidator:AbstractValidator<CreateRestaurantPhotoVM>
    {
        public CreateRestaurantPhotoVMValidator()
        {
            RuleFor(x => x.Photo).NotEmpty().NotNull();
        }
    }
}
