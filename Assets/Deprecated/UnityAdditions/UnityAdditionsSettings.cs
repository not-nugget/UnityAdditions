using UnityEngine;

namespace UnityAdditions
{
    /// <summary>
    /// Static helper class containing various helper methods and information that can be used package-wide
    /// </summary>
    public static class UAStaticHelpers
    {
        /// <summary>
        /// Generate an informative message through Debug.Log
        /// </summary>
        /// <typeparam name="TClass">Owning class this message originates from</typeparam>
        /// <param name="callingMethod">Method this message is coming from</param>
        /// <param name="message">Message to be shown to the developer</param>
        public static void GenerateMessage<TClass>(string callingMethod, string message)
        {
            Debug.Log($"{nameof(TClass)}.{callingMethod}: {message}");
        }

        /// <summary>
        /// Generate a warning to pass through Debug.LogWarning using a variety of types and strings.
        /// </summary>
        /// <typeparam name="TClass">Owning class this warning originates from</typeparam>
        /// <typeparam name="TParam">Type of parameter which initiated this warning</typeparam>
        /// <param name="callingMethod">Name of method which caught this error</param>
        /// <param name="invalidParameter">Name of parameter which initiated this warning</param>
        /// <param name="parameterValue">Value of the invalid parameter</param>
        /// <param name="warningMessage">Message to give the developer</param>
        public static void GenerateWarning<TClass, TParam>(string callingMethod, string invalidParameter, TParam parameterValue, string warningMessage = null)
        {
            Debug.LogWarning($"{nameof(TClass)}.{callingMethod}: Paramater {invalidParameter}:{nameof(TParam)} was passed with a value of {parameterValue.ToString()}. {warningMessage}");
        }

        /// <summary>
        /// Generate an error to pass through Debug.LogError using a variety of types and strings.
        /// </summary>
        /// <typeparam name="TClass">Owning class this warning originates from</typeparam>
        /// <typeparam name="TParam">Type of parameter which initiated this warning</typeparam>
        /// <param name="callingMethod">Name of method which caught this error</param>
        /// <param name="invalidParameter">Name of parameter which initiated this warning</param>
        /// <param name="parameterValue">Value of the invalid parameter</param>
        /// <param name="errorMessage">Error message to give the developer</param>
        public static void GenerateError<TClass, TParam>(string callingMethod, string invalidParameter, TParam parameterValue, string errorMessage = null)
        {
            Debug.LogError($"{nameof(TClass)}.{callingMethod}: Parameter {invalidParameter}:{nameof(TParam)} was passed with a value of {parameterValue.ToString()}. {errorMessage}");
        }
    }

    /// <summary>
    /// Static base class for storing configuration data used by other classes and extensions
    /// </summary>
    public static class UnityAdditionsSettings
    {
        /// <summary>
        /// Should all methods suppress warnings which would otherwise be produced when valid but unnecesary input is provided? Note: this can be
        /// disabled on a per-method bases. When set to false, can cause massive console bloat if being called improperly within a loop. Default: false
        /// </summary>
        public static bool SuppressWarnings { get; set; } = false;

        /// <summary>
        /// Danger zone! Should methods suppress errors or exceptions which would otherwise be produced by calling methods with bad input? Default: false
        /// </summary>
        public static bool SuppressErrors { get; set; } = false;

        /// <summary>
        /// Not reccommended. Should methods throw errors as opposed to writing to the debug console? Default: false
        /// </summary>
        public static bool ThrowExceptions { get; set; } = false;

        /// <summary>
        /// Enable verbose logging. Useful for debugging. Default: false
        /// </summary>
        public static bool VerboseLogging { get; set; } = false;

        ///// <summary>
        ///// Should all classes and extensions use Unity.Mathematics as opposed to UnityEngine.Mathf for mathematic and random operations? NOTE: The new
        ///// mathematics library uses a shader-similar syntax, such as float3 instead of Vector3. Thus, conversions may add overhead and decrease
        ///// performance of your application. However, Unity.Mathematics is neccesary in Unity's Burst compiler, and can often produce better
        ///// performance when using it with burst as well. Your mileage may vary.
        ///// </summary>
        //public static bool UseMathematics { get; set; } = false;
    }
}