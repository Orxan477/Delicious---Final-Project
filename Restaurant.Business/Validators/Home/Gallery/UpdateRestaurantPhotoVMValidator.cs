using FluentValidation;
using Restaurant.Business.ViewModels.Home.Gallery;

namespace Restaurant.Business.Validators.Home.Gallery
{
    public class UpdateRestaurantPhotoVMValidator:AbstractValidator<UpdateRestaurantPhotoVM>
    {
        public UpdateRestaurantPhotoVMValidator()
        {
            RuleFor(x => x.Photo).NotEmpty().NotNull();
        }
    }
}
