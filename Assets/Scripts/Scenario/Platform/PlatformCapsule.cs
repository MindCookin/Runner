using UnityEngine;
using System.Collections;

public class PlatformCapsule : MonoBehaviour
{
	public Transform capsulePrefab;
	
	Transform capsule;
	
	CapsulePlace capsulePlace;
	PickupItem capsulePickup;
	
	void Awake()
	{
		GameObject itemsContainer = GameObject.Find("DropItemsContainer");
		
		capsule					= (Transform)Instantiate(capsulePrefab);
		
		capsule.transform.parent = itemsContainer.transform;
		
		capsulePickup 	= capsule.GetComponent<PickupItem>();
		capsulePlace	= capsule.GetComponent<CapsulePlace>();
	}
	
	public void TryToAddCapsule() {
	
		if( ( transform.position.x > StaticItemsManager.LastPos && transform.position.x < StaticItemsManager.NextPos )
				&&	Random.value < LevelStateManager.GetInstance().DropPickerPercent )
			AddCapsule();
		else 
			capsulePickup.Disable();
	}
	
	public void AddCapsule() {
		capsulePickup.Reset();
		capsulePlace.Place( transform );
	}
	
	public void Remove() {
		capsulePickup.Disable();
	}
}

