using System;

[Serializable()]
public class LoadedData
{
	public int max_distance = 0;
	public int max_altitude = 0;
	public int max_velocity = 0;
	public int max_capsules = 0;
	public int max_coins = 0;
	public int max_size = 0;
	
	public int total_distance = 0;
	public int total_capsules = 0;
	public int total_coins = 0;
	public int total_enemies_shooted = 0;
	public int total_enemies_collided = 0;
	public int total_enemies = 0;
	public int total_missiles_collided = 0;
	public int total_missiles = 0;
}
