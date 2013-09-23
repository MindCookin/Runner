using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerDataManager : MonoBehaviour
{
	LoadedData loaded;
	SessionData session;
	
	static PlayerDataManager instance;
	
	void Awake() {
		
		instance= this;
		session = new SessionData();
		
		GameEventManager.GameInit 	+= GameInit;
		GameEventManager.GameStart 	+= GameStart;
		
		Load();
	}
	
    void Load()
    {
        //Get the data
        var data = PlayerPrefs.GetString("PlayerData");
        
		//If not blank then load it
        if(!string.IsNullOrEmpty(data))
        {
            //Binary formatter for loading back
            var b = new BinaryFormatter();
            //Create a memory stream with the data
            var m = new MemoryStream(Convert.FromBase64String(data));
            //Load back the player data
			loaded = (LoadedData)b.Deserialize(m);
        } else {
			loaded = new LoadedData();	
		}
    }
	
    void Save()
    {
		UpdateLoadedData();
		
        //Get a binary formatter
        var b = new BinaryFormatter();
        //Create an in memory stream
        var m = new MemoryStream();
        //Save the scores
        b.Serialize(m, loaded);
        //Add it to player prefs
        PlayerPrefs.SetString("PlayerData", 
            Convert.ToBase64String(
                m.GetBuffer()
            )
        );
    }
	
	void GameInit() {
		
		if ( loaded != null )
			Save();	
	}
	
	void GameStart() {
		
		session.Reset();
	}
	
	public static void SetValue ( string name, int quantity) {
		instance.session.Set( name, quantity );
	}
	
	public static void AddToValue ( string name, int quantity) {
		instance.session.Add( name, quantity );
	}
	
	void UpdateLoadedData() {
		
		loaded.total_capsules 			+= session.capsules; 
		loaded.total_coins 				+= session.coins;
		loaded.total_distance 			+= session.distance;
		loaded.total_enemies 			+= session.enemies;
		loaded.total_enemies_collided 	+= session.enemies_collided;
		loaded.total_missiles 			+= session.missiles;
		loaded.total_missiles_collided 	+= session.missiles_collided;
			
		loaded.max_altitude = ( session.altitude > loaded.max_altitude ) ? session.altitude : loaded.max_altitude;
		loaded.max_capsules = ( session.capsules > loaded.max_capsules ) ? session.capsules : loaded.max_capsules;
		loaded.max_coins 	= ( session.coins > loaded.max_coins )		 ? session.coins : loaded.max_coins;
		loaded.max_distance = ( session.distance > loaded.max_distance ) ? session.distance : loaded.max_distance;
		loaded.max_size 	= ( session.size > loaded.max_size )		 ? session.size : loaded.max_size;
		loaded.max_velocity = ( session.velocity > loaded.max_velocity ) ? session.velocity : loaded.max_velocity;
	}
	
	
	void OnGUI()
	{
		GUILayout.Label ( string.Format ( "{0} : {1:#,0}", session.coins, loaded.max_coins ) );
		GUILayout.Label ( string.Format ( "{0} : {1:#,0}", session.distance, loaded.max_distance ) );
	}
	
	public LoadedData Loaded { get { return loaded; } }
	public SessionData Session { get { return session; } }
}

