using App.Domain.Entities.Enum;
using App.Domain.Entities.Framework;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Threading.Tasks;

// see: https://www.learnrazorpages.com/advanced/custom-model-binder

namespace App.Client.WebUI.Framework
{   
    public class BookFormatTypeBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(BookFormatType))
                return new BinderTypeModelBinder(typeof(BookFormatTypeBinder));

            return null;
        }
    }

    public class BookFormatTypeBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelName = bindingContext.ModelName;

            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            string value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
                return Task.CompletedTask;

            if (!int.TryParse(value, out int id) && Enumeration.FromValue<BookFormatType>(id) != null)
            {
                bindingContext.ModelState.TryAddModelError(modelName, $"{nameof(BookFormatType)} Id must be an integer.");

                return Task.CompletedTask;
            }

            // convert the id to the correct result
            bindingContext.Result = ModelBindingResult.Success(Enumeration.FromValue<BookFormatType>(id));

            return Task.CompletedTask;
        }
    }
}