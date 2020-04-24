using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    private Color32 active;
    private Color32 inactive;
    
    public Button maleButton;
    public Button femaleButton;

    public GameObject maleModel;
    public GameObject femaleModel;

    public Material maleMat;
    public Material femaleMat;
    private Material currentMat;

    public Slider xSlider;
    public Slider ySlider;
    public Slider logoTypeSlider;

    public ColorPicker picker;

    public Button colorFront, colorBack, colorOther;
    public Button animsOn, animsOff;
    public Button exitButton;
    private bool areAnimsOn;

    void Start()
    {
        active = new Color32(100, 215, 100, 255);
        inactive = new Color32(240, 90, 90, 255);

        TurnOnMaleModel();
        SetColorFront();
        TurnOffAnims();

        maleButton.onClick.AddListener(TurnOnMaleModel);
        femaleButton.onClick.AddListener(TurnOnFemaleModel);
        colorFront.onClick.AddListener(SetColorFront);
        colorBack.onClick.AddListener(SetColorBack);
        colorOther.onClick.AddListener(SetColorOther);
        animsOff.onClick.AddListener(TurnOffAnims);
        animsOn.onClick.AddListener(TurnOnAnims);
        exitButton.onClick.AddListener(ExitApp);
    }
    
    void Update()
    {
        SetLogoPosition();

        SetLogoType();
    }

    void ExitApp()
    {
        Application.Quit();
    }

    void SetColorFront()
    {
        colorFront.GetComponent<Image>().color = active;
        colorBack.GetComponent<Image>().color = inactive;
        colorOther.GetComponent<Image>().color = inactive;

        picker.onValueChanged.RemoveAllListeners();

        picker.onValueChanged.AddListener(color =>
        {
            currentMat.SetColor("_Color2", color);
        });
        currentMat.SetColor("_Color2", picker.CurrentColor);
    }

    void SetColorBack()
    {
        colorFront.GetComponent<Image>().color = inactive;
        colorBack.GetComponent<Image>().color = active;
        colorOther.GetComponent<Image>().color = inactive;

        picker.onValueChanged.RemoveAllListeners();

        picker.onValueChanged.AddListener(color =>
        {
            currentMat.SetColor("_Color", color);
        });
        currentMat.SetColor("_Color", picker.CurrentColor);
    }

    void SetColorOther()
    {
        colorFront.GetComponent<Image>().color = inactive;
        colorBack.GetComponent<Image>().color = inactive;
        colorOther.GetComponent<Image>().color = active;

        picker.onValueChanged.RemoveAllListeners();

        picker.onValueChanged.AddListener(color =>
        {
            currentMat.SetColor("_Color3", color);
        });
        currentMat.SetColor("_Color3", picker.CurrentColor);
    }

    void TurnOnAnims()
    {
        animsOn.GetComponent<Image>().color = active;
        animsOff.GetComponent<Image>().color = inactive;
        currentMat.SetInt("_IsAnimOn", 1);
        areAnimsOn = true;
    }

    void TurnOffAnims()
    {
        animsOn.GetComponent<Image>().color = inactive;
        animsOff.GetComponent<Image>().color = active;
        currentMat.SetInt("_IsAnimOn", 0);
        areAnimsOn = false;
    }

    void SetLogoPosition()
    {
        currentMat.SetTextureOffset("_MainTex", new Vector2(-xSlider.value, -ySlider.value));
    }

    void SetLogoType()
    {
        if(logoTypeSlider.value < 4)
        {
            currentMat.SetFloat("_CenterY", 0.1f);
        }
        else if(logoTypeSlider.value < 7)
        {
            currentMat.SetFloat("_CenterY", 0.4f);
        }
        else if (logoTypeSlider.value < 10)
        {
            currentMat.SetFloat("_CenterY", 0.8f);
        }
        currentMat.SetFloat("_CenterX", logoTypeSlider.value / 10f * 3f);
    }

    void TurnOnMaleModel()
    {
        maleModel.SetActive(true);
        femaleModel.SetActive(false);
        currentMat = maleMat;


        currentMat.SetInt("_ModelType", 0);
        maleButton.GetComponent<Image>().color = active;
        femaleButton.GetComponent<Image>().color = inactive;

        if(areAnimsOn)
        {
            TurnOnAnims();
        }
        else
        {
            TurnOffAnims();
        }
    }

    void TurnOnFemaleModel()
    {
        maleModel.SetActive(false);
        femaleModel.SetActive(true);
        currentMat = femaleMat;

        currentMat.SetInt("_ModelType", 1);
        maleButton.GetComponent<Image>().color = inactive;
        femaleButton.GetComponent<Image>().color = active;

        if (areAnimsOn)
        {
            TurnOnAnims();
        }
        else
        {
            TurnOffAnims();
        }
    }
}
