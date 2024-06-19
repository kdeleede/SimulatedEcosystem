// using UnityEngine;
// using System.Collections.Generic;
// using System.IO;

// public class AnimalManager : MonoBehaviour
// {
//     public static AnimalManager Instance { get; private set; }

//     private int animalCount = 0;
//     private List<int> animalCountLog = new List<int>();
//     private List<float> timeLog = new List<float>();

//     private float logInterval = 1.0f; // Log every second
//     private float nextLogTime = 0f;

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject); // Optional: keep the manager alive across scenes
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     private void Update()
//     {
//         if (Time.time >= nextLogTime)
//         {
//             LogAnimalCount();
//             nextLogTime = Time.time + logInterval;
//         }
//     }

//     private void LogAnimalCount()
//     {
//         animalCountLog.Add(animalCount);
//         timeLog.Add(Time.time);
//     }

//     public void AddAnimal()
//     {
//         animalCount++;
//         Debug.Log("Animal added. Current count: " + animalCount);
//     }

//     public void RemoveAnimal()
//     {
//         animalCount--;
//         Debug.Log("Animal removed. Current count: " + animalCount);
//     }

//     public int GetAnimalCount()
//     {
//         return animalCount;
//     }

//     public List<int> GetAnimalCountLog()
//     {
//         return animalCountLog;
//     }

//     public List<float> GetTimeLog()
//     {
//         return timeLog;
//     }

//     public void SaveLogToFile(string filePath)
//     {
//         using (StreamWriter writer = new StreamWriter(filePath))
//         {
//             writer.WriteLine("Time,AnimalCount");
//             for (int i = 0; i < timeLog.Count; i++)
//             {
//                 writer.WriteLine($"{timeLog[i]},{animalCountLog[i]}");
//             }
//         }
//         Debug.Log("Log saved to " + filePath);
//     }

//     private void OnApplicationQuit()
//     {
//         SaveLogToFile(Application.dataPath + "/animal_count_log.csv");
//     }
// }

using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class AnimalManager : MonoBehaviour
{
    public static AnimalManager Instance { get; private set; }

    private int duckCount = 0;
    private int wolfCount = 0;
    
    private List<int> duckCountLog = new List<int>();
    private List<int> wolfCountLog = new List<int>();
    private List<float> timeLog = new List<float>();

    private float logInterval = 1.0f; // Log every second
    private float nextLogTime = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keep the manager alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Time.time >= nextLogTime)
        {
            LogAnimalCounts();
            nextLogTime = Time.time + logInterval;
        }
    }

    private void LogAnimalCounts()
    {
        duckCountLog.Add(duckCount);
        wolfCountLog.Add(wolfCount);
        timeLog.Add(Time.time);
    }

    public void AddDuck()
    {
        duckCount++;
        Debug.Log("Duck added. Current count: " + duckCount);
    }

    public void RemoveDuck()
    {
        duckCount--;
        Debug.Log("Duck removed. Current count: " + duckCount);
    }

    public void AddWolf()
    {
        wolfCount++;
        Debug.Log("Wolf added. Current count: " + wolfCount);
    }

    public void RemoveWolf()
    {
        wolfCount--;
        Debug.Log("Wolf removed. Current count: " + wolfCount);
    }

    public int GetDuckCount()
    {
        return duckCount;
    }

    public int GetWolfCount()
    {
        return wolfCount;
    }

    public List<int> GetDuckCountLog()
    {
        return duckCountLog;
    }

    public List<int> GetWolfCountLog()
    {
        return wolfCountLog;
    }

    public List<float> GetTimeLog()
    {
        return timeLog;
    }

    public void SaveLogToFile(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Time,DuckCount,WolfCount");
            for (int i = 0; i < timeLog.Count; i++)
            {
                writer.WriteLine($"{timeLog[i]},{duckCountLog[i]},{wolfCountLog[i]}");
            }
        }
        Debug.Log("Log saved to " + filePath);
    }

    private void OnApplicationQuit()
    {
        SaveLogToFile(Application.dataPath + "/animal_count_log.csv");
    }
}
