using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class DictationScript : MonoBehaviour {
    //TODO: rename this thing into something
    //REMEMBER: all we want from this is the input stuff
    //This is the file that will handle all the required details for all the programs
    //now we will have a save file prob sav wav
    //then a stt
    //then the dialogflow stuff
    //then the tts stuff


    //private DictationRecognizer dictationRecognizer;

    //void Start()
    //{
    //    dictationRecognizer = new DictationRecognizer();

    //    dictationRecognizer.DictationResult += (text, confidence) =>
    //    {
    //        Debug.LogFormat("Dictation result: {0}", text);
    //        //m_Recognitions.text += text + "\n";
    //    };

    //    dictationRecognizer.DictationHypothesis += (text) =>
    //    {
    //        Debug.LogFormat("Dictation hypothesis: {0}", text);
    //        // m_Hypotheses.text += text;
    //    };

    //    dictationRecognizer.DictationComplete += (completionCause) =>
    //    {
    //        if (completionCause != DictationCompletionCause.Complete)
    //            Debug.LogFormat("Dictation completed unsuccessfully: {0}.", completionCause);
    //    };

    //    dictationRecognizer.DictationError += (error, hresult) =>
    //    {
    //        // Debug.LogFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
    //    };
    //}

    //void Update() {
    //    // Debug.Log(dictationRecognizer.Status);
    //    // if (Input.GetKeyDown(KeyCode.Space)) dictationRecognizer.Start();
    //    // if (Input.GetKeyUp(KeyCode.Space)) dictationRecognizer.Stop();
    //    // Debug.Log(dictationRecognizer.Status);
    //}
}