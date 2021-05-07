using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class socket : SocketIOComponent
{

    public GameObject DetectLocation;

    public override void Start()
    {
        base.Start();
        setupEvents();
    }

    public override void Update()
    {
        base.Update();
    }

    private void setupEvents(){
        On("open", (E) => {
            Debug.Log("Connected");
        });

        On("UserKeyPair", (E) => {
            const PublicKey = E.publicKey;
            const PrivateKey = E.privateKey;
        });

        if(DetectLocation.Card){
            socket.Emit("Card");
        }
    }

}
