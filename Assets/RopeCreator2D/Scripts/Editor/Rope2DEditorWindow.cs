using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

// The "Rope2DEditorWindow" script has to be placed in the folder called "Editor" (or a subfolder) in you project's "Assets" folder.

public class Rope2DEditorWindow : EditorWindow  {


    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject ropeBit;

    public GameObject customEndingA;
    public GameObject customEndingB;
    public float endPrefabScaleA = 1;
    public float endPrefabScaleB = 1;

    public bool fixStart;
    public bool wristStart;
    public bool customEndA;

    public bool fixEnd;
    public bool wristEnd;
    public bool customEndB;

    public bool lineRendererRope;
    public Material lineMaterial;
    public int segmentsNumber = 10;
    public float ropeWidth = 10;

    public float prefabScale = 1;

    public bool addPhysicsMaterial;
    public PhysicsMaterial2D physicsMaterial;
    public bool enableCollision;

    public bool useBreakForce;
    public float breakForce;

    public float mass = 0.1f;
    public float grav = 1;
    public string layer;

    GameObject startPointGo;
    GameObject endPointGo;

    List<GameObject> ropeBitsList = new List<GameObject>();

    // strings/floats for EditorPrefs
    string namePointA = "";
    string namePointB = "";

    string nameRopePrefab = "";
    string nameLineMaterial = "";
    string namePhysicsMaterial = "";
    string nameCustomEndA = "";
    string nameCustomEndB = "";

    float nameMass;
    float nameGrav;
    string nameLayer = "";


    // Add MenuItem named "Rope Creator 2D" to the Window menu
    [MenuItem("Window/Rope Creator 2D")]
    static void Init()
    {
        // Get existing open window or if none, make a new one
        Rope2DEditorWindow window = (Rope2DEditorWindow)EditorWindow.GetWindow(typeof(Rope2DEditorWindow));
        window.Show();
    }

    void OnGUI()
    {

        GetEditorPrefs();

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Point A", EditorStyles.boldLabel, GUILayout.Width(145));
        startPoint = (GameObject)EditorGUILayout.ObjectField(startPointGo, typeof(GameObject), true);
        if (GUILayout.Button("Clear"))
        {
            startPoint = null;
            EditorPrefs.DeleteKey("NamePointA");
        }
        EditorGUILayout.EndHorizontal();

        wristStart = EditorGUILayout.Toggle("End fixing: wrist", wristStart);
        fixStart = EditorGUILayout.Toggle("End fixing: fixed", fixStart);
        customEndA = EditorGUILayout.Toggle("Custom ending", customEndA);

        if(customEndA)
        {
            customEndingA = (GameObject)EditorGUILayout.ObjectField(customEndingA, typeof(GameObject), true);
            endPrefabScaleA = EditorGUILayout.FloatField("Prefab Scale: ", endPrefabScaleA);
        }
            
        EditorGUILayout.Separator();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Point B", EditorStyles.boldLabel, GUILayout.Width(145));
        endPoint = (GameObject)EditorGUILayout.ObjectField(endPointGo, typeof(GameObject), true);
        if (GUILayout.Button("Clear"))
        {
            endPoint = null;
            EditorPrefs.DeleteKey("NamePointB");
        }
        EditorGUILayout.EndHorizontal();

        wristEnd = EditorGUILayout.Toggle("End fixing: wrist", wristEnd);
        fixEnd = EditorGUILayout.Toggle("End fixing: fixed", fixEnd);
        customEndB = EditorGUILayout.Toggle("Custom ending", customEndB);

        if (customEndB)
        { 
            customEndingB = (GameObject)EditorGUILayout.ObjectField(customEndingB, typeof(GameObject), true);
            endPrefabScaleB = EditorGUILayout.FloatField("Prefab Scale: ", endPrefabScaleB);
        }

        EditorGUILayout.Separator();

        lineRendererRope = EditorGUILayout.Toggle("Line Renderer Rope", lineRendererRope);

        EditorGUILayout.Separator();

        if (!lineRendererRope)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Rope Prefab", EditorStyles.boldLabel, GUILayout.Width(145));
            ropeBit = (GameObject)EditorGUILayout.ObjectField(ropeBit, typeof(GameObject), true);
            EditorGUILayout.EndHorizontal();

            prefabScale = EditorGUILayout.FloatField("Prefab Scale: ", prefabScale);
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Line Material", GUILayout.Width(145));
            lineMaterial = (Material)EditorGUILayout.ObjectField(lineMaterial, typeof(Material), true);
            EditorGUILayout.EndHorizontal();

            segmentsNumber = EditorGUILayout.IntField("Segments: ", segmentsNumber);
            ropeWidth = EditorGUILayout.FloatField("Rope width: ", ropeWidth);
        }

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        // Hingejonint2D - enable collision On/Off
        enableCollision = EditorGUILayout.Toggle("Enable collision", enableCollision);

