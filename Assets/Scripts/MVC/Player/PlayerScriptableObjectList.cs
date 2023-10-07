using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObjectList", menuName = "ScriptableObjectsList/PlayerScriptableObjectList")]

public class PlayerScriptableObjectList : ScriptableObject
{
    public PlayerScriptableObject[] playerObjects;
}
