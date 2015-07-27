namespace Urho
{
    /// <summary>
    /// Set of predefined colors
    /// </summary>
    public static class Colors
    {
        public static Color White { get { return new Color(); } }
        public static Color Gray { get { return new Color(0.5f, 0.5f, 0.5f); } }
        public static Color Black { get { return new Color(0.0f, 0.0f, 0.0f); } }
        public static Color Red { get { return new Color(1.0f, 0.0f, 0.0f); } }
        public static Color Green { get { return new Color(0.0f, 1.0f, 0.0f); } }
        public static Color Blue { get { return new Color(0.0f, 0.0f, 1.0f); } }
        public static Color Cyan { get { return new Color(0.0f, 1.0f, 1.0f); } }
        public static Color Magenta { get { return new Color(1.0f, 0.0f, 1.0f); } }
        public static Color Yellow { get { return new Color(1.0f, 1.0f, 0.0f); } }
        public static Color Transparent { get { return new Color(0.0f, 0.0f, 0.0f, 0.0f); } }
    }
}
