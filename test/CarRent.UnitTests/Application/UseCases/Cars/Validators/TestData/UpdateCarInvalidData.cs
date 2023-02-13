using CarRent.Application.UseCases.Cars.Handlers;

namespace CarRent.UnitTests.Application.UseCases.Cars.Validators.TestData
{
    public class UpdateCarInvalidData : TheoryData<UpdateCar.Command, IList<(string property, string errorCode)>>
    {
        public UpdateCarInvalidData()
        {
            Add(new UpdateCar.Command() { Id = 0, Make = "TestMake", Model = "TestModel", UniqueId = "C12345" }, new List<(string, string)> { ("Id", ValidationErrorCodes.NotEmpty) });
            Add(new UpdateCar.Command() { Id = 2, Make = "TestMake", Model = "TestModel", UniqueId = "C12345" }, new List<(string, string)> { ("Id", ValidationErrorCodes.AsyncPredicate) });

            Add(new UpdateCar.Command() { Id = 1, Make = null!, Model = "TestModel", UniqueId = "C12345" }, new List<(string, string)> { ("Make", ValidationErrorCodes.NotEmpty) });
            Add(new UpdateCar.Command() { Id = 1, Make = "", Model = "TestModel", UniqueId = "C12345" }, new List<(string, string)> { ("Make", ValidationErrorCodes.NotEmpty) });
            Add(new UpdateCar.Command() { Id = 1, Make = new string(Enumerable.Repeat('1', 100).ToArray()), Model = "TestModel", UniqueId = "C12345" }, new List<(string, string)> { ("Make", ValidationErrorCodes.MaximumLength) });

            Add(new UpdateCar.Command() { Id = 1, Make = "TestMake", Model = null!, UniqueId = "C12345" }, new List<(string, string)> { ("Model", ValidationErrorCodes.NotEmpty) });
            Add(new UpdateCar.Command() { Id = 1, Make = "TestMake", Model = "", UniqueId = "C12345" }, new List<(string, string)> { ("Model", ValidationErrorCodes.NotEmpty) });
            Add(new UpdateCar.Command() { Id = 1, Make = "TestMake", Model = new string(Enumerable.Repeat('1', 500).ToArray()), UniqueId = "C12345" }, new List<(string, string)> { ("Model", ValidationErrorCodes.MaximumLength) });

            Add(new UpdateCar.Command() { Id = 1, Make = "TestMake", Model = "TestModel", UniqueId = null! }, new List<(string, string)> { ("UniqueId", ValidationErrorCodes.NotEmpty) });
            Add(new UpdateCar.Command() { Id = 1, Make = "TestMake", Model = "TestModel", UniqueId = "" }, new List<(string, string)> { ("UniqueId", ValidationErrorCodes.NotEmpty) });
            Add(new UpdateCar.Command() { Id = 1, Make = "TestMake", Model = "TestModel", UniqueId = "C" + new string(Enumerable.Repeat('1', 500).ToArray()) }, new List<(string, string)> { ("UniqueId", ValidationErrorCodes.MaximumLength) });
            Add(new UpdateCar.Command() { Id = 1, Make = "TestMake", Model = "TestModel", UniqueId = "C2" }, new List<(string, string)> { ("UniqueId", ValidationErrorCodes.AsyncPredicate) });
        }
    }
}
