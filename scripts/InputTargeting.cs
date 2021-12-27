using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTargeting : MonoBehaviour
{
    [SerializeField]
    private GameObject selectedPlayer;
    [SerializeField]
    private bool playerChar;
    RaycastHit hit;

    void Start()
    {
        selectedPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // targeting
        if(Input.GetMouseButtonDown(1))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                // if target is targetable
                Debug.Log(hit.collider.gameObject);
                if(hit.collider.gameObject.GetComponent<Targetable>() != null)
                {
                    if(hit.collider.gameObject.GetComponent<Targetable>().GetEnemyType() == Targetable.EnemyType.Minion)
                    {
                        selectedPlayer.GetComponent<Combat>().SetTargetable(hit.collider.gameObject);
                    }
                }
                else if(hit.collider.gameObject.GetComponent<Targetable>() == null)
                {
                    selectedPlayer.GetComponent<Combat>().SetTargetable(null);
                }
            }
        }
    }
}
