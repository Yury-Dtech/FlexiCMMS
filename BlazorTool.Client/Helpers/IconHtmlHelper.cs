using Microsoft.AspNetCore.Components;
using System.Drawing;
using System;

namespace BlazorTool.Client.Helpers
{
    public static class IconHtmlHelper
    {
        public static MarkupString GetIcon(string iconName, int size = 20)
        {
            return new MarkupString($"<img style='width: {size}px; height: {size}px;' src='icons/{iconName}' />");
        }

        public static MarkupString Save(int size = 20) => GetIcon("save_color.png", size);
        public static MarkupString Cancel(int size = 20) => GetIcon("cancel.png", size);
        public static MarkupString CloseOrder(int size = 20) => GetIcon("success.png", size);
        public static MarkupString Warn(int size = 20) => GetIcon("warn.png", size);
        public static MarkupString Delete(int size = 20) => GetIcon("cross_red.png", size);
        public static MarkupString Edit(int size = 20) => GetIcon("edit_color.png", size);
        public static MarkupString Info(int size = 20) => GetIcon("info.png", size);
        public static MarkupString Refresh(int size = 20) => GetIcon("refresh.png", size);
        public static MarkupString TakeOrder(int size = 20) => GetIcon("hand.png", size);
        public static MarkupString TakeOrderBW(int size = 20) => GetIcon("hand_bw.png", size);
        public static MarkupString AddAction(int size = 20) => GetIcon("action.png", size);
        public static MarkupString AssignToMyself(int size = 20) => GetIcon("assign.png", size);
    }


    public static class ColorHelper
    {
        /// <summary>
        /// Возвращает CSS-строку rgba(...) с пониженной прозрачностью или яркостью.
        /// </summary>
        /// <param name="colorName">Имя цвета (например, "red", "green")</param>
        /// <param name="alpha">Прозрачность от 0.0 до 1.0 (по умолчанию 0.3)</param>
        /// <param name="brightnessFactor">Фактор яркости от 0.0 до 1.0 (по умолчанию 1.0 — без изменения)</param>
        /// <returns>CSS-строка вида rgba(r, g, b, a)</returns>
        public static string GetSoftenedColor(string colorName, float alpha = 0.3f, float brightnessFactor = 1.0f)
        {
            var color = Color.FromName(colorName);

            if (!color.IsKnownColor)
                return "rgba(0, 0, 0, 0.1)"; // fallback на случай неизвестного цвета

            int r = Math.Min(255, (int)(color.R * brightnessFactor));
            int g = Math.Min(255, (int)(color.G * brightnessFactor));
            int b = Math.Min(255, (int)(color.B * brightnessFactor));

            return $"rgba({r}, {g}, {b}, {alpha.ToString(System.Globalization.CultureInfo.InvariantCulture)})";
        }
    }

}
