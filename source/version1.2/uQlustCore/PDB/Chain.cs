//=============================================================================
// This file is part of The Scripps Research Institute's C-ME Application built
// by InterKnowlogy.  
//
// Copyright (C) 2006, 2007 Scripps Research Institute / InterKnowlogy, LLC.
// All rights reserved.
//
// For information about this application contact Tim Huckaby at
// TimHuck@InterKnowlogy.com or (760) 930-0075 x201.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//=============================================================================

using System.Collections.Generic;
using System.Text;
//using System.Windows.Media;

namespace uQlustCore.PDB
{
    /// <summary>
    /// Container object to group residues by chain and set chain-based temperature colors.
    /// </summary>
    public class Chain
    {
        public string chainSequence;
        private char chainIdentifier;
        private List<Residue> residues;

        internal Chain(char chainIdentifier)
        {
            this.chainIdentifier = chainIdentifier;
            this.residues = new List<Residue>();
        }

        public void ClearChain()
        {
            residues.Clear();
        }
        internal char ChainIdentifier { get { return this.chainIdentifier; } }

        internal List<Residue> Residues { get { return this.residues; } }
        public void CreateChainString()
        {
            StringBuilder st = new StringBuilder(residues.Count);           
            for (int i = 0; i < residues.Count; i++)
                    st.Append(residues[i].ResidueName);

            chainSequence = st.ToString();
        }
    }
}
