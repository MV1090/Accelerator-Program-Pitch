using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{   
    public Collider weaponCollider;

    void Awake()
    {
        weaponCollider = GetComponent<Collider>();
        weaponCollider.enabled = false;
    }

    public void DetectHit()
    {
        weaponCollider.enabled = true;
        StartCoroutine(EnableWeaponCollider());
    }

    private IEnumerator EnableWeaponCollider()
    {
        yield return new WaitForSeconds(0.2f);
        weaponCollider.enabled = false;
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

        Destroy(other.gameObject);
    }


}