        EditorGUILayout.Separator();

        addPhysicsMaterial = EditorGUILayout.Toggle("Add Physics2D Material", addPhysicsMaterial);

        if(addPhysicsMaterial)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Physics2D Material", GUILayout.Width(145));
            physicsMaterial = (PhysicsMaterial2D)EditorGUILayout.ObjectField(physicsMaterial, typeof(PhysicsMaterial2D), true);
            EditorGUILayout.EndHorizontal();
        }

        useBreakForce = EditorGUILayout.Toggle("Use breakforce", useBreakForce);

        if (useBreakForce)
        {
            breakForce = EditorGUILayout.FloatField("Breakforce value: ", breakForce);
        }

        mass = EditorGUILayout.FloatField("Mass: ", mass);
        grav = EditorGUILayout.FloatField("Gravity Scale: ", grav);
        layer = EditorGUILayout.TextField("Layer: ", layer);

        GUILayout.Space(20);

        // Create button
        if (GUILayout.Button("Create!"))
        {
            ropeBitsList = new List<GameObject>();

            if (!lineRendererRope)
            {

                if(ropeBit != null && endPoint != null && startPoint != null && prefabScale != 0)
                    GenerateRope();
                else if (ropeBit == null)
                    Debug.LogError("Rope Creator: Attach the rope prefab!");
                else if (startPoint == null)
                    Debug.LogError("Rope Creator: Attach the point A gameobject!");
                else if (endPoint == null)
                    Debug.LogError("Rope Creator: Attach the point B gameobject!");
                else if(prefabScale == 0)
                    Debug.LogError("Rope Creator: RopeBit prefab scale is 0!");

                if (startPoint != null)
                    EditorPrefs.SetString("NamePointA", startPoint.name);
                if (endPoint != null)
                    EditorPrefs.SetString("NamePointB", endPoint.name);

                Debug.Log("Rope Created!");
            }
            else
            {
                if (lineMaterial != null && endPoint != null && startPoint != null && segmentsNumber > 1)
                    GenerateLineRendererRope();
                else if (lineMaterial == null)
                    Debug.LogError("Rope Creator: Attach the rope material!");
                else if (startPoint == null)
                    Debug.LogError("Rope Creator: Attach the point A gameobject!");
                else if (endPoint == null)
                    Debug.LogError("Rope Creator: Attach the point B gameobject!");
                else if (segmentsNumber <= 1)
                    Debug.LogError("Rope Creator: The segments number is to low!");

                if (startPoint != null)
                    EditorPrefs.SetString("NamePointA", startPoint.name);
                if (endPoint != null)
                    EditorPrefs.SetString("NamePointB", endPoint.name);

                Debug.Log("LineRenderer Rope Created!");
            }

        }

        // If you drag the prefab into the labelfield of the editor window, it saves the name of the prefab 
        if (customEndingA != null)
            EditorPrefs.SetString("NameCustomEndA", customEndingA.name);

        if (customEndingB != null)
            EditorPrefs.SetString("NameCustomEndB", customEndingB.name);

        if (ropeBit != null)
            EditorPrefs.SetString("NameRopePrefab", ropeBit.name);

        if(lineMaterial != null)
            EditorPrefs.SetString("NameLineMaterial", lineMaterial.name);

        if (physicsMaterial != null)
            EditorPrefs.SetString("NamePhysicsMaterial", physicsMaterial.name);

        EditorPrefs.SetFloat("NameMass", mass);
        EditorPrefs.SetFloat("NameGrav", grav);
        EditorPrefs.SetString("NameLayer", layer);

    }


    public void GenerateRope()
    {

        customEndingA = Resources.Load("Prefabs/" + nameCustomEndA) as GameObject;
        customEndingB = Resources.Load("Prefabs/" + nameCustomEndB) as GameObject;

        // The distance between the rope's first and last point
        float distance = Vector2.Distance(new Vector2(endPoint.transform.position.x, endPoint.transform.position.y), new Vector2(startPoint.transform.position.x, startPoint.transform.position.y));

        if (distance == 0)
            Debug.LogError("Rope Creator: The distance between the rope's endpoints = 0!");


        // The length of the ropeBit prefab (CircleCollider2D diameter * scale)
        float bitlength = prefabScale * 2 * ropeBit.GetComponent<CircleCollider2D>().radius;
        // The number of the sections we need for the rope
        int sectionsNumber = Mathf.CeilToInt(distance / bitlength);

        // Triangle data
        float triangleX = endPoint.transform.position.x - startPoint.transform.position.x;
        float triangleY = endPoint.transform.position.y - startPoint.transform.position.y;
        float triangleDiagonal = Mathf.Sqrt(Mathf.Pow(triangleX, 2) + Mathf.Pow(triangleY, 2));

        // The angle of the triangle
        float angle = Mathf.Atan2(triangleY, triangleX) * Mathf.Rad2Deg;

        // Data for the ropeBit prefab's HingeJoint2D component
        float localX = Mathf.Abs(ropeBit.GetComponent<CircleCollider2D>().radius * triangleX / triangleDiagonal);
        float localY = Mathf.Abs(ropeBit.GetComponent<CircleCollider2D>().radius * triangleY / triangleDiagonal);

        // Signs for the ropeBit prefab's HingeJoint2D's ConnectedAnchor / Anchor component
        int signX, signY;

        if (triangleX >= 0) {signX = 1;}
        else {signX = -1;}
        if (triangleY >= 0) {signY = 1;}
        else {signY = -1;}


        // Loading the ropeBit prefab from the "Resources/Prefabs/" folder (the ropeBit prefabs must be placed in the "Resources/Prefabs/" folder in the project's "Assets" folder!)
        ropeBit = Resources.Load("Prefabs/" + nameRopePrefab) as GameObject;
        // ropeBit = AssetDatabase.LoadAssetAtPath<GameObject>(string.Format("Assets/ExampleFolder/" + nameRopePrefab + ".prefab"));

        // If you want to use CUSTOM FOLDER for the prefabs:
        // Comment out: ropeBit = Resources.Load("Prefabs/" + nameRopePrefab) as GameObject;
        // Use this with your folder path: ropeBit = AssetDatabase.LoadAssetAtPath<GameObject>(string.Format("Assets/ExampleFolder/" + nameRopePrefab + ".prefab"));

        if (ropeBit == null)
            Debug.LogError("The ropeBit prefab is not loaded! Place the ropeBit prefab in the Resources/Prefabs/ folder");

        if (ropeBit != null)
        {

            // Create a folder for the ropeBit prefabs
            GameObject ropeFolder = new GameObject("Rope");
            // Set the folder's position to (0,0,0) - optional
            ropeFolder.transform.position = new Vector3(0, 0, 0);

            // Unity - Edit - Undo function
            Undo.RegisterCreatedObjectUndo(ropeFolder, "Create " + ropeFolder.name);

            // Creating the rope
            for (int i = 0; i < sectionsNumber; i++)
            {

                Vector3[] pPoints = new Vector3[sectionsNumber];

                // The positions of the ropeBit prefabs
                pPoints[i] = new Vector3(((distance - bitlength * i) * startPoint.transform.position.x + bitlength * i * endPoint.transform.position.x) / distance,
                                         ((distance - bitlength * i) * startPoint.transform.position.y + bitlength * i * endPoint.transform.position.y) / distance,
                                           0);

                // Instantiate the ropeBit prefabs
                GameObject ropeBitPref = (GameObject)PrefabUtility.InstantiatePrefab(ropeBit);

                // Set every ropeBit prefab's position to the calculated position
                ropeBitPref.transform.position = pPoints[i];

                // Give an index to the ropeBit prefabs's name - optional
                string name = string.Format(ropeBitPref.name + "_{0}", i);
                ropeBitPref.name = name;

                // Scale the prefab
                ropeBitPref.transform.localScale = new Vector3(prefabScale, prefabScale, prefabScale);

                // Add Gizmos to see the CircleCollider2D component
                if(ropeBitPref.GetComponent<CircleCollider2D>() == null)
                {
                    Debug.LogError("Rope Creator: No CircleCollider2D added to the ropeBit prefab!");
                }
                else
                {
                    ropeBitPref.AddComponent<Rope2DCircleGizmo>();
                    ropeBitPref.GetComponent<Rope2DCircleGizmo>().gizmoRadius = ropeBitPref.GetComponent<CircleCollider2D>().radius;

                    if (addPhysicsMaterial && physicsMaterial != null)
                    {
                        physicsMaterial = Resources.Load("Materials/" + namePhysicsMaterial) as PhysicsMaterial2D;
                        ropeBitPref.GetComponent<CircleCollider2D>().sharedMaterial = physicsMaterial;
                    }
                    else if (addPhysicsMaterial && physicsMaterial == null)
                        Debug.LogError("Rope2D: The LphysicsMaterial is not loaded!Place the Line Material in the Resources/Materials/ folder");

                }

                // Add the prefabs to a list
                ropeBitsList.Add(ropeBitPref);

                // Put the rope prefabs to the created folder
                ropeBitsList[i].transform.parent = ropeFolder.transform;

                // The ropeBit prefabs must contain a child gameobject with a Sprite Renderer component (rope graphics)! - the child gameobject (graphics) will be rotated, not the actual ropeBit prefab
                if (ropeBitsList[i].transform.childCount == 0)
                    Debug.LogError("No child object in the rope prefab. You need to add a child object with a Sprite Renderer component to the rope prefab!");
                if (ropeBitsList[i].transform.GetComponentInChildren<SpriteRenderer>() == null)
                    Debug.LogError("No Sprite Renderer component on the ropeBit prefab's child object. You need to add a Sprite Renderer component to the rope prefab's child gameobject!");

                // Get the child ("Graphics") form the ropeBit prefab, and rotate it to the correct angle
                if (ropeBitsList[i].transform.childCount != 0 && ropeBitsList[i].transform.GetComponentInChildren<SpriteRenderer>() != null)
                    ropeBitsList[i].transform.GetComponentInChildren<SpriteRenderer>().transform.GetComponent<Transform>().Rotate(0, 0, angle);

                // Add Joints to the ropeBit prefabs
                if (ropeBitsList[i].GetComponent<DistanceJoint2D>() == null)
                    ropeBitsList[i].AddComponent<DistanceJoint2D>();

                if (ropeBitsList[i].GetComponentInChildren<HingeJoint2D>() == null)
                    ropeBitsList[i].AddComponent<HingeJoint2D>();

                // Set the mass and gravity scale for the ropeBit prefab
                if (ropeBitsList[i].GetComponent<Rigidbody2D>() != null)
                {
                    ropeBitsList[i].GetComponent<Rigidbody2D>().mass = nameMass;
                    ropeBitsList[i].GetComponent<Rigidbody2D>().gravityScale = nameGrav;
                }
                else
                {
                    ropeBitsList[i].AddComponent<Rigidbody2D>();
                    ropeBitsList[i].GetComponent<Rigidbody2D>().mass = nameMass;
                    ropeBitsList[i].GetComponent<Rigidbody2D>().gravityScale = nameGrav;

                }

                // Set the layer for the rope prefabs - optional
                int layerInt = LayerMask.NameToLayer(nameLayer);

                if (layerInt == -1)
                    Debug.LogWarning("Rope Creator: There is no such layer! - use an existing Layer");
                else
                    ropeBitsList[i].layer = layerInt;

                // Configure the Joints
                if (i == 0)
                {
                    if (ropeBitsList[i].GetComponent<HingeJoint2D>() != null)
                        ropeBitsList[i].GetComponent<HingeJoint2D>().enabled = false;
                    if (ropeBitsList[i].GetComponent<DistanceJoint2D>() != null)
                        ropeBitsList[i].GetComponent<DistanceJoint2D>().enabled = false;
                }


                if (i >= 1)
                {
                    // DistanceJoint2D
                    ropeBitsList[i].GetComponent<DistanceJoint2D>().enabled = true;
                    ropeBitsList[i].GetComponent<DistanceJoint2D>().connectedBody = ropeBitsList[i - 1].GetComponent<Rigidbody2D>();
                    ropeBitsList[i].GetComponent<DistanceJoint2D>().connectedAnchor = new Vector2(0, 0);
                    ropeBitsList[i].GetComponent<DistanceJoint2D>().anchor = new Vector2(0, 0);
                    ropeBitsList[i].GetComponent<DistanceJoint2D>().distance = ropeBitsList[i - 1].GetComponent<CircleCollider2D>().radius * 2.04f * prefabScale;
                    ropeBitsList[i].GetComponent<DistanceJoint2D>().maxDistanceOnly = true;

                    if(useBreakForce)
                        ropeBitsList[i].GetComponent<DistanceJoint2D>().breakForce = breakForce;


                    // HingeJoint2D
                    ropeBitsList[i].GetComponent<HingeJoint2D>().enabled = true;
                    ropeBitsList[i].GetComponent<HingeJoint2D>().connectedBody = ropeBitsList[i - 1].GetComponent<Rigidbody2D>();
                    ropeBitsList[i].GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(localX * signX, localY * signY);
                    ropeBitsList[i].GetComponent<HingeJoint2D>().anchor = new Vector2(localX * signX * -1, localY * signY * -1);

                    if (useBreakForce)
                        ropeBitsList[i].GetComponent<HingeJoint2D>().breakForce = breakForce;

                    if (enableCollision == true)
                    {
                        ropeBitsList[i].GetComponent<DistanceJoint2D>().enableCollision = true;
                        ropeBitsList[i].GetComponent<HingeJoint2D>().enableCollision = true;
                    }
                    else
                    {
                        ropeBitsList[i].GetComponent<DistanceJoint2D>().enableCollision = false;
                        ropeBitsList[i].GetComponent<HingeJoint2D>().enableCollision = false;
                    }
                }

                //  Unity - Edit - Undo function
                Undo.RegisterCreatedObjectUndo(ropeBitPref, "Created rope");
            }

            // Custom endings
            if (customEndA)
            {
                GameObject custEndA = (GameObject)PrefabUtility.InstantiatePrefab(customEndingA);
                custEndA.transform.position = ropeBitsList[0].transform.position;
                custEndA.transform.parent = ropeBitsList[0].transform;
                custEndA.transform.localScale = new Vector3(endPrefabScaleA, endPrefabScaleA, endPrefabScaleA);
            }

            if (customEndB)
            {
                GameObject custEndB = (GameObject)PrefabUtility.InstantiatePrefab(customEndingB);
                custEndB.transform.position = ropeBitsList[sectionsNumber - 1].transform.position;
                custEndB.transform.parent = ropeBitsList[sectionsNumber - 1].transform;
                custEndB.transform.localScale = new Vector3(endPrefabScaleB, endPrefabScaleB, endPrefabScaleB);
            }

        }


        // Rope's first point fixing
        if (fixStart == true)
            ropeBitsList[0].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        if (wristStart == true)
            ropeBitsList[0].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;

        // Rope's last point fixing
        if (fixEnd == true)
            ropeBitsList[sectionsNumber - 1].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        if (wristEnd == true)
            ropeBitsList[sectionsNumber - 1].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }


    void GenerateLineRendererRope()
    {

        customEndingA = Resources.Load("Prefabs/" + nameCustomEndA) as GameObject;
        customEndingB = Resources.Load("Prefabs/" + nameCustomEndB) as GameObject;

        // The distance between the rope's first and last point
        float distance = Vector2.Distance(new Vector2(endPoint.transform.position.x, endPoint.transform.position.y), new Vector2(startPoint.transform.position.x, startPoint.transform.position.y));

        if (distance == 0)
            Debug.LogError("Rope Creator: The distance between the rope's endpoints = 0!");

        // The length of the ropeBit prefab (CircleCollider2D diameter * scale)
        float bitlength = distance / segmentsNumber;

        // Triangle data
        float triangleX = endPoint.transform.position.x - startPoint.transform.position.x;
        float triangleY = endPoint.transform.position.y - startPoint.transform.position.y;
        float triangleDiagonal = Mathf.Sqrt(Mathf.Pow(triangleX, 2) + Mathf.Pow(triangleY, 2));

        // The angle of the triangle
        float angle = Mathf.Atan2(triangleY, triangleX) * Mathf.Rad2Deg;

        // Loading the Line Renderer material from the "Resources/Materials/" folder (the Line Renderer material must be placed in the "Resources/Materials/" folder in the project's "Assets" folder!)
        lineMaterial = Resources.Load("Materials/" + nameLineMaterial) as Material;
        // lineMaterial = AssetDatabase.LoadAssetAtPath<Material>(string.Format("Assets/ExampleFolder/" + nameLineMaterial + ".mat"));

        // If you want to use CUSTOM FOLDER for the lineMaterial:
        // Comment out: lineMaterial = Resources.Load("Materials/" + nameLineMaterial) as Material;
        // Use this with your folder path: lineMaterial = AssetDatabase.LoadAssetAtPath<Material>(string.Format("Assets/ExampleFolder/" + nameLineMaterial + ".mat"));

        if (lineMaterial == null)
            Debug.LogError("The Line Material is not loaded! Place the Line Material in the Resources/Materials/ folder");

        if (lineMaterial != null)
        {

            // Create a folder for the ropeBit prefabs
            GameObject ropeFolder = new GameObject("Rope");
            // Set the folder's position to (0,0,0) - optional
            ropeFolder.transform.position = new Vector3(0, 0, 0);

            // Unity - Edit - Undo function
            Undo.RegisterCreatedObjectUndo(ropeFolder, "Create " + ropeFolder.name);

            // Creating the rope
            for (int i = 0; i < segmentsNumber; i++)
            {

                Vector3[] pPoints = new Vector3[segmentsNumber];

                // The positions of the ropeBit prefabs
                pPoints[i] = new Vector3(((distance - bitlength * i) * startPoint.transform.position.x + bitlength * i * endPoint.transform.position.x) / distance,
                                         ((distance - bitlength * i) * startPoint.transform.position.y + bitlength * i * endPoint.transform.position.y) / distance,
                                           0);


                // Create the ropeBit prefabs
                GameObject ropeBit = new GameObject();

                // Give an index to the ropeBit prefabs's name - optional
                string name = string.Format("RopeBit" + "_{0}", i);
                ropeBit.name = name;

                // Set every ropeBit's position to the calculated position
                ropeBit.transform.position = pPoints[i];

                // Add the ropeBit prefabs the the list
                ropeBitsList.Add(ropeBit);

                // Put the ropeBit prefabs to the created folder
                ropeBitsList[i].transform.parent = ropeFolder.transform;

                // Add CircleCollider2D component to the ropeBit prefabs
                ropeBitsList[i].AddComponent<CircleCollider2D>();
                ropeBitsList[i].GetComponent<CircleCollider2D>().radius = bitlength / 2;
                ropeBitsList[i].GetComponent<CircleCollider2D>().offset = new Vector2(0, 0);

                if(addPhysicsMaterial && physicsMaterial != null)
                {
                    physicsMaterial = Resources.Load("Materials/" + namePhysicsMaterial) as PhysicsMaterial2D;
                    ropeBitsList[i].GetComponent<CircleCollider2D>().sharedMaterial = physicsMaterial;
                }
                else if (addPhysicsMaterial && physicsMaterial == null)
                    Debug.LogError("Rope2D: The LphysicsMaterial is not loaded!Place the Line Material in the Resources/Materials/ folder");

                // Add Joints to the ropeBit prefabs
                ropeBitsList[i].AddComponent<DistanceJoint2D>();
                ropeBitsList[i].AddComponent<HingeJoint2D>();

                //// Data for the ropeBit prefabs's HingeJoint2D component
                float localX = Mathf.Abs(ropeBitsList[i].GetComponent<CircleCollider2D>().radius * triangleX / triangleDiagonal);
                float localY = Mathf.Abs(ropeBitsList[i].GetComponent<CircleCollider2D>().radius * triangleY / triangleDiagonal);

                // Signs for the ropeBit prefabs's HingeJoint2D's ConnectedAnchor / Anchor component
                int signX, signY;

                if (triangleX >= 0) { signX = 1; }
                else { signX = -1; }
                if (triangleY >= 0) { signY = 1; }
                else { signY = -1; }

                // Add a Rigidbody2D component and set the mass and gravity scale for the ropeBits
                if (ropeBitsList[i].GetComponent<Rigidbody2D>() == null)
                    ropeBitsList[i].AddComponent<Rigidbody2D>();

                ropeBitsList[i].GetComponent<Rigidbody2D>().mass = nameMass;
                ropeBitsList[i].GetComponent<Rigidbody2D>().gravityScale = nameGrav;

                // Add gizmo to the ropeBit prefabs to see the circlecolliders
                ropeBitsList[i].AddComponent<Rope2DCircleGizmo>();
                ropeBitsList[i].GetComponent<Rope2DCircleGizmo>().gizmoRadius = ropeBitsList[i].GetComponent<CircleCollider2D>().radius;

                // Add Line Renderer component
                ropeBit.AddComponent<LineRenderer>();
                ropeBit.GetComponent<LineRenderer>().material = lineMaterial;
                ropeBit.GetComponent<LineRenderer>().useWorldSpace = false;
                ropeBit.GetComponent<LineRenderer>().SetWidth(ropeWidth, ropeWidth);
                ropeBit.GetComponent<LineRenderer>().useLightProbes = false;
                ropeBit.GetComponent<LineRenderer>().receiveShadows = false;
                ropeBit.GetComponent<LineRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

                // Add the Rope2DLineRendSetPoints script
                ropeBit.AddComponent<Rope2DLineRendSetPoints>();

                // Set the layer for the ropeBit prefabs - optional
                int layerInt = LayerMask.NameToLayer(nameLayer);

                if (layerInt == -1)
                    Debug.LogWarning("Rope Creator: There is no such layer!");
                else
                    ropeBitsList[i].layer = layerInt;

                // Configure the Joints
                if (i == 0)
                {
                    ropeBitsList[i].GetComponent<HingeJoint2D>().enabled = false;
                    ropeBitsList[i].GetComponent<DistanceJoint2D>().enabled = false;                    
                }

                if (i >= 1)
                {
                    // DistanceJoint2D
                    ropeBitsList[i].GetComponent<DistanceJoint2D>().enabled = true;
                    ropeBitsList[i].GetComponent<DistanceJoint2D>().connectedBody = ropeBitsList[i - 1].GetComponent<Rigidbody2D>();
                    ropeBitsList[i].GetComponent<DistanceJoint2D>().connectedAnchor = new Vector2(0, 0);
                    ropeBitsList[i].GetComponent<DistanceJoint2D>().anchor = new Vector2(0, 0);
                    ropeBitsList[i].GetComponent<DistanceJoint2D>().distance = ropeBitsList[i - 1].GetComponent<CircleCollider2D>().radius * 2.04f;
                    ropeBitsList[i].GetComponent<DistanceJoint2D>().maxDistanceOnly = true;

                    if (useBreakForce)
                        ropeBitsList[i].GetComponent<DistanceJoint2D>().breakForce = breakForce;

                    // HingeJoint2D
                    ropeBitsList[i].GetComponent<HingeJoint2D>().enabled = true;
                    ropeBitsList[i].GetComponent<HingeJoint2D>().connectedBody = ropeBitsList[i - 1].GetComponent<Rigidbody2D>();
                    ropeBitsList[i].GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(localX * signX, localY * signY);
                    ropeBitsList[i].GetComponent<HingeJoint2D>().anchor = new Vector2(localX * signX * -1, localY * signY * -1);

                    if (useBreakForce)
                        ropeBitsList[i].GetComponent<HingeJoint2D>().breakForce = breakForce;

                    if (enableCollision == true)
                    {
                        ropeBitsList[i].GetComponent<DistanceJoint2D>().enableCollision = true;
                        ropeBitsList[i].GetComponent<HingeJoint2D>().enableCollision = true;
                    }
                    else
                    {
                        ropeBitsList[i].GetComponent<DistanceJoint2D>().enableCollision = false;
                        ropeBitsList[i].GetComponent<HingeJoint2D>().enableCollision = false;
                    }
                }

                //  Unity - Edit - Undo function
                Undo.RegisterCreatedObjectUndo(ropeBit, "Created rope");

            }


            // Custom endings
            if (customEndA)
            {
                GameObject custEndA = (GameObject)PrefabUtility.InstantiatePrefab(customEndingA);
                custEndA.transform.position = ropeBitsList[0].transform.position;
                custEndA.transform.parent = ropeBitsList[0].transform;
                custEndA.transform.localScale = new Vector3(endPrefabScaleA, endPrefabScaleA, endPrefabScaleA);
            }

            if (customEndB)
            {
                GameObject custEndB = (GameObject)PrefabUtility.InstantiatePrefab(customEndingB);
                custEndB.transform.position = ropeBitsList[segmentsNumber - 1].transform.position;
                custEndB.transform.parent = ropeBitsList[segmentsNumber - 1].transform;
                custEndB.transform.localScale = new Vector3(endPrefabScaleB, endPrefabScaleB, endPrefabScaleB);
            }


            // Rope's first point fixing
            if (fixStart == true)
                ropeBitsList[0].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            if (wristStart == true)
                ropeBitsList[0].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;

            // Rope's last point fixing
            if (fixEnd == true)
                ropeBitsList[segmentsNumber - 1].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            if (wristEnd == true)
                ropeBitsList[segmentsNumber - 1].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;

        }
    }

    void GetEditorPrefs()
    {
        namePointA = EditorPrefs.GetString("NamePointA");
        namePointB = EditorPrefs.GetString("NamePointB");

        nameCustomEndA = EditorPrefs.GetString("NameCustomEndA");
        nameCustomEndB = EditorPrefs.GetString("NameCustomEndB");

        nameRopePrefab = EditorPrefs.GetString("NameRopePrefab");

        nameLineMaterial = EditorPrefs.GetString("NameLineMaterial");
        namePhysicsMaterial = EditorPrefs.GetString("NamePhysicsMaterial");

        nameMass = EditorPrefs.GetFloat("NameMass");
        nameGrav = EditorPrefs.GetFloat("NameGrav");
        nameLayer = EditorPrefs.GetString("NameLayer");

        if (!string.IsNullOrEmpty(namePointA))
            startPointGo = GameObject.Find(namePointA);
        else
            startPointGo = startPoint;

        if (!string.IsNullOrEmpty(namePointB))
            endPointGo = GameObject.Find(namePointB);
        else
            endPointGo = endPoint;
    }


    void OnInspectorUpdate()
    {
        // Call Repaint on OnInspectorUpdate as it repaints the windows less times as if it was OnGUI/Update
        Repaint();
    }


    // Clearing the EditorPrefs
    //void OnDestroy()
    //{
    //    EditorPrefs.DeleteKey("NamePointA");
    //    EditorPrefs.DeleteKey("NamePointB");
    //    EditorPrefs.DeleteKey("NameCustomEndA");
    //    EditorPrefs.DeleteKey("NameCustomEndB");
    //    EditorPrefs.DeleteKey("NameRopePrefab");
    //    EditorPrefs.DeleteKey("NameLineMaterial");
    //    EditorPrefs.DeleteKey("NamePhysicsMaterial");
    //    EditorPrefs.DeleteKey("NameMass");
    //    EditorPrefs.DeleteKey("NameGrav");
    //    EditorPrefs.DeleteKey("NameLayer");
    //}

}

