using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace VehicleAccounting.Filters
{
    public class FilterExceptions : IExceptionFilter
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public FilterExceptions(IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            var result = new ViewResult { ViewName = "Error" };
            result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState)
            {
                { "Exception", context.Exception }
            };

            context.ExceptionHandled = true;
            context.Result = result;
        }
    }
}
