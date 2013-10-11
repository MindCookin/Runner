public class SessionData
{
	public const string DISTANCE 			= "DISTANCE";
	public const string CAPSULES 			= "CAPSULES";
	public const string ALTITUDE 			= "ALTITUDE";
	public const string VELOCITY 			= "VELOCITY";
	public const string COINS 				= "COINS";
	public const string SIZE 				= "SIZE";  
	public const string ENEMIES_SHOOTED 	= "ENEMIES_SHOOTED";
	public const string ENEMIES_COLLIDED 	= "ENEMIES_COLLIDED";
	public const string ENEMIES 			= "ENEMIES";		
	public const string MISSILES_COLLIDED 	= "MISSILES_COLLIDED";
	public const string MISSILES 			= "MISSILES";
	
	public int distance;
	public int capsules;
	public int altitude;
	public int velocity;
	public int coins;
	public int size;
	public int enemies_shooted;
	public int enemies_collided;
	public int enemies;
	public int missiles_collided;
	public int missiles;
	
	public void Reset() {
			
		distance = 0;
		capsules = 0;
		altitude = 0;
		velocity = 0;
		coins = 0;
		size = 0;
		enemies_shooted  = 0;
		enemies_collided = 0;
		enemies = 0;
		missiles_collided = 0;
		missiles = 0;
	}
	
	
	public void Set ( string name, int quantity) {
		
		switch( name )
		{
			case DISTANCE 			: distance = quantity;	break;
			case CAPSULES 			: capsules = quantity; 	break;
			case ALTITUDE 			: altitude = quantity; 	break;
			case VELOCITY 			: velocity = quantity; 	break;
			case COINS 				: coins = quantity; 	break;
			case SIZE 				: size = quantity; 		break;  
			case ENEMIES_SHOOTED	: enemies_shooted = quantity;  break;  
			case ENEMIES_COLLIDED 	: enemies_collided = quantity; break;
			case ENEMIES 			: enemies = quantity; 	break;		
			case MISSILES_COLLIDED 	: missiles_collided = quantity; break;
			case MISSILES 			: missiles = quantity; 	break;	
		}
	}
	
	public void Add ( string name, int quantity) {
		
		switch( name )
		{
			case DISTANCE 			: distance++;	break;
			case CAPSULES 			: capsules++; 	break;
			case ALTITUDE 			: altitude++; 	break;
			case VELOCITY 			: velocity++; 	break;
			case COINS 				: coins++; 	break;
			case SIZE 				: size++; 		break;  
			case ENEMIES_SHOOTED	: enemies_shooted++;  break;
			case ENEMIES_COLLIDED 	: enemies_collided++; break;
			case ENEMIES 			: enemies++; 	break;		
			case MISSILES_COLLIDED 	: missiles_collided++; break;
			case MISSILES 			: missiles++; 	break;	
		}
	}
}

