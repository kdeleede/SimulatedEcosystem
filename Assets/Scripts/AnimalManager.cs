using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class AnimalManager : MonoBehaviour
{
    public static AnimalManager Instance { get; private set; }

    private int duckCount = 0;
    private int wolfCount = 0;
    private int tigerCount = 0;

    private List<int> duckCountLog = new List<int>();
    private List<int> wolfCountLog = new List<int>();
    private List<int> tigerCountLog = new List<int>();
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
        tigerCountLog.Add(tigerCount);
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

    public void AddTiger()
    {
        tigerCount++;
        Debug.Log("Tiger added. Current count: " + tigerCount);
    }

    public void RemoveTiger()
    {
        tigerCount--;
        Debug.Log("Tiger removed. Current count: " + tigerCount);
    }

    public int GetDuckCount()
    {
        return duckCount;
    }

    public int GetWolfCount()
    {
        return wolfCount;
    }

    public int GetTigerCount()
    {
        return tigerCount;
    }

    public List<int> GetDuckCountLog()
    {
        return duckCountLog;
    }

    public List<int> GetWolfCountLog()
    {
        return wolfCountLog;
    }

    public List<int> GetTigerCountLog()
    {
        return tigerCountLog;
    }

    public List<float> GetTimeLog()
    {
        return timeLog;
    }

    public void SaveLogToFile(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Time,DuckCount,WolfCount,TigerCount");
            for (int i = 0; i < timeLog.Count; i++)
            {
                writer.WriteLine($"{timeLog[i]},{duckCountLog[i]},{wolfCountLog[i]},{tigerCountLog[i]}");
            }
        }
        Debug.Log("Log saved to " + filePath);
    }

    private void OnApplicationQuit()
    {
        SaveLogToFile(Application.dataPath + "/animal_count_log.csv");
    }
}