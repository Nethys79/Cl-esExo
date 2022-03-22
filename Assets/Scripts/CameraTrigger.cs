using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Event m_camEvent;

    [SerializeField, Tooltip("layer chest")]
    private LayerMask m_playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        // verifie que c un joueur

        if((m_playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            Debug.Log("la camera voit le joueur");
            m_camEvent?.Raise(other.transform.position);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if ((m_playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            Debug.Log("la camera voit le joueur");
            m_camEvent?.Exit(other.transform.position);
        }
    }
}
