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
        public static MarkupString Picture(int size = 20) => GetIcon("nophoto.png", size);


        public static MarkupString GetNoImage()
        {
            MarkupString str = new MarkupString ("<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 240 256\"\r\n     role=\"img\" aria-label=\"Фото скоро будет — маскот\"\r\n     style=\"max-width: 150px; height: auto;\">\r\n  <!-- Тень/подставка -->\r\n  <ellipse cx=\"128\" cy=\"224\" rx=\"64\" ry=\"10\"\r\n           fill=\"var(--brand-muted, #C8D0DD)\" opacity=\"0.25\"/>\r\n\r\n  <!-- Голова -->\r\n  <rect x=\"80\" y=\"32\" width=\"96\" height=\"56\" rx=\"16\"\r\n        fill=\"var(--brand-bg, #F7F9FC)\"\r\n        stroke=\"var(--brand-primary, #5B8DEF)\" stroke-width=\"6\"/>\r\n  <!-- Антенна -->\r\n  <line x1=\"128\" y1=\"20\" x2=\"128\" y2=\"32\"\r\n        stroke=\"var(--brand-primary, #5B8DEF)\" stroke-width=\"6\" stroke-linecap=\"round\"/>\r\n  <circle cx=\"128\" cy=\"16\" r=\"6\" fill=\"var(--brand-primary, #5B8DEF)\"/>\r\n\r\n  <!-- Глаза -->\r\n  <circle cx=\"106\" cy=\"60\" r=\"6\" fill=\"var(--brand-primary, #5B8DEF)\"/>\r\n  <circle cx=\"150\" cy=\"60\" r=\"6\" fill=\"var(--brand-primary, #5B8DEF)\"/>\r\n\r\n  <!-- Тело -->\r\n  <rect x=\"76\" y=\"94\" width=\"104\" height=\"66\" rx=\"18\"\r\n        fill=\"var(--brand-bg, #F7F9FC)\"\r\n        stroke=\"var(--brand-primary, #5B8DEF)\" stroke-width=\"6\"/>\r\n\r\n  <!-- Руки -->\r\n  <path d=\"M76 120 C60 128, 48 140, 48 156\"\r\n        fill=\"none\" stroke=\"var(--brand-primary, #5B8DEF)\" stroke-width=\"6\" stroke-linecap=\"round\"/>\r\n  <path d=\"M180 120 C196 128, 208 140, 208 156\"\r\n        fill=\"none\" stroke=\"var(--brand-primary, #5B8DEF)\" stroke-width=\"6\" stroke-linecap=\"round\"/>\r\n\r\n  <!-- Табличка -->\r\n  <rect x=\"40\" y=\"156\" width=\"176\" height=\"56\" rx=\"12\"\r\n        fill=\"var(--brand-bg, #FFFFFF)\"\r\n        stroke=\"var(--brand-primary, #5B8DEF)\" stroke-width=\"6\"/>\r\n  <text x=\"128\" y=\"190\"\r\n        fill=\"var(--brand-primary, #5B8DEF)\"\r\n        font-family=\"Segoe UI, Inter, Arial, sans-serif\"\r\n        " +
                "font-size=\"16\" text-anchor=\"middle\">No photo</text>\r\n\r\n  <!-- Ножки -->\r\n  <rect x=\"96\" y=\"162\" width=\"16\" height=\"16\" rx=\"4\"\r\n        fill=\"var(--brand-primary, #5B8DEF)\" opacity=\"0.18\"/>\r\n  <rect x=\"144\" y=\"162\" width=\"16\" height=\"16\" rx=\"4\"\r\n        fill=\"var(--brand-primary, #5B8DEF)\" opacity=\"0.18\"/>\r\n</svg>\r\n");
            return str;
        }
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
