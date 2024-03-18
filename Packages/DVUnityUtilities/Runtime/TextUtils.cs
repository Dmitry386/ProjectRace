using UnityEngine;

namespace DVUnityUtilities
{
    public static class TextUtils
    {
        public static string AddColor(object text, Color col) => $"<color={ColorHexFromUnityColor(col)}>{text.ToString()}</color>";
        public static string ColorHexFromUnityColor(Color unityColor) => $"#{ColorUtility.ToHtmlStringRGBA(unityColor)}";
    }
}
