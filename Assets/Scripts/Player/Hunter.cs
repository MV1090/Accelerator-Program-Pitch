
using UnityEngine;

public class Hunter : Role
{
    //Animator animator;
    CameraController cameraController;           
    private GameObject weaponInstance;

    Player player;
    void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetCameraMode(CameraController.CameraMode.FirstPerson); 
        player = GetComponent<Player>();
        SpawnWeapon();
    }
     public void SpawnWeapon()
    {
        if (player.weaponInstance != null && player.weaponSocket != null)
        {
            weaponInstance = Instantiate(player.weaponInstance, player.weaponSocket.position, player.weaponSocket.rotation, player.weaponSocket);
        }
        else
        {
            Debug.LogError("WeaponPrefab or WeaponSocket not set on Hunter!");
        }
    }
    public override void FirstAction()
    {
        //temp placeholder for testing. Remove this later. when animation is in.
        DetectHit();
    
        //animator.SetTrigger("Swing");

        Debug.Log("Hunter FirstAction");
    }

    public override void SecondAction()
    {        
        Debug.Log("Hunter SecondAction");
        return;
    }

    public override void ThirdAction()
    {
        Debug.Log("Hunter ThirdAction");
        return;
    }

    public void DetectHit()
    {
        weaponInstance.GetComponent<Weapon>().DetectHit();
    }
}
