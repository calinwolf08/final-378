using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float runSpeed;
    public int direction; // -1 for left, 1 for right

    private static Vector3 standCenter = new Vector3((float)-.2, (float)-.4, 0);
    private static Vector3 standSize = new Vector3((float)1.3, (float)4.3, (float).8);

    private static Vector3 runCenter = new Vector3((float)-.45, (float)-.87, 0);
    private static Vector3 runSize = new Vector3((float)2.2, (float)3.4, (float).8);

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

        } else if (horiz != 0) { //going right or left
            moveSide(horiz);

        } else { //not moving

            if (animator.GetBool("RunningLeftRight")) { //if running
                animator.SetBool("RunningLeftRight", false);
            }

            BoxCollider bc = GetComponent<BoxCollider>();
            bc.center = standCenter;
            bc.size = standSize;
        }
    }

    //for moving player left or right
    void moveSide(float horiz) {
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
            transform.Translate(Vector3.left * runSpeed * Time.deltaTime);
        } else {
            transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
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