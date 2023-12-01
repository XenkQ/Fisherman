using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject endMenuContent;
    [SerializeField] private TextMeshProUGUI scoreText;
    private PointsCounter pointsCounter;
    private Timer timer;

    private void Awake()
    {
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        pointsCounter = GameObject.FindGameObjectWithTag("PointsCounter").GetComponent<PointsCounter>();
    }

    private void Update()
    {
        if(timer.IsEndOfTime())
        {
            ActivateEndMenuContentProcess();
        }
    }

    public void ActivateEndMenuContentProcess()
    {
        if(endMenuContent.active == false)
        {
            Time.timeScale = 0;
            endMenuContent.gameObject.SetActive(true);
            scoreText.text = pointsCounter.GetPoints().ToString();
        }
    }

    public void OnPlayAgainButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
