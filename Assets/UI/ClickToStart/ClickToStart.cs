using UnityEngine;

public class ClickToStart : MonoBehaviour
{
    [SerializeField] private GameObject clickToStartContent;
    public bool canDisplay = true;

    private void Awake()
    {
        canDisplay = true;
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (CanStartGame())
        {
            StartGame();
        }
    }

    private bool CanStartGame()
    {
        return canDisplay && Input.GetMouseButton(0);
    }

    private void StartGame()
    {
        canDisplay = false;
        clickToStartContent.SetActive(false);
        Time.timeScale = 1;
    }
}
