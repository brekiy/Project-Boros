using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabby : MonoBehaviour {

    // Use this for initialization
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.name == "GreggBody")
        {
            Vector3 dir = collision.transform.position - transform.position;
            dir = dir.normalized;
            collision.GetComponent<Rigidbody>().AddForce(dir*200);
        }
    }
}
