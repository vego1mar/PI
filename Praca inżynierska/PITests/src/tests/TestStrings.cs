namespace PITests.src.tests
{
    public static class TestStrings
    {
        public static bool IsEmpty( string str )
        {
            return str.Equals( string.Empty );
        }

        public static bool IsVersionNumber( string str )
        {
            foreach ( char ch in str ) {
                if ( !char.IsNumber( ch ) && !ch.Equals( '.' ) ) {
                    return false;
                }
            }

            return true;
        }
    }
}
