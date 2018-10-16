using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CityGenWindow : EditorWindow
{
	[MenuItem("Window/City Gen")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        EditorWindow.GetWindow<CityGenWindow>().Show();
    }

    private void OnGUI()
    {
        //
    }

    private void MakeCitySelectionMenu()
    {
        //
    }

    private void MakeCityAssetsManager()
    {
        //
    }

    private void MakeCityScenePreview()
    {
        //
    }

    private void MakeCitySettings()
    {
        //
    }
}
