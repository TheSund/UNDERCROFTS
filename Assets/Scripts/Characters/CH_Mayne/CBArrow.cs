using UnityEngine;
using System.Collections;

public class CBArrow : MonoBehaviour 
{
	public float distance;
	public float damage;
	public float shotSpeed;
	public LayerMask solidObject;
	public float lifetime;
	public GameObject droppedArrow;
	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, lifetime);

	}
	
	// Update is called once per frame
	void Update () 
	{
		/*RaycastHit2D hitInfo = Physics2D.Raycast (transform.position, new Vector3(1,-1), distance, solidObject);
		if (hitInfo.collider != null && hitInfo.collider.isTrigger != true) 
		{
			if (hitInfo.collider.CompareTag ("Enemy")) {
				hitInfo.collider.GetComponent<EnemyScript> ().TakeDamage (damage);
				hitInfo.collider.GetComponent<Rigidbody2D> ().AddForce ((hitInfo.collider.transform.position - transform.position) * 10, ForceMode2D.Impulse);
			}
			Destroy (gameObject);
		}*/
		transform.Translate (new Vector2 (1, -1) * shotSpeed * Time.deltaTime);
	}
	

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		if (!hitInfo.CompareTag("Player") && gameObject.GetComponent<Collider2D>().IsTouchingLayers(solidObject) &&  hitInfo.GetComponent<Collider2D>() != null &&  hitInfo.GetComponent<Collider2D> ().isTrigger != true)
		{
			if (hitInfo.CompareTag ("Enemy")) {
				hitInfo.GetComponent<EnemyScript> ().TakeDamage (damage);
				hitInfo.GetComponent<Rigidbody2D> ().AddForce ((hitInfo.transform.position - transform.position) * 10, ForceMode2D.Impulse);
			}
			if (hitInfo.CompareTag ("Wall")) 
			{
				Destroy (gameObject);
			}
			Destroy (gameObject);
		}
	}
	void OnDestroy () 
	{
		Instantiate (droppedArrow, transform.position, Quaternion.Euler(0f, 0f, 90f));
	}
}
