namespace Nancy.JsonPatch.Core.Demo
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.JsonPatch.Operations;
    using Nancy.ModelBinding;
    using Newtonsoft.Json.Serialization;
    using System.Net.Http;
    using Nancy;
    using Nancy.JsonPatch.Core;
    using Nancy.JsonPatch.Core.Demo;

    public class HomeModule : NancyModule
    {
        private Contact contact = new Contact
        {
            FirstName = "Vincent",
            LastName = "Vega",
            Age = 42,
            Links = new List<string> { "http://vincentvega.com" }
        };

        public HomeModule()
        {
            Get("/", args => "Nancy.JsonPatch demo");

            Patch("/", _ =>
            {
                var incomingModel = this.Bind<List<Operation<Contact>>>();
                var patch = new JsonPatchDocument<Contact>(incomingModel, new DefaultContractResolver());
                var patched = contact.Copy();

                patch.ApplyTo(patched, ModelValidationResult);

                if (!ModelValidationResult.IsValid)
                {
                    return Response.AsJson(ModelValidationResult.Errors).WithStatusCode(422);
                }

                var model = new { original = contact, patched = patched };

                return model;
            });
        }
    }
}