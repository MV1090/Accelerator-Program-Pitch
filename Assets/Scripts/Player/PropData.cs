using UnityEngine;

[CreateAssetMenu(fileName = "NewPropData", menuName = "VR Prop Hunt/Prop Data", order = 1)]
public class PropData : ScriptableObject
{
    [Header("Other Properties")]
    public string propName;

    [Header("Visual & Prefab")]
    public GameObject propPrefab;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float jumpHeight = 1f;

    [Header("Audio Settings")]
    public AudioClip deathSound;


}