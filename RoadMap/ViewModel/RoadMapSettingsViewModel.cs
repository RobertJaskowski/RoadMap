using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadMap.ViewModel
{
    class RoadMapSettingsViewModel
    {
        private IModuleController host;
        private RoadMap roadMap;

        public RoadMapSettingsViewModel(IModuleController host, RoadMap roadMap)
        {
            this.host = host;
            this.roadMap = roadMap;
        }
    }
}
