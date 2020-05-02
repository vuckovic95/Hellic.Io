using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;

public class Food : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Food")
        {
            Physics.IgnoreCollision(GetComponent<SphereCollider>(), collision.gameObject.GetComponent<SphereCollider>(), true);
        }
    }
}