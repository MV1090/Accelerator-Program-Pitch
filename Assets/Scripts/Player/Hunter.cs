
using UnityEngine;

public class Hunter : Role
{
     CameraController cameraController;
    void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetCameraMode(CameraController.CameraMode.FirstPerson);
    }
    public override void FirstAction()
    {
         Debug.Log("Hunter FirstAction");
    }

    public override void SecondAction()
    {
        Debug.Log("Hunter SecondAction");
    }

    public override void ThirdAction()
    {
        Debug.Log("Hunter ThirdAction");
    }
}
