using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Google.Protobuf;
using Google.Cloud.Dialogflow.V2;


public class DialogFlow : MonoBehaviour
{
    //Unity construct to get and play music
    public AudioSource audioIn, audioOut;
    string MicName;//, relativeSoundPath;
    SessionsClient client;

    //Defined the output audio we want to get back
    private OutputAudioConfig outputAudioConfig; 

    //Defined the output audio we would get
    private InputAudioConfig inputAudioConfig;

    //Defines the query will be of a the type audio
    private QueryInput query;

    //Defining the session Later on, 
    //Defining the required data
    //to get agent name and session id refer to the Read.md to file
    private string session, 
        agent = "AGENT NAME",
        sessionId = "INSERT SESSION KEY HERE";
        

    private void Start()
    {
        //Linear16 is for .wav files which are supported by this program
        //and it needs to have a sample rate of 16000 or 16K Hz
        outputAudioConfig = new OutputAudioConfig()
        {
            AudioEncoding = OutputAudioEncoding.Linear16,
            SampleRateHertz = 16000,
            SynthesizeSpeechConfig = new SynthesizeSpeechConfig()
            {
                Voice = new VoiceSelectionParams()
                {
                    SsmlGender = SsmlVoiceGender.Female
                }
            }
        };

        //Defines what kind of audio bytes we are sending
        inputAudioConfig = new InputAudioConfig()
        {
            AudioEncoding = AudioEncoding.Linear16,
            LanguageCode = "en-US",
            SampleRateHertz = 16000
        };


        //Specifies the audio config to show that the input is audio bytes
        query = new QueryInput()
        {
            AudioConfig = inputAudioConfig
        };

        session = string.Format("projects/{0}/agent/sessions/{1}", agent, sessionId);

    }

    //Don't quite understand whether this is called before or after Start
    private void Awake()
    {

        //Need to set the environment variable to create a the client
        Environment.SetEnvironmentVariable(
            "GOOGLE_APPLICATION_CREDENTIALS",
            @"INSERT JSON FILE LOCATION HERE");
        client = SessionsClient.Create();//creating the client

        //Creating two AudioSource objects through code
        audioIn = gameObject.AddComponent<AudioSource>();
        audioOut = gameObject.AddComponent<AudioSource>();

        //The mic name is required to record audio
        MicName = Microphone.devices[0].ToString();
        
    }

    /*
     * We need the recording to be shorter than 60 seconds else dialogflow 
     * will reject the input
     */
    void StartRecording()
    {

        Debug.Log("Starting the Recording");
        audioIn.clip = Microphone.Start(MicName, true, 5, 16000);

    }

    //After the recording is over it will just send the audio file
    //after having worked on it for a bit to Dialogflow
    //and output the audio
    void StopRecording()
    {
        Microphone.End(MicName);
        Debug.Log("Ended recording now starting the api call?");

        //The input needs to be in the form of a byteString
        //byteString is a class in google.protobuf;
        //Using the WavBuf utility to convert the audioClip into 
        //a Wav byte file with the header and everything
        byte[] bytes = WavBuf.Save(audioIn.clip);

        //GetResponse will create a DetectIntentRequest and make the DetectIntent API
        //Call
        //TODO: make this an async call
        var response = GetResponse(bytes);

        //This will take a DetectIntentResponse and get the Audio files from it
        //then use the Bytes to create a clip and further play the audio
        PlayAudio(response);
    }


    DetectIntentResponse GetResponse(byte[] array)
    {
        var byteString = ByteString.CopyFrom(array);
        Debug.Log("Getting requst and making request");
        var request = new DetectIntentRequest()
        {
            InputAudio = byteString,
            QueryInput = query,
            OutputAudioConfig = outputAudioConfig,
            Session = session
        };

        return client.DetectIntent(request);
    }

    void PlayAudio(DetectIntentResponse response)
    {
        Debug.Log("Playing the audio");
        byte[] bytes = response.OutputAudio.ToByteArray();

        var wav = new Wav(bytes);
        var audioClip = AudioClip.Create("Sound", 
            wav.SampleCount, 1, wav.Frequency, false);
        audioClip.SetData(wav.LeftChannel, 0);

        audioOut.clip = audioClip;
        audioOut.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartRecording();
        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            StopRecording();
        }
    }
}
