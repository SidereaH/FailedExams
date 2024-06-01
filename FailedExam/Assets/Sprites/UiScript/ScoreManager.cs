using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int kills;
    public int gold;
    [SerializeField] GameObject soundRampage;
    [SerializeField] int time, castTime;

    // Start is called before the first frame update

    private void Start()
    {
        if (PlayerPrefs.HasKey("kills"))
        {
            kills = PlayerPrefs.GetInt("kills");

        }
        if(PlayerPrefs.HasKey("gold")){
            gold = PlayerPrefs.GetInt("gold");
  
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (kills == 5)
        {
            if (time == 0)
            {
                time = castTime;
                GameObject _temp = Instantiate(soundRampage, transform.position, Quaternion.identity);
                //_temp.GetComponent<AudioSource>().Play();
                Destroy(_temp, 4);
            }
        }
    }
    
    public int addKill()
    {

        return kills++;

    }
    public int addGold(int goldCost)
    {
        return gold+=goldCost;
    }
    public void validateKill()
    {
        //kills--;
    }
}
