using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CityGenWindow : EditorWindow
{
    private float separation = 10.0f;

    Scene currentScene;
    GameObject cityWell = null;
    CityGen cityGen;// = new CityGen();
    private bool saved { get; set; } = true;

    public void Awake()
    {
        this.SetupSceneEnvironment();

        if (cityGen != null)
            this.cityGen.Start();
    }

    public void Update()
    {
        if (cityGen != null)
            this.cityGen.Update();
    }

    public void OnFocus()
    {
        if (cityGen != null)
            this.cityGen.OnFocus();
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
            GUILayout.BeginVertical();
            {
                Color oldColor = GUI.backgroundColor;
                GUI.backgroundColor = Color.blue;
                //GUILayout.Button("fuck you!");
                GUILayout.Label("Active scene: " + this.currentScene.name);
                GUI.backgroundColor = oldColor;
                GUILayout.BeginScrollView(Vector2.zero);
                {
                    GUILayout.TextField("Add buttons for loading and creating cities here!");
                }
                GUILayout.EndScrollView();
            }
            GUILayout.EndVertical();
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
                if (cityGen != null)
                    cityGen.DrawCity();
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

    public void SetupSceneEnvironment()
    {
        // Get the active scene.
        this.currentScene = SceneManager.GetActiveScene();

        // Set all root objects in the scene.
        foreach (GameObject gameObject in this.currentScene.GetRootGameObjects().ToList())
        {
            // Find gameobject named "Cities"
            if (gameObject.name != "Cities")
                continue;

            // Found it! Save it!
            this.cityWell = gameObject;
            break;                          // Stop.
        }
        
        // "Cities" gameobject not found. Create it.
        if(!this.cityWell)
        {
            this.cityWell = new GameObject("Cities");
            this.cityWell.transform.position = Vector3.zero;
        }

        cityGen = this.cityWell.GetComponent<CityGen>();
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