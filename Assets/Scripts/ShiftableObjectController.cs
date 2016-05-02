using UnityEngine;
using System.Collections;

public class ShiftableObjectController : MonoBehaviour {

    private const float yMin = -200;

    public void Shift(Vector3 amount)
    {
        Vector3 pos = this.gameObject.transform.position;
        Vector3 newPos = pos + amount;
        this.gameObject.transform.position = newPos;
    }
}
