using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyTopMovies.Validation
{
    public class GenresValidationAttribute : ValidationAttribute
    {
        private readonly string[] _allowedGenres;
        private readonly char _separator;

        public GenresValidationAttribute(char separator, params string[] allowedGenres)
        {
            _separator = separator;
            _allowedGenres = allowedGenres;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success; 

            var genres = ((string)value).Split(_separator).Select(g => g.Trim()).ToArray(); 

            foreach (var genre in genres)
            {
                if (!_allowedGenres.Contains(genre))
                {
                    return new ValidationResult($"The genre '{genre}' is not allowed.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
