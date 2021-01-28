using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public FollowCam S; // a follow cam singleton
    public float easing = 0.05f;
    public Vector2 minXY; 
    public bool ______________________;

    public GameObject poi;
    public float camZ;

     void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
	{
		// if there's only one line following an if, it doesn't need braces
		if (poi == null) return; // return if there is no poi
								 // Get the position of the poi

		//Commented out line below
		// Vector3 destination = poi.transform.position;

		// This is where it said void FixedUpdate () {

		Vector3 destination;

		Camera.main.fieldOfView = 30 + poi.transform.position.y / 5.0f;

		// If there is no poi, return to P:[0,0,0]
		if (poi == null)
		{
			destination = Vector3.zero;
		}
		else
		{
			// Get the position of the poi
			destination = poi.transform.position;
			// If poi is a Projectile, check to see if it's at rest
			if (poi.tag == "Projectile")
			{
				// if it is sleeping (that is, not moving)
				if (poi.GetComponent<Rigidbody>().IsSleeping())
				{
					// return to default view
					poi = null;
					// in the next update
					return;
				}
			}
		}
		//End of the last pasted block of code
		// Limit the X & Y to minimum values
		destination.x = Mathf.Max(minXY.x, destination.x);
		destination.y = Mathf.Max(minXY.y, destination.y);
		// Interpolate from the current Camera position toward destination
		destination = Vector3.Lerp(transform.position, destination, easing);
		// Retain a destination.z of camZ
		destination.z = camZ;
		// Set the camera to the destination
		transform.position = destination;
		//Ending
	}
}