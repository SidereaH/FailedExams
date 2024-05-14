using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int kills;
    public int gold;

    // Start is called before the first frame update
    

    // Update is called once per frame
    public int addKill()
    {
        return kills++;

    }
    public int addGold()
    {
        return gold++;
    }
    public void validateKill()
    {
        kills--;
    }
}
