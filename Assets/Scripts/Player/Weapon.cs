using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{   
    public Collider weaponCollider;

    public int destructionCount = 0;

    public float rotateDuration = 0.2f; // Time it takes to rotate
    private Vector3 initialLocalRotation;
    private bool isRotating = false;

    void Awake()
    {
        weaponCollider = GetComponent<Collider>();
        weaponCollider.enabled = false;        
    }

    public void DetectHit()
    {
        if (isRotating == true)
            return;

        weaponCollider.enabled = true;
        initialLocalRotation = transform.localEulerAngles;
        StartCoroutine(RotateObjectCoroutine());
    }

    private IEnumerator EnableWeaponCollider()
    {
        yield return new WaitForSeconds(0.2f);
        weaponCollider.enabled = false;
        destructionCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Prop>(out Prop prop)) 
        {
        //prop.OnHitByHunter();
        return;
        } 

        // else if (other.TryGetComponent<DestructibleProp>(out DestructibleProp destructible)) 
        // {
        // destructible.OnSmashed();
        // return;
        // }

        Debug.Log("Weapon hit " + other.gameObject.name);
        if (destructionCount == 0)
        {
            Destroy(other.gameObject);
            destructionCount++;
        }
    }

    public IEnumerator RotateObjectCoroutine()
    {
        isRotating = true;

        float targetRotationZ = 40f;
        float elapsedTime = 0f;
        
        Vector3 startLocalRotation = transform.localEulerAngles;
        Vector3 targetLocalRotation = new Vector3(
            startLocalRotation.x, 
            startLocalRotation.y, 
            startLocalRotation.z + targetRotationZ 
        );
        
        while (elapsedTime < rotateDuration)
        {
            transform.localEulerAngles = Vector3.Lerp(startLocalRotation, targetLocalRotation, elapsedTime / rotateDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localEulerAngles = targetLocalRotation;

        StartCoroutine(EnableWeaponCollider());

        yield return new WaitForSeconds(0.05f);
        
        elapsedTime = 0f;
        Vector3 returnRotation = initialLocalRotation;

        while (elapsedTime < rotateDuration)
        {
            transform.localEulerAngles = Vector3.Lerp(targetLocalRotation, returnRotation, elapsedTime / rotateDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localEulerAngles = returnRotation;

        isRotating = false;
    }

}
