using UnityEngine;


public class exercise : MonoBehaviour
{
  private Vector3 position;
  private Vector3 velocity;
  private Vector3 acceleration;
  private float time;

  

  public float dt = 0.1f; //step size

  private float g = 9.8f;

  public float totalTime = 0.4f;


  void Start()
  {
    position = new Vector3(1, 0, 2);
    velocity = new Vector3(1, 1, 20);
    acceleration = new Vector3(0, 0, -g);
    transform.position = position;
    time = 0;
  }

  void Update(){

  if (time < totalTime){
   
  (position, velocity, time) = EluerMethod(position, velocity, acceleration, time);
  
  transform.position = position;

  if (position.z <= 0){
    velocity = new Vector3(0, 0, 0);
  }
   Debug.Log("position" + position);
  Debug.Log("dt" + dt);
 } else {return;}


  

  }

 (Vector3, Vector3 , float) EluerMethod(Vector3 position, Vector3 velocity, Vector3 acceleration, float time){
    Vector3 newPosition = position + velocity*(time+ dt); 
    Vector3 newVelocity = velocity + acceleration*(time + dt);
    time += dt;
    return (newPosition, newVelocity, time);
}
}
