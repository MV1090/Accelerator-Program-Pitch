using UnityEngine;
public class Prop : Role
{     
    CameraController cameraController;

    void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetCameraMode(CameraController.CameraMode.FirstPerson);
    }

    public override void FirstAction()
    {
        Debug.Log("Prop FirstAction");
    }

    public override void SecondAction()
    {
        Debug.Log("Prop SecondAction");
    }

    public override void ThirdAction()
    {       
       cameraController.ToggleCameraMode();
    }
}
