using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.Validation.Attributes.AspNetCore.Mvc;
using RzR.Validation.Attributes.AspNetCore.Options;

namespace AttributeValidator.AspNetCore.Tests.Tests;

[TestClass]
public class RzRValidateModelFilterTests
{
    private static RzRValidateModelFilter BuildFilter(int invalidStatusCode = StatusCodes.Status400BadRequest)
    {
        var options = Options.Create(new RzRValidationOptions
        {
            InvalidStatusCode = invalidStatusCode
        });
        var logger = NullLogger<RzRValidateModelFilter>.Instance;

        return new RzRValidateModelFilter(options, logger);
    }

    private static ActionExecutingContext BuildActionContext(ModelStateDictionary modelState)
    {
        var httpContext = new DefaultHttpContext();
        var routeData = new RouteData();
        var actionDescriptor = new ActionDescriptor();
        var actionContext = new ActionContext(httpContext, routeData, actionDescriptor, modelState);

        return new ActionExecutingContext(
            actionContext,
            filters: new List<IFilterMetadata>(),
            actionArguments: new Dictionary<string, object?>(),
            controller: new object());
    }

    [TestMethod]
    public void OnActionExecuting_InvalidModelState_SetsResultToValidationProblemDetails_Status400()
    {
        // Arrange
        var modelState = new ModelStateDictionary();
        modelState.AddModelError("Name", "required");
        var ctx = BuildActionContext(modelState);
        var filter = BuildFilter();

        // Act
        filter.OnActionExecuting(ctx);

        // Assert
        Assert.IsNotNull(ctx.Result);

        var objectResult = ctx.Result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);

        var problemDetails = objectResult.Value as ValidationProblemDetails;
        Assert.IsNotNull(problemDetails);
        Assert.AreEqual(StatusCodes.Status400BadRequest, problemDetails.Status);
    }

    [TestMethod]
    public void OnActionExecuting_ValidModelState_DoesNotSetResult()
    {
        // Arrange
        var modelState = new ModelStateDictionary(); 
        var ctx = BuildActionContext(modelState);
        var filter = BuildFilter();

        // Act
        filter.OnActionExecuting(ctx);

        // Assert
        Assert.IsNull(ctx.Result);
    }

    [TestMethod]
    public void OnActionExecuting_CustomStatusCode422_InvalidModelState_Sets422()
    {
        // Arrange
        var modelState = new ModelStateDictionary();
        modelState.AddModelError("Name", "required");
        var ctx = BuildActionContext(modelState);
        var filter = BuildFilter(invalidStatusCode: StatusCodes.Status422UnprocessableEntity);

        // Act
        filter.OnActionExecuting(ctx);

        // Assert
        Assert.IsNotNull(ctx.Result);

        var objectResult = ctx.Result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, objectResult.StatusCode);

        var problemDetails = objectResult.Value as ValidationProblemDetails;
        Assert.IsNotNull(problemDetails);
        Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, problemDetails.Status);
    }

    [TestMethod]
    public void OnActionExecuted_DoesNotThrow()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        var routeData = new RouteData();
        var actionDescriptor = new ActionDescriptor();
        var modelState = new ModelStateDictionary();
        var actionContext = new ActionContext(httpContext, routeData, actionDescriptor, modelState);
        var actionExecutedContext = new ActionExecutedContext(
            actionContext,
            filters: new List<IFilterMetadata>(),
            controller: new object());
        var filter = BuildFilter();

        // Act & Assert — must not throw
        filter.OnActionExecuted(actionExecutedContext);
    }
}
