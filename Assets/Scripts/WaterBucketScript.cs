using UnityEngine;

public class WaterBucketScript : MonoBehaviour
{
    private float idleTimer = 0.0f;
    private readonly float maxidleTime = 5.0f;


    // Update is called once per frame
    void Update()
    {
        // if drill is idle for 2 seconds, destroy it
        idleTimer += Time.deltaTime;
        if(idleTimer >= maxidleTime){
            Destroy(gameObject);
        }
    }

}
