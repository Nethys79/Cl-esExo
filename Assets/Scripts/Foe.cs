using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using Random = UnityEngine.Random;

public class Foe : MonoBehaviour
{

    [SerializeField, Tooltip("layer chest")]
    private LayerMask m_playerLayer;
    
    //liste waypoint
    public List<Transform> l_path;
    private Vector3 waypoint;

    private NavMeshAgent m_agent;

    private Transform m_player;

    private bool isTrigger = false;
    private bool isTriggerCam = false;
    
    private int m_state = 0;

    [SerializeField, Tooltip("link l'event ici")] private Event m_triggeredEvent;

    //enable
    private void OnEnable()
    {
        if (m_triggeredEvent == null) return;

        m_triggeredEvent.onTriggered += HandleTriggerEvent;

    }
    //disable
    private void OnDisable()
    {
        if (m_triggeredEvent == null) return;
        m_triggeredEvent.onTriggered -= HandleTriggerEvent;
    }

    /// <summary>
    /// start qwa
    /// </summary>
    private void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggerCam)
        {
            m_agent.SetDestination(waypoint);
                        
            CheckPosPlayer();
            return;
        }

        if (isTrigger)
        {
            m_agent.SetDestination(m_player.position);

            return;
        }

        m_agent.SetDestination(l_path[m_state].position);
        CheckPos();
    }

    void CheckPos()
    {
        if (transform.position.x == l_path[m_state].position.x)
        {
            if (transform.position.z == l_path[m_state].position.z)
            {
                UpdatePos();
            }
        }
    }

    void CheckPosPlayer()
    {
        Debug.Log($"{waypoint}");

        if(Vector3.Distance(transform.position, waypoint) < 1)
        {
            UpdatePos();
        }
    }

    void UpdatePos()
    {
        m_state = Random.Range(0, l_path.Count - 1);
        isTriggerCam = false;
    }

    private void OnTriggerEnter(Collider other)
    {
                    
        if ((m_playerLayer.value &(1 << other.gameObject.layer)) >0)
        {
            isTrigger = true;
            if (other.GetComponent<Player>() != null)
            {
                m_player = other.gameObject.GetComponent<Transform>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (m_player == other.GetComponent<Transform>())
        {
            
            isTrigger = false;
        }
    }

    void HandleTriggerEvent(Vector3 _position)
    {
        waypoint = _position;
        Debug.Log($"le foe reçoit l'ordre !!");
        isTriggerCam = true;
    }

}
