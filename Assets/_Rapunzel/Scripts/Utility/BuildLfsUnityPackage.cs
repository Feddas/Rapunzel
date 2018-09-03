﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class BuildLfsUnityPackage
{
    [MenuItem("File/Create RapunzelLfs.unitypackage")]
    public static void CreateBuild()
    {
        string[] lfsFiles =
        {
            "Assets/_Rapunzel/Credits/ColiasGladKeyPickup.ogg",
            "Assets/_Rapunzel/Credits/ColiasPutKeyDown.ogg",
            "Assets/_Rapunzel/Credits/DadDoorNeedsKey.ogg",
            "Assets/_Rapunzel/Credits/DadRichInNoTime.ogg",
            "Assets/_Rapunzel/Credits/DadWhatKeyFor.ogg",
            "Assets/_Rapunzel/Credits/Veronica2Coins2Pass.ogg",
            "Assets/_Rapunzel/Credits/VeronicaNothingHurts.ogg",
            "Assets/_Rapunzel/Credits/VeronicaThanksForCoins.ogg",

            "Assets/_Rapunzel/Credits/FamilyColias.png",
            "Assets/_Rapunzel/Credits/FamilyDad.png",
            "Assets/_Rapunzel/Credits/FamilyVeronica.png",

        };
        UnityEditor.AssetDatabase.ExportPackage(lfsFiles, "RapunzelLfs" + Application.version + ".unitypackage");

    }
}
#endif