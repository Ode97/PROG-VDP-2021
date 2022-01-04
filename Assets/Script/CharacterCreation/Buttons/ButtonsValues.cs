using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsValues : MonoBehaviour
{
    // Crystal coat - Acid / Fireproof - fire / Rubber coating - electro / Hardened - Traps
    //                                              Acid Electric,  Acid Fire,         Acid Trap,      Electric Fire,     Electric Trap,  Fire Trap
    public static string[] armLabels = new string[]{
        "Antishock Steel",
        "Fireproof Alloy",
        "Reinforced Plates",
        "Fireproof Rubber",
        "Adaptative Rubber",
        "Fireproof Shell"
    };
    public static string[] armDetail = new string[]{
        "Resistant to Acid and Electric damage types",
        "Resistant to Acid and Fire damage types",
        "Resistant to Acid and Traps damage types",
        "Resistant to Electric and Fire damage types",
        "Resistant to Electric and Traps damage types",
        "Resistant to Fire and Traps damage types"
    };
    public static int[] armValues = new int[]{0,1,2,3,4,5};
    public static string[] atkLabels = new string[]{"Fire", "Electric", "Acid"};
    public static int[] atkValues = new int[]{0,1,2};

    public static string[] movLabels = new string[]{"Precise", "Balanced", "Fast"};
    public static int[] movValues = new int[]{0,1,2};
    
    public static string[] visLabels = new string[]{"Long", "Balanced", "Wide"};
    public static int[] visValues = new int[]{0,1,2};
    
    public static string[] specLabels = new string[]{"None", "Bombs", "Critical Hit", "First Attack",  "High Battery", "Efficient Power"};
    public static int[] specValues = new int[]{0,1,2,3,4,5};
}
