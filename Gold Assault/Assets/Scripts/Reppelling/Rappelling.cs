using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rappelling : MonoBehaviour
{
    [SerializeField] Vector2 minAreas = new Vector2(-1, -1);
    [SerializeField] Vector2 maxAreas = new Vector2(1, 1);
    [SerializeField] GameObject box;

    public bool atWindow = false;
    public bool outsideTrigger = false;

    void OnDrawGizmos()
    {
        float posX = transform.localPosition.x;
        float posY = transform.localPosition.y;
        float posZ = transform.localPosition.z;


        box.GetComponent<BoxCollider>().center = new Vector3(Utils.GetPointBetweenTwoNumbers(posX + maxAreas.x, posX + minAreas.x), Utils.GetPointBetweenTwoNumbers(posY + maxAreas.y, posY + minAreas.y), posZ);
        box.GetComponent<BoxCollider>().size = new Vector3(maxAreas.x - minAreas.x, maxAreas.y - minAreas.y, 1);
        //     Gizmos.DrawSphere(new Vector3(Utils.GetPointBetweenTwoNumbers(posX + maxAreas.x, posX + minAreas.x), posY + maxAreas.y, posZ), 0.5f);
        //     Gizmos.DrawSphere(new Vector3(posX + maxAreas.x, Utils.GetPointBetweenTwoNumbers(posY + maxAreas.y, posY + minAreas.y), posZ), 0.5f);
        //     Gizmos.DrawSphere(new Vector3(Utils.GetPointBetweenTwoNumbers(posX + maxAreas.x, posX + minAreas.x), posY + minAreas.y, posZ), 0.5f);
        //     Gizmos.DrawSphere(new Vector3(posX + minAreas.x, Utils.GetPointBetweenTwoNumbers(posY + maxAreas.y, posY + minAreas.y), posZ), 0.5f);

    }

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController PC = other.GetComponent<PlayerController>();
            PC.canRappel = true;
            PC.rappellingObject = gameObject;
            PC.lowerLimit = minAreas;
            PC.upperLimit = maxAreas;
            //PC.targPos = box;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController PC = other.GetComponent<PlayerController>();
            PC.canRappel = false;
            //PC.rappellingObject = null;
        }
    }
}
