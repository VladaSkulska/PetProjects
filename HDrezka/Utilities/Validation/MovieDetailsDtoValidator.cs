using FluentValidation;
using HDrezka.Models;
using HDrezka.Models.DTOs;

namespace HDrezka.Utilities.Validation
{
    public class MovieDetailsDtoValidator : AbstractValidator<MovieDetailsDto>
    {
        public MovieDetailsDtoValidator() 
        {
            RuleFor(dto => dto.Genre)
            .NotEmpty().WithMessage("Genre is required")
            .Must(BeAValidGenre).WithMessage("Invalid genre");

            RuleFor(dto => dto.MovieType)
                .NotEmpty().WithMessage("Movie type is required")
                .Must(BeAValidMovieType).WithMessage("Invalid movie type");
        }

        private bool BeAValidGenre(MovieGenre genre)
        {
            return Enum.IsDefined(typeof(MovieGenre), genre);
        }

        private bool BeAValidMovieType(MovieType movieType)
        {
            return Enum.IsDefined(typeof(MovieType), movieType);
        }
    }
}