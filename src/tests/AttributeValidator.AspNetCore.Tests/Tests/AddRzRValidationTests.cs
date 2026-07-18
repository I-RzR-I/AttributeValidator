using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.Validation.Attributes.AspNetCore;
using RzR.Validation.Attributes.AspNetCore.Options;

namespace AttributeValidator.AspNetCore.Tests.Tests;

[TestClass]
public class AddRzRValidationTests
{
    [TestMethod]
    public void AddRzRValidation_NoConfiguration_DefaultInvalidStatusCode400()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddRzRValidation();
        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<RzRValidationOptions>>().Value;

        // Assert
        Assert.AreEqual(StatusCodes.Status400BadRequest, options.InvalidStatusCode);
    }

    [TestMethod]
    public void AddRzRValidation_NoConfiguration_DefaultProblemTitle()
    {
        // Arrange & Act
        var services = new ServiceCollection();
        services.AddRzRValidation();
        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<RzRValidationOptions>>().Value;

        // Assert
        Assert.AreEqual("One or more validation errors occurred.", options.ProblemTitle);
    }

    [TestMethod]
    public void AddRzRValidation_ConfigureDelegate_OverridesInvalidStatusCode422()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddRzRValidation(o => o.InvalidStatusCode = StatusCodes.Status422UnprocessableEntity);
        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<RzRValidationOptions>>().Value;

        // Assert
        Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, options.InvalidStatusCode);
    }

    [TestMethod]
    public void AddRzRValidation_ReturnsServiceCollection_IsChainable()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var returned = services.AddRzRValidation();

        // Assert
        Assert.AreSame(services, returned);
    }

    [TestMethod]
    public void AddRzRValidation_WithConfigure_ReturnsServiceCollection_IsChainable()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var returned = services.AddRzRValidation(_ => { });

        // Assert
        Assert.AreSame(services, returned);
    }

    [TestMethod]
    public void AddRzRValidation_NoConfiguration_DefaultProblemTypeUriIsNull()
    {
        // Arrange & Act
        var services = new ServiceCollection();
        services.AddRzRValidation();
        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<RzRValidationOptions>>().Value;

        // Assert
        Assert.IsNull(options.ProblemTypeUri);
    }

    [TestMethod]
    public void AddRzRValidation_NoConfiguration_DefaultMemberNameTransformerIsNull()
    {
        // Arrange & Act
        var services = new ServiceCollection();
        services.AddRzRValidation();
        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<RzRValidationOptions>>().Value;

        // Assert
        Assert.IsNull(options.MemberNameTransformer);
    }
}
