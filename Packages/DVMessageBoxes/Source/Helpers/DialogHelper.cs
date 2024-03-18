using Packages.DVMessageBoxes.Source.Dialogs;
using Packages.DVMessageBoxes.Source.Events;
using System;
using TMPro;
using UnityEngine.UI;

namespace Packages.DVMessageBoxes.Source.Helpers
{
    internal static class DialogHelper
    {
        public static void SetButtonTextOrDeactivate(Button button, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                button.gameObject.SetActive(false);
            }
            else
            {
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<TextMeshProUGUI>().text = text;
            }
        }

        public static void SetButtonResponse(Button button, Dialog dial, DialogResponseEventArgs args, Action act = null)
        {
            button.onClick.AddListener(() =>
            {
                act?.Invoke();
                dial.InvokeInternalRespose(args);
            });
        }
    }
}