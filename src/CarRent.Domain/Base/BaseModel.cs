namespace CarRent.Domain.Base
{
    /// <summary>
    /// Base model used by all other domain models, with generic id type
    /// </summary>
    public abstract class BaseModel<T>
    {
        /// <summary>
        /// Id of model
        /// </summary>
        public T Id { get; set; } = default!;
    }

    /// <summary>
    /// Base model alternative with Id type integer
    /// </summary>
    public abstract class BaseModel : BaseModel<int>
    {

    }
}
