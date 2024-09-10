using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] levelParts;
    [SerializeField] private Vector3 nextPartPosition;

    
    [SerializeField] private float distanceToSpawn;
    [SerializeField] private float distanceToDelete;
    [SerializeField] private Transform playerTransform;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GeneratePlatform();
        DeletePlatform();
    }

    private void GeneratePlatform()
    {
        if (Vector2.Distance(playerTransform.position,nextPartPosition) < distanceToSpawn)
        {
            Transform part = levelParts[Random.Range(0, levelParts.Length)];

            Vector2 newPosition = new Vector2(nextPartPosition.x - part.Find("StartPoint").position.x, 0); // to make sure that we spawned levels at 0 on the y axis

            Transform newPart = Instantiate(part, newPosition, Quaternion.identity, transform);

            nextPartPosition = newPart.Find("EndPoint").position;
        }
    }

    private void DeletePlatform()
    {
        if(transform.childCount > 0)
        {
            Transform partToDelete = transform.GetChild(0);
            if(Vector2.Distance(playerTransform.position,partToDelete.position) > distanceToDelete)
            {
                Destroy(partToDelete.gameObject);
            }
        }


    }

}
