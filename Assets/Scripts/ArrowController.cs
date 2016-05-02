using UnityEngine;
using System.Collections;
using System;

public class ArrowController : MonoBehaviour {

    public Vector3 ArrowDirection;
    public GameObject[] ConnectedObjects;    
    public GameObject InverseArrow;
    public GameObject Halo;    
    public float MaxDistance;

    private ArrowController inverseArrowController;    
    const float stepAmount = 50;
    private int clickCount = 0;

    const float moveVelocity = 400;
    private bool isPressed = false;
    private float totalDistance = 0;

    public void Start()
    {
        if(ConnectedObjects == null || ConnectedObjects.Length == 0)
        {
            if(InverseArrow != null)
            {
                this.inverseArrowController = InverseArrow.GetComponent<ArrowController>();
                this.ConnectedObjects = this.inverseArrowController.ConnectedObjects;
            }
        }
    }

    public void Pressed()
    {
        this.isPressed = true;
        if(this.Halo != null)
        {
            this.Halo.SetActive(true);
        }
    }

    public void Released()
    {
        this.isPressed = false;
        if (this.Halo != null)
        {
            this.Halo.SetActive(false);
        }
    }

    public void Update()
    {
        if (ConnectedObjects == null)
        {
            throw new Exception("ConnectedObjects must be defined for arrow.");
        }

        if (!this.isPressed || totalDistance > MaxDistance)
        {
            return;
        }        
        
        if (this.totalDistance < MaxDistance)
        {
            var translation = (this.ArrowDirection * moveVelocity * Time.fixedDeltaTime);
            foreach (var obj in ConnectedObjects)
            {
                if (obj == null)
                    continue;

                obj.SendMessage("Shift", translation);
            }

            if (InverseArrow != null)
            {
                InverseArrow.SendMessage("ReduceDistance", translation.magnitude);
            }

            this.totalDistance += translation.magnitude;
        }
    }

    public void ReduceDistance(float amount)
    {
        this.totalDistance -= amount;
    }
}
