using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private Vector3 target;
    private float range;
    private float speed;
    private int damage;
    private float distanceTravelled;

    public void SetValues(Vector3 v, float r, float s, int d)
    {
        target = v;
        range = r;
        speed = s;
        damage = d;
        StartCoroutine("GoToTarget");
    }

    IEnumerator GoToTarget()
    {
        Vector3 direction = target - transform.position;
        direction.Normalize();
        while (transform.position != target)
        {
            if (distanceTravelled > range)
            {
                Destroy(this.gameObject);
            }
            transform.position += direction * speed * Time.deltaTime;
            distanceTravelled += (direction * speed * Time.deltaTime).magnitude;
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.LogWarning("hit enemy " + damage);
            //Debug.Log(other.gameObject + " " + other.gameObject.tag);
            other.GetComponent<Stats>().SetHealth(other.GetComponent<Stats>().GetHealth() - damage);
        }

        //if (other.gameObject.tag != "Player")
        //{
        //    Destroy(this.gameObject);
        //}
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
