using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //important
using UnityEngine.Animations; //important
using UnityEngine.Audio; //important

//if you use this code you are contractually obligated to like the YT video
public class RandomMovement : MonoBehaviour //don't forget to change the script name if you haven't
{
    public NavMeshAgent agent;
    public float range; //radius of sphere

    public Transform centrePoint; //centre of the area the agent wants to move around in
    public GameObject player; //centre of the area the agent wants to move around in
    public float distance;

    public Animator anim;

    public RaycastHit hit;

    public AiVision aiV;

    public MonsterAudio moad;

    public bool canSee;

    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        centrePoint = GameObject.FindGameObjectWithTag("centre").GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        moad = GetComponent<MonsterAudio>();
        canSee = true;
    }


    void Update()
    {
        
        distance = Vector3.Distance(player.transform.position, transform.position);

        if (!aiV.seePlayer)
        {

                if (agent.remainingDistance <= agent.stoppingDistance) //done with path
                {
                    agent.speed = 0.5f;
                    anim.SetBool("walk", true);
                    anim.SetBool("run", false);

                    moad.running = false;

                    Vector3 point;
                    if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
                    {
                        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                        agent.SetDestination(point);
                    }
                }
            
        }
        else if(aiV.seePlayer && canSee)
        {
          moad.running = true;
          anim.SetBool("run", true);
          anim.SetBool("walk", false);

          agent.speed = 1f;
          agent.SetDestination(player.transform.position);
        }

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 1;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.black);
        }

    }


    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

   




}

