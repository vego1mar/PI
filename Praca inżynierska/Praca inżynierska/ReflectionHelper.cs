using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PI
{

    static class ReflectionHelper
    {

        [MethodImpl( MethodImplOptions.NoInlining )]
        [Description( "Provide MethodBase.GetCurrentMethod() as an argument." )]
        internal static string GetMethodFullName( MethodBase methodBase )
        {
            if ( methodBase == null ) {
                return null;
            }

            return methodBase.ReflectedType.FullName + "." + methodBase.Name;
        }

    }

}
