using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clapton.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Determined whether this <see cref="string"/> and a specified <see cref="string"/> object have the same value, ignoring the case of the strings being compared.
        /// </summary>
        /// <param name="obj">The selected <see cref="string"/> object</param>
        /// <param name="value">The <see cref="string"/> to compare to this instance</param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string obj, string value)
        {
            return obj.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
