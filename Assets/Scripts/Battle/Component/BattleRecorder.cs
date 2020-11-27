using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRecorder
{
    public int magicThisGame = 0;
    public int magicThisTurn = 0;

    public int overLoadThisGame = 0;
    public int overLoadThisTurn = 0;

    

    public void ResetTurn()
    {
        magicThisTurn = 0;
        overLoadThisTurn = 0;
    }
    public void Magic(int magicNum)
    {
        magicThisGame += magicNum;
        magicThisTurn += magicNum;
    }
    public void OverLoad(int overLoadNum)
    {
        overLoadThisGame += overLoadNum;
        overLoadThisTurn += overLoadNum;
    }

}
