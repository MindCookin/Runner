using UnityEngine;
using System.Collections;
using System.Timers;

public class LevelStateManager {
	
	static int TIMER_SECONDS 	= 1;
	static int UPDATE_DISTANCE 	= 50;
	static int UPDATE_PLATFORMS_EACH = 4;
 
	static float GLOBAL_MAX_ENEMY_PERCENT 	= 0.8f; 
	static float GLOBAL_ENEMY_MAX_SIZE 		= 2f;
	static float GLOBAL_MAX_MISSILE_PERCENT = 0.6f;
	static float GLOBAL_PLATFORM_MIN_X_GAP 	= 0; 
	static float GLOBAL_PLATFORM_MAX_X_GAP 	= 10; 
	static float GLOBAL_PLATFORM_MIN_Y_GAP 	= -5; 
	static float GLOBAL_PLATFORM_MAX_Y_GAP 	= 5; 
	static float GLOBAL_PLATFORM_MAX_SIZE 	= 30;
	static float GLOBAL_MAX_DROP_PERCENT 	= .5f;
	
	float EnemyIncrement			= .025f; 
	float EnemySizeIncrement		= .1f;
	float MissileIncrement			= .005f;
	float PlatformMinGapXIncrement	= 0f; 
	float PlatformMaxGapXIncrement	= .5f;
	float PlatformMinGapYIncrement	= .5f;
	float PlatformMaxGapYIncrement	= .2f;
	float PlatformSizeIncrement		= .05f;
	float DropPickerIncrement		= .05f;
	
	float _enemyPercent, _enemyMinSize, _enemyMaxSize, _missilePercent, _platformMinXGap, _platformMaxXGap, _platformMinYGap, _platformMaxYGap, _platformMinSize, _platformMaxSize, _dropPickerPercent; 
	Vector3 _playerInitialPosition;
	
	private PlayerMove player;
	private int lastDistanceUpdated;
	
	private static bool isOKToCreate = false;
	private static LevelStateManager instance = null;
	
	Timer timer;
	
	public LevelStateManager(){
		
		if ( !isOKToCreate )
			throw new UnityException("You need to access to LEvelStateManager through a Singleton!!!" );
		else
			Initialize();
	}
	
   	public static LevelStateManager GetInstance()
   	{
    	if (instance == null)
		{
			isOKToCreate = true;
        	instance = new LevelStateManager();
			isOKToCreate = false;
		}
 
     	return instance;
   	}
	
	void Initialize() {
		
		InitializeVariables();
		
		lastDistanceUpdated = 1;
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameInit += GameInit;
		GameEventManager.GameOver += GameOver;
		
		timer = new Timer();
		timer.Interval = TIMER_SECONDS * 1000;
		timer.Elapsed += new ElapsedEventHandler(UpdateVariables);
		
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
		_playerInitialPosition = player.transform.position;
	}
	
	void InitializeVariables() {
		
		lastDistanceUpdated = 1;
		_enemyPercent 		= 0; 
		_enemyMinSize 		= 1f; 
		_enemyMaxSize 		= 1.5f;
		_missilePercent 	= 0;
		_platformMinXGap 	= 1; 
		_platformMaxXGap 	= 1; 
		_platformMinYGap 	= 0; 
		_platformMaxYGap	= 0.5f; 
		_platformMinSize 	= 10;
		_platformMaxSize 	= 15;
		_dropPickerPercent 	= 0;
	}
	
	void UpdateVariables (object source, ElapsedEventArgs e) {
		
		if ( player.DistanceTraveled / UPDATE_DISTANCE > lastDistanceUpdated )
		{
			
			_enemyPercent 	= ( _enemyPercent <= GLOBAL_MAX_ENEMY_PERCENT ) ? _enemyPercent + EnemyIncrement : _enemyPercent;
			_enemyMaxSize 	= ( _enemyMaxSize <= GLOBAL_ENEMY_MAX_SIZE ) ? _enemyMaxSize + EnemySizeIncrement : _enemyMaxSize;
			_missilePercent =  ( _missilePercent <= GLOBAL_MAX_MISSILE_PERCENT ) ? _missilePercent + MissileIncrement : _missilePercent; 
			_dropPickerPercent = ( _dropPickerPercent <= GLOBAL_MAX_DROP_PERCENT ) ? _dropPickerPercent + DropPickerIncrement : _dropPickerPercent; 
			
			if ( lastDistanceUpdated % UPDATE_PLATFORMS_EACH == 0 )
			{	
				_platformMinXGap = ( _platformMinXGap <= GLOBAL_PLATFORM_MIN_X_GAP ) ? _platformMinXGap + PlatformMinGapXIncrement : _platformMinXGap;
				_platformMaxXGap = ( _platformMaxXGap <= GLOBAL_PLATFORM_MAX_X_GAP ) ? _platformMaxXGap + PlatformMaxGapXIncrement : _platformMaxXGap;
				_platformMinYGap = ( _platformMinYGap >= GLOBAL_PLATFORM_MIN_Y_GAP ) ? _platformMinYGap - PlatformMinGapYIncrement : _platformMinYGap; 
				_platformMaxYGap = ( _platformMaxYGap <= GLOBAL_PLATFORM_MAX_Y_GAP ) ? _platformMaxYGap + PlatformMaxGapYIncrement : _platformMaxYGap; 
				_platformMaxSize = ( _platformMaxSize <= GLOBAL_PLATFORM_MAX_SIZE ) ? _platformMaxSize + PlatformSizeIncrement : _platformMaxSize;
			}
			
			lastDistanceUpdated = Mathf.FloorToInt( player.DistanceTraveled / UPDATE_DISTANCE ) + 1;
		}
	}
	
	void GameStart() {
		
		lastDistanceUpdated = 1;
		timer.Start();
	}
	
	void GameInit() {
	}
	
	void GameOver() {
		
		timer.Stop();
		
		InitializeVariables();
	}
	
	public void SetInitialPosition( Vector3 pos ){
		
		_playerInitialPosition = pos;
		
		InitializeVariables();	
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
	public Vector3 PlayerInitialPosition { get { return _playerInitialPosition; } }
}
