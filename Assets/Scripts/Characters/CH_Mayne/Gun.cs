using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour 
{
	[Header("Settings")]
	public float offset;
	public GameObject Arrow;
	public Transform shotPoint;
	private GameObject characterFire;
	private Animator anim;

	[Header("Reload Related")]
	public Image tint;
	public Text tintReload;
	public float startReloadTime;
	private float reloadTime;

	[Header("Arrow Count")]
	public float maxArrowCount;
	public float arrowCount;
	public Text arrowCounterText;

	private float aimDirection;
	public bool isShooting;
	// Use this for initialization
	void Start () 
	{
		characterFire = GameObject.Find ("PlayerMayne");
		anim = characterFire.GetComponent<Animator>();
		arrowCount = maxArrowCount;
		arrowCounterText.text = arrowCount + "/" + maxArrowCount;

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

			if (reloadTime <= 0 && !PauseMenu.gameIsPaused)
			{
				if (Input.GetMouseButton(0))
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

						Instantiate(Arrow, shotPoint.position, transform.rotation);
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
