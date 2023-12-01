using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timerValue = 60f;
    private bool isTimeDecreasing = false;
    private bool isEndOfTime = false;

    private void Start()
    {
        SetTimerTextRelatedToValue();
    }

    private void Update()
    {
        if (CanDecreaseTime())
        {
            StartCoroutine(TimeDecreasingProcess());
        }

        if (int.Parse(timerText.text) <= 0)
        {
            isEndOfTime = true;
        }
    }


    private bool CanDecreaseTime()
    {
        return isTimeDecreasing == false && timerValue > 0;
    }

    private IEnumerator TimeDecreasingProcess()
    {
        isTimeDecreasing = true;
        timerValue--;
        yield return new WaitForSeconds(1);
        SetTimerTextRelatedToValue();
        isTimeDecreasing = false;
    }

    private void SetTimerTextRelatedToValue()
    {
        timerText.text = timerValue.ToString();
    }

    public bool IsEndOfTime()
    {
        return isEndOfTime;
    }
}
