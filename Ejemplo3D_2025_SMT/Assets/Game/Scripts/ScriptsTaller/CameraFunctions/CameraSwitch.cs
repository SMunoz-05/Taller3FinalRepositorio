using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera thirdPearsonCamera;
    public Camera firstPearsonCamera;
    private bool firstPearsonEnabled = true;

    //Weapon Change view

    public Transform[] weaponsTransformFirstPearson;
    public Transform[] weaponsTransformThirdPearson;

    public GameObject[] weapons;

    public bool disableMeshPlayerFirstPearson = true;
    public SkinnedMeshRenderer meshPlayer;

    private void Start()
    {
        if (disableMeshPlayerFirstPearson)
        {
            meshPlayer.enabled = false; 
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            firstPearsonEnabled =! firstPearsonEnabled;
            ChangeCamera();
        }
    }

    public void ChangeCamera()
    {
        if (firstPearsonEnabled)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                firstPearsonEnabled = !firstPearsonEnabled;
                ChangeCamera();
            }
            firstPearsonCamera.enabled = true; 
            thirdPearsonCamera.enabled=false; 
            ChangeWeaponsFirstPearson();
        }

        else
        {
            meshPlayer.enabled = true;
            firstPearsonCamera.enabled = false;
            thirdPearsonCamera.enabled = true;
            ChangeWeaponsThirdPearson();
        }
    }

    public void ChangeWeaponsFirstPearson()
    {

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].transform.position = weaponsTransformFirstPearson[i].transform.position;
            weapons[i].transform.rotation = weaponsTransformFirstPearson[i].transform.rotation;
            weapons[i].transform.localScale = weaponsTransformFirstPearson[i].transform.localScale;


        }

    }

    public void ChangeWeaponsThirdPearson()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].transform.position = weaponsTransformThirdPearson[i].transform.position;
            weapons[i].transform.rotation = weaponsTransformThirdPearson[i].transform.rotation;
            weapons[i].transform.localScale = weaponsTransformThirdPearson[i].transform.localScale;


        }
    }
}
