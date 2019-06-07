using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float countdown = 2f;

    private int range;
   
    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f)
        {
            FindObjectOfType<MapController>().Explode(transform.position, range);
            AudioManager.Instance.Play("explosion");
            Destroy(gameObject);
        }
    }

    public void SetRange(int range)
    {
        this.range = range;
    }
}
