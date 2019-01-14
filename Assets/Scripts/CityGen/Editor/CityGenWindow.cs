using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using Object = UnityEngine.Object;

public class CityGenWindow : EditorWindow
{
    private float separation = 10.0f;

    Scene currentScene;
    GameObject cityWell = null;
    CityGen cityGen;// = new CityGen();
    private bool saved { get; set; } = true;

    private GameObject cameraObj;
    private Camera camera;
    private RenderTexture renderTexture;
    //private Rect cityPreviewRect;

    protected string newCityName = "";

    public void Awake()
    {
        this.SetupSceneEnvironment();
        this.SetupSceneRenderTexture();

        if (cityGen == null)
            return;

        this.cityGen.Start();
    }

    public void Start()
    {
        //
    }

    public void Update()
    {
        if (cityGen != null)
            this.cityGen.Update();

        Rect scenePreview = new Rect((this.position.width * 0.20f) + (separation / 2.0f), Vector2.zero.y, (this.position.width * 0.40f) - (separation / 2.0f), (this.position.height * 0.50f) - (separation / 2.0f));
        if (this.renderTexture.width != scenePreview.width || 
            this.renderTexture.height != scenePreview.height)
            this.renderTexture = new RenderTexture((int)scenePreview.width, (int)scenePreview.height, (int)RenderTextureFormat.ARGB32);

        if (this.camera != null)
        {
            this.camera.targetTexture = renderTexture;
            this.camera.Render();
            this.camera.targetTexture = null;
        }
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
            MakeCitySelectionMenu(new Rect(Vector2.zero.x, Vector2.zero.y, (this.position.width * 0.20f) - (separation / 2.0f), (this.position.height * 0.50f) - (separation / 2.0f)));
            MakeSplitter(new Rect((this.position.width * 0.20f) - (separation / 2.0f), Vector2.zero.y, separation, (this.position.height * 0.5f) - (separation / 2.0f)));
            MakeCityScenePreview(new Rect((this.position.width * 0.20f) + (separation / 2.0f), Vector2.zero.y, (this.position.width * 0.40f) - (separation / 2.0f), (this.position.height * 0.50f) - (separation / 2.0f)));
            MakeSplitter(new Rect((this.position.width * 0.60f) - (separation / 2.0f), Vector2.zero.y, separation, (this.position.height * 0.5f) - (separation / 2.0f)));
            MakeCitySettings(new Rect((this.position.width * 0.60f) + (separation / 2.0f), Vector2.zero.y, (this.position.width * 0.40f) - (separation / 2.0f), (this.position.height * 0.50f) - (separation / 2.0f)));
        }
        EditorGUILayout.EndHorizontal();
        MakeSplitter(new Rect(Vector2.zero.x, (this.position.height * 0.5f) - (separation / 2.0f), this.position.width, separation));
        MakeCityAssetsManager(new Rect(Vector2.zero.x, (this.position.height * 0.5f) + (separation / 2.0f), this.position.width, (this.position.height * 0.5f) - (separation / 2.0f)));
    }

    public void OnDestroy()
    {
        CityGenWindow.SafeDestroyGameObject(this.camera);
    }

    private void MakeCitySelectionMenu(Rect rect)
    {
        GUIStyle sceneTitleColor = new GUIStyle();
        sceneTitleColor.normal.background = MakeUniformTex((int) rect.width, (int) rect.height, GUI.backgroundColor);

        MakeSection(rect, () =>
        {
            GUILayout.BeginVertical();
            {
                EditorGUILayout.BeginVertical(sceneTitleColor);
                {
                    GUILayout.Label("Active scene: " + this.currentScene.name);
                }
                EditorGUILayout.EndVertical();
                GUILayout.BeginScrollView(Vector2.zero);
                {
                    // Make buttons for cities.
                    if (this.cityGen != null && this.cityGen.cities != null && this.cityGen.cities.Count > 0)
                        for (int i = 0; i < this.cityGen.cities.Count; i++)
                            if (this.cityGen.cities[i] != null)
                            {
                                if (GUILayout.Button(this.cityGen.cities[i].name))//, ((this.cityGen.cityLoaded != i) ? new GUIStyle(GUI.skin.button) : buttonStyle)))
                                    this.cityGen.LoadCity(i);
                            }

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    //GUILayout.BeginHorizontal();
                    {
                        this.newCityName = EditorGUILayout.TextField("City name: ", this.newCityName);
                        if (GUILayout.Button("Add city"))
                        {
                            this.cityGen.NewCity(this.newCityName);
                            this.newCityName = "";
                        }
                    }
                    //GUILayout.EndHorizontal();
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
            //GUILayout.Button("TODO:");
            /*GUILayout.TextField("Add scene preview previewing the city to generate");*/
            GUI.DrawTexture(new Rect(0, 0, rect.width, rect.height), renderTexture);
            //if (GUI.Button(new Rect(rect.max.x - (rect.size.x * 0.1f), rect.max.y - (rect.size.x * 0.1f), 10.0f, 10.0f), "" ))
        });
    }

    private void MakeCitySettings(Rect rect)
    {
        MakeSection(rect, () =>
        {
            GUIStyle sceneTitleColor = new GUIStyle();
            sceneTitleColor.normal.background = MakeUniformTex((int)rect.width, (int)rect.height, GUI.backgroundColor);
            GUILayout.BeginVertical();
            {
                GUILayout.Label("City loaded");
            }
            GUILayout.EndVertical();
            //Debug.Log("MakeCitySettings! " + (cityGen != null));
            GUILayout.Button("TODO:");
            GUILayout.TextField("Add inspector like city settings screen, where user can edit city properties");
            if (GUILayout.Button("Draw"))
            {

                Debug.Log("CLICK! " + (cityGen == null));
                if (cityGen == null)
                    return;

                Debug.Log("DDDDDDDRRRRRAAAAWWWWW!!!!!!");
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
        GUI.Box(rect, "");//, GUIStyle.none);
    }

    private void MakeSection(Rect rect, Action content)
    {
        GUILayout.BeginArea(rect);
        {
            content();
        }
        GUILayout.EndArea();
    }

    private void MakeButton(string text, Action content)
    {
        if (GUILayout.Button(text))
            content();
    }

    // Static Initiation function
    [MenuItem("Window/City Gen")]
    public static void Init()
    {
        // Get existing open window or if none, make a new one:
        CityGenWindow window = (CityGenWindow) EditorWindow.GetWindow<CityGenWindow>();
        window.titleContent.text = "City Gen";
        window.autoRepaintOnSceneChange = true;
        window.Show();
    }

    public void SetupSceneEnvironment()
    {
        // Get currently active scene.
        this.currentScene = SceneManager.GetActiveScene();

        // Find gameobject named "Cities"
        this.cityWell = GameObject.Find("Cities");
        
        // "Cities" gameobject not found. Create it.
        if(!this.cityWell)
        {
            this.cityWell = new GameObject("Cities");
            this.cityWell.transform.position = Vector3.zero;
        }

        // Try to find CityGen component.
        this.cityGen = this.cityWell.GetComponent<CityGen>();

        // Not found! Create it.
        if (!this.cityGen)
        {
            this.cityGen = this.cityWell.AddComponent<CityGen>();
        }
    }

    public void SetupSceneRenderTexture()
    {
        // Try to find city cam.
        this.cameraObj = GameObject.Find("CityCam");
        // Couldn't find city cam. Make it!
        if (this.cameraObj == null)
            this.cameraObj = new GameObject("CityCam");
        // Hide it from the user in scene.
        //this.cameraObj.hideFlags = HideFlags.HideAndDontSave;
        this.cameraObj.hideFlags = HideFlags.DontSave;
        // Add camera.
        this.camera = this.cameraObj.AddComponent<Camera>();
        // Set render texture.
        this.renderTexture = new RenderTexture((int) this.position.width, (int) this.position.height, (int) RenderTextureFormat.ARGB32);
    }

    private Texture2D MakeUniformTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];

        for (int i = 0; i < pix.Length; i++)
            pix[i] = col;

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }

    /*private Texture2D MakeTintedTex(int width, int height, GUIStyle style, Color colorToAdd)
    {
        Color[] pix = style.normal.background.GetPixels();
        Texture2D background = new Texture2D(width, height);
        background.SetPixels(pix);
        if (background.Resize(width, height))
            Debug.Log("Not able to resize button skin!");

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                background.SetPixel(x, y, background.GetPixel(x, y) + colorToAdd);

        background.Apply();

        return background;
    }*/

    public static T SafeDestroy<T>(T obj) where T : Object
    {
        if (Application.isEditor)
            Object.DestroyImmediate(obj);
        else
            Object.Destroy(obj);

        return null;
    }

    public static T SafeDestroyGameObject<T>(T component) where T : Component
    {
        if (component != null)
            SafeDestroy(component.gameObject);
        return null;
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