﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed, lean;
    public int direction; // -1 for left, 1 for right

    public Vector3 standCenter;// = new Vector3((float)-.2, (float)-.4, 0);
    public Vector3 standSize;// = new Vector3((float)1.3, (float)4.3, (float).8);

    public Vector3 runCenter;// = new Vector3((float)-.45, (float)-.87, 0);
    public Vector3 runSize;// = new Vector3((float)2.2, (float)3.4, (float).8);

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        
        if (vert != 0) { //going in or out
            moveInOut(vert, horiz);
        } else if (horiz != 0) { //going right or left
            this.transform.localEulerAngles = new Vector3((float)0, (float)0, (float)0);
            moveHoriz(horiz);
        } else { //not moving

            if (animator.GetBool("RunningLeftRight")) { //if running
                animator.SetBool("RunningLeftRight", false);

                BoxCollider bc = GetComponent<BoxCollider>();
                bc.center = standCenter;
                bc.size = standSize;
            }

            this.transform.localEulerAngles = new Vector3((float)0, (float)0, (float)0);
        }
    }

    //for moving player in and out
    void moveInOut(float vert, float horiz) {
        if (!animator.GetBool("RunningLeftRight")) { //if not already running
            animator.SetBool("RunningLeftRight", true);

            BoxCollider bc = GetComponent<BoxCollider>();
            bc.center = runCenter;
            bc.size = runSize;
        }

        //move in or out and rotate
        if (vert < 0) {
            this.transform.localEulerAngles = new Vector3((float)-lean, (float)0, (float)0);
            transform.Translate(Vector3.back * speed * Time.deltaTime, transform.parent);
        }
        else {
            this.transform.localEulerAngles = new Vector3((float)lean, (float)0, (float)0);
            transform.Translate(Vector3.forward * speed * Time.deltaTime, transform.parent);
        }

        //if also moving left or right
        if (horiz != 0) {
            moveHoriz(horiz);
        }
    }

    //for moving player left or right
    void moveHoriz(float horiz) {
        if (!animator.GetBool("RunningLeftRight")) { //if not already running
            animator.SetBool("RunningLeftRight", true);

            BoxCollider bc = GetComponent<BoxCollider>();
            bc.center = runCenter;
            bc.size = runSize;
        }

        //if direction is not the same as horizontal input
        if ((direction < 0) != (horiz < 0)) {
            FlipAnimation();
        }

        if (direction < 0) {
            transform.Translate(Vector3.left * speed * Time.deltaTime, transform.parent);
        } else {
            transform.Translate(Vector3.right * speed * Time.deltaTime, transform.parent);
        }
    }

    //change direction player is moving
    void FlipAnimation() {
        direction *= -1; //switch direction player is facing

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}