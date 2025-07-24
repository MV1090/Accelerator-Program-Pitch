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
        //Logic for swapping prop in one direction goes here
        Debug.Log("Prop FirstAction");
    }

    public override void SecondAction()
    {
        //Logic for swapping prop in the other direction goes here
        Debug.Log("Prop SecondAction");
    }

    public override void ThirdAction()
    {       
       cameraController.ToggleCameraMode();
    }
}
