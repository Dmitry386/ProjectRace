using Packages.DVMessageBoxes.Source.Dialogs;
using Packages.DVMessageBoxes.Source.Events;
using UnityEngine;

public class TestMessageBoxes : MonoBehaviour
{
    private void Start()
    {
        new MessageDialog("Caption", "Message Test Text text text", "Button1", "Button2", "Button3").Show().OnResponse += OnResponse;
        new InputDialog("Caption", "Message Test Text text text", "Button1", "Button2").Show().OnResponse += OnResponse;
        new ListDialog("Caption", "Button1", "Button2").Show().OnResponse += OnResponse;
        new TabListDialog("Caption", "Button1", "Button2").Show().OnResponse += OnResponse;
    }

    private void OnResponse(DialogResponseEventArgs args)
    {
        Debug.Log(JsonUtility.ToJson(args));
    }
}