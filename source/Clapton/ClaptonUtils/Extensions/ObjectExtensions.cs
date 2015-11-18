namespace Clapton.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Gets the value of a property within an object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj);
        }
    }
}
