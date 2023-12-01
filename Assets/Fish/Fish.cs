using System;
using UnityEngine;

[RequireComponent(typeof(FishMovement))]
public class Fish : MonoBehaviour
{
    [SerializeField] private FishType fishType;
    [SerializeField] private BoxCollider[] colliders;
    [SerializeField] private float protectFromRestartDelay = 0.1f;
    private float startProtectFromRestartDelay;
    public int spawningPointIndex = -1;
    private SpawningPoint spawningPoint;
    private FishMovement fishMovement;
    private Rigidbody rb;
    private Transform maxLeftPoint;
    private Transform maxRightPoint;
    private FishSpawner fishSpawner;
    private Quaternion startRotation;

    private void Awake()
    {
        colliders = GetComponents<BoxCollider>();
        fishMovement = GetComponent<FishMovement>();
        rb = GetComponent<Rigidbody>();
        maxLeftPoint = GameObject.FindGameObjectWithTag("MaxLeftPoint").transform;
        maxRightPoint = GameObject.FindGameObjectWithTag("MaxRightPoint").transform;
        fishSpawner = GameObject.FindGameObjectWithTag("FishSpawner").GetComponent<FishSpawner>();
        startProtectFromRestartDelay = protectFromRestartDelay;
    }

    private void OnEnable()
    {
        protectFromRestartDelay = startProtectFromRestartDelay;
        fishSpawner.BlockCorrespondingSpawningPointsIndexes(spawningPointIndex);
        spawningPoint = transform.parent.GetComponent<SpawningPoint>();
        startRotation = transform.rotation;
        fishMovement.ResumeFishMovement();
        ActiveColliders();
    }

    private void Update()
    {
        FixRotation();
    }

    private void FixedUpdate()
    {
        RestartFishAfterGoingToFar();
    }

    private void FixRotation()
    {
        if(transform.rotation != startRotation)
        {
            transform.rotation = startRotation;
        }
    }

    private void RestartFishAfterGoingToFar()
    {
        if(protectFromRestartDelay < 0)
        {
            if (maxLeftPoint.position.x >= transform.position.x || maxRightPoint.position.x <= transform.position.x)
            {
                fishSpawner.UnblockCorrespondingSpawningPointsIndexes(spawningPointIndex);
                DisableFishProcess();
            }
        }
        else
        {
            protectFromRestartDelay -= Time.deltaTime;
        }
    }

    public Fish(FishType type)
    {
        fishType = type;
    }

    public void DisableFishProcess()
    {
        if (fishType != FishType.None)
        {
            transform.position = spawningPoint.transform.position;
            transform.parent = spawningPoint.transform;
            gameObject.SetActive(false);
        }
    }

    public FishType GetFishType()
    {
        return fishType;
    }

    public void GrabProcess(Transform parent)
    {
        fishSpawner.UnblockCorrespondingSpawningPointsIndexes(spawningPointIndex);
        fishMovement.StopFishMovement();
        MakeKinematic(true);
        ChangeParent(parent);
        DisableColliders();
    }

    private void MakeKinematic(bool value)
    {
        rb.isKinematic = value;
    }

    private void ChangeParent(Transform parent)
    {
        transform.parent = parent;
    }

    private void DisableColliders()
    {
        foreach (BoxCollider collider in colliders)
        {
            collider.enabled = false;
        }
    }

    private void ActiveColliders()
    {
        foreach (BoxCollider collider in colliders)
        {
            collider.enabled = true;
        }
    }
}