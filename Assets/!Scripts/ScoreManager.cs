using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;

    private int[] frameScores = new int[10];
    private int currentFrame = 0;
    private int currentThrow = 1;
    private int totalScore = 0;

    private bool hasSpareBonus = false;
    private int strikeBonusThrows = 0;

    public void SetFrameScore(int fallenPins)
    {
        uiManager.SetFrameValue(currentFrame, currentThrow, fallenPins);

        if (hasSpareBonus)
        {
            frameScores[currentFrame - 1] += fallenPins;
            hasSpareBonus = false;
        }

        if (strikeBonusThrows > 0)
        {
            frameScores[currentFrame - 1] += fallenPins;
            strikeBonusThrows--;
        }

        frameScores[currentFrame] += fallenPins;
        Debug.Log("Frame " + (currentFrame + 1) + ", throw " + currentThrow + ": " + fallenPins + " pins");

        if (currentFrame == 9)
        {
            ScoreLastFrame(fallenPins);
            return;
        }

        if (currentThrow == 1)
        {
            if (fallenPins == 10)
            {
                Debug.Log("Strike!");
                uiManager.ShowStrike();
                strikeBonusThrows = 2;
                NextFrame();
            }
            else
            {
                currentThrow++;
            }
        }
        else
        {
            if (frameScores[currentFrame] == 10)
            {
                Debug.Log("Spare!");
                uiManager.ShowSpare();
                hasSpareBonus = true;
            }

            NextFrame();
        }
    }

    private void ScoreLastFrame(int fallenPins)
    {
        if (currentThrow == 1)
        {
            if (fallenPins == 10)
            {
                Debug.Log("Strike!");
                uiManager.ShowStrike();
                gameManager.ResetAllPins();
            }

            currentThrow++;
        }
        else if (currentThrow == 2)
        {
            int firstThrow = frameScores[currentFrame] - fallenPins;

            if (firstThrow == 10)
            {
                if (fallenPins == 10)
                {
                    Debug.Log("Strike!");
                    uiManager.ShowStrike();
                    gameManager.ResetAllPins();
                }

                currentThrow++;
            }
            else if (frameScores[currentFrame] == 10)
            {
                Debug.Log("Spare!");
                uiManager.ShowSpare();
                gameManager.ResetAllPins();
                currentThrow++;
            }
            else
            {
                NextFrame();
            }
        }
        else
        {
            if (fallenPins == 10)
            {
                Debug.Log("Strike!");
                uiManager.ShowStrike();
            }

            NextFrame();
        }
    }

    private void NextFrame()
    {
        gameManager.ResetAllPins();
        Debug.Log("Frame " + (currentFrame + 1) + " done. Total so far: " + CalculateTotalScore());

        currentFrame++;
        currentThrow = 1;
    }

    public int CalculateTotalScore()
    {
        totalScore = 0;
        foreach (int score in frameScores)
        {
            totalScore += score;
        }
        return totalScore;
    }

    public int GetCurrentFrame()
    {
        return currentFrame;
    }

    public int[] GetFrameTotals()
    {
        int[] totals = new int[frameScores.Length];
        int runningTotal = 0;
        for (int i = 0; i < frameScores.Length; i++)
        {
            runningTotal += frameScores[i];
            totals[i] = runningTotal;
        }
        return totals;
    }
}