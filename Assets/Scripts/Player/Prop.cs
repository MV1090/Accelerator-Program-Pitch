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

    private void Start()
    {
        mf = GetComponent<MeshFilter>();

    }

    void SetNewMesh(int meshIndex)
    {
        mf.mesh = props[meshIndex];
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

        //Logic for swapping prop in one direction goes here
        Debug.Log("Prop FirstAction");
    }

    public override void SecondAction()
    {
        meshTracker --;
        SetNewMesh(CheckMeshIndex());
        //Logic for swapping prop in the other direction goes here
        Debug.Log("Prop SecondAction");
    }

    public override void ThirdAction()
    {       
       cameraController.ToggleCameraMode();
    }
}
