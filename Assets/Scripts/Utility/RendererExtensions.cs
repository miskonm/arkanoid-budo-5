using UnityEngine;

namespace Arkanoid.Utility
{
    public static class RendererExtensions
    {
        public static void SetAlpha(this SpriteRenderer sr, float alpha)
        {
            Color color = sr.color;
            color.a = alpha;
            sr.color = color;
        }
    }
}