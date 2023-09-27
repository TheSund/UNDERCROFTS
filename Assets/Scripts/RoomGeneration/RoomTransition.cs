using UnityEngine;
using System.Collections;

public class RoomTransition : MonoBehaviour
{
	private Camera cam;
	public Vector3 cameraPosition;
	public Vector3 cameraPositionOld;
	public Vector3 playerPosition;
	public bool timeToMoveCam;

	private Vector3 velocity = Vector3.zero;
	private float smoothTime = 0.085f;

	public GameObject block;
	// Use this for initialization
	void Start () 
	{
		cam = Camera.main.GetComponent<Camera>();
		timeToMoveCam = false;
	}

	private void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.CompareTag ("Player")) 
		{
			col.transform.position = transform.position + playerPosition;
			cameraPositionOld = cam.transform.position;
			timeToMoveCam = true;
			block.SetActive(true);
			StartCoroutine(Solver());
		}
	}

	void Update () 
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
			block.SetActive(false);
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
			block.SetActive(false);
		}
	}
}
