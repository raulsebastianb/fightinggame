using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInRange : MonoBehaviour
{
    EnemyMovement emove;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PlayerInRangeScript " + this.gameObject);
        emove = GetComponentInParent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            emove.player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == emove.player)
        {
            emove.player = null;
        }
    }
}
