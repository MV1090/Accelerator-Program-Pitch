using UnityEngine;

public class BellRing : MonoBehaviour
{
    public Transform pin;

    public float moveDistance = 1.723f;     // How far to move up
    private float upSpeed;         // Speed going up
    public float downSpeed = 1.5f;        // Speed going down

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingUp = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = pin.transform.position;        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingUp)
        {
            pin.transform.position = Vector3.MoveTowards(pin.transform.position, targetPosition, upSpeed * Time.deltaTime);

           
            if (Vector3.Distance(pin.transform.position, targetPosition) < 0.01f)
            {
                movingUp = false;
            }
        }
        else
        {
            pin.transform.position = Vector3.MoveTowards(pin.transform.position, startPosition, downSpeed * Time.deltaTime);            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        MovePin();
    }

    private void MovePin()
    {
        float randomDistance = Random.Range(startPosition.y, moveDistance);
        targetPosition = startPosition + Vector3.up * randomDistance;
        upSpeed = 10 * randomDistance;
        movingUp = true;
        Debug.Log("Pin Moved");
    }
}
