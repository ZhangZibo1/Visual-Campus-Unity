using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;
public class navogationBus : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent navMeshAgent;
    public Transform[] checkPoints;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();   
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(checkPoints[0].position);
    }
}
