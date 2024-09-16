using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    [SerializeField] protected float changeToSpawn = 50f;

    protected virtual void Start()
    {
        bool canSpawn = Random.Range(0, 100) >= changeToSpawn;

        if (!canSpawn)
            Destroy(gameObject);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            Player player = collision.GetComponent<Player>();
            player.Damage();
        }
    }


}
