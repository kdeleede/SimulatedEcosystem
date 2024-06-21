using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Tiger : MonoBehaviour
{
    public NavMeshAgent agent;
    public float destinationThreshold = 0.15f; // Distance to determine if the destination is reached
    public float decisionInterval = 2.0f; // Interval for making movement decisions
    public float waitTimeBeforeMove = .1f; // Wait time before moving to the new location
    public float moveDistance = 1.0f; // Distance to move each time
    public LayerMask unwalkableLayer; // Layer mask for unwalkable areas

    private bool overrideDestination = false;
    private Vector3 newDestination;
    private Vector3 lastDirection;
    private bool isNearWater = false;

    private bool isNearDuck = false;
    private bool isNearWolf = false;
    private bool foundWater = false;

    [Header("State")]
    private float hunger;
    private float thirst;

    private float reproductiveUrge;
    float timeToDeathByHunger = 100f;
    float timeToDeathByThirst = 100f;

    float maxUrgeValue = 100f;

    private Animator animator;
    private bool isMakingDecision = false;

    private float timeToDrink = 1.5f;

    [Header("Reproduction")]

    public bool isMale;
    public bool isReadyToMate = false;

    private bool isMatingCooldown = false;

    public GameObject TigerPrefab;

    [Header("Inheritable")]
    public float constantSpeed = 3.5f;

    public float hungerRate = 1.5f;

    public float thirstRate = 1.5f;

    public float reproductiveUrgeRate = 1;
    public float sensoryDistance = 5;
    
    public string currentTarget = "";
    public float targetTrackingTime = 15f; // Time to track the current target
    public float targetTrackingCooldown = 0f; // Cooldown timer for tracking

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = constantSpeed;
        lastDirection = UnityEngine.Random.insideUnitSphere.normalized;
        isMale = UnityEngine.Random.Range(0, 2) == 0;
        AnimalManager.Instance?.AddTiger();
        AnimalManager.Instance?.AddTigerGenetics(constantSpeed, sensoryDistance, reproductiveUrgeRate);    
    }

    void Update()
    {
        if (targetTrackingCooldown > 0)
        {
            targetTrackingCooldown -= Time.deltaTime;
        }
        
        // Check if the agent is close to the destination
        if (agent.remainingDistance <= destinationThreshold)
        {
            if (!isMakingDecision)
            {
                agent.isStopped = true;
                isMakingDecision = true;
                StartCoroutine(MakeDecisionAfterDelay());
            }
        }

        // Check if the agent is near water, Wolf, or Duck
        isNearWater = HelperFunctions.CheckIsNearObject(transform, "Water", 2f);
        isNearDuck = HelperFunctions.CheckIsNearObject(transform, "Duck", 1.0f);
        isNearWolf = HelperFunctions.CheckIsNearObject(transform, "Wolf", 1.0f);

        hunger += Time.deltaTime * hungerRate / timeToDeathByHunger;
        thirst += Time.deltaTime * thirstRate / timeToDeathByThirst;
        reproductiveUrge += Time.deltaTime * reproductiveUrgeRate / maxUrgeValue;

        if (hunger >= 1 || thirst >= 1)
        {
            // delete
            delete();
        }

        if (!isMatingCooldown)
        {
            isReadyToMate = true;
        }
        else
        {
            isReadyToMate = false;
        }
    }

    IEnumerator MakeDecisionAfterDelay()
    {
        yield return new WaitForSeconds(waitTimeBeforeMove);

        
        if (hunger >= thirst && hunger > .3f)
        {
            if(hunger > .7f)
            {
                if (!isNearWolf)
                {
                    HelperFunctions.LookingForObject(transform, ref agent, "Wolf", sensoryDistance, moveDistance, destinationThreshold, ref lastDirection);
                }
                else
                {
                    EatWolf();
                }
            }
            else
            {
                if (!isNearDuck)
                {
                    HelperFunctions.LookingForObject(transform, ref agent, "Duck", sensoryDistance, moveDistance, destinationThreshold, ref lastDirection);
                }
                else
                {
                    EatDuck();
                }
            }
            targetTrackingCooldown = targetTrackingTime;
        }
        else if(thirst > hunger && thirst > .3f)
        {
            if(!isNearWater)
            {
                HelperFunctions.LookingForObject(transform, ref agent, "Water", sensoryDistance, moveDistance, destinationThreshold, ref lastDirection);
            }
            else
            {
                yield return new WaitForSeconds(timeToDrink);
                DrinkWater();
            }
        }
        else if(reproductiveUrge > .4f)
        {
            if(isReadyToMate)
            {
                if(isMale)
                {
                    if(HelperFunctions.CheckIsNearObject(transform, "Tiger", 1f, true, false))
                    {
                        Mate();
                    }
                    else
                    {
                        HelperFunctions.LookingForObject(transform, ref agent, "Tiger", sensoryDistance, moveDistance, destinationThreshold, ref lastDirection, true, false);
                    }
                }
                else
                {
                    if(HelperFunctions.CheckIsNearObject(transform, "Tiger", 1f, true, true))
                    {
                        Birth();
                    }
                    else
                    {
                        if (HelperFunctions.CheckIsNearObject(transform, "Tiger", sensoryDistance, true, true))
                        {

                        }
                        else
                        {
                            HelperFunctions.Exploring(transform, ref agent, ref lastDirection, moveDistance);
                        }
                    }
                }
            }
            else
            {
                HelperFunctions.Exploring(transform, ref agent, ref lastDirection, moveDistance);
            }
        
        }
        else
        {
            HelperFunctions.Exploring(transform, ref agent, ref lastDirection, moveDistance);
        }

        agent.isStopped = false;
        isMakingDecision = false; // Reset the flag to allow for new decisions after the current action is finished
    }

    void DrinkWater()
    {
        thirst = 0f;
        agent.isStopped = false;    
    }

    void EatWolf()
    {
        hunger = 0f;
        Collider collider = HelperFunctions.GetClosestObject(transform, "Wolf", 1.0f);
        if (collider != null)
        {
            GameObject WolfObject = collider.gameObject;
            Destroy(WolfObject);
        }
        agent.isStopped = false;  
    }

    void EatDuck()
    {
        hunger -= .5f;
        hunger = Mathf.Max(0, hunger);
        Collider collider = HelperFunctions.GetClosestObject(transform, "Duck", 1.0f);
        if (collider != null)
        {
            GameObject DuckObject = collider.gameObject;
            Destroy(DuckObject);
        }
        agent.isStopped = false;  
    }

    void Mate()
    {
        isReadyToMate = false;
        isMatingCooldown = true;
        reproductiveUrge = 0;
        StartCoroutine(MatingCooldown());
    }

    void Birth()
    {
        Collider collider = HelperFunctions.GetClosestObject(transform, "Tiger", 1.0f, true, true);
        Tiger father = collider.GetComponent<Tiger>();

        GameObject babyTiger = Instantiate(TigerPrefab, transform.position, Quaternion.identity);
        babyTiger.GetComponent<Tiger>().InheritGenes(father, this);
        babyTiger.transform.parent = father.transform.parent;
        Mate();
    }

    public void InheritGenes(Tiger Father, Tiger Mother)
    {
        float meanSpeed = (Father.constantSpeed + Mother.constantSpeed) / 2;

        float stdDev = 1f;
        float sampledSpeed = HelperFunctions.SampleNormalDistribution(meanSpeed, stdDev);

        constantSpeed = Mathf.Max(sampledSpeed, .1f);

        float RateOfHungerThirst = DetermineThirstAndHungerRate(constantSpeed);

        thirstRate = RateOfHungerThirst;
        hungerRate = RateOfHungerThirst;

        float meanReproductiveUrge = (Father.reproductiveUrgeRate + Mother.reproductiveUrgeRate) / 2;
        float reproductiveUrgeStdDev = 0.15f;
        float sampleReproductiveUrge = HelperFunctions.SampleNormalDistribution(meanReproductiveUrge, reproductiveUrgeStdDev);


        reproductiveUrgeRate = Mathf.Max(sampleReproductiveUrge, .1f);

        float meanSensoryDist = (Father.sensoryDistance + Mother.sensoryDistance) / 2;
        float sensoryDistStdDev = 0.2f;
        float sampleSensoryDist = HelperFunctions.SampleNormalDistribution(meanSensoryDist, sensoryDistStdDev);

        sensoryDistance = Mathf.Max(sampleSensoryDist, .1f);
    }

    public static float DetermineThirstAndHungerRate(float speed)
    {
        // default speed is 3.5 if larger than 3.5 get thirstier/hungrier faster
        return speed / 8.0f;
    }
    
    IEnumerator MatingCooldown()
    {
        yield return new WaitForSeconds(30f);
        isMatingCooldown = false;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position to visualize the check radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2.0f);
    }

    private void delete()
    {
        Destroy(gameObject);
    }
    
    private void OnDestroy()
    {
        AnimalManager.Instance?.RemoveTigerGenetics(constantSpeed, sensoryDistance, reproductiveUrgeRate);    
        AnimalManager.Instance?.RemoveTiger();
    }
}
