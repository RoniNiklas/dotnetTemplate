using OneOf;

namespace Shared.Requests;
public record GetSingleWeatherForecast(int Id) : IRequest<OneOf<WeatherForecastViewModel, ValidationError>>
{
    public ValidationResult Validate()
    {
        return new Validator().Validate(this);
    }

    public class Validator : AbstractValidator<GetSingleWeatherForecast>
    {
        public Validator()
        {
            RuleFor(item => item.Id).NotEmpty();
        }
    }
}
