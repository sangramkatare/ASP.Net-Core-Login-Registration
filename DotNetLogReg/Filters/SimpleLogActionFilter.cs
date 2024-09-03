using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNetLogReg.Filters
{
    public class SimpleLogActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Code that run before the Action execute
            Console.WriteLine("before Executing he action");
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //Code that run after execution
            Console.WriteLine("After Executing the Action: ");
        }

    }
}
