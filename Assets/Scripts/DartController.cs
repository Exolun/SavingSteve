using UnityEngine;
using System.Collections;
using System;

public class DartController : MonoBehaviour {

    public Vector3 Direction;
    public float Velocity;
    public float LifetimeSeconds;

    DateTime startTime;

	void Start () {
        startTime = DateTime.Now;
	}
	
	void Update () {
        var timeElapsed = DateTime.Now - startTime;

        if(timeElapsed > TimeSpan.FromSeconds(this.LifetimeSeconds))
        {
            Destroy(this.gameObject);
        }
        else
        {
            var movement = Direction * Velocity * Time.deltaTime;
            this.gameObject.transform.position = this.gameObject.transform.position + movement;
        }
	}
}
