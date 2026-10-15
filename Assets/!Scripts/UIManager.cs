using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform framesBar;
    [SerializeField] private GameObject strikeMessage;
    [SerializeField] private GameObject spareMessage;
    [SerializeField] private float messageDuration = 1.5f;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TMP_Text finalScoreText;

    private void Start()
    {
        ResetAllFrames();
        strikeMessage.SetActive(false);
        spareMessage.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    public void ResetAllFrames()
    {
        for (int i = 0; i < framesBar.childCount; i++)
        {
            FrameUI frame = framesBar.GetChild(i).GetComponent<FrameUI>();
            frame.SetFrameNumber(i + 1);
        }
    }

    public void SetFrameValue(int frameIndex, int throwNumber, int score)
    {
        FrameUI frame = framesBar.GetChild(frameIndex).GetComponent<FrameUI>();
        frame.UpdateScore(throwNumber, score);
    }

    public void SetFrameTotal(int frameIndex, int total)
    {
        FrameUI frame = framesBar.GetChild(frameIndex).GetComponent<FrameUI>();
        frame.UpdateTotal(total);
    }

    public void ShowStrike()
    {
        strikeMessage.SetActive(true);
        Invoke(nameof(HideMessages), messageDuration);
    }

    public void ShowSpare()
    {
        spareMessage.SetActive(true);
        Invoke(nameof(HideMessages), messageDuration);
    }

    private void HideMessages()
    {
        strikeMessage.SetActive(false);
        spareMessage.SetActive(false);
    }

    public void ShowGameOver(int finalScore)
    {
        finalScoreText.text = finalScore.ToString();
        gameOverScreen.SetActive(true);
    }
}