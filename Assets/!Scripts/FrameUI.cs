using UnityEngine;
using TMPro;

public class FrameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text frameNumberText;
    [SerializeField] private TMP_Text throw1Text;
    [SerializeField] private TMP_Text throw2Text;
    [SerializeField] private TMP_Text throw3Text;
    [SerializeField] private TMP_Text totalText;

    public void UpdateScore(int throwNumber, int score)
    {
        if (throwNumber == 1)
        {
            throw1Text.text = score.ToString();
        }
        else if (throwNumber == 2)
        {
            throw2Text.text = score.ToString();
        }
        else if (throwNumber == 3)
        {
            throw3Text.text = score.ToString();
        }
    }

    public void UpdateTotal(int total)
    {
        totalText.text = total.ToString();
    }

    public void SetFrameNumber(int frameNumber)
    {
        frameNumberText.text = frameNumber.ToString();
        throw1Text.text = "";
        throw2Text.text = "";
        totalText.text = "";

        if (throw3Text != null)
        {
            throw3Text.text = "";
        }
    }
}