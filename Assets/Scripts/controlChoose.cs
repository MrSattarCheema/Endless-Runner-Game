using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class controlChoose : MonoBehaviour
{
    ToggleGroup toggleGroup;
    public Toggle touch;
    public Toggle button;
    string controlName;
    public GameObject controlUi;
    // Start is called before the first frame update
    void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        if (PlayerPrefs.HasKey("Controls"))
        {
            controlName = PlayerPrefs.GetString("Controls");
            Debug.Log(controlName + "saved string");
            if (controlName == "Button")
            {
                button.isOn = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void saveControl()
    {

        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        Debug.Log(toggle.name);
        PlayerPrefs.SetString("Controls", toggle.name);
        PlayerPrefs.Save();
        controlUi.SetActive(false);
    }
}
