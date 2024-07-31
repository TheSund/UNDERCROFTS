using UnityEngine;
using System.Collections;

public class ArrowPickup : MonoBehaviour {
	private Rigidbody2D rb;
	private Gun crossbow;
	[HideInInspector]public bool preventivePull;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.AddForce(Vector2.down,ForceMode2D.Impulse);
		rb.AddTorque(Random.Range(-1.1f,1.1f), ForceMode2D.Impulse);
		crossbow = GameObject.Find("CrossBow").GetComponent<Gun> ();
		preventivePull = false;
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.CompareTag("Player") && crossbow.arrowCount < crossbow.maxArrowCount) 
		{
			crossbow.ChangeAmmoCount(1);
			Physics2D.IgnoreLayerCollision(4,12, ignore: false);
			Physics2D.IgnoreLayerCollision(8,12, ignore: false);
			Destroy(gameObject);
		}
		if (collider.CompareTag("Pit"))
		    {
			Physics2D.IgnoreLayerCollision(4,12);
			preventivePull = true;
		}
	}
	void Update () {
		if (preventivePull) {
			rb.AddForce((GameObject.FindGameObjectWithTag("Player").transform.position - transform.position)*2);
		}
	}
}
