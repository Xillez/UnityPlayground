using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CityGenWindow : EditorWindow
{
    public static readonly Color splitterColor = new Color(130.0f / 256.0f, 130.0f / 256.0f, 130.0f / 256.0f, 1.0f);
    private float separation = 10.0f;

	[MenuItem("Window/City Gen")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        CityGenWindow window = EditorWindow.GetWindow<CityGenWindow>();
        window.titleContent.text = "City Gen";
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        {
            MakeCitySelectionMenu(new Rect(Vector2.zero.x, Vector2.zero.y, (this.position.width * 0.2f) - (separation / 2.0f), (this.position.height * 0.5f) - (separation / 2)));
            MakeSplitter(new Rect((this.position.width * 0.2f) - (separation / 2.0f), Vector2.zero.y, separation, (this.position.height * 0.5f) - (separation / 2)));
            MakeCityScenePreview(new Rect((this.position.width * 0.2f) + (separation / 2.0f), Vector2.zero.y, this.position.width * 0.40f - separation, this.position.height * 0.5f));
            MakeSplitter(new Rect((this.position.width * 0.60f) - (separation / 2.0f), Vector2.zero.y, separation, (this.position.height * 0.5f) - (separation / 2)));
            MakeCitySettings(new Rect((this.position.width * 0.60f) + (separation / 2.0f), Vector2.zero.y, this.position.width * 0.40f - (separation / 2.0f), this.position.height * 0.5f));
        }
        EditorGUILayout.EndHorizontal();
        MakeSplitter(new Rect(Vector2.zero.x, (this.position.height * 0.5f) - (separation / 2.0f), this.position.width, separation));
        MakeCityAssetsManager(new Rect(Vector2.zero.x, (this.position.height * 0.5f) + (separation / 2.0f), this.position.width, (this.position.height * 0.5f) - (separation / 2.0f)));
        
    }

    private void MakeCitySelectionMenu(Rect rect)
    {
        MakeSection(rect, () =>
        {
            GUILayout.BeginScrollView(Vector2.zero);
            {
                GUILayout.TextField("Add buttons for loading and creating cities here!");
            }
            GUILayout.EndScrollView();
        });
    }

    private void MakeCityScenePreview(Rect rect)
    {
        MakeSection(rect, () =>
        {
            GUILayout.Button("TODO:");
            GUILayout.TextField("Add scene preview previewing the city to generate");
        });
    }

    private void MakeCitySettings(Rect rect)
    {
        MakeSection(rect, () =>
        {
            GUILayout.Button("TODO:");
            GUILayout.TextField("Add inspector like city settings screen, where user can edit city properties");
        });
    }

    private void MakeCityAssetsManager(Rect rect)
    {
        MakeSection(rect, () =>
        {
            GUILayout.Button("TODO:");
            GUILayout.TextField("Add asset managment (like the tree addition on terrain tool)");
        });
    }

    private void MakeSplitter(Rect rect)
    {
        GUI.Box(rect, ""); //, GUIStyle.none);
    }

    private void MakeSection(Rect rect, Action content)
    {
        GUILayout.BeginArea(rect);
        {
            content();
        }
        GUILayout.EndArea();
    }
}

/*
TODO: 
 - Make SectionManager (manaes the different sections with ids and sections)
 - Make Sections (relative window-size in percentage and content (lambd / void function))
*/

/*public class LayoutOrganizer
{
    Dictionary<int, Section> sections = new Dictionary<int, Section>();

    public void AddSection(Vector2 sizePercent);
}

public class Section
{
    public Vector2 sizePercentage;
}*/