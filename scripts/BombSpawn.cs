using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawn : MonoBehaviour
{
    public Vector3 target;
    private float damage;

    private float duration;

    public void SetValues(Vector3 v, float d, float dur)
    {
        target = v;
        damage = d;
        duration = dur;
        StartCoroutine("SpawnBomb");
    }

    IEnumerator SpawnBomb()
    {
        Vector3 clickPosition = -Vector3.one;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            clickPosition = hit.point;
        }

        transform.position = clickPosition;

        yield return new WaitForEndOfFrame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<Stats>().SetHealth(other.GetComponent<Stats>().GetHealth() - damage);
        }

        if (other.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update() { }
}
