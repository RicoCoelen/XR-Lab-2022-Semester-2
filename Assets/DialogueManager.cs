using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //Meerdere character voor het gevoel van community
    public enum Character {Puber, Kind, Kunstenaar}

    public Character character;
    public Image characterSprite;
    public Sprite puberSprite;
    


    private void Start()
    {
        switch (character)
        {
            case Character.Puber:
                {
                    characterSprite.sprite = puberSprite;
                    break;
                }
            case Character.Kind:
                {

                    break;
                }
            case Character.Kunstenaar:
                {

                    break;
                }

        }
    }
}
