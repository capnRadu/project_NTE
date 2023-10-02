using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnObject : XRBaseInteractable
{
    [SerializeField] private GameObject prefabObject;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject newObject = Instantiate(prefabObject, this.transform.position, Quaternion.identity);
        XRGrabInteractable objectInteractable = newObject.GetComponent<XRGrabInteractable>();
        interactionManager.SelectEnter(args.interactorObject, objectInteractable);

        base.OnSelectEntered(args);
    }
}
