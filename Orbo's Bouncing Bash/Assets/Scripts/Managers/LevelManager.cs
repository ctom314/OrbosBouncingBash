using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<string> levels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string chooseLevel()
    {
        // Randomly choose a level
        int index = UnityEngine.Random.Range(0, levels.Count);
        return levels[index];
    }

    public string chooseLevelNoRepeat(string curLevel)
    {
        List<string> noRepeatLevels = new List<string>();

        // Randomly choose a level other than the current level
        foreach (string level in levels)
        {
            if (level != curLevel)
            {
                noRepeatLevels.Add(level);
            }
        }

        int index = UnityEngine.Random.Range(0, noRepeatLevels.Count);
        return noRepeatLevels[index];
    }
}
