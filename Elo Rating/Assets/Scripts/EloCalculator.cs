using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EloCalculator {

    public static int startingElo = 1000;


    private static float mod = 400;
    //Higher K means less volatile
    private static float K = 32;
    private static float pointsPerWin = 1.0f;
    private static float pointsPerDraw = 0.0f;
    private static float pointsPerLoss = 0.0f;



    public static void CalculateElo(int winnerID, int loserID)
    {
        int WinnerElo = DatabaseController.GetElo(winnerID);
        int LoserElo = DatabaseController.GetElo(loserID);

        float expectedScoreA = 1 / (1 + Mathf.Pow(10, (LoserElo - WinnerElo) / mod));
        float expectedScoreB = 1 / (1 + Mathf.Pow(10, (WinnerElo - LoserElo) / mod));

        int winnerNewElo = CalculationOfElo(WinnerElo, K, pointsPerWin, expectedScoreA);
        int loserNewElo = CalculationOfElo(LoserElo, K, pointsPerLoss, expectedScoreB);

        DatabaseController.SetElo(winnerID, winnerNewElo);
        DatabaseController.SetElo(loserID, loserNewElo);

    }

    public static int CalculationOfElo(int elo, float K, float points, float expectedScore)
    {
        return System.Convert.ToInt16(elo + K * (points - expectedScore));
    }

}
