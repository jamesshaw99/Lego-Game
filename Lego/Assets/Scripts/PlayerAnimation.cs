using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    
    protected Animator Animator;
    protected Player Player;
    protected Rigidbody Rigidbody;

    void Awake()
    {
        Animator = GetComponent<Animator>();
        Player = transform.parent.GetComponent<Player>();
        Rigidbody = transform.parent.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Animator.SetFloat("Speed", Mathf.Sign(Vector3.Dot(Rigidbody.velocity, Rigidbody.transform.forward)) * Vector3.Project(Rigidbody.velocity, Rigidbody.transform.forward).magnitude);
        Animator.SetFloat("Side", Mathf.Sign(Vector3.Dot(Rigidbody.velocity, Rigidbody.transform.right)) * Vector3.Project(Rigidbody.velocity, Rigidbody.transform.right).magnitude);
        Animator.SetFloat("Jump", Mathf.Clamp(Player.DistanceFromGround(), 0, 1));
    }
}
