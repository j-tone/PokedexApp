using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexNamespace
{
    public class Pokemon
    {
        /*Lots of private variables lol*/
        private int dexNumber;
        private string dexLabel;
        private string name;
        private string classification;
        private string[] types = new string[2];
        private string height;
        private string weight;
        private int[] evos = new int[3];
 
        private string imageLocation;
        
        private int[] stats = new int[6];
        
        private string description;
        
        public Pokemon(string n, string imageL)
        {
            name = n;
            imageLocation = imageL;
            retrieveInfo();
        }

        /*Retrieves all the info for a specific pokemon text file in a specific order*/
        private void retrieveInfo()
        {
            StreamReader myIn = new StreamReader(name + ".txt");
            dexLabel = myIn.ReadLine();
            dexNumber = int.Parse(dexLabel);
            classification = myIn.ReadLine() + " Pokemon";
            types[0] = myIn.ReadLine();
            types[1] = myIn.ReadLine();
            height = myIn.ReadLine() + "m";
            weight = myIn.ReadLine() + "kg";
            description = myIn.ReadLine();
            evos[0] = int.Parse(myIn.ReadLine());
            evos[1] = int.Parse(myIn.ReadLine());
            evos[2] = int.Parse(myIn.ReadLine());
            for(int i = 0; i < 6; i++)
            {
                stats[i] = int.Parse(myIn.ReadLine());
            }
            myIn.Dispose();
        }

        /*Even more properties lolololol*/
        public int DexNumber
        {
            get { return dexNumber; }
        }
        public string DexLabel
        {
            get { return dexLabel; }
        }
        public string Name
        {
            get { return name; }
        }
        public string Height
        {
            get { return height; }
        }
        public string Weight
        {
            get { return weight; }
        }
        public string Classification
        {
            get { return classification; }
        }
        public string Type1
        {
            get { return types[0]; }
        }
        public string Type2
        {
            get { return types[1]; }
        }
        public string ImageLocation
        {
            get { return imageLocation;}
        }
        public string Description
        {
            get { return description; }
        }
        public int Evo1
        {
            get { return evos[0]; }
        }
        public int Evo2
        {
            get { return evos[1]; }
        }
        public int[] Evos
        {
            get { return evos; }
        }
        public int[] Stats
        {
            get { return stats; }
        }
        public int getStat(int c)
        {
            return stats[c];
        }
        
    }
}
