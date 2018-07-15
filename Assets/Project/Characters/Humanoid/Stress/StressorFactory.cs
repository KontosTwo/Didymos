using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressorFactory {
    

    public static AcuteStressor CreateSuppressiveFireStressor(float power, 
                                                              float distance,
                                                              float suppressiveFirePowerStress, 
                                                              float suppressiveFireDistanceModifiers){
        var stressSeverity = ((suppressiveFirePowerStress * power * distance * suppressiveFireDistanceModifiers));
        return new AcuteStressor(stressSeverity);
    } 

    public static ChronicStressor CreateSightStressor(float coverDisparity,
                                                      float coverDisparityModifier){
        var stressSeverity = (coverDisparity * coverDisparityModifier);
        return new ChronicStressor(stressSeverity);
    }

}
