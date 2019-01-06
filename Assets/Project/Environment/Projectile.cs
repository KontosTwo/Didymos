using System;


public class Projectile
{
	private readonly int strength;
    private readonly float suppressiveRadius;
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

    public override bool Equals(object obj){
        Projectile other = (Projectile)obj;
        return other.strength == this.strength &&
            (other.suppressiveRadius - this.suppressiveRadius).CloseToZero(0.01f);
    }

    public override int GetHashCode(){
        int hash = 17;
        // Suitable nullity checks etc, of course :)
        hash = hash * 23 + strength;
        hash = hash * 23 + (int)Math.Round(suppressiveRadius);
        return hash;
    }
}


