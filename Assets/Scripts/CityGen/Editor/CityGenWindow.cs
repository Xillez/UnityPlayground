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

    GameObject cityWell = null;
    CityGen cityGen;// = new CityGen();
    private bool saved { get; set; } = true;

    Scene currentScene;
    private GameObject cameraObj;
    private Camera camera;
    private RenderTexture renderTexture;
    private Vector2 scrollPos;
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
        if (this.cityGen.cities.Count < 0) this.cityGen.cityLoaded = -1;

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

        if (this.cityGen.ValidateCityGenState(false))
            this.FocusCameraOnCity();
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

        this.MakeSection(rect, () =>
        {
            this.MakeVertical(() =>
            {
                EditorGUILayout.BeginVertical(sceneTitleColor);
                {
                    GUILayout.Label("Active scene: " + this.currentScene.name);
                }
                EditorGUILayout.EndVertical();
                GUILayout.BeginScrollView(this.scrollPos);
                {
                    // Make buttons for cities.
                    if (this.cityGen != null && this.cityGen.cities != null && this.cityGen.cities.Count > 0)
                        for (int i = 0; i < this.cityGen.cities.Count; i++)
                        {
                            GUILayout.BeginHorizontal();
                            {
                                // Save old background color.
                                Color oldColor = GUI.backgroundColor;
                                // Set button color to green if loaded, old color if not.
                                GUI.backgroundColor = ((this.cityGen.cityLoaded != i) ? oldColor : Color.green);
                                if (GUILayout.Button(this.cityGen.cities[i].name) && this.cityGen.cities[i] != null)    // Load city if it exists.
                                    this.cityGen.LoadCity(i);
                                // Reset color
                                GUI.backgroundColor = oldColor;

                                // Add delete button.
                                if (GUILayout.Button("Delete", GUILayout.ExpandWidth(false)) && this.cityGen.cities[i] != null)
                                    this.cityGen.DeleteCity(i);
                            }
                            GUILayout.EndHorizontal();
                        }

                    this.MakeSplitBar();

                    this.newCityName = EditorGUILayout.TextField("City name: ", this.newCityName);
                    if (GUILayout.Button("Add city"))
                    {
                        this.cityGen.NewCity(this.newCityName);
                        this.newCityName = "";
                    }
                }
                GUILayout.EndScrollView();
            });
        });
    }

    private void MakeCityScenePreview(Rect rect)
    {
        this.MakeSection(rect, () =>
        {
            //GUILayout.Button("TODO:");
            /*GUILayout.TextField("Add scene preview previewing the city to generate");*/
            GUI.DrawTexture(new Rect(0, 0, rect.width, rect.height), renderTexture);
            //if (GUI.Button(new Rect(rect.max.x - (rect.size.x * 0.1f), rect.max.y - (rect.size.x * 0.1f), 10.0f, 10.0f), ""))
        });
    }

    private void MakeCitySettings(Rect rect)
    {
        if (this.cityGen == null)
            return;

        this.MakeSection(rect, () =>
        {
            GUIStyle sceneTitleColor = new GUIStyle();
            sceneTitleColor.normal.background = MakeUniformTex((int)rect.width, (int)rect.height, GUI.backgroundColor);
            this.MakeVertical(() =>
            {
                GUILayout.Label("City loaded: " + ((this.cityGen != null && this.cityGen.ValidateCityGenState(false)) ? this.cityGen.cities[this.cityGen.cityLoaded].name : ""));
            });
            this.MakeSplitBar();
            if (this.cityGen.cities.Count > 0 && this.cityGen.cityLoaded >= 0)
                this.MakeVertical(() =>
                {
                    
                    City loadedCity = new City(this.cityGen.cities[this.cityGen.cityLoaded]);

                    loadedCity.name = EditorGUILayout.TextField("City name: ", loadedCity.name);
                    loadedCity.genPoint = EditorGUILayout.Vector3Field("Gen Point: ", loadedCity.genPoint);
                    loadedCity.cityRadius = EditorGUILayout.FloatField("City radius: ", loadedCity.cityRadius);
                    loadedCity.nrBranches = EditorGUILayout.IntField("Nr. Base branches: ", loadedCity.nrBranches);
                    loadedCity.nrIterations = EditorGUILayout.IntField("Nr. iterations: ", loadedCity.nrIterations);
                    //GUILayout.Label("NOTE! Min block width will be overruled if assets are physicaly larger!");
                    //loadedCity.minBlockWidth = EditorGUILayout.FloatField("Min Block Width: ", loadedCity.minBlockWidth);

                    /*
                        TODO:
                        - Add viewing for: public RoadNetwork network = new RoadNetwork();
                    */

                    this.cityGen.cities[this.cityGen.cityLoaded] = loadedCity;
                    loadedCity = null;

                    this.MakeButton("Generate!", () => {
                        this.cityGen.GenerateCityBase();
                    });
                    

                    //Debug.Log("MakeCitySettings! " + (cityGen != null));
                    /*GUILayout.Button("TODO:");
                    GUILayout.TextField("Add inspector like city settings screen, where user can edit city properties");
                    if (GUILayout.Button("Draw"))
                    {

                        Debug.Log("CLICK! " + (cityGen == null));
                        if (cityGen == null)
                            return;

                        Debug.Log("DDDDDDDRRRRRAAAAWWWWW!!!!!!");
                        cityGen.DrawCity();
                    }*/
                });
        });
    }

    private void MakeCityAssetsManager(Rect rect)
    {
        this.MakeSection(rect, () =>
        {
            GUILayout.Button("TODO:");
            GUILayout.TextField("Add asset managment (like the tree addition on terrain tool)");
        });
    }

    private void MakeSplitter(Rect rect)
    {
        GUI.Box(rect, "");//, GUIStyle.none);
    }


    private void MakeSplitBar()
    {
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
    }

    private void MakeSection(Rect rect, Action content)
    {
        GUILayout.BeginArea(rect);
        {
            content();
        }
        GUILayout.EndArea();
    }

    private void MakeVertical(Action content)
    {
        GUILayout.BeginVertical();
        {
            content();
        }
        GUILayout.EndVertical();
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

    public void FocusCameraOnCity()
    {
        if (this.cameraObj == null || this.camera == null)
            return;

        this.cameraObj.transform.position = this.cityGen.cities[this.cityGen.cityLoaded].genPoint + (new Vector3(0.0f, 1.0f, 1.0f) * this.cityGen.cities[this.cityGen.cityLoaded].cityRadius * 1.5f);
        this.cameraObj.transform.LookAt(this.cityGen.cities[this.cityGen.cityLoaded].genPoint);
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