using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uQlustCore.Interface;
using uQlustCore.Profiles;

namespace uQlustCore
{
    public enum COL_SELECTION
    {
        ENTROPY,
        META_COL
    };
    public class HashCInput:BaseCInput,IAutomaticProfiles
    {
        [Description("Hash clustering: Do you want to use regularization")]
        public bool regular=false;
        [Description("Regularization: size of the window ")]
        public int wSize=7;
        [Description("Regularization: Threshold distance below the threshold will lead to consensus state ")]
        public int regThreshold = 1;
        [Description("Combine clusters based on the hamming distance between them")]
        public bool combine = false;
        [Description("Hamming distance below this threshold will lead to joining clusters")]
        public int hDistance = 1;
        [Description("Use 1Djury to find consesnsus states ")]
        public bool jury;
        [Description("Profile name")]
        public string profileName;
        [Description("Profile name for regularization")]
        public string profileNameReg;
        [Description("Find clusters by automatic columns selection")]
        public bool fcolumns = false;
        [Description("Columns selection method")]
        public COL_SELECTION selectionMethod;
        [Description("Number of relevant clusters")]
        public int relClusters=5;
        [Description("Percent of data that must be in the relevant clusters")]
        public int perData=30;
        [Description("Number of requested clusters")]
        public int reqClusters = 1000;

        public void GenerateAutomaticProfiles(string fileName)
        {
            ProfileTree t = ProfileAutomatic.AnalyseProfileFile(fileName, SIMDIST.SIMILARITY);
            string profileName = "automatic_similarity.profile";
            t.SaveProfiles(profileName);
            this.profileName = profileName;
            this.profileNameReg = profileName;
        }

    }
}
