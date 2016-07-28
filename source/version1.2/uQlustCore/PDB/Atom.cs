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

using System;
using System.Collections.Generic;
//using System.Windows.Media;
//using System.Windows.Media.Media3D;
using System.Globalization;
using uQlustCore;

namespace uQlustCore.PDB
{
	public class Point3D
	{
		private short x,y,z;
		
        public float X
        {
            set
            {
                x = (short)(value*10);
            }
            get
            {
                return ((float)x) / 10;
            }
        }
        public float Y
        {
            set
            {
                y = (short)(value * 10);
            }
            get
            {
                return ((float)y) / 10;
            }
        }
        public float Z
        {
            set
            {
                z = (short)(value * 10);
            }
            get
            {
                return ((float)z) / 10;
            }
        }

		public Point3D(float xp,float yp, float zp)
		{
			x=(short)(xp*10);y=(short)(yp*10);z=(short)(zp*10);
		}
        public Point3D()
        {
            x = y = z = 0;
        }
        public static Point3D operator +(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.x + p2.x, p1.y + p2.y, p1.z + p2.z);
        }
        public static Point3D operator -(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z);
        }
		
	}
    public class Atom
    {
        private static Dictionary<byte, string> AtomNamesToBytes = new Dictionary<byte, string>();
        private static Dictionary<string, byte> AtomNames = new Dictionary<string, byte>();

      // private byte atomIndex;

        public string AtomName;
        /*
        {
            get
            {
                return AtomNamesToBytes[atomIndex];
            }
            set
            {
                if (!AtomNames.ContainsKey(value))
                {
                    AtomNames.Add(value, (byte)(AtomNames.Keys.Count + 1));
                    AtomNamesToBytes.Add(AtomNames[value], value);
            
                }
                atomIndex = AtomNames[value];
            }
        }*/

        //public char ResidueName;
        //public char ChainIdentifier;
       // public short ResidueSequenceNumber;
        public short[] tabParam = new short[3];
        public Point3D Position;
        protected virtual bool CheckResidue(string residueName)
        {
            if (Residue.IsAminoName(residueName))
                return true;
            
            return false;
        }
        protected virtual char ResidueIdentifier(string residueName)
        {
            return Residue.GetResidueIdentifier(residueName);
        }
        protected virtual bool CheckAtomName(string atName)
        {
            if (atName.StartsWith("H"))
                return false;

            return true;
        }
        public  string ParseAtomLine(Molecule molecule, string pdbLine, PDBMODE flag)
        {
            
            try
            {
                string atomName = pdbLine.Substring(12, 4).Trim();

                if (!CheckAtomName(atomName))
                    return "Wrong Atom name: " + atomName+" atom will be removed";

                string residueName = pdbLine.Substring(17, 3).Trim();
                if (!CheckResidue(residueName))
                    return "Incorrect residue name: "+residueName;

                this.AtomName = atomName;
                //ResidueName = ResidueIdentifier(residueName);
                tabParam[0] = (short)ResidueIdentifier(residueName);

                //ResidueSequenceNumber = Convert.ToInt16(pdbLine.Substring(22, 4));
                tabParam[2] = Convert.ToInt16(pdbLine.Substring(22, 4));

                //ChainIdentifier = (pdbLine.Substring(21, 1))[0];
                tabParam[1] = (short)(pdbLine.Substring(21, 1))[0];
                //if (ResidueName == 'O') ChainIdentifier = ' ';
                //if (tabParam[0] == 'O') ChainIdentifier = ' ';
                if (tabParam[0] == 'O') tabParam[1] =(short) ' ';
                else //if (ChainIdentifier == ' ') ChainIdentifier = '1';
                    if (tabParam[1] == ' ') tabParam[1] =(short) '1';
              
                    float x = float.Parse(pdbLine.Substring(30, 7), CultureInfo.InvariantCulture);
                    float y = float.Parse(pdbLine.Substring(38, 7), CultureInfo.InvariantCulture);
                    float z = float.Parse(pdbLine.Substring(46, 7), CultureInfo.InvariantCulture);


                    if (flag != PDBMODE.ONLY_SEQ)                    
                        Position = new Point3D(x, y, z);
                    
            }
            catch(Exception ex)
            {
                return "Error in reading ATOM line: " + ex.Message;
            }
			
            return null;
        }

    }
}
