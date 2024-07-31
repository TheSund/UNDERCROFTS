using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour 
{
	[Header("Settings")]
	public float offset;
	public GameObject Arrow;
	public Object newArrow;
	public Transform shotPoint;
	private GameObject characterFire;
	private Animator anim;
	private float damage;

	[Header("Reload Related")]
	public Image tint;
	public Text tintReload;
	public float startReloadTime;
	private float reloadTime;

	[Header("Arrow Count")]
	public float maxArrowCount;
	public float arrowCount;
	public Text arrowCounterText;

	public Image arrowCounterFillAmount;
	private float arrowPullCounter;

	private float aimDirection;
	public bool isShooting;

	[Header("Charged Shot")]
	private LineRenderer chargedShotLineR;
	public GameObject droppedArrow;
	public Image tint2;
	public Text tintReload2;
	public float startReloadTime2;
	private float reloadTime2;
	private float charge;
	private Vector3 screenPoint;
	private Vector3 worldPoint;
	public bool isShooting2;
	private float colorR;
	private float colorG;
	private float colorB;
	private float colorA;

	private CharacterManager manager;
	// Use this for initialization
	void Start () 
	{
		manager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
		damage = manager.currentDamage;
		characterFire = GameObject .FindGameObjectWithTag("Player");
		anim = characterFire.GetComponent<Animator>();

		chargedShotLineR = GetComponent<LineRenderer>();
		chargedShotLineR.SetVertexCount(2);
		colorR = 0.34f;
		colorG = 0.56f;
		colorB = 0.46f;
		colorA = 0.5f;
		chargedShotLineR.SetColors(new Color(colorR,colorG, colorB, colorA),new Color(colorR,colorG, colorB, colorA));
		chargedShotLineR.SetWidth (0.5f, 0.5f);
		chargedShotLineR.enabled = false;

		arrowCount = maxArrowCount;
		arrowCounterText.text = arrowCount + "/" + maxArrowCount;

		arrowPullCounter = 0;

		isShooting = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if (!PauseMenu.gameIsPaused)
		{
			Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

			aimDirection = rotZ;

			if (reloadTime == 0)
			{
				if (Input.GetMouseButton(0) && !characterFire.GetComponent<PlayerScript>().lockMovement && !isShooting2)
				{
					if (arrowCount > 0)
					{
						isShooting = true;
						if (aimDirection >= 45 && aimDirection < 135)
						{
							anim.Play("attackback");
						}
						else if ((aimDirection >= 135 && aimDirection <= 180) || (aimDirection <= -135 && aimDirection >= -180))
						{
							anim.Play("attackleft");
						}
						else if (aimDirection >= -45 && aimDirection < 45)
						{
							anim.Play("attackright");
						}
						else if (aimDirection <= -45 && aimDirection > -135)
						{
							anim.Play("attackfront");
						}
						StartCoroutine(AnimTimer(0.2f));

						ChangeAmmoCount(-1);

						newArrow = Instantiate(Arrow, shotPoint.position, transform.rotation);
						(newArrow as GameObject).GetComponent<CBArrow>().damage = damage;
						characterFire.GetComponent<Rigidbody2D>().AddForce(characterFire.transform.position - shotPoint.position, ForceMode2D.Impulse);
						reloadTime = startReloadTime;
					}
				}
				tintReload.color = new Color(0, 0, 0, 0);
			}
			else
			{
				reloadTime -= Time.deltaTime;
				if (reloadTime <= 0.01f)
				{
					tintReload.color = new Color(0, 0, 0, 0);
					tint2.fillAmount = 0;
					reloadTime = 0;
				}
				else
				{
					tintReload.color = new Color(237, 240, 298, 255);
					tintReload.text = Mathf.Ceil(reloadTime).ToString();
				}
			}

			if (arrowCount > 0 || reloadTime > 0)
			{
				tint.fillAmount = reloadTime / startReloadTime;
			}
			else if (arrowCount == 0 && reloadTime <= 0)
			{
				tint.fillAmount = 1;
			}


			if (Input.GetKey(KeyCode.E) && arrowCount < maxArrowCount)
			{
				arrowPullCounter += Time.deltaTime;
				arrowCounterFillAmount.fillAmount = arrowPullCounter/3;
				if (arrowPullCounter >= 3)
				{
					foreach (GameObject arrow in GameObject.FindGameObjectsWithTag("PlayerObject"))
							Destroy(arrow);
						ChangeAmmoCount(maxArrowCount - arrowCount);
                    arrowPullCounter = 0;
					arrowCounterFillAmount.fillAmount = 1;
				}

			}
			else if (Input.GetKeyUp(KeyCode.E))
			{
				arrowPullCounter = 0;
				arrowCounterFillAmount.fillAmount = 1;
			}



			if (reloadTime2 == 0 && arrowCount > 0)
			{
				if (Input.GetMouseButton(1) && !characterFire.GetComponent<PlayerScript>().lockMovement)
				{
					screenPoint = Input.mousePosition;
					screenPoint.z = 24.5f;
					worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
					chargedShotLineR.enabled = true;
					chargedShotLineR.SetPosition(0,transform.position);
					chargedShotLineR.SetPosition(1,worldPoint);
				}
				else if (Input.GetMouseButtonUp(1))
				{
					chargedShotLineR.enabled = false;
					charge = 0.5f;
					isShooting2 = true;
					characterFire.GetComponent<PlayerScript>().lockMovement = true;
					if (aimDirection >= -90 && aimDirection < 90)
						anim.Play("chargedShotRight");
					else
						anim.Play("chargedShotLeft");

                }
				if (isShooting2)
					charge -= Time.deltaTime;
				if (charge <= 0 && isShooting2)
				{
					ChangeAmmoCount(-1);
					RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, (worldPoint-transform.position), 50);
					Debug.DrawRay(transform.position,(worldPoint-transform.position), Color.red);
					for (int i = 0; i < hits.Length; i++)
					{
						if (hits[i].transform.CompareTag("Enemy") && !hits[i].collider.isTrigger)
						{
							hits[i].transform.GetComponent<EnemyScript>().TakeDamage(damage * 2);
							hits[i].transform.GetComponent<Rigidbody2D>().AddForce((hits[i].transform.position - transform.position).normalized * 10, ForceMode2D.Impulse);
						}
						else if (hits[i].transform.CompareTag("Wall") || hits[i].transform.CompareTag("CorridorWall") || i == hits.Length)
						{
							Instantiate(droppedArrow, new Vector2((hits[i].point.x + (transform.position.x - hits[i].point.x) * (1 / (hits[i].point - (Vector2)transform.position).magnitude)), (hits[i].point.y + (transform.position.y - hits[i].point.y) * (1 / (hits[i].point - (Vector2)transform.position).magnitude))), Quaternion.Euler(0f, 0f, 90f));
							chargedShotLineR.SetPosition(1, hits[i].point);
							colorR = 0.44f;
							colorG = 0.86f;
							colorB = 0.76f;
							colorA = 1f;
							chargedShotLineR.SetColors(new Color(colorR, colorG, colorB, colorA), new Color(colorR, colorG, colorB, colorA));
							break;
						}
					}
                    chargedShotLineR.SetPosition(0,transform.position);
					chargedShotLineR.enabled = true;
					characterFire.GetComponent<Rigidbody2D>().AddForce((characterFire.transform.position - shotPoint.position)*1.8f, ForceMode2D.Impulse);
					characterFire.GetComponent<PlayerScript>().lockMovement = false;
					reloadTime2 = startReloadTime2;
				}
			}
			else
			{
				reloadTime2 -= Time.deltaTime;
				if (reloadTime2 <= 0.01f)
				{
					tintReload2.color = new Color(0, 0, 0, 0);
					tint2.fillAmount = 0;
					reloadTime2 = 0;
				}
				else
				{
					tintReload2.color = new Color(237, 240, 298, 255);
					tintReload2.text = Mathf.Ceil(reloadTime2).ToString();
				}

				if (chargedShotLineR.enabled == true)
				{
					colorR -= 0.05f;
					colorG -= 0.05f;
					colorB -= 0.05f;
					colorA -= 0.08f;
				}

				chargedShotLineR.SetColors(new Color(colorR,colorG, colorB, colorA),new Color(colorR,colorG, colorB, colorA));
				if (colorA <= 0f)
				{
                    isShooting2 = false;
                    chargedShotLineR.enabled = false;
					colorR = 0.34f;
					colorG = 0.56f;
					colorB = 0.46f;
					colorA = 0.5f;
					chargedShotLineR.SetColors(new Color(colorR,colorG, colorB, colorA),new Color(colorR,colorG, colorB, colorA));
				}
			}
			if (arrowCount > 0 || reloadTime2 > 0)
			{
				tint2.fillAmount = reloadTime2 / startReloadTime2;
			}
			else if (arrowCount == 0 && reloadTime2 <= 0)
			{
				tint2.fillAmount = 1;
			}
		}
	}

	public void ChangeAmmoCount(float count) 
	{
		arrowCount += count;
		arrowCounterText.text = arrowCount + "/" + maxArrowCount;
	}
	private IEnumerator AnimTimer(float time)
	{
		yield return new WaitForSeconds(time);
		isShooting = false;
	}
}
