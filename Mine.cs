﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MineTier
{
    public int output;
    public int outputCap;
}

public class Mine : Building, IInteractable
{
    public MineTier[] mineTiers;
    public int eCrystals;
    public GameObject eCrystal;

    public Player player;

    // Start is called before the first frame update
    void Start(){
        InvokeRepeating("ProduceCrystals", 20.0f, 20.0f);
    }

    // Update is called once per frame
    void Update(){
        
    }

    //the player interacts with the mine
    //the mine checks the players crystals
    //if theres enough it will



    //The mines interact upgrades the mine
    public void Interact(){

        if(buildingTiers[currentTier].upgradeCost == 0){
            //flash the message "MAX LEVEL REACHED"
            interactionPrompt.SetActive(false);
            maxLevelAlert.SetActive(true);
            Invoke("CloseAlerts", 5.0f);

        }else if(buildingTiers[currentTier].upgradeCost > player.eCrystals){
            //flash the message "NOT ENOUGH RESOURCES"
            interactionPrompt.SetActive(false);
            notEnoughResourcesAlert.SetActive(true);
            Invoke("CloseAlerts", 5.0f);

        }else if(roomReference.isRoomPowered){
            //flash upgrade message
            player.eCrystals -= buildingTiers[currentTier].upgradeCost;
            buildingTiers[currentTier].buildingSprite.SetActive(false);
            currentTier++;
            buildingTiers[currentTier].buildingSprite.SetActive(true);   
        }else{
            //flash message that room is not powered
        }
    }



    public void ProduceCrystals(){

        eCrystals += mineTiers[currentTier].output;

        if(eCrystals > mineTiers[currentTier].outputCap){
            eCrystals = mineTiers[currentTier].outputCap;
        }
        Debug.Log("We made some crystals");
    }

    public new void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            
            player = other.GetComponent<Player>();
            Debug.Log("Player collided with Mine");

			interactionPrompt.SetActive(true);

			for(int i = 0; i < eCrystals; i++){
                Instantiate(eCrystal);
                Instantiate(eCrystal, gameObject.transform.position, Quaternion.identity);
            }
            eCrystals = 0;
        }
    }
    public new void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){

            player = null;
			interactionPrompt.SetActive(false);
        }
    }
}
