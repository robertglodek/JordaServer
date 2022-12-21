using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Jorda.Server.WebUi.Exceptions;

public class InvalidModelStateException:Exception
{
    public InvalidModelStateException(ModelStateDictionary modelState)
    {
        ModelState = modelState;
    }

    public ModelStateDictionary ModelState { get; set; }
}
