using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Pin[] pins;
    [SerializeField] private float settleDelay = 2f;

    public void SetNextThrow()
    {
        Invoke(nameof(ProcessThrow), settleDelay);
    }

    private void ProcessThrow()
    {
        int fallen = CountFallenPins();


        scoreManager.SetFrameScore(fallen);

        int currentFrame = scoreManager.GetCurrentFrame();
        int[] frameTotals = scoreManager.GetFrameTotals();
        for (int i = 0; i < currentFrame; i++)
        {
            uiManager.SetFrameTotal(i, frameTotals[i]);
        }

        if (currentFrame >= frameTotals.Length)
        {
            uiManager.ShowGameOver(scoreManager.CalculateTotalScore());
            return;
        }

        playerController.StartThrow();
    }

    public int CountFallenPins()
    {
        int fallenCount = 0;
        foreach (Pin pin in pins)
        {
            if (pin.gameObject.activeSelf && pin.HasFallen())
            {
                fallenCount++;
                pin.gameObject.SetActive(false);
            }
        }
        return fallenCount;
    }

    public void ResetAllPins()
    {
        foreach (Pin pin in pins)
        {
            pin.ResetPin();
        }
    }
}