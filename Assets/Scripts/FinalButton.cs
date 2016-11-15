using UnityEngine;

public class FinalButton : MonoBehaviour
{
    [HideInInspector]
    public bool quitNow;
    public GameObject confirmEnd;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("Press");
        }
    }

    void Update()
    {
        if (quitNow)
        {
            confirmEnd.SetActive(true);
        }
    }
}
