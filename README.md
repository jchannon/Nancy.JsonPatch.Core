# Nancy.JsonPatch

A library that allows usage of [JsonPatch](http://jsonpatch.com/) with [Nancy](http://github.com/nancyfx/nancy)

```csharp
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
```