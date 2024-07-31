using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
	private Camera cam;
	private Vector3 cameraPosition;
	private Vector3 cameraPositionOld;
	private bool timeToMoveCam;
	
	private Vector3 velocity = Vector3.zero;
	private float smoothTime = 0.085f;

	// Use this for initialization
	void Start () 
	{
		cam = Camera.main.GetComponent<Camera>();
		timeToMoveCam = false;
	}
	
	public void Move(float y) 
	{
		cameraPosition = new Vector3(0, y);
		cameraPositionOld = cam.transform.position;
		timeToMoveCam = true;
		StartCoroutine(Solver());
	}
	
	void Update() 
	{
		if (timeToMoveCam) 
		{
			cam.transform.position = Vector3.SmoothDamp(cam.transform.position, (cameraPositionOld + cameraPosition), ref velocity, smoothTime);
		}
		if (cam.transform.position == (cameraPositionOld + cameraPosition)) 
		{
			cam.transform.position = cameraPositionOld + cameraPosition;
			timeToMoveCam = false;
			cameraPositionOld = Vector3.zero;
		}
	}
	private IEnumerator Solver()
	{
		yield return new WaitForSeconds(1.1f);
		if (timeToMoveCam) 
		{
			cam.transform.position = cameraPositionOld + cameraPosition;
			timeToMoveCam = false;
			cameraPositionOld = Vector3.zero;
		}
	}
}
