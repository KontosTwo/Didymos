using System;


public class Projectile
{
	private readonly int strength;
    private float suppressiveRadius;
	private int currentStrength;


	public Projectile (int power, float suppressiveRadius)
	{
		strength = power;
		currentStrength = power;
        this.suppressiveRadius = suppressiveRadius;
	}

	public void SlowedBy(IObstructable obstacle){
		currentStrength -= obstacle.GetResistance();
	}

	public bool IsStillActive(){
		return currentStrength >= 0;
	}

	public void ResetStrength(){
		currentStrength = strength;
	}

	public int GetStrength(){
		if (currentStrength < 0) {
			return 0;
		} else {
			return currentStrength;
		}
	}

    public float GetSuppressiveRadius(){
        return suppressiveRadius;
    }

	public interface IObstructable{
		int GetResistance();
	}
}


