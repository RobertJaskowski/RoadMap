using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadMap
{
    public class Data
    {
        private static RoadMapSettings _settings;

        public static RoadMapSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = new RoadMapSettings()
                    {
                        roadmapItems = new List<RoadmapItem>()
                    };

                }
                return _settings;
            }
            internal set
            {
                _settings = value;


            }
        }
    }
}
