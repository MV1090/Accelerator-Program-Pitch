using UnityEngine;

public class Prop : Role
{
    [SerializeField] ParticleSystem _particleSystem;

    CameraController cameraController;
    MeshFilter mf;
            
    public GameObject[] playerProps;
    private GameObject currentModel;

    [SerializeField] Collider propCollider;

    string[] modelAddress =
    {
        "Prefabs/PlayerProps/Balloon_low",
        "Prefabs/PlayerProps/Balls",
        "Prefabs/PlayerProps/Barrell_low",
        "Prefabs/PlayerProps/Bucket_low",
        "Prefabs/PlayerProps/CanFall",
        "Prefabs/PlayerProps/CanStack",
        "Prefabs/PlayerProps/Crate_low",
        "Prefabs/PlayerProps/Firework_low",        
        "Prefabs/PlayerProps/JugglingPin_low",        
        "Prefabs/PlayerProps/MilkJug_low",
        "Prefabs/PlayerProps/Popcorn_low",
        "Prefabs/PlayerProps/SodaCan_low",
        "Prefabs/PlayerProps/Stool_low",
        "Prefabs/PlayerProps/Teddy_low",
        "Prefabs/PlayerProps/TrashBag_low",
        "Prefabs/PlayerProps/TrashBin_low"

    };

    int meshTracker;
    void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetCameraMode(CameraController.CameraMode.FirstPerson);

        propCollider = GetComponent<Collider>();

        propCollider.enabled = false;

        LoadModel();
        ChangeModel(0);
        meshTracker = 0;
    }
    
    private void LoadModel()
    {
        playerProps = new GameObject[modelAddress.Length];

        for (int i = 0; i < modelAddress.Length; i++) 
        {
            playerProps[i] = Resources.Load<GameObject>(modelAddress[i]);
        }
    }   
   

    private void Start()
    {
        mf = GetComponent<MeshFilter>();
        
    }
    public void ChangeModel(int modelIndex)
    {
        if (modelIndex < 0 || modelIndex >= playerProps.Length)
        {
            Debug.LogWarning("Invalid model index");
            return;
        }
                
        if (currentModel != null)
        {
            Destroy(currentModel);
        }
        
        currentModel = Instantiate(playerProps[modelIndex], transform.position, playerProps[modelIndex].transform.rotation);
        currentModel.transform.SetParent(transform);  
    }       

    int CheckMeshIndex()
    {
        if (meshTracker >= playerProps.Length)
            meshTracker = 0;

        if (meshTracker < 0)
            meshTracker = playerProps.Length - 1;

        return meshTracker;
    }

    public override void FirstAction()
    {
        meshTracker ++;
        ChangeModel(CheckMeshIndex());
    }

    public override void SecondAction()
    {
        meshTracker --;
        ChangeModel(CheckMeshIndex());
    }

    public override void ThirdAction()
    {       
       cameraController.ToggleCameraMode();
    }

    public void OnHit()
    {
        ParticleSystem particleSystem = Instantiate(_particleSystem, gameObject.transform.position, gameObject.transform.rotation);
        particleSystem.Play();
                
        Destroy(gameObject);
        Debug.Log("Prop Hit");
    }
}
