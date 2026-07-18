using AttributeValidator.AspNetCore.Tests.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.Validation.Attributes.AspNetCore.Minimal;
using RzR.Validation.Attributes.AspNetCore.Options;

namespace AttributeValidator.AspNetCore.Tests.Tests;

[TestClass]
public class ValidationFilterTests
{
    private static ValidationFilter<CreateThingModel> BuildFilter(int invalidStatusCode = StatusCodes.Status400BadRequest)
    {
        var options = Options.Create(new RzRValidationOptions
        {
            InvalidStatusCode = invalidStatusCode
        });
        var logger = NullLogger<ValidationFilter<CreateThingModel>>.Instance;

        return new ValidationFilter<CreateThingModel>(options, logger);
    }

    private static DefaultEndpointFilterInvocationContext BuildContext(
        HttpContext http, params object[] args)
        => new DefaultEndpointFilterInvocationContext(http, args);

    [TestMethod]
    public async Task InvokeAsync_InvalidModel_ShortCircuitsWithValidationProblem_Status400()
    {
        // Arrange
        var http = new DefaultHttpContext();
        var model = new CreateThingModel { Name = "", Qty = 0 };
        var ctx = BuildContext(http, model);
        var filter = BuildFilter();
        bool nextCalled = false;
        EndpointFilterDelegate next = _ =>
        {
            nextCalled = true;
            return ValueTask.FromResult<object?>("OK");
        };

        // Act
        var result = await filter.InvokeAsync(ctx, next);

        // Assert
        Assert.IsFalse(nextCalled);
        Assert.IsNotNull(result
        );
        var statusCodeResult = result as IStatusCodeHttpResult;
        Assert.IsNotNull(statusCodeResult);
        Assert.AreEqual(StatusCodes.Status400BadRequest, statusCodeResult.StatusCode);
    }

    [TestMethod]
    public async Task InvokeAsync_ValidModel_CallsNextAndReturnsItsValue()
    {
        // Arrange
        var http = new DefaultHttpContext();
        var model = new CreateThingModel { Name = "ok", Qty = 5 };
        var ctx = BuildContext(http, model);
        var filter = BuildFilter();
        bool nextCalled = false;
        EndpointFilterDelegate next = _ =>
        {
            nextCalled = true;

            return ValueTask.FromResult<object?>("OK");
        };

        // Act
        var result = await filter.InvokeAsync(ctx, next);

        // Assert
        Assert.IsTrue(nextCalled);
        Assert.AreEqual("OK", result);
    }

    [TestMethod]
    public async Task InvokeAsync_NoArgumentOfTypeT_CallsNext()
    {
        // Arrange
        var http = new DefaultHttpContext();
        var ctx = BuildContext(http, (object)42);
        var filter = BuildFilter();
        bool nextCalled = false;
        EndpointFilterDelegate next = _ =>
        {
            nextCalled = true;

            return ValueTask.FromResult<object?>("PASSTHROUGH");
        };

        // Act
        var result = await filter.InvokeAsync(ctx, next);

        // Assert
        Assert.IsTrue(nextCalled);
        Assert.AreEqual("PASSTHROUGH", result);
    }

    [TestMethod]
    public async Task InvokeAsync_CustomStatusCode422_InvalidModel_Returns422()
    {
        // Arrange
        var http = new DefaultHttpContext();
        var model = new CreateThingModel { Name = "", Qty = 0 };
        var ctx = BuildContext(http, model);
        var filter = BuildFilter(invalidStatusCode: StatusCodes.Status422UnprocessableEntity);
        bool nextCalled = false;
        EndpointFilterDelegate next = _ =>
        {
            nextCalled = true;

            return ValueTask.FromResult<object?>("OK");
        };

        // Act
        var result = await filter.InvokeAsync(ctx, next);

        // Assert
        Assert.IsFalse(nextCalled);
        Assert.IsNotNull(result);

        var statusCodeResult = result as IStatusCodeHttpResult;
        Assert.IsNotNull(statusCodeResult);
        Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, statusCodeResult.StatusCode);
    }
}
