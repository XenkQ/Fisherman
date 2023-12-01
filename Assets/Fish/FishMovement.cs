using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 2f;
    private bool canMove = true;

    private void Update()
    {
        if(canMove)
        {
            MoveFish();
        }
    }

    public void StopFishMovement()
    {
        rb.isKinematic = true;
        canMove = false;
    }

    public void ResumeFishMovement()
    {
        rb.isKinematic = false;
        canMove = true;
    }

    private void MoveFish()
    {
        rb.velocity = (-transform.right * Time.deltaTime).normalized * speed;
    }
}
