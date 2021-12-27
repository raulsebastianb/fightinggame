using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldowns : MonoBehaviour
{
    Animator anim;
    RaycastHit hit;
    CharacterMovement characterMovement;

    [Header("Ability 1")]
    public Image abilityImage1;
    public float cooldown1 = 5;
    bool isCooldown1 = false;
    public KeyCode ability1;
    bool canShoot = true;
    public GameObject projectilePrefab;
    public Vector3 projectileSpawnOffset;

    // Ability 1 Input Variables
    Vector3 position;
    public Canvas ability1Canvas;
    public Image shot;
    public Transform player;

    [Header("Ability 2")]
    public Image abilityImage2;
    public float cooldown2 = 10;
    bool isCooldown2 = false;
    public KeyCode ability2;
    bool canBomb = true;
    public GameObject bombPrefab;

    // Ability 2 Input Variables
    public Image targetCircle;
    public Image indicatorRangeCircle;
    public Canvas ability2Canvas;
    private Vector3 positionUp;
    public float maxAbility2Distance;


    // Start is called before the first frame update
    void Start()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        shot.GetComponent<Image>().enabled = false;
        targetCircle.GetComponent<Image>().enabled = false;
        indicatorRangeCircle.GetComponent<Image>().enabled = false;

        characterMovement = GetComponent<CharacterMovement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Ability1();
        Ability2();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Ability 1 Inputs
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        // Ability 2 Inputs
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider.gameObject != this.gameObject)
            {
                positionUp = new Vector3(hit.point.x, 10f, hit.point.z);
                position = hit.point;
            }
        }

        // Ability 1 Canvas Inputs
        var ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit1 = new RaycastHit();
        Vector3 mousePosition =  Vector3.zero;
        if (Physics.Raycast(ray1, out hit1))
        {
            mousePosition = hit1.point;
        }

        //Quaternion transRot = Quaternion.LookRotation(position - mousePosition);
        //transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, 0);
        //ability1Canvas.transform.rotation = Quaternion.Lerp(ability1Canvas.transform.rotation, transRot, 0f);
        //Debug.Log(transRot);
        Vector3 whatevernumipasa = new Vector3(mousePosition.y, mousePosition.x, mousePosition.z);
        ability1Canvas.transform.LookAt(mousePosition);
        ability1Canvas.transform.Rotate(90, 0, 0);
        //ability1Canvas.transform.rotation = new Quaternion(ability1Canvas.transform.rotation.x, ability1Canvas.transform.rotation.y, ability1Canvas.transform.rotation.z, ability1Canvas.transform.rotation.w);

        // Ability 2 Canvas Inputs
        var hitPosDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, maxAbility2Distance);

        var newHitPos = transform.position + hitPosDir * distance;
        newHitPos.y = 1f;
        ability2Canvas.transform.position = (newHitPos);
    }

    void Ability1()
    {
        if(Input.GetKey(ability1) && isCooldown1 == false)
        {
            shot.GetComponent<Image>().enabled = true;

            // Disable other UI elements
            indicatorRangeCircle.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;
        }

        if(shot.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {
            // Character rotation
            Quaternion rotationToLookAt = Quaternion.LookRotation(position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref characterMovement.rotateVelocity, 0);

            transform.eulerAngles = new Vector3(0, rotationY, 0);

            characterMovement.GetAgent().SetDestination(transform.position);
            characterMovement.GetAgent().stoppingDistance = 0;

            if (canShoot)
            {
                isCooldown1 = true;
                abilityImage1.fillAmount = 1;

                StartCoroutine(corShot(position));
            }

        }

        if(isCooldown1)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            shot.GetComponent<Image>().enabled = false;

            if(abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown1 = false;
            }
        }
    }

    void Ability2()
    {
        if (Input.GetKey(ability2) && isCooldown2 == false)
        {
            indicatorRangeCircle.GetComponent<Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled = true;

            // Disable other UI elements
            shot.GetComponent<Image>().enabled = false;
        }

        if (targetCircle.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {
            if (canBomb)
            {
                isCooldown2 = true;
                abilityImage2.fillAmount = 1;

                StartCoroutine(corBomb(position));
            }
        }

        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            indicatorRangeCircle.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;

            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }

    IEnumerator corShot(Vector3 pos)
    {
        canShoot = false;
        anim.SetTrigger("Shot");

        yield return new WaitForSeconds(0.5f);

        SpawnShot(pos);
    }

    public void SpawnShot(Vector3 pos)
    {
        projectileSpawnOffset = transform.forward * 0.5f;
        canShoot = true;
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnOffset+transform.position, transform.rotation);
        projectile.GetComponent<ProjectileMovement>().SetValues(pos, 30f, 10f, 10);
    }

    IEnumerator corBomb(Vector3 pos)
    {
        canBomb = false;
        anim.SetTrigger("Bomb");
        yield return new WaitForSeconds(0.5f);

        SpawnBomb(pos);
    }

    public void SpawnBomb(Vector3 pos)
    {
        canBomb = true;
        GameObject bomb = Instantiate(bombPrefab, transform.position, transform.rotation);
        bomb.GetComponent<BombSpawn>().SetValues(pos, 10, 0.2f);
    }
}
