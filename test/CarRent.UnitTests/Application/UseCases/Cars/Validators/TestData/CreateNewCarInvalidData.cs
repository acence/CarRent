using CarRent.Application.Behaviours;
using CarRent.Application.UseCases.Cars.Handlers;

namespace CarRent.UnitTests.Application.UseCases.Cars.Validators.TestData
{
    public class CreateNewCarInvalidData : TheoryData<CreateNewCar.Command, IList<(string property, string errorCode)>>
    {
        public CreateNewCarInvalidData()
        {
            Add(new CreateNewCar.Command() { Make = null!, Model = "TestModel", UniqueId = "C12345" }, new List<(string, string)> { ("Make", ValidationErrorCodes.NotEmpty) });
            Add(new CreateNewCar.Command() { Make = "", Model = "TestModel", UniqueId = "C12345" }, new List<(string, string)> { ("Make", ValidationErrorCodes.NotEmpty) });
            Add(new CreateNewCar.Command() { Make = new string(Enumerable.Repeat('1', 100).ToArray()), Model = "TestModel", UniqueId = "C12345" }, new List<(string, string)> { ("Make", ValidationErrorCodes.MaximumLength) });

            Add(new CreateNewCar.Command() { Make = "TestMake", Model = null!, UniqueId = "C12345" }, new List<(string, string)> { ("Model", ValidationErrorCodes.NotEmpty) });
            Add(new CreateNewCar.Command() { Make = "TestMake", Model = "", UniqueId = "C12345" }, new List<(string, string)> { ("Model", ValidationErrorCodes.NotEmpty) });
            Add(new CreateNewCar.Command() { Make = "TestMake", Model = new string(Enumerable.Repeat('1', 500).ToArray()), UniqueId = "C12345" }, new List<(string, string)> { ("Model", ValidationErrorCodes.MaximumLength) });

            Add(new CreateNewCar.Command() { Make = "TestMake", Model = "TestModel", UniqueId = null! }, new List<(string, string)> { ("UniqueId", ValidationErrorCodes.NotEmpty) });
            Add(new CreateNewCar.Command() { Make = "TestMake", Model = "TestModel", UniqueId = "" }, new List<(string, string)> { ("UniqueId", ValidationErrorCodes.NotEmpty) });
            Add(new CreateNewCar.Command() { Make = "TestMake", Model = "TestModel", UniqueId = "C" + new string(Enumerable.Repeat('1', 500).ToArray()) }, new List<(string, string)> { ("UniqueId", ValidationErrorCodes.MaximumLength) });
            Add(new CreateNewCar.Command() { Make = "TestMake", Model = "TestModel", UniqueId = "C2" }, new List<(string, string)> { ("UniqueId", ValidationErrorCodes.NotUnique) });
        }
    }
}
