# Usage Guide — ASP.NET Core Integration

`RzR.Validation.Attributes.AspNetCore` is a host-specific companion to [`RzR.Validation.Attributes`](usage.md). It doesn't add any new validation rules of its own — instead it wires the core `Val*` attributes into ASP.NET Core request pipelines and shapes whatever fails into a consistent RFC 7807 `application/problem+json` response.

It exists to close two gaps that show up in different places:

- **Minimal APIs** don't run `DataAnnotations` validation on bound parameters on their own (true for `net6.0` through `net9.0`). Without this package, a model decorated with `[ValRequiredNotEmpty]`, `[ValEmail]`, etc. gets bound and handed straight to the handler, and none of that validation ever runs.
- **MVC controllers** already run `DataAnnotations` during model binding and populate `ModelState`, but every action still ends up with the same boilerplate: check `ModelState.IsValid`, translate it into a problem-details response, repeat. This package removes that boilerplate.

> **NOTE**
> .NET 10 adds `AddValidation()` and `[ValidatableType]`, which run DataAnnotations natively on Minimal API parameters. If you're targeting .NET 10+, use the platform mechanism for new endpoints. This package still earns its keep on `net6.0`–`net9.0`, for standardizing MVC responses, and for DI-backed attributes that need `HttpContext.RequestServices`. Don't enable both this filter and the platform's native validation on the same endpoint at once — you'll get duplicate error entries.

---

## Install

This package depends on the core `RzR.Validation.Attributes` package — your models still need to be decorated with its attributes — and targets `net8.0`, so it only makes sense inside an ASP.NET Core project.

```powershell
dotnet add package RzR.Validation.Attributes
dotnet add package RzR.Validation.Attributes.AspNetCore
```

---

## Register services

Call `AddRzRValidation()` once during startup and you're done:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRzRValidation();
```

If the defaults don't fit, use the configure overload:

```csharp
builder.Services.AddRzRValidation(options =>
{
    options.InvalidStatusCode = StatusCodes.Status422UnprocessableEntity;
    options.ProblemTitle = "Request failed validation.";
    options.ProblemTypeUri = "https://example.com/problems/validation-error";
    options.MemberNameTransformer = name => char.ToLowerInvariant(name[0]) + name[1..];
});
```

`RzRValidationOptions` (namespace `RzR.Validation.Attributes.AspNetCore`) has four settings worth knowing:

| Property | Default | Purpose |
| --- | --- | --- |
| `InvalidStatusCode` | `400` | HTTP status returned when validation fails. Common alternative: `422`. |
| `ProblemTitle` | `"One or more validation errors occurred."` | The `title` field of the RFC 7807 body. Set `null` to let the framework choose its own default. |
| `ProblemTypeUri` | `null` | The `type` field of the RFC 7807 body. `null` uses the framework's built-in type URI. |
| `MemberNameTransformer` | `null` | `Func<string, string>` applied to every key in the `errors` dictionary, e.g. to convert `PascalCase` property names to `camelCase`. |

Both the Minimal API filter and the MVC filter read from the same `RzRValidationOptions`, so you configure this once and both pipelines respond the same way.

---

## Minimal API usage

Start by decorating your request model with core attributes from `RzR.Validation.Attributes.Attributes.*`:

```csharp
using RzR.Validation.Attributes.Attributes.Require;
using RzR.Validation.Attributes.Attributes.Identity;
using RzR.Validation.Attributes.Attributes.Greater;

public class CreateThing
{
    [ValRequiredNotEmpty]
    public string Name { get; set; } = string.Empty;

    [ValEmail]
    public string ContactEmail { get; set; } = string.Empty;

    [ValGreaterThan(0)]
    public int Quantity { get; set; }
}
```

Then wire up a single endpoint with `WithValidation<T>()` (namespace `RzR.Validation.Attributes.AspNetCore.Minimal`):

```csharp
using RzR.Validation.Attributes.AspNetCore.Minimal;

app.MapPost("/things", (CreateThing thing) => Results.Ok(thing))
   .WithValidation<CreateThing>();
```

Or, if every endpoint in a group needs the same treatment, apply it once to the whole group:

```csharp
var api = app.MapGroup("/api").WithValidation<CreateThing>();

