using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : Trap
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform[] movePoints;
    private int i; //stands for movePoint index


    private void Start()
    {
        transform.position = movePoints[0].position;
    }

    private void Update()
    {
        MoveController();
        
    }

    private void MoveController()
    {
        if (Vector2.Distance(transform.position, movePoints[i].position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoints[i].position, speed * Time.deltaTime);
        }
        else
        {
            i++;
        }

      
        if (i == 2)
            i = 0;
    }

}
