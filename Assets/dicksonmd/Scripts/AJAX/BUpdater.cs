using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[Serializable]
public class UpdateDirectory
{
    public string version;
    public string link;
}


public class BUpdater : MonoBehaviour
{

    public string checkForUpdateLink = "";

    public BWebLinkButton updateButton;

    // Use this for initialization
    IEnumerator Start()
    {
        if (!BPersistData.i.isPublic)
        {
            updateButton.GetComponentInChildren<Text>().text = "Preview version";
            this.enabled = false;
            yield break;
        }
        updateButton.GetComponentInChildren<Text>().text = "Checking for update...";
        WWW www = new WWW(checkForUpdateLink);
        yield return www;

        if (www.error != null)
        {
            updateButton.GetComponentInChildren<Text>().text = "Error connecting to update server.";
        }

        UpdateDirectory updateDirectory;
        try
        {
            updateDirectory = JsonUtility.FromJson<UpdateDirectory>(www.text);

            if (updateDirectory.version != BPersistData.i.version)
            {
                updateButton.setLink(updateDirectory.link);
                updateButton.GetComponentInChildren<Text>().text = "Click here to update to version " + updateDirectory.version;
            }
            else
            {
                updateButton.gameObject.SetActive(false);
                updateButton.GetComponentInChildren<Text>().text = "";
            }
        }
        catch (Exception)
        {
            updateButton.GetComponentInChildren<Text>().text = "Error parsing update info";
        }



    }


}
