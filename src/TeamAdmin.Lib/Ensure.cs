using System;

namespace TeamAdmin.Lib
{
    public static class Ensure
    {
        public static void NotNull(object t)
        {
            if (t == null) throw new ArgumentNullException();
        }
    }
}
