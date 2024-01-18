﻿namespace GuessingGame.GameLogic
{
    public class GuessResult
    {
        public bool IsWin { get; }
        public string DistanceToWin { get; }

        public GuessResult(bool isWin, string distanceToWin)
        {
            IsWin = isWin;
            DistanceToWin = distanceToWin;
        }
    }
}