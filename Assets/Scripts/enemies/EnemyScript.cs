using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	[Header("Enemy Params")]
	public string myName;
	public float health;
	[Header("Damage and Death")]
	public float deathTime;
	private AddRoom room;
	private bool isDying;
	public float damageFlashTime;
	private SpriteRenderer spriteRenderer;
	private Color originalColor;


	// Use this for initialization
	void Start () 
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		originalColor = spriteRenderer.color;
		isDying = false;
		room = GetComponentInParent<AddRoom>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (health <= 0) 
		{
			isDying = true;
			GetComponent<Animator> ().SetTrigger("dead");

			StartCoroutine(DeathInTime());
			if (room != null)
			{
				room.enemies.Remove(gameObject);
				room.CheckEnemies();
			}
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
		spriteRenderer.color = Color.red;
		Invoke("ResetFlash", damageFlashTime);

	}

	private void ResetFlash()
	{
		spriteRenderer.color = originalColor;
	}

	public void OnCollisionStay2D (Collision2D col) 
	{
		if (col.gameObject.tag == "Player" && !col.gameObject.GetComponent<PlayerScript>().isInvincible && !isDying) {
			col.gameObject.GetComponent<PlayerScript>().DamageTaken();
			col.gameObject.GetComponent<PlayerScript>().lastDamagedBy = myName;
			col.gameObject.GetComponent<Rigidbody2D>().AddForce((col.transform.position - transform.position) * 2.2f, ForceMode2D.Impulse);
		}
	}
}
