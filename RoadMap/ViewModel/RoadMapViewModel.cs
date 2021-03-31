using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Collections.Specialized;

namespace RoadMap.ViewModel
{
    class RoadMapViewModel : ObservableObject
    {

        #region Properties
       


        //private bool _isEmptyLabelVisible;
        //public bool IsEmptyLabelVisible
        //{
        //    get
        //    {


        //        return _isEmptyLabelVisible;
        //    }
        //    set
        //    {

        //        _isEmptyLabelVisible = value;
        //        OnPropertyChanged(nameof(IsEmptyLabelVisible));
        //    }

        //}



        private ObservableCollection<RoadmapItem> _roadmapItems;
        public ObservableCollection<RoadmapItem> RoadmapItems
        {
            get
            {
                if (_roadmapItems == null)
                {
                    _roadmapItems = new ObservableCollection<RoadmapItem>(Data.Settings.roadmapItems);

                    if (_roadmapItems.Count <= 0)
                        _roadmapItems.Add(new RoadmapItem("Click to create roadmap item"));
                }

                //if (_roadmapItems.Count > 0)
                //    IsEmptyLabelVisible = false;
                //else
                //    IsEmptyLabelVisible = true;

                return _roadmapItems;
            }
            set
            {

                _roadmapItems = value;

                OnPropertyChanged(nameof(RoadmapItems));
                SaveSettings();

            }
        }


        #endregion


        #region Commands


        private ICommand _roadmapItemLeftClicked;
        public ICommand RoadmapItemLeftClicked
        {
            get
            {
                if (_roadmapItemLeftClicked == null)
                    _roadmapItemLeftClicked = new RelayCommand(
                       (object o) =>
                       {
                           Debug.WriteLine("left click");

                           AddTextBlock(RoadmapAddingDirection.LEFT, o as RoadmapItem);

                       },
                       (object o) =>
                       {
                           return RoadmapItems.Count > 0;
                       });

                return _roadmapItemLeftClicked;

            }
        }

        private ICommand _RoadmapItemRightClicked;
        public ICommand RoadmapItemRightClicked
        {
            get
            {
                if (_RoadmapItemRightClicked == null)
                    _RoadmapItemRightClicked = new RelayCommand(
                       (object o) =>
                       {


                           Debug.WriteLine("right click");

                           AddTextBlock(RoadmapAddingDirection.RIGHT, o as RoadmapItem);
                       },
                       (object o) =>
                       {
                           return RoadmapItems.Count > 1;
                       });

                return _RoadmapItemRightClicked;

            }
        }



        private ICommand _roadmapItemMiddleClicked;
        public ICommand RoadmapItemMiddleClicked
        {
            get
            {
                if (_roadmapItemMiddleClicked == null)
                    _roadmapItemMiddleClicked = new RelayCommand(
                       (object o) =>
                       {
                           if (o is RoadmapItem)
                           {

                               RoadmapItems.Remove(o as RoadmapItem);
                           }

                           Debug.WriteLine("wheel click");

                       },
                       (object o) =>
                       {
                           return RoadmapItems.Count > 1;
                       });

                return _roadmapItemMiddleClicked;

            }
        }


        #endregion


        private IModuleController _host;
        private RoadMap coreModule;



        public RoadMapViewModel(IModuleController host, RoadMap roadMap)
        {
            this._host = host;
            this.coreModule = roadMap;
            

        }

        private void RoadmapItemsItemChanged(object sender, PropertyChangedEventArgs e)
        {
            Data.Settings.roadmapItems = RoadmapItems.ToList();
            SaveSettings();


        }

        private void RoadmapItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Data.Settings.roadmapItems = RoadmapItems.ToList();
            SaveSettings();

        }



        private void AddTextBlock(RoadmapAddingDirection direction, RoadmapItem from)
        {


            RoadmapItem roadmapItem = new RoadmapItem("empty");

            if (direction == RoadmapAddingDirection.LEFT)
                RoadmapItems.Insert(RoadmapItems.IndexOf(from), roadmapItem);
            if (direction == RoadmapAddingDirection.RIGHT)
                RoadmapItems.Insert(RoadmapItems.IndexOf(from) + 1, roadmapItem);



            SaveSettings();

        }
        public void SaveSettings()
        {
            _host.SaveModuleInformation(coreModule.GetModuleName(), "RoadMapSettings", Data.Settings);
        }

    }



}
