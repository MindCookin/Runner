using UnityEngine;
using System.Collections;

public class LevelStateManager : MonoBehaviour {
	 
	static float GLOBAL_MAX_ENEMY_PERCENT 	= 1; 
	static float GLOBAL_ENEMY_MAX_SIZE 		= 2f;
	static float GLOBAL_MAX_MISSILE_PERCENT = .7f;
	static float GLOBAL_PLATFORM_MIN_X_GAP 	= 0; 
	static float GLOBAL_PLATFORM_MAX_X_GAP 	= 20; 
	static float GLOBAL_PLATFORM_MIN_Y_GAP 	= 0; 
	static float GLOBAL_PLATFORM_MAX_Y_GAP 	= 3; 
	static float GLOBAL_PLATFORM_MAX_SIZE 	= 30;
	static float GLOBAL_MAX_DROP_PERCENT 	= 1;
	
	float _enemyPercent; 
	float _enemyMinSize; 
	float _enemyMaxSize;
	float _missilePercent;
	float _platformMinXGap; 
	float _platformMaxXGap; 
	float _platformMinYGap; 
	float _platformMaxYGap; 
	float _platformMinSize;
	float _platformMaxSize;
	float _dropPickerPercent; 
	
	public float EnemyIncrement, EnemySizeIncrement;
	public float MissileIncrement;
	public float PlatformMinGapXIncrement, PlatformMaxGapXIncrement, PlatformMinGapYIncrement, PlatformMaxGapYIncrement, PlatformSizeIncrement;
	public float DropPickerIncrement;
	
	public int UPDATE_TIME;
	
	public static LevelStateManager instance;
	
	void Awake() {
		
		instance = this;
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameInit += GameInit;
		GameEventManager.GameOver += GameOver;
	}
	
	void UpdateVariables () {
			
		_enemyPercent 	= ( _enemyPercent <= GLOBAL_MAX_ENEMY_PERCENT ) ? _enemyPercent + EnemyIncrement : _enemyPercent;
		_enemyMaxSize 	= ( _enemyMaxSize <= GLOBAL_ENEMY_MAX_SIZE ) ? _enemyMaxSize + EnemySizeIncrement : _enemyMaxSize;
		
		_missilePercent =  ( _missilePercent <= GLOBAL_MAX_MISSILE_PERCENT ) ? _missilePercent + MissileIncrement : _missilePercent; 
		
		_platformMinXGap = ( _platformMinXGap <= GLOBAL_PLATFORM_MIN_X_GAP ) ? _platformMinXGap + PlatformMinXGap : _platformMinXGap;
		_platformMaxXGap = ( _platformMaxXGap <= GLOBAL_PLATFORM_MAX_X_GAP ) ? _platformMaxXGap + PlatformMaxXGap : _platformMaxXGap;
		
		_platformMinYGap = ( _platformMinYGap <= GLOBAL_PLATFORM_MIN_Y_GAP ) ? _platformMinYGap + PlatformMinYGap : _platformMinYGap; 
		_platformMaxYGap = ( _platformMaxYGap <= GLOBAL_PLATFORM_MAX_Y_GAP ) ? _platformMaxYGap + PlatformMaxYGap : _platformMaxYGap; 
		
		_platformMaxSize = ( _platformMaxSize <= GLOBAL_PLATFORM_MAX_SIZE ) ? _platformMaxSize + PlatformSizeIncrement : _platformMaxSize;
		
		_dropPickerPercent = ( _dropPickerPercent <= GLOBAL_MAX_DROP_PERCENT ) ? _dropPickerPercent + DropPickerIncrement : _dropPickerPercent; 

	}
	
	void GameStart() {
		
		InvokeRepeating( "UpdateVariables", 0, UPDATE_TIME );
	}
	
	void GameInit() {
			
		_enemyPercent = 0; 
		_enemyMinSize = 0.5f; 
		_enemyMaxSize = 0.5f;
		_missilePercent = 0;
		_platformMinXGap = 0; 
		_platformMaxXGap = 0; 
		_platformMinYGap = 0; 
		_platformMaxYGap = 0; 
		_platformMinSize = 5;
		_platformMaxSize = 0;
		_dropPickerPercent = 0; 
	}
	
	void GameOver() {
		
		CancelInvoke( "UpdateVariables" );
	}
	
	public float EnemyPercent { get { return _enemyPercent; } } 
	public float EnemyMinSize { get { return _enemyMinSize; } } 
	public float EnemyMaxSize { get { return _enemyMaxSize; } }
	public float MissilePercent { get { return _missilePercent; } }
	public float PlatformMinXGap { get { return _platformMinXGap; } }
	public float PlatformMaxXGap { get { return _platformMaxXGap; } }
	public float PlatformMinYGap { get { return _platformMinYGap; } }
	public float PlatformMaxYGap { get { return _platformMaxYGap; } }
	public float PlatformMinSize { get { return _platformMinSize; } }
	public float PlatformMaxSize { get { return _platformMaxSize; } }
	public float DropPickerPercent { get { return _dropPickerPercent; } }
	
}
