using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverChallenge
{
    public class TerrainZone
    {
        public TerrainZone(string[] commandLineItems, string dimensions)
        {
            this.HorizontalLength = Convert.ToInt32(commandLineItems[0]);
            this.VerticalLength = Convert.ToInt32(commandLineItems[1]);
            this.CartesianZoneGrid = new string[this.HorizontalLength, this.VerticalLength];
        }
        public int HorizontalLength { get; set; }
        public int VerticalLength { get; set; }
        public string[,] CartesianZoneGrid { get; set; }
    }
}
