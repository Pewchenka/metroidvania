using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(live());
    }

    IEnumerator live()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(.16f);
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
    }
}
