using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaLiveTime : MonoBehaviour
{
    float livetime = 30;

    private void Update()
    {
        livetime -= Time.deltaTime;
        if (livetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
