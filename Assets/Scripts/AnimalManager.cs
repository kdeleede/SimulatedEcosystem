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

    private float duckSpeedTotal = 0;    
    private float wolfSpeedTotal = 0;
    private float tigerSpeedTotal = 0;
    private List<float> duckSpeedLog = new List<float>();
    private List<float> wolfSpeedLog = new List<float>();
    private List<float> tigerSpeedLog = new List<float>();

    private float duckSensoryDistTotal = 0;    
    private float wolfSensoryDistTotal = 0;
    private float tigerSensoryDistTotal = 0;
    private List<float> duckSensoryLog = new List<float>();
    private List<float> wolfSensoryLog = new List<float>();
    private List<float> tigerSensoryLog = new List<float>();

    private float duckReproductiveRateTotal = 0;    
    private float wolfReproductiveRateTotal = 0;
    private float tigerReproductiveRateTotal = 0;
    private List<float> duckReproductiveRateLog = new List<float>();
    private List<float> wolfReproductiveRateLog = new List<float>();
    private List<float> tigerReproductiveRateLog = new List<float>();

    private List<float> timeLog = new List<float>();

    private float logInterval = 5.0f; // Log every 5 second
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

        duckSpeedLog.Add(duckSpeedTotal);
        wolfSpeedLog.Add(wolfSpeedTotal);
        tigerSpeedLog.Add(tigerSpeedTotal);

        duckReproductiveRateLog.Add(duckReproductiveRateTotal);
        duckSensoryLog.Add(duckSensoryDistTotal);

        wolfReproductiveRateLog.Add(wolfReproductiveRateTotal);
        wolfSensoryLog.Add(wolfSensoryDistTotal);  

        tigerReproductiveRateLog.Add(tigerReproductiveRateTotal);
        tigerSensoryLog.Add(tigerSensoryDistTotal);
        
        timeLog.Add(Time.time);
    }
    
    // ############### //
    // #### DUCKS #### //
    // ############### //
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

    public void AddDuckGenetics(float constantSpeed, float sensoryDistance, float reproductiveUrgeRate) 
    {
        duckSpeedTotal += constantSpeed;
        duckSensoryDistTotal += sensoryDistance;
        duckReproductiveRateTotal += reproductiveUrgeRate;
        Debug.Log("Duck total speed = " + duckSpeedTotal);
        Debug.Log("Duck sensory total = " + duckSensoryDistTotal);
        Debug.Log("Duck reproductive total = " + duckReproductiveRateTotal);
    }  

    public void RemoveDuckGenetics(float constantSpeed, float sensoryDistance, float reproductiveUrgeRate)
    {
        duckSpeedTotal -= constantSpeed;
        duckSensoryDistTotal -= sensoryDistance;
        duckReproductiveRateTotal -= reproductiveUrgeRate;
        Debug.Log("Duck total speed = " + duckSpeedTotal);
    }




    // ############### //
    // #### Wolves ### //
    // ############### //
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

    public void AddWolfGenetics(float constantSpeed, float sensoryDistance, float reproductiveUrgeRate) 
    {
        wolfSpeedTotal += constantSpeed;
        wolfSensoryDistTotal += sensoryDistance;
        wolfReproductiveRateTotal += reproductiveUrgeRate;
        Debug.Log("wolf total speed = " + wolfSpeedTotal);
        Debug.Log("wolf sensory total = " + wolfSensoryDistTotal);
        Debug.Log("wolf reproductive total = " + wolfReproductiveRateTotal);
    }  

    public void RemoveWolfGenetics(float constantSpeed, float sensoryDistance, float reproductiveUrgeRate)
    {
        wolfSpeedTotal -= constantSpeed;
        wolfSensoryDistTotal -= sensoryDistance;
        wolfReproductiveRateTotal -= reproductiveUrgeRate;
        Debug.Log("wolf total speed = " + wolfSpeedTotal);
    }


    // ############### //
    // #### Tigers ### //
    // ############### //
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

    public void AddTigerGenetics(float constantSpeed, float sensoryDistance, float reproductiveUrgeRate) 
    {
        tigerSpeedTotal += constantSpeed;
        tigerSensoryDistTotal += sensoryDistance;
        tigerReproductiveRateTotal += reproductiveUrgeRate;
        Debug.Log("tiger total speed = " + tigerSpeedTotal);
        Debug.Log("tiger sensory total = " + tigerSensoryDistTotal);
        Debug.Log("tiger reproductive total = " + tigerReproductiveRateTotal);
    }  

    public void RemoveTigerGenetics(float constantSpeed, float sensoryDistance, float reproductiveUrgeRate)
    {
        tigerSpeedTotal -= constantSpeed;
        tigerSensoryDistTotal -= sensoryDistance;
        tigerReproductiveRateTotal -= reproductiveUrgeRate;
        Debug.Log("tiger total speed = " + tigerSpeedTotal);
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

    public void SaveLogToFile(string filePath, bool ifDuck, bool ifWolf, bool ifTiger)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {   
            string columnNames = "Time";
            if (ifDuck) {
                columnNames = columnNames + ",DuckCount,DuckSpeed,DuckReproductiveRate,DuckSensoryDist";
            } 
            if (ifWolf) {
                columnNames = columnNames + ",WolfCount,WolfSpeed,WolfReproductiveRate,WolfSensoryDist";
            }
            if (ifTiger) {
                columnNames = columnNames + ",TigerCount,TigerSpeed,TigerReproductiveRate,TigerSensoryDist";
            }
            writer.WriteLine(columnNames);
            
            for (int i = 0; i < timeLog.Count; i++)
            {   
                string entry = $"{timeLog[i]}";
                if (ifDuck) {
                    entry = entry + $",{duckCountLog[i]},{duckSpeedLog[i]},{duckReproductiveRateLog[i]},{duckSensoryLog[i]}";
                }
                if (ifWolf) {
                    entry = entry + $",{wolfCountLog[i]},{wolfSpeedLog[i]},{wolfReproductiveRateLog[i]},{wolfSensoryLog[i]}";
                }
                if (ifTiger) {
                    entry = entry + $",{tigerCountLog[i]},{tigerSpeedLog[i]},{tigerReproductiveRateLog[i]},{tigerSensoryLog[i]}";
                }
                Debug.Log("En = " + entry);
                writer.WriteLine(entry);
            }
        }
        Debug.Log("Log saved to " + filePath);
    }

    private void OnApplicationQuit()
    {
        bool ifDuck = duckCount > 0;
        bool ifWolf = wolfCount > 0;
        bool ifTiger = tigerCount > 0;
        SaveLogToFile(Application.dataPath + "/animal_count_log.csv", ifDuck, ifWolf, ifTiger);
    }
}