using System.Windows.Media;

namespace MusicLogicExtendedWp7.Repository
{
    public static class ColorRepositiory
    {
        public static Color GetOrange()
        {
            var newColor = new Color() { A = 255, R = 255, G = 103, B = 0 };
            return newColor;
        }

        public static Color GetGrey()
        {
            var newColor = new Color() { A = 255, R = 51, G = 51, B =31 };
            return newColor;
        }
    }
}