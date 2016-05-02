using UnityEngine;
using System.Collections;

public class ArrowPositioner : MonoBehaviour {    
	void Start () {
	
	}
	
	void Update () {
        var desiredX = Screen.width * .45f;
        var desiredY = Screen.height * .45f;
        var pos = Camera.main.ScreenToWorldPoint(new Vector3(desiredX, desiredY));

        this.gameObject.transform.position = new Vector3(pos.x, pos.y, gameObject.transform.position.z);
	}
}
