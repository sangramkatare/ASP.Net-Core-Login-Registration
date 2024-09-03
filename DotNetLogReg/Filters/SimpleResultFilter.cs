using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace DotNetLogReg.Filters
{
    public class SimpleResultFilter:ResultFilterAttribute
    {
       
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            //base.OnResultExecuting(context);
            if (context.Result is ObjectResult result)
            {
                result.Value = new
                {
                    data = result.Value,
                    info = "this is the custom message added by the  Result Filter"
                };
            }

        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {

            //
        }
    }
}
