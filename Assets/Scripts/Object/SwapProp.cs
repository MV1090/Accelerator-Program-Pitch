using Unity.VisualScripting;
using UnityEngine;

public class SwapProp : MonoBehaviour
{
    [SerializeField]Prop prop;
    private void OnTriggerEnter(Collider other)
    {
       
            prop.FirstAction();            
        Destroy(gameObject);
    }
}
