using UnityEngine;
using System.Collections;
using System;

public class DartLauncherController : MonoBehaviour {
    public GameObject Dart;
    public Vector3 Direction;
    public float RateOfFire;

    public DateTime lastFireTime;

	void Start () {
        lastFireTime = DateTime.Now;    
	}
	
	void Update () {
        var timeElapsed = DateTime.Now - lastFireTime;

        if(timeElapsed > TimeSpan.FromSeconds(RateOfFire))
        {
            var dart = Instantiate(this.Dart);
            dart.transform.position = this.transform.position;
            dart.transform.rotation = this.transform.rotation;
            dart.GetComponent<DartController>().Direction = this.Direction;
            lastFireTime = DateTime.Now;

            GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>().PlaySound("Dart");
        }
    }
}
