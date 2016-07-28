using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uQlustCore.Interface;
using uQlustCore.Profiles;

namespace uQlustCore
{
    public class KmeansInput:BaseCInput,IAutomaticProfiles
    {
        [Description("K-means algorithm: K in K-means")]
        public int maxK;
        [Description("K-means algorithm: How To initialize k-means (RANDOM,JURY1D)")]
        public Initialization kMeans_init;
        [Description("K-means algorithm: name of the file with alignment needed when 1djury for initialization is used")]
        public string alignFileName;
        [Description("If you want to use automaticly generated alignment set this variable to true")]
        public bool alignGenerate = true;
        [Description("Distance measures used for clustering")]
        public DistanceMeasures kDistance = DistanceMeasures.HAMMING;
        [Description("In case of RMSD or MAXSUB which atoms should be used")]
        public PDB.PDBMODE kAtoms = PDB.PDBMODE.ONLY_CA;
        [Description("Path to the profile for weighted hamming distance [KMEANS]")]
        public string hammingProfile;
        [Description("Path to the profile for 1djury for initialization[KMEANS]")]
        public string jury1DProfile;
        [Description("Max number of iterations [KMEANS]")]
        public int maxIter=30;
        [Description("Use 1DJury to find reference vectors")]
        public bool reference1Djury;

        public void GenerateAutomaticProfiles(string fileName)
        {
            ProfileTree t = ProfileAutomatic.AnalyseProfileFile(fileName, SIMDIST.DISTANCE);
            string profileName = "automatic_distance.profile";
            t.SaveProfiles(profileName);
            hammingProfile = profileName;
            t = ProfileAutomatic.AnalyseProfileFile(fileName, SIMDIST.SIMILARITY);
            profileName = "automatic_similarity.profile";
            t.SaveProfiles(profileName);
            jury1DProfile = profileName;
        }


    }
}
