using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using uQlustCore;


namespace uQlustCore.PDB
{
    public class Molecule
    {
        protected List<Atom> atoms;
        protected List<Residue> residues;
        protected List<Chain> chains;
        protected PDBMODE flag;    

        internal Molecule(PDBMODE flag)
        {
            this.flag = flag;
        }
        public virtual bool ReadMolecule(StreamReader pdbStream)
        {
            bool res;
            res = this.ReadAtoms(pdbStream);
            if (!res)
                return false;
            this.CreateResidues();
            this.CreateChains();

            atoms.Clear();
            if(chains.Count==0)
                return false;
            residues = chains[0].Residues;

            if (flag == PDBMODE.ONLY_SEQ)
            {
                atoms.Clear();
                residues.Clear();
                foreach (var item in chains)
                    item.ClearChain();
            }

            return true;
        }
        
        
        public List<Atom> Atoms { get { return this.atoms; } }

        public List<Residue> Residues { get { return this.residues; } }

        public  List<Chain> Chains { get { return this.chains; } }


        internal virtual bool ReadAtoms(StreamReader pdbStream)
        {
            List<Atom> auxList = new List<Atom>();
            //using (StreamReader pdbReader = new StreamReader(pdbStream))
            //{
                string pdbLine = pdbStream.ReadLine();
               
                while (pdbLine != null)
                {
                    if (pdbLine.StartsWith("ENDMDL") || pdbLine.StartsWith("TER") || pdbLine.StartsWith("END")) break;

                    if (pdbLine.StartsWith("ATOM") || pdbLine.StartsWith("HETATM"))
                    {
                        if (pdbLine.Contains("\t"))
                        {
                            ErrorBase.AddErrors("Error in file: " + ((FileStream)pdbStream.BaseStream).Name + " " + "ATOM line containes tab what is not allowed");
                            return false;
                        }
                        Atom atom = new Atom();
                        string error=atom.ParseAtomLine(this, pdbLine,flag);
                        if (error==null)
                        {
                            //Check if the atom already exists!!
                            if (flag == PDBMODE.ONLY_CA || flag == PDBMODE.CA_CB || flag == PDBMODE.ONLY_CB || flag==PDBMODE.ONLY_SEQ)
                            {
                                if (flag == PDBMODE.ONLY_CA || flag == PDBMODE.CA_CB || flag == PDBMODE.ONLY_SEQ)
                                    if (atom.AtomName == "CA")                                                                            
                                        auxList.Add(atom);                                    


                                if (flag == PDBMODE.ONLY_CB || flag == PDBMODE.CA_CB )//|| (atom.ResidueName=='G' && flag==PDBMODE.ONLY_CA))
                                    if (atom.AtomName == "CB")
                                        auxList.Add(atom);
                            }
                            else
                                auxList.Add(atom);
                        }
                        else
                            ErrorBase.AddErrors("Error in file: " + ((FileStream)pdbStream.BaseStream).Name+" "+error);
      
                    }

                    pdbLine = pdbStream.ReadLine();
                }
                this.atoms = new List<Atom>(auxList);
          //  }
            return true;
        }
        
        private void CreateResidues()
        {
            List<Residue> auxRes = new List<Residue>();
            List<Atom> localAtoms = new List<Atom>();
            //this.residues = new List<Residue>();

            Residue residue = null;
           
            foreach (Atom atom in this.atoms)
            {
                //if (residue == null ||  atom.ResidueSequenceNumber != residue.ResidueSequenceNumber||
                if (residue == null || atom.tabParam[2] != residue.ResidueSequenceNumber ||
                    atom.tabParam[1] != residue.ChainIdentifier)
                    //atom.ChainIdentifier != residue.ChainIdentifier)
                {
                    residue = new Residue(this, atom,flag);                    
                    auxRes.Add(residue);
                }
                else
                {
                    
                    bool test = false;                   
                    foreach(var item in residue.Atoms )
                        if (atom.AtomName == item.AtomName)
                        {
                            ErrorBase.AddErrors("Residue " + item.tabParam[0] + " has two the same atoms " + atom.AtomName);
                            test = true;
                        }
                    if (!test)
                        residue.Atoms.Add(atom);
                        //residue.AddAtom(atom);
                    
                }
                atom.tabParam = null;

            }
            this.residues = new List<Residue>(auxRes);
            auxRes = null;

        }
       

        private void CreateChains()
        {
            this.chains = new List<Chain>();

            Chain chain = null;

            foreach (Residue residue in this.residues)
            {
                    if (chain == null || residue.ChainIdentifier != chain.ChainIdentifier)
                    {
                        chain = new Chain(residue.ChainIdentifier);
                        this.chains.Add(chain);
                    }

                    chain.Residues.Add(residue);

                    residue.tabParam = null;
            }

            for(int i=0;i<chains.Count;i++)
              chains[i].CreateChainString();

        }
    }
}
