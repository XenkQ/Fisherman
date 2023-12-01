using UnityEngine;
using TMPro;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private int maxNumberOfPoints = 99999999;
    private Animator animator;
    private int points = 0;

    [Header("Audio")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip pointIncreaseSound;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public int GetPoints()
    {
        return points;
    }

    public void IncreasePoints(int value)
    {
        int newValue = points + value;
        if(newValue <= maxNumberOfPoints)
        {
            if(newValue > points) { 
                animator.SetTrigger("GettingPoints");
                audioSource.PlayOneShot(pointIncreaseSound);
            }
            points += value;
            pointsText.text = points.ToString();
        }
    }
}
