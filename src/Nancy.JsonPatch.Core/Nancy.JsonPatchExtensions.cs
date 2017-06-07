namespace Nancy.JsonPatch.Core
{
    using System;
    using Microsoft.AspNetCore.JsonPatch;
    using Nancy.Validation;
    using Nancy;

    public static class NancyJsonPatchExtensions
    {
        /// <summary>
        /// Applies JSON patch operations on object and logs errors in <see cref="ModelValidationResult"/>.
        /// </summary>
        /// <param name="patchDoc">The <see cref="JsonPatchDocument{T}"/>.</param>
        /// <param name="objectToApplyTo">The entity on which <see cref="JsonPatchDocument{T}"/> is applied.</param>
        /// <param name="modelValidationResult">The <see cref="ModelValidationResult"/> to add errors.</param>
        public static void ApplyTo<T>(
            this JsonPatchDocument<T> patchDoc,
            T objectToApplyTo,
            ModelValidationResult modelValidationResult) where T : class
        {
            if (patchDoc == null)
            {
                throw new ArgumentNullException(nameof(patchDoc));
            }

            if (objectToApplyTo == null)
            {
                throw new ArgumentNullException(nameof(objectToApplyTo));
            }

            if (modelValidationResult == null)
            {
                throw new ArgumentNullException(nameof(modelValidationResult));
            }

            patchDoc.ApplyTo(objectToApplyTo, modelValidationResult, prefix: string.Empty);
        }

        /// <summary>
        /// Applies JSON patch operations on object and logs errors in <see cref="ModelStateDictionary"/>.
        /// </summary>
        /// <param name="patchDoc">The <see cref="JsonPatchDocument{T}"/>.</param>
        /// <param name="objectToApplyTo">The entity on which <see cref="JsonPatchDocument{T}"/> is applied.</param>
        /// <param name="modelValidationResult">The <see cref="ModelValidationResult"/> to add errors.</param>
        /// <param name="prefix">The prefix to use when looking up values in <see cref="ModelValidationResult"/>.</param>
        public static void ApplyTo<T>(
            this JsonPatchDocument<T> patchDoc,
            T objectToApplyTo,
            ModelValidationResult modelState,
            string prefix) where T : class
        {
            if (patchDoc == null)
            {
                throw new ArgumentNullException(nameof(patchDoc));
            }

            if (objectToApplyTo == null)
            {
                throw new ArgumentNullException(nameof(objectToApplyTo));
            }

            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }

            patchDoc.ApplyTo(objectToApplyTo, jsonPatchError =>
            {
                var affectedObjectName = jsonPatchError.AffectedObject.GetType().Name;
                var key = string.IsNullOrEmpty(prefix) ? affectedObjectName : prefix + "." + affectedObjectName;

                modelState.Errors.Add(key, jsonPatchError.ErrorMessage);
            });
        }
    }
}