using UnityEngine;

public class NPCNameDisplay : MonoBehaviour
{
    [Header("Nama NPC")]
    public string npcName;             
    public Vector3 nameOffset = new Vector3(0, 1.5f, 0); 

    private GameObject nameTextObject; 
    private Camera mainCamera;         

    void Start()
    {
        mainCamera = Camera.main;

        nameTextObject = new GameObject("NPCName");
        nameTextObject.transform.SetParent(transform);

        TextMesh textMesh = nameTextObject.AddComponent<TextMesh>();
        textMesh.text = npcName;
        textMesh.characterSize = 0.2f;  
        textMesh.fontSize = 64;         
        textMesh.anchor = TextAnchor.MiddleCenter; 
        textMesh.color = Color.white;  
        textMesh.alignment = TextAlignment.Center;

        nameTextObject.transform.localPosition = nameOffset;
    }

    void Update()
    {
        if (mainCamera != null)
        {
            nameTextObject.transform.rotation = mainCamera.transform.rotation;
        }
    }
}
