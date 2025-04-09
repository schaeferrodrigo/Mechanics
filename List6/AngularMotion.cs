using UnityEngine;
using UnityEngine.InputSystem;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class AngularMotion : MonoBehaviour
{

    //Object's properties

    private float angularVelocity;
    private GameObject disk;

    // initial properties

    public float initialAngularVelocity = -4.6f;
    public float angularAcceleration = 0.015f;
    private float angle = 0f;
  
    //time properties

    public float stepTime = 0.1f;
    private float time;
    
    void Start()
    {
        disk = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        disk.transform.localScale = new Vector3(2f, 0.2f, 2f);
        disk.transform.position = Vector3.zero;
        angularVelocity = initialAngularVelocity;

        time = 0;
    }

    void Update()
    {

        if (angularVelocity >= 0)
        {
            Debug.Log("Total time = " + time);
            return;
        }
        else {
            (angle, angularVelocity, time) = dynamicsEquations(angle, angularVelocity);

            disk.transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg * angle, 0); // y axis
        }
                  
    }

    (float, float, float) dynamicsEquations(float oldAngle, float oldVelocity) { 
        
        float newAngle, newVelocity;

        newAngle = oldAngle + oldVelocity * stepTime + 0.5f * angularAcceleration * stepTime * stepTime;
        newVelocity = oldVelocity + angularAcceleration * stepTime;
        time += stepTime;


        return (newAngle, newVelocity, time);
 
    }
    
}


