using AuthServiceIN6BM.Api.Models;
using AuthServiceIN6BM.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuthServiceIN6BM.Api.ModelBinders;

public class FileDataModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);
        if (!typeof(IFileData).IsAssignableFrom(bindingContext.ModelType))
        {
            return Task.CompletedTask;
        }

        var request = bindingContext.HttpContext.Request;

        var file = request.From.Files.GetFiles(bindingContext.FileName);

        if (file != null && file.Length > 0)
        {
            var fileData = new FormFileAdapter(file);
            bindingContext.Result = ModelBindingResult.Success(fileData);
        }
        else
        {
            bindingContext.Result = ModelBindingResult.Success(null);
        }
        return Task.CompletedTask;
    }

}


public class FileDataModelBinderProvinder : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (typeof(IFileData).IsAssignableFrom(context.Metadata.ModelType))
        {
            return new FileDataModelBinder();
        }
        return null; 
    }
}