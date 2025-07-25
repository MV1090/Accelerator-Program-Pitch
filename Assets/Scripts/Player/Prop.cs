using UnityEngine;

public class Prop : Role
{     
    CameraController cameraController;
    MeshFilter mf;

    public Mesh[] props;
    int meshTracker;
    void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetCameraMode(CameraController.CameraMode.FirstPerson);
        props = new Mesh[4];
        props[0] = GetPrimitiveMesh(PrimitiveType.Cylinder);
        props[1] = GetPrimitiveMesh(PrimitiveType.Cube);
        props[2] = GetPrimitiveMesh(PrimitiveType.Capsule);
        props[3] = GetPrimitiveMesh(PrimitiveType.Sphere);

        meshTracker = 0;
    }
    Mesh GetPrimitiveMesh(PrimitiveType type)
    {
        GameObject temp = GameObject.CreatePrimitive(type);
        Mesh mesh = temp.GetComponent<MeshFilter>().sharedMesh;
        Destroy(temp);
        return mesh;
    }

    void ApplyMeshAndCollider(Mesh mesh)
    {        
        MeshFilter targetMeshFilter = GetComponent<MeshFilter>();
        
        if (targetMeshFilter != null)
        {
            targetMeshFilter.sharedMesh = mesh;
        }
       
        Collider oldCollider = GetComponent<Collider>();
        if (oldCollider != null)
            Destroy(oldCollider);
       
        if (mesh == props[0])
            gameObject.AddComponent<CapsuleCollider>();
        else if (mesh == props[1])
            gameObject.AddComponent<BoxCollider>();
        else if (mesh == props[2])
            gameObject.AddComponent<CapsuleCollider>();
        else if (mesh == props[3])
            gameObject.AddComponent<SphereCollider>();
    }

    private void Start()
    {
        mf = GetComponent<MeshFilter>();
    }

    void SetNewMesh(int meshIndex)
    {
        Mesh newMesh = props[meshIndex];
        ApplyMeshAndCollider(newMesh);
    }

    int CheckMeshIndex()
    {
        if (meshTracker >= props.Length)
            meshTracker = 0;

        if (meshTracker < 0)
            meshTracker = props.Length - 1;

       return meshTracker;
    }

    public override void FirstAction()
    {
        meshTracker ++;
        SetNewMesh(CheckMeshIndex());
    }

    public override void SecondAction()
    {
        meshTracker --;
        SetNewMesh(CheckMeshIndex());          
    }

    public override void ThirdAction()
    {       
       cameraController.ToggleCameraMode();
    }
}
