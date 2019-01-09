/*===============================================================================
Copyright (c) 2015-2018 PTC Inc. All Rights Reserved.
 
Copyright (c) 2010-2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Vuforia;
using Image = UnityEngine.UI.Image;

public class CloudTrackableEventHandler : DefaultTrackableEventHandler
{
    #region PRIVATE_MEMBERS
    CloudRecoBehaviour m_CloudRecoBehaviour;
    CloudContentManager m_CloudContentManager;
    #endregion // PRIVATE_MEMBERS

    public Text titulo;
    public Image imagen;
    public Sprite frozenImage;
    public Sprite elReyLeonImage;
    public Sprite toyStoryThreeImage;
    public Text descripcion;

    #region MONOBEHAVIOUR_METHODS
    protected override void Start()
    {
        base.Start();

        m_CloudRecoBehaviour = FindObjectOfType<CloudRecoBehaviour>();
        m_CloudContentManager = FindObjectOfType<CloudContentManager>();
        //imagen = GetComponent<Image>();
    }
    #endregion // MONOBEHAVIOUR_METHODS


    #region BUTTON_METHODS
    public void OnReset()
    {
        Debug.Log("<color=blue>OnReset()</color>");

        OnTrackingLost();
        TrackerManager.Instance.GetTracker<ObjectTracker>().GetTargetFinder<ImageTargetFinder>().ClearTrackables(false);       
    }
    #endregion BUTTON_METHODS


    #region PUBLIC_METHODS
    /// <summary>
    /// Method called from the CloudRecoEventHandler
    /// when a new target is created
    /// </summary>
    public void TargetCreated(TargetFinder.TargetSearchResult targetSearchResult)
    {
        m_CloudContentManager.HandleTargetFinderResult(targetSearchResult);
    }
    #endregion // PUBLIC_METHODS


    #region PROTECTED_METHODS
    
    protected override void OnTrackingFound()
    {
        Debug.Log("<color=blue>OnTrackingFound()</color>");

        base.OnTrackingFound();

        if (m_CloudRecoBehaviour)
        {
            // Changing CloudRecoBehaviour.CloudRecoEnabled to false will call TargetFinder.Stop()
            // and also call all registered ICloudRecoEventHandler.OnStateChanged() with false.
            m_CloudRecoBehaviour.CloudRecoEnabled = false;
        }

        if (m_CloudContentManager)
        {
            m_CloudContentManager.ShowTargetInfo(true);
        }
        
        Debug.Log("Carlos estas aqui " + base.mTrackableBehaviour.TrackableName);
        if (base.mTrackableBehaviour.TrackableName == "frozen")
        {
            titulo.text = "FROZEN";
            /*string m_Path;
            m_Path = Application.dataPath;

            Debug.Log(m_Path);*/
            imagen.sprite = frozenImage;
            descripcion.text = "When the newly-crowned Queen Elsa accidentally uses her " +
                "power to turn things into ice to curse her home in infinite winter, her " +
                "sister Anna teams up with a mountain man, his playful reindeer, and a snowman " +
                "to change the weather condition.";
        }
        else if(base.mTrackableBehaviour.TrackableName == "el_rey_leon")
        {
            titulo.text = "EL REY LEON";
            imagen.sprite = elReyLeonImage;
            descripcion.text = "To survive and grow into a powerful adult lion, Simba must perfect " +
                "his savage pounce and master fighting with all four paws. Scrap with hyenas, dash through " +
                "an elephant grave-yard, defeat your evil uncle Scar and recapture the Pridelands.";
        }
        else if (base.mTrackableBehaviour.TrackableName == "toy")
        {
            titulo.text = "TOY STORY 3";
            imagen.sprite = toyStoryThreeImage;
            descripcion.text = "The toys are mistakenly delivered to a day - care center instead of the attic " +
                "right before Andy leaves for college, and it's up to Woody to convince the other toys that they " +
                "weren't abandoned and to return home.";
        }
        else
        {
            Debug.Log(base.mTrackableBehaviour.TrackableName);
        }
        /*if (targetSearchResult.TargetName == "frozen")
        {
            Debug.Log("Frozen");
            titulo.text = "frozen";
        }*/
    }

    protected override void OnTrackingLost()
    {
        Debug.Log("<color=blue>OnTrackingLost()</color>");

        titulo.text = "";

        base.OnTrackingLost();

        if (m_CloudRecoBehaviour)
        {
            // Changing CloudRecoBehaviour.CloudRecoEnabled to true will call TargetFinder.StartRecognition()
            // and also call all registered ICloudRecoEventHandler.OnStateChanged() with true.
            m_CloudRecoBehaviour.CloudRecoEnabled = true;
        }

        if (m_CloudContentManager)
        {
            m_CloudContentManager.ShowTargetInfo(false);
        }
    }

    #endregion // PROTECTED_METHODS
}
