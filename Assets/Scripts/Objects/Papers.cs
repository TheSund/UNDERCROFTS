using UnityEngine;
using System.Collections;

public class Papers : MonoBehaviour {
    [SerializeField]
    private GameObject bigPaper;

    // Use this for initialization
    public void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.CompareTag("Player"))
            if (!bigPaper.activeSelf)
                bigPaper.SetActive(true);
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            if (bigPaper.activeSelf)
                bigPaper.SetActive(false);
    }
}
