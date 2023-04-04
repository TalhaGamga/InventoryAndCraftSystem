using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] Transform interactPoint;
    [SerializeField] float scanRadius;
    [SerializeField] LayerMask scanLayer;

    [SerializeField] Collider[] scanColliders = new Collider[3];

    private void Update()
    {
        Physics.OverlapSphereNonAlloc(interactPoint.position, scanRadius, scanColliders, scanLayer);
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            for (int i = 0; i < scanColliders.Length; i++)
            {
                if (scanColliders[i] == null)
                {
                    return;
                }

                if (Vector3.Distance(transform.position, scanColliders[i].transform.position) <= scanRadius &&
                    scanColliders[i].TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    EventManager.OnSwitchToUIControllerState?.Invoke();
                    interactable.Interact();
                }
            }
        }
    }

}
