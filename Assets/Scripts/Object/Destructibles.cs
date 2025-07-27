using UnityEngine;

public class Destructibles : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    public void OnSmashed()
    {
        ParticleSystem particleSystem = Instantiate(_particleSystem, gameObject.transform.position, gameObject.transform.rotation);
        particleSystem.Play();

        Debug.Log("Destructible Hit");
        Destroy(gameObject);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
