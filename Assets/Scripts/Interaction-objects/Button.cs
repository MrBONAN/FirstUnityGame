using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interaction_objects;

public class Button : MonoBehaviour
{
    public int turnOn;
    private Animator animator;
    public List<ICallable> objectsToCall = new();

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        animator.SetBool("Pressed", turnOn != 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag is "Player")
        {
            turnOn += 1;
            foreach (var callable in objectsToCall)
                callable.BeginInteraction();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag is "Player")
        {
            foreach (var callable in objectsToCall)
                callable.StayInInteraction();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag is "Player")
        {
            turnOn -= 1;
            foreach (var callable in objectsToCall)
                callable.EndInteractions();
        }
    }
}