using UnityEngine;
using System.Collections;

public class PlayerSize : MonoBehaviour {
	
	public float speed, growSpeed, shrinkSpeed;
	public int duration, velocityDivider;
	
	private Vector3 targetSize;
	private float maxSize;
	
	PlayerSounds playerSounds;
	
	void Awake () {
		
		GameEventManager.GameInit += GameInit;
		GameEventManager.GameOver += GameOver;
		
		playerSounds = GetComponent<PlayerSounds>();
		
		targetSize = Vector3.one;
	}
	
	public void Grow()
	{
		playerSounds.PlaySounds( playerSounds.grow );
		
		targetSize += Vector3.one * growSpeed;
		targetSize.z = 1;
		
		if ( targetSize.x > maxSize )
			maxSize = targetSize.x;
	}
	
	public void Shrink()
	{
		playerSounds.PlaySounds( playerSounds.shrink );
		
		targetSize -= Vector3.one * shrinkSpeed;
		targetSize.z = 1;
		
		if ( targetSize.x < 1 )
			targetSize = Vector3.one;
	}
	
	void Update () {
	
		if( transform.localScale != targetSize )
			transform.localScale = Vector3.Lerp( transform.localScale, targetSize, speed * Time.deltaTime );	
	}
	
	void GameInit() {
		targetSize = Vector3.one;
		maxSize = 0;
	}
	
	void GameOver() {
		PlayerDataManager.SetValue( SessionData.SIZE, Mathf.FloorToInt( maxSize ) );
	}
}
