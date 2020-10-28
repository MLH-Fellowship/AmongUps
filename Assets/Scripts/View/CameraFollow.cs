 using UnityEngine; 
 using System.Collections; 
 using System.Collections.Generic;

public class CameraFollow : MonoBehaviour { 
    
    public static CameraFollow cFollow;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public float MidX;
    public float MidY;
    public float MidZ;
    public Transform target1;
    public Transform target2;
    public Vector3 Midpoint;
    public Vector3 distance;
    public float camDistance;
    public float CamOffset;
    public float bounds;
    
    void Awake () 
    { 
        cFollow = this;
    }
    
    void Start () 
    {
        camDistance = 10.0f;
        bounds = 12.0f;
    }
    
    void Update () 
    {
        distance = target1.position - target2.position;
        if(camDistance >= 100.0f)
            camDistance = 19.0f;
        if (camDistance <= 10.0f)
            camDistance = 10.0f;
        if(distance.x < 0)
            distance.x = distance.x * -1;
        if(distance.z < 0)
            distance.z = distance.z * -1;
        if(target1.position.x < (transform.position.x -bounds)) {
            Vector3 pos = target1.position;
            pos.x =  transform.position.x -bounds;
            target1.position = pos;
        }
        if(target2.position.x < (transform.position.x -bounds)) {
            Vector3 pos = target2.position;
            pos.x =  transform.position.x -bounds;
            target2.position = pos;
        }
        if(target1.position.x > (transform.position.x +bounds)) {
            Vector3 pos = target1.position;
            pos.x =  transform.position.x +bounds;
            target1.position = pos;
        }
        if(target2.position.x > (transform.position.x +bounds)) {
            Vector3 pos = target2.position;
            pos.x =  transform.position.x +bounds;
            target2.position = pos;
        }
        if(distance.x > 15.0f) {
            CamOffset = distance.x * 0.3f;
            if(CamOffset >=8.5f)
                CamOffset = 8.5f;
        }else if(distance.x < 14.0f) {
            CamOffset = distance.x * 0.3f;
        } else if( distance.z < 14.0f) {
            CamOffset = distance.x * 0.3f;
        }
        MidX = (target2.position.x + target1.position.x) /2; 
        MidY = (target2.position.y + target1.position.y) /2;
        MidZ = (target2.position.z + target1.position.z) /2;
        Midpoint = new Vector3 (MidX, MidY, MidZ);
        if (target1)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(Midpoint);
            Vector3 delta = Midpoint - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camDistance + CamOffset));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.Slerp(transform.position, destination, dampTime);
        }
        
    }
}