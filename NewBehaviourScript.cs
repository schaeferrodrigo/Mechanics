using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{   

    private Vector2 position;
    private Vector2 velocity;
    private Vector2 acceleration;

    public float springConstant = 4 ;
    public float stepTime = 0.001f;

    private float time;
    public float totalTime  = 5*Mathf.PI;

    // Start is called before the first frame update
    void Start()
    {
     position = new Vector2(2,0);
     velocity = new Vector2(0,0);
     acceleration = Acceleration(position);
     time = 0;
     transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (time < totalTime){
            (position, velocity, time) = RungeKutta(position, velocity, time);
            transform.position = position;
        }
        else{
            return;
        }
    }

    Vector2 Acceleration(Vector2 position){

         Vector2 force = new Vector2(-springConstant * position.x, 0);

         return force;

    }

    (Vector2 , Vector2, float) RungeKutta(Vector2 position, Vector2 velocity, float time){

        Vector2 K1p, K1v, K2p, K2v, K3p, K3v, K4p, K4v;

        K1p = velocity;
        K1v = Acceleration(position);
        K2p = velocity + stepTime/2 * K1v;
        K2v = Acceleration(position + stepTime/2 * K1p);
        K3p = velocity + stepTime/2 * K2v;
        K3v = Acceleration(position + stepTime/2 * K2p);
        K4p = velocity + stepTime * K3v;
        K4v =  Acceleration(position + stepTime * K3p);
        Vector2 newPosition = position + stepTime/6 * (K1p + 2*K2p + 2*K3p + K4p);
        Vector2 newVelocity = velocity + stepTime/6 * (K1v + 2*K2v + 2*K3v + K4v);
        float newTime = time + stepTime;
        return (newPosition, newVelocity, newTime);
    }
}
