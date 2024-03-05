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

        private bool BeAValidGenre(string genre)
        {
            return Enum.TryParse(genre, true, out MovieGenre _);
        }

        private bool BeAValidMovieType(string movieType)
        {
            return Enum.TryParse(movieType, true, out MovieType _);
        }
    }
}
