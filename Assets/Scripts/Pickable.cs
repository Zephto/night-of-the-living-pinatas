using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : Interactable {

    #region Private variables
    private CharacterController playerReference;
    private Collider coll;
    private Rigidbody rb;
    #endregion

    public CandyEstant candyEstantReference;
    public int CantidadDeDulces;
    public GameObject[] candyBags;

    void Awake() {
        playerReference = GameObject.FindObjectOfType<CharacterController>();
        coll            = this.GetComponent<Collider>();
        rb              = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(CantidadDeDulces == 0)
        {
            playerReference.cameraInteraction.ArmBox = false;
            Destroy(gameObject);
        }
    }

    public void RemoveBag(){
        CantidadDeDulces--;
        if(CantidadDeDulces < candyBags.Length){
            candyBags[CantidadDeDulces].gameObject.SetActive(false);
        }
    }

    public override void Interact()
    {
        base.Interact();

        transform.SetParent(playerReference.zoneArm.transform);
        transform.position = new Vector3(0,0,0);
        transform.rotation = new Quaternion(0,0,0,0);
        transform.position = playerReference.zoneArm.transform.position;

        //Set estant reference where the player needs to put the candy bags
        //playerReference.cameraInteraction.SetEstant(candyEstantReference.gameObject);
        playerReference.audios.PlayGrab();
        
        rb.isKinematic = true; coll.enabled = false; rb.useGravity = false;
    }

}
