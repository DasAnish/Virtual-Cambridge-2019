# Virtual-Cambridge-2019
## Requirements
* Unity
* Visual Studio
* GoogleVR Unity Library

##Setup
Make a fresh Unity Project
Clone this project into assest
Once you are done make sure GoogleVR is added within Unity -> See [Google VR](https://github.com/googlevr/gvr-unity-sdk)

The Project also has some more depencies: 
* Idle MoCap (found here: [Idle MoCap](https://assetstore.unity.com/packages/3d/animations/idle-mocap-28345))
* Modern Female Proffessional Secretary (found here: [Secretary](https://assetstore.unity.com/packages/3d/characters/humanoids/modern-female-professional-secretary-44429))
* Treasure Box (found here: [Treasure Box](https://assetstore.unity.com/packages/3d/props/classic-treasure-box-8952)) 

One you have added all these you will need to add the Images and the Materials (present in Resources)
You should be able to download them from the Google Drive. One you download them put them in the assests folder as well.
(Since these are large files this will take some time)

Check if it runs.
If it doesn't run then use the package manager (in visual studio) to download Google Dialogflow
using ``` Install-Package Google.Cloud.Dialogflow.V2 -Version 1.0.0
```

**Your Project should be ready to run and test**
