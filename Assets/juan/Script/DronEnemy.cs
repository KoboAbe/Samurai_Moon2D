using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronEnemy : MonoBehaviour
{
    [SerializeField] private float speed, limit;
    public float speedRun;
    [SerializeField] private bool moveRight;
    [SerializeField] private Transform groundController;
    [SerializeField] private RangoEnemy rangoVision;
    [SerializeField] private RangoEnemy rangoVisionBack;
    [SerializeField] private HitEnemy2D hit;
   

    public GameObject bullet;
    public Transform bulletPos;
    private float coldDown;


    public SamuraiController samurai;

    public Enemy2D damage;

    public Animator ani;

    public Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        samurai = GameObject.FindFirstObjectByType<SamuraiController>();

    }

    private void FixedUpdate()
    {
        coldDown += Time.fixedDeltaTime;


        if (!rangoVision.visto)
        {


            rb.velocity = new Vector2(speed, rb.velocity.y);
            ani.SetBool("walk", true);
            ani.SetBool("run", false);
            ani.SetBool("attack", false);

        }
        else if (rangoVision.visto)
        {
            rb.velocity = new Vector2(speedRun, rb.velocity.y);
            ani.SetBool("walk", false);
            ani.SetBool("run", true);
            ani.SetBool("attack", false);

            coldDown += Time.fixedDeltaTime;
            if (coldDown > 1)
            {
                Debug.Log(coldDown);
                coldDown = 0;
                GunShoot();
            }

        }

        if (hit.hit)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ani.SetBool("walk", false);
            ani.SetBool("run", false);
            ani.SetBool("attack", true);
        }
        else
        {
            ani.SetBool("attack", false);
        }




        RaycastHit2D groundInfo = Physics2D.Raycast(groundController.position, Vector2.down, limit);

        if (groundInfo == false)
        {
            //Girar
            Girar();

        }
        if (rangoVisionBack.visto)
        {
            Girar();
        }

        DistaceFromPlayer();
    }

    private void GunShoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    private void Girar()
    {
        moveRight = !moveRight;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        speed *= -1;
        speedRun *= -1;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundController.transform.position, groundController.transform.position + Vector3.down * limit);
    }

    public void StartAttack()
    {
        damage.EnabledBox(true);
    }

    public void EndedAttack()
    {
        damage.EnabledBox(false);
    }

    public void DistaceFromPlayer()
    {
        var distance = Vector2.Distance(gameObject.transform.position, samurai.gameObject.transform.position);
        // Debug.Log(distance);
    }
}