api.MapPost("/things", (CreateThing thing) => Results.Created($"/things/{thing.Name}", thing));
```

Under the hood, `WithValidation<T>()` registers `ValidationFilter<T>`, an `IEndpointFilter`. It finds the first bound argument of type `T`, validates it through the core `TryValidate` extension (passing along `HttpContext.RequestServices` so DI-backed attributes can resolve services), and short-circuits with `Results.ValidationProblem(...)` if anything fails. If validation passes, it calls `next(context)` and your handler runs as normal.

Here's what a failing request looks like. **Sample 400 response** (`Content-Type: application/problem+json`) for a `CreateThing` posted with an empty `Name`, an invalid `ContactEmail`, and `Quantity = 0`:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Name": ["'Name' must not be empty."],
    "ContactEmail": ["'ContactEmail' is not a valid email address."],
    "Quantity": ["'Quantity' must be greater than 0."]
  }
}
```

The `type` URI here comes from the ASP.NET Core problem-details defaults unless you set `ProblemTypeUri` yourself; `title` and `status` come straight from your `RzRValidationOptions`.

---

## MVC usage

MVC model binding already runs `DataAnnotations` (including the core `Val*` attributes) and populates `ModelState` before your action even executes. `RzRValidateModelFilter` and `[ValidateModel]` don't re-run that validation — they take an already-invalid `ModelState` and standardize it into the same RFC 7807 shape used by the Minimal API filter.

**Per-controller or per-action opt-in**, using `[ValidateModel]` (namespace `RzR.Validation.Attributes.AspNetCore.Mvc`):

```csharp
using Microsoft.AspNetCore.Mvc;
using RzR.Validation.Attributes.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[ValidateModel]
public class ThingsController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(CreateThing thing) => Ok(thing);
}
```

**Global registration**, if you'd rather not decorate every controller — apply the filter once and it covers all of them:

```csharp
using RzR.Validation.Attributes.AspNetCore.Mvc;

builder.Services.AddControllers(options =>
{
    options.Filters.Add<RzRValidateModelFilter>();
});
```

Pick one or the other. Adding `[ValidateModel]` to a controller that already has the filter registered globally just runs the same check twice — harmless, but pointless.

---

## Response contract

Regardless of which pipeline caught the failure, the response comes back in the same RFC 7807 problem-details shape:

| Field | Source |
| --- | --- |
| `type` | `RzRValidationOptions.ProblemTypeUri`, or the framework default when `null`. |
| `title` | `RzRValidationOptions.ProblemTitle`. |
| `status` | `RzRValidationOptions.InvalidStatusCode`. |
| `errors` | A dictionary keyed by member name; each value is a deduplicated array of error messages for that member. Validation results with no associated member name use `""` as the key. |

Want `422 Unprocessable Entity` instead of the default `400`? Set it directly:

```csharp
builder.Services.AddRzRValidation(options =>
    options.InvalidStatusCode = StatusCodes.Status422UnprocessableEntity);
```

And if your API contract is camelCase JSON but your C# properties are PascalCase, convert the member-name keys on the way out:

```csharp
builder.Services.AddRzRValidation(options =>
    options.MemberNameTransformer = name => char.ToLowerInvariant(name[0]) + name[1..]);
```

---

## Notes and constraints

- Model classes still need attributes from the core `RzR.Validation.Attributes` package (`RzR.Validation.Attributes.Attributes.*` namespaces) — this package only validates, it doesn't define any rules of its own.
- Requires ASP.NET Core (`net8.0`). Because the package references `Microsoft.AspNetCore.App` via `FrameworkReference`, it only works inside an ASP.NET Core project — not a plain class library or console app.
- Both `ValidationFilter<T>` and `RzRValidateModelFilter` take an `ILogger<T>` constructor dependency and log at `Debug` level when they short-circuit a request (failing member names only, never the values themselves). As long as your host has the default ASP.NET Core logging set up, these messages show up automatically — nothing extra to register.
- `ValidationFilter<T>` only checks the first bound argument matching type `T`. If an endpoint takes multiple parameters of the same type, the rest are skipped.
- Class-targeted and cross-property core attributes (`ValAtLeastOneOf`, `ValMutuallyExclusive`, `ValExactlyOneOf`, `ValChronological`, `ValRequiredIf`, `ValRequiredUnless`, `ValCompareProperty`) work correctly under both pipelines, because the core `TryValidate` extension and MVC's own binding both validate with `validateAllProperties: true`. See [usage.md](usage.md#cross-property-and-object-level-attributes-require-validateallproperties-true) for more on those attributes.

## See also

- [docs/usage.md](usage.md) — full core attribute reference.
