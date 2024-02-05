using MaterialSkin;

namespace Casino.Theme {
    public static class Colors {
        public static Color HexToColor(string hex) {
            try {
                return ColorTranslator.FromHtml(hex);
            } catch (Exception ex) {
                Console.WriteLine($"Error converting hex to Color: {ex.Message}");
                return Color.Empty;
            }
        }

        public static Color primary = HexToColor("#2b3147");
        public static Color primaryDark = HexToColor("#242738");
        public static Color primaryLight = HexToColor("#343c55");
        public static Color accent = HexToColor("#f46b67");
        public static TextShade textShade = TextShade.WHITE;
    }
}
