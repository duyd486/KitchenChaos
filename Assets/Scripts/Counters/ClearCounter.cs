using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitcchenObjectParent
{
    public override void Interact(Player player) {
        if(!HasKitchenObject()) {
            // There is no KitchenObject here
            if (player.HasKitchenObject()) {
                //Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                //Player has nothing

            }
        } else {
            // There is a KitchenObject here
            if (player.HasKitchenObject()) {
                //Player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    //player is holding plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKichenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    //player not carrying plate but smt else
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        //Counter is holding a Plate
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKichenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }

            } else {
                //Player has nothing
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        return;
    }
}
