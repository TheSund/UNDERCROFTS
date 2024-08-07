﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking.NetworkSystem;

public class PlayerScript : MonoBehaviour 
{
	[Header("Movement & Sprite Related")]
	public bool lockMovement;
	public float speed;
	private Rigidbody2D rb;
	private Vector2 moveInput;
	private Vector2 velocity;
	private Animator anim;
	private SpriteRenderer sprite;
	private int direction;

	[Header("Health & Damage Related")]
	private float health;
	private float maxHealth;
	public float invFrames;
	public float invFramesDeltaTime;
	public bool isInvincible;

    [Header("Health Bar Related")]
    public Image healthBarImage;
	public Image healthBarDamagedImage;
	private bool hbDamageTaken;
	public Text healthtext;

	private GameObject gameoverScreen;
	[HideInInspector] public string lastDamagedBy;

    private bool animDamageTaken;

    [Header("Portraits Related")]
	public Image portrait;
	public Sprite portraitSprite;
	public Sprite portraitSpritePain;
	[Header("SpawnTime")]
	public int spawnTime;

	private CharacterManager manager;
    // Use this for initialization
    void Start () 
	{
		manager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
		gameoverScreen = GameObject.Find("Canvas").transform.FindChild("GameOverScreen").gameObject;
		lockMovement = true;
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		direction = 2;
		sprite = transform.GetComponentInChildren<SpriteRenderer>();
		maxHealth = manager.currentMaxHealth;
		health = manager.currentHealth;
		hbDamageTaken = false;
		animDamageTaken = false;
        isInvincible = false;
		healthtext.text = health + "/" + maxHealth;
		healthBarImage.fillAmount = health / maxHealth;
		healthBarDamagedImage.fillAmount = health / maxHealth;
		StartCoroutine("EnableCollision");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!lockMovement && !PauseMenu.gameIsPaused)
		{
			if (hbDamageTaken)
			{
				UpdateHealthBar();
			}
			moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			velocity = moveInput.normalized * speed * 50;

			if (!gameObject.GetComponentInChildren<Gun>().isShooting && !gameObject.GetComponentInChildren<Gun>().isShooting2 && !animDamageTaken)
			{
				if (moveInput.x == 0 && moveInput.y == 0)
				{
					switch (direction)
					{
						case 0:
							anim.Play("idleBack");
							break;
						case 1:
							anim.Play("idleRight");
							break;
						case 2:
							anim.Play("idleFront");
							break;
						case 3:
							anim.Play("idleLeft");
							break;
					}
				}
				else if (moveInput.x > 0)
				{
					direction = 1;
					anim.Play("runRight");
				}
				else if (moveInput.x < 0)
				{
					direction = 3;
					anim.Play("runLeft");
				}
				else if (moveInput.y > 0)
				{
					direction = 0;
					anim.Play("runBack");
				}
				else if (moveInput.y < 0)
				{
					direction = 2;
					anim.Play("runFront");
				}
			}
		}
		else velocity = Vector2.zero;
	}
	
	void FixedUpdate() 
	{	
		rb.AddForce (velocity * Time.fixedDeltaTime);

	}

	public void DamageTaken() 
	{	
		health--;
		manager.currentHealth = health;
		if (health > 0f) {
            hbDamageTaken = true;
            animDamageTaken = true;
            if (direction == 1)
				anim.Play("damageRight");
			else
                anim.Play("damageLeft");

            portrait.sprite = portraitSpritePain;
			StartCoroutine (BecomeTemporarilyInvincible());
			StartCoroutine (ChangePortraitAfterDamage());

			healthBarImage.fillAmount = health / maxHealth;
		} else
			DeathSequence();
	}
	public void UpdateHealthBar()
	{
		if (healthBarDamagedImage.fillAmount > health / maxHealth)
		{
			healthBarDamagedImage.fillAmount -= Time.deltaTime * 0.5f;
		}
		else
		{
			healthBarDamagedImage.fillAmount = health / maxHealth;
            hbDamageTaken = false;
        }
		healthtext.text = health + "/" + maxHealth;
	}

	private IEnumerator BecomeTemporarilyInvincible()
	{
		isInvincible = true;
        for (float i = 0; i < invFrames; i += invFramesDeltaTime)
		{
			if (i >= 0.25f && animDamageTaken)
				animDamageTaken = false;
			// Alternate between 0 and 1 scale to simulate flashing
			if (sprite.color == new Color(255,255,255, 255))
			{
				sprite.color = new Color (0,0,0,0);
			}
			else
			{
				sprite.color = new Color(255,255,255,255);
			}
			yield return new WaitForSeconds(invFramesDeltaTime);
		}

		sprite.color = new Color(255,255,255,255);
		isInvincible = false;
	}
	private IEnumerator ChangePortraitAfterDamage()
	{
		yield return new WaitForSeconds (0.25f);
		portrait.sprite = portraitSprite;
	}
	private IEnumerator EnableCollision()
	{
		yield return new WaitForSeconds(spawnTime);
		lockMovement = false;
		GetComponent<Collider2D> ().enabled = true;
		sprite.sortingOrder = 0;
	}
	private void DeathSequence()
	{
		lockMovement = true;
		isInvincible = true;
		velocity = new Vector2(0,0);
        CursorController.ChangeCursor(CursorController.cursorMenu, 2f, 2f);

        anim.Play("death");

		GameObject.Find ("Canvas/Minimap").SetActive(false);
		foreach (Transform child in transform)
			if (!child.CompareTag("PlayerSprite"))
				Destroy(child.gameObject);
		foreach (GameObject playerObject in GameObject.FindGameObjectsWithTag("PlayerObject"))
			GameObject.Destroy (playerObject);
		gameoverScreen.SetActive(true);
	}
}

