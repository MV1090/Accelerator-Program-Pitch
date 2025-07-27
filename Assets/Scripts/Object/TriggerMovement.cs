using UnityEngine;

public class TriggerMovement : MonoBehaviour
{
    public MoveBetweenPoints mover;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mover.StartMoving();
        }
    }
}
