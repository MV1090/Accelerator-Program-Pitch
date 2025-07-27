using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    public Transform[] targetPoints;
    public float moveSpeed = 2f;

    private int currentPointIndex = 0;
    private bool shouldMove = false;

    void Update()
    {
        if (shouldMove && targetPoints.Length > 0)
        {
            Transform target = targetPoints[currentPointIndex];
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            // Check if we've reached the target
            if (Vector3.Distance(transform.position, target.position) < 0.01f)
            {
                currentPointIndex++;

                if (currentPointIndex >= targetPoints.Length)
                {
                    shouldMove = false; // Stop moving after last point
                }
            }
        }
    }

    public void StartMoving()
    {
        currentPointIndex = 0;
        shouldMove = true;
    }       
}
