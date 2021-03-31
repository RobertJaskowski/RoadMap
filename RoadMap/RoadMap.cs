using RoadMap.View;
using RoadMap.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RoadMap
{
    public class RoadMap : ICoreModule
    {
        public string ModuleName => "RoadMap";

        private UserControl _view;
        public UserControl View
        {
            get
            {
                if (_view == null)
                {
                    _view = new RoadMapView();
                    _view.DataContext = new RoadMapViewModel(_host, this);
                }

                return _view;
            }
            set { _view = value; }
        }



        private UserControl _settingsView;
        public UserControl SettingsView
        {
            get
            {
                if (_settingsView == null)
                {
                    _settingsView = new RoadMapSettingsView();
                    _settingsView.DataContext = new RoadMapSettingsViewModel(_host, this);
                }

                return _settingsView;
            }
            set { _settingsView = value; }
        }
        private IModuleController _host;

        public string GetModuleName()
        {
            return ModuleName;
        }

        public ModulePosition GetModulePosition()
        {
            return ModulePosition.BOT;
        }

        public UserControl GetModuleUserControlView()
        {
            return View;

        }

        public UserControl GetSettingsUserControlView()
        {
            return SettingsView;
        }

        public void Init(IModuleController host)
        {
            _host = host;
            Data.Settings = _host.LoadModuleInformation<RoadMapSettings>(GetModuleName(), "RoadMapSettings");

        }

        public void OnFullViewEntered()
        {

        }

        public void OnInteractableEntered()
        {

        }

        public void OnInteractableExited()
        {

        }

        public void OnMinViewEntered()
        {

        }

        public void ReceiveMessage(string message)
        {

        }

        public void Start()
        {
        }

        public void Stop()
        {

            Data.Settings.roadmapItems = ((RoadMapViewModel)View.DataContext).RoadmapItems.ToList();
            ((RoadMapViewModel)View.DataContext).SaveSettings();
        }
    }
}
