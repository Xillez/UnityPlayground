using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CityGenWindow : EditorWindow
{
    private float separation = 10.0f;

    CityGen generator = new CityGen();
    private bool saved { get; set; } = true;

    public void Awake()
    {
        this.generator.Start();
    }

    public void Update()
    {
        this.generator.Update();
    }

    public void OnFocus()
    {
        this.generator.OnFocus();
    }

    public void OnGUI()
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
            if (GUILayout.Button("Draw"))
            {
                generator.DrawCity();
            }
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

    // Static Initiation function
    [MenuItem("Window/City Gen")]
    public static void Init()
    {
        // Get existing open window or if none, make a new one:
        CityGenWindow window = (CityGenWindow) EditorWindow.GetWindow<CityGenWindow>();
        window.titleContent.text = "City Gen";
        window.Show();
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