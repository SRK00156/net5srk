using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebApp.CustomFilters
{
	/// <summary>
	/// class used to handle Custom Exceptions
	/// </summary>
	public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
	{
		private readonly IModelMetadataProvider modelMetadataProvider;

		/// <summary>
		/// Let the constructor injected with IModelMetadtaProvider
		/// THis interface is used in Http Request Processing to read the Model Metadata used
		/// in the Http Request
		/// THis will be resolved in AddContrtollerWithViews() method of the
		/// ConfigureServices() method of STartup class
		/// </summary>
		public CustomExceptionFilterAttribute(IModelMetadataProvider modelMetadataProvider)
		{
			this.modelMetadataProvider = modelMetadataProvider;
		}


		/// <summary>
		/// Logfic for Custom  Exceptions
		/// </summary>
		/// <param name="context"></param>
		public override void OnException(ExceptionContext context)
		{
			// Handle the Exception
			context.ExceptionHandled = true;
			// Read the Exception Message
			var message = context.Exception.Message;

			// Return the Result as a ViewResult to Error Page
			ViewResult viewResult = new ViewResult();
			// Use the ErrorView which is present in the Shared Folder of the Views 
			viewResult.ViewName = "Error";

			// To pass the error infromation to the View, create a ViewDataDictionary, because the Model property of the ViewResult is ReadOnly
			ViewDataDictionary dictionary = new ViewDataDictionary(modelMetadataProvider, context.ModelState);
			dictionary["ControllerName"] = context.RouteData.Values["controller"].ToString();
			dictionary["ActionName"] = context.RouteData.Values["action"].ToString();
			dictionary["ErrorMessage"] = message;

			viewResult.ViewData = dictionary;

			// set the result
			context.Result = viewResult;
		}
	}
}
