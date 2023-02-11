using FluentValidation;

namespace CarRent.Application.Extensions
{
    public static class ValidatorExtensions
    {
        /// <summary>
        /// Performs validation of a typed object of type <typeparamref name="T"/> using the supplied set of 
        /// <see cref="IValidator{T}"/>.
        /// </summary>
        public static async Task Validate<T>(this IEnumerable<IValidator<T>> validators, T instance, CancellationToken cancellation)
        {
            instance = instance ?? throw new ArgumentNullException(nameof(instance));

            var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(
                new ValidationContext<T>(instance),
                cancellation)));

            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
        }
    }
}
