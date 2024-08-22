using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletScript : NetworkBehaviour
{
    public PlayerController owner;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            owner.shotsHit.Value++;
            Destroy(this.gameObject);
        }
    }
}
