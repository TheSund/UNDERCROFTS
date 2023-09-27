using UnityEngine;
using System.Collections;

public class PickUpRadius : MonoBehaviour 
{


	public float torque;
	

	void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.CompareTag("ArrowPickup"))
		{
			collider.GetComponent<Rigidbody2D>().AddForce((transform.position - collider.transform.position)*10);
			collider.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-1,2)*0.1f, ForceMode2D.Impulse);
		}
	}

}
