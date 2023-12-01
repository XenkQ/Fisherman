using UnityEngine;

public class HarpoonProjectile : MonoBehaviour
{
    [SerializeField] private BoxCollider[] colliders;
    [SerializeField] private float speed = 8f;
    public float maxDistanceToReturn = 10f;
    private bool canGrab = true;
    private bool playerHitFish = false;
    private bool playerHitObstacle = false;
    private Fish currentHitFish = new Fish(FishType.None);
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Fish") && canGrab)
        {
            canGrab = false;
            currentHitFish = collision.transform.GetComponent<Fish>();
            currentHitFish.GrabProcess(transform);
            rb.isKinematic = true;
            playerHitFish = true;
        }

        if (collision.transform.CompareTag("Obstacle"))
        {
            rb.isKinematic = true;
            playerHitObstacle = true;
        }
    }

    public void DisableColliders()
    {
        foreach(BoxCollider collider in colliders)
        {
            collider.enabled = false;
        }
    }

    public void EnableColliders()
    {
        foreach (BoxCollider collider in colliders)
        {
            collider.enabled = true;
        }
    }

    public void MoveProjectileForward()
    {
        rb.velocity = (-transform.up * Time.deltaTime).normalized * speed;
    }

    public void RestartProjectile()
    {
        canGrab = true;
        playerHitFish = false;
        playerHitObstacle = false;
        currentHitFish = new Fish(FishType.None);
    }

    public Fish GetCaughtFish()
    {
        if (PlayerHitFish()) { return currentHitFish; }
        else { return new Fish(FishType.None); }
    }

    public bool PlayerHitFish()
    {
        return playerHitFish;
    }

    public bool PlayerHitObstacle()
    {
        return playerHitObstacle;
    }
}