using System;
using System.Web.Mvc;

namespace Quintessence.QPlanet.Webshell.Infrastructure.ModelBinders
{
    public class ProjectCandidateCategoryDetailTypeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".DetailType");

            if (result == null)
                throw new ArgumentOutOfRangeException("bindingContext", "Missing DetailType");

            var derivedType = bindingContext.ModelType.Assembly.GetType(result.AttemptedValue);

            var model = Activator.CreateInstance(derivedType);

            bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, model.GetType());

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}