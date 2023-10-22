namespace Ticketer.Business.Services.Errors;

public record ValidationError(string Field, string Message);