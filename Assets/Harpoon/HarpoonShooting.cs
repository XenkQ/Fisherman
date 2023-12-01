using UnityEngine;

public class HarpoonShooting : MonoBehaviour
{
    [SerializeField] private float harpoonFunctionsStartDelay = 0.2f;
    [SerializeField] private float poolOutSpeed = 8f;
    [SerializeField] private float minDistanceForHarpoonDraw = 0.2f;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform lineStartPoint;
    [SerializeField] private HarpoonProjectile harpoonProjectile;
    private bool missileIsLoaded = true;
    private bool projectileIsToFar = false;
    private Rigidbody projectileRb;
    private LineRenderer projectileLineRenderer;
    private PointsCounter pointsCounter;
    private ClickToStart clickToStart;

    [Header("Animations")]
    private Animator animator;
    private bool canPlayShootAnim = true;

    [Header("Sounds")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;

    private void Awake()
    {
        pointsCounter = GameObject.FindGameObjectWithTag("PointsCounter").GetComponent<PointsCounter>();
        projectileRb = harpoonProjectile.GetComponent<Rigidbody>();
        projectileLineRenderer = harpoonProjectile.GetComponent<LineRenderer>();
        harpoonProjectile.DisableColliders();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        clickToStart = GameObject.FindGameObjectWithTag("ClickToStart").GetComponent<ClickToStart>();
    }

    void Update()
    {
        if (!clickToStart.canDisplay)
        {
            if (projectileLineRenderer.positionCount > 0)
            {
                projectileLineRenderer.SetPosition(0, lineStartPoint.position);
                projectileLineRenderer.SetPosition(1, harpoonProjectile.transform.position);
            }

            if (CanShoot())
            {
                Shoot();
            }
            else
            {
                harpoonFunctionsStartDelay -= Time.deltaTime;
            }

            if (HarpoonIsToFar())
            {
                projectileIsToFar = true;
            }

            if (CanPollOut())
            {
                harpoonProjectile.DisableColliders();
                PullOutHarpoon();
            }
        }
    }

    private bool CanPollOut()
    {
        return harpoonProjectile.PlayerHitFish() || harpoonProjectile.PlayerHitObstacle() || projectileIsToFar;
    }

    private bool HarpoonIsToFar()
    {
        return Vector3.Distance(shootingPoint.position, harpoonProjectile.transform.position) >= harpoonProjectile.maxDistanceToReturn;
    }

    private bool CanShoot()
    {
        return Input.GetMouseButton(0) && missileIsLoaded == true && harpoonFunctionsStartDelay < 0;
    }

    private void Shoot()
    {
        if (canPlayShootAnim)
        {
            animator.SetTrigger("Shoot");
            audioSource.PlayOneShot(shootSound);
            canPlayShootAnim = false;
        }
        harpoonProjectile.EnableColliders();
        missileIsLoaded = false;
        projectileRb.isKinematic = false;
        projectileLineRenderer.positionCount = 2;
        harpoonProjectile.transform.parent = null;
        harpoonProjectile.MoveProjectileForward();
    }

    private void PullOutHarpoon()
    {
        if (Vector3.Distance(shootingPoint.position, harpoonProjectile.transform.position) >= minDistanceForHarpoonDraw)
        {
            projectileRb.isKinematic = true;
            harpoonProjectile.transform.position = Vector3.MoveTowards(harpoonProjectile.transform.position, shootingPoint.position,
                poolOutSpeed * Time.deltaTime);
        }
        else
        {
            SendPointsForCurrentCaughtFish();
            harpoonProjectile.GetCaughtFish().DisableFishProcess();
            harpoonProjectile.RestartProjectile();
            RestartHarpoonShooting();
        }
    }

    private void RestartHarpoonShooting()
    {
        projectileIsToFar = false;
        projectileLineRenderer.positionCount = 0;
        harpoonProjectile.transform.rotation = shootingPoint.rotation;
        harpoonProjectile.transform.position = shootingPoint.position;
        harpoonProjectile.transform.parent = shootingPoint;
        harpoonProjectile.DisableColliders();
        projectileRb.isKinematic = true;
        missileIsLoaded = true;
        canPlayShootAnim = true;
    }

    private void SendPointsForCurrentCaughtFish()
    {
        pointsCounter.IncreasePoints((int)harpoonProjectile.GetCaughtFish().GetFishType());
    }
}
