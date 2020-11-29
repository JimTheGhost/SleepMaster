using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal linkedPortal;
    private bool _portalActive = true;
    [SerializeField] private AudioClip portalSfx;

    public IEnumerator ReactivatePortal()
    {
        Debug.Log(name + " Waiting to Reactivate");
        yield return new WaitForSeconds(0.4f);
        _portalActive = true;
        Debug.Log(name + " Reactivated");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _portalActive)
        {
            linkedPortal._portalActive = false;
            other.transform.position = linkedPortal.transform.position;
            linkedPortal.StartCoroutine(linkedPortal.ReactivatePortal());
            AudioManager.Instance.PlaySoundEffect(portalSfx);
        }
    }
}
