using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using Assets.Scripts;

public class PlayerController : MonoBehaviour {

    public float PlayerSpeed;

    private Vector3 direction = new Vector3();    
    private GameObject closestSphere;
    private float sphereCollisionDistance = 30;
    private const float minYPosition = -384;
    private Animator anim;

    void Start () {
        this.anim = GetComponent<Animator>();
        this.direction = Vector3.right;        
        this.acquireDirection();

        if(GameManager.CurrentState() == GameStates.Intro)
        {
            this.anim.Stop();
        }
	}

    private void acquireDirection()
    {
        GameObject closestSphere = null;
        float shortestDistance = float.MaxValue;
        var trailSpheres = GameObject.FindGameObjectsWithTag("TrailSphere").Where(sp => {
            return sp.transform.position.y > minYPosition;
        });

        foreach (var sphere in trailSpheres)
        {
            float dist = Math.Abs(Vector3.Distance(sphere.transform.position, this.transform.position));
            if(dist < shortestDistance)
            {
                shortestDistance = dist;
                closestSphere = sphere;
            }
        }

        if(closestSphere != null)
        {
            this.closestSphere = closestSphere;
            this.direction = (this.closestSphere.transform.position - this.transform.position + new Vector3(0, 120, 0)).normalized;
        }
    }

    void Update () {        
        if(GameManager.CurrentState() == GameStates.Paused || GameManager.CurrentState() == GameStates.Dead || GameManager.CurrentState() == GameStates.Intro)
        {
            return;
        }

        if(this.closestSphere != null)
        {
            float distance = Math.Abs(Vector3.Distance(this.closestSphere.transform.position, this.transform.position - new Vector3(0, 120, 0)));
            if(distance < sphereCollisionDistance)
            {
                Destroy(closestSphere);                
            }
        }
        else if (this.closestSphere == null)
        {
            this.acquireDirection();
        }

        this.transform.Translate(this.direction * PlayerSpeed  * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
        if (GameManager.CurrentState() == GameStates.Paused || GameManager.CurrentState() == GameStates.Dead)
        {
            return;
        }

        if (other.gameObject.tag == "Hazard")
        {
            this.anim.Play("Death");
            GameManager.PlayerDied();
        }
        else if (other.gameObject.CompareTag("Respawn"))
        {
            GameManager.PlayerCompletedLevel();
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            GameManager.PlayerWins();
        }
    }
}
