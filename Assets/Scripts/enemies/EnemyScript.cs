using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	public string myName;
	public float health;
	public float deathTime;
	private AddRoom room;

	// Use this for initialization
	void Start () 
	{
		room = GetComponentInParent<AddRoom>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (health <= 0) 
		{
			GetComponent<Animator> ().SetTrigger("dead");

			StartCoroutine(DeathInTime());
			room.enemies.Remove(gameObject);
			room.CheckEnemies();
		}
	}

	private IEnumerator DeathInTime()
	{	
		yield return new WaitForSeconds (deathTime);
		Destroy(gameObject);
	}

	public void TakeDamage (float damage) 
	{
		health -= damage;
	}

	public void OnCollisionStay2D (Collision2D col) 
	{
		if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<PlayerScript>().isInvincible == false) {
			col.gameObject.GetComponent<PlayerScript>().DamageTaken();
			col.gameObject.GetComponent<PlayerScript>().lastDamagedBy = myName;
			col.gameObject.GetComponent<Rigidbody2D>().AddForce((col.transform.position - transform.position) * 2.2f, ForceMode2D.Impulse);
		}
	}
}
