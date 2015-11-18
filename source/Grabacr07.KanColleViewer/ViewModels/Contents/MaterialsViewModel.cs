using Clapton.Extensions;
using Grabacr07.KanColleViewer.Models.Settings;
using Grabacr07.KanColleViewer.Properties;
using Grabacr07.KanColleWrapper;
using Livet;
using MetroTrilithon.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grabacr07.KanColleViewer.ViewModels.Contents
{
    public class MaterialsViewModel : ViewModel
	{
		public Materials Model { get; }

		public ICollection<MaterialViewModel> Values { get; }

		#region SelectedItem1 変更通知プロパティ

		private MaterialViewModel _SelectedItem1;

		public MaterialViewModel SelectedItem1
		{
			get { return this._SelectedItem1; }
			set
			{
				if (this._SelectedItem1 != value)
				{
					this._SelectedItem1 = value;
					this.RaisePropertyChanged();
					KanColleSettings.DisplayMaterial1.Value = value?.Key;
				}
			}
		}

		#endregion

		#region SelectedItem2 変更通知プロパティ

		private MaterialViewModel _SelectedItem2;

		public MaterialViewModel SelectedItem2
		{
			get { return this._SelectedItem2; }
			set
			{
				if (this._SelectedItem2 != value)
				{
					this._SelectedItem2 = value;
					this.RaisePropertyChanged();
					KanColleSettings.DisplayMaterial2.Value = value?.Key;
				}
			}
		}

        #endregion

        #region SelectedItem3 変更通知プロパティ

        private MaterialViewModel _SelectedItem3;

        public MaterialViewModel SelectedItem3
        {
            get { return this._SelectedItem3; }
            set
            {
                if (this._SelectedItem3 != value)
                {
                    this._SelectedItem3 = value;
                    this.RaisePropertyChanged();
                    KanColleSettings.DisplayMaterial3.Value = value?.Key;
                }
            }
        }

        #endregion

        #region SelectedItem4 変更通知プロパティ

        private MaterialViewModel _SelectedItem4;

        public MaterialViewModel SelectedItem4
        {
            get { return this._SelectedItem4; }
            set
            {
                if (this._SelectedItem4 != value)
                {
                    this._SelectedItem4 = value;
                    this.RaisePropertyChanged();
                    KanColleSettings.DisplayMaterial4.Value = value?.Key;
                }
            }
        }

        #endregion

        public MaterialsViewModel()
		{
			this.Model = KanColleClient.Current.Homeport.Materials;

			var fuel = new MaterialViewModel(nameof(Materials.Fuel), Resources.Common_Resources_Fuel).AddTo(this);
			this.Model.Subscribe(fuel.Key, () =>
            {
                fuel.Value = this.Model.Fuel;
                DateTime stopTime = GetMaterialRegenStopTime(nameof(Materials.Fuel));
                TimeSpan timeRemaining = GetMaterialRegenTimeRemaining(nameof(Materials.Fuel));
                fuel.Tooltip = "SoftCap: " + (int)timeRemaining.TotalHours + timeRemaining.ToString(@"\:mm\:ss") +
                               "\n" + stopTime.ToString(@"MM\/dd HH\:mm");
            }).AddTo(this);

			var ammunition = new MaterialViewModel(nameof(Materials.Ammunition), Resources.Common_Resources_Ammo).AddTo(this);
			this.Model.Subscribe(ammunition.Key, () =>
            {
                ammunition.Value = this.Model.Ammunition;
                DateTime stopTime = GetMaterialRegenStopTime(nameof(Materials.Ammunition));
                TimeSpan timeRemaining = GetMaterialRegenTimeRemaining(nameof(Materials.Ammunition));
                ammunition.Tooltip = "SoftCap: " + (int)timeRemaining.TotalHours + timeRemaining.ToString(@"\:mm\:ss") +
                                     "\n" + stopTime.ToString(@"MM\/dd HH\:mm");

            }).AddTo(this);

			var steel = new MaterialViewModel(nameof(Materials.Steel), Resources.Common_Resources_Steel).AddTo(this);
			this.Model.Subscribe(steel.Key, () =>
            {
                steel.Value = this.Model.Steel;
                DateTime stopTime = GetMaterialRegenStopTime(nameof(Materials.Steel));
                TimeSpan timeRemaining = GetMaterialRegenTimeRemaining(nameof(Materials.Steel));
                steel.Tooltip = "SoftCap: " + (int)timeRemaining.TotalHours + timeRemaining.ToString(@"\:mm\:ss") +
                                  "\n" + stopTime.ToString(@"MM\/dd HH\:mm");
            }).AddTo(this);

			var bauxite = new MaterialViewModel(nameof(Materials.Bauxite), Resources.Common_Resources_Bauxite).AddTo(this);
			this.Model.Subscribe(bauxite.Key, () =>
            {
                bauxite.Value = this.Model.Bauxite;
                DateTime stopTime = GetMaterialRegenStopTime(nameof(Materials.Bauxite));
                TimeSpan timeRemaining = GetMaterialRegenTimeRemaining(nameof(Materials.Bauxite));
                bauxite.Tooltip = "SoftCap: " + (int)timeRemaining.TotalHours + timeRemaining.ToString(@"\:mm\:ss") +
                                  "\n" + stopTime.ToString(@"MM\/dd HH\:mm");
            }).AddTo(this);

			var develop = new MaterialViewModel(nameof(Materials.DevelopmentMaterials), Resources.Common_Resources_DevKits).AddTo(this);
			this.Model.Subscribe(develop.Key, () => develop.Value = this.Model.DevelopmentMaterials).AddTo(this);

			var repair = new MaterialViewModel(nameof(Materials.InstantRepairMaterials), Resources.Common_Resources_InstantRepair).AddTo(this);
			this.Model.Subscribe(repair.Key, () => repair.Value = this.Model.InstantRepairMaterials).AddTo(this);

			var build = new MaterialViewModel(nameof(Materials.InstantBuildMaterials), Resources.Common_Resources_InstantBuild).AddTo(this);
			this.Model.Subscribe(build.Key, () => build.Value = this.Model.InstantBuildMaterials).AddTo(this);

			var improvement = new MaterialViewModel(nameof(Materials.ImprovementMaterials), Resources.Common_Resources_Improvement).AddTo(this);
			this.Model.Subscribe(improvement.Key, () => improvement.Value = this.Model.ImprovementMaterials).AddTo(this);

			this.Values = new List<MaterialViewModel>
			{
				fuel,
				ammunition,
				steel,
				bauxite,
				develop,
				repair,
				build,
				improvement,
			};

			this._SelectedItem1 = this.Values.FirstOrDefault(x => x.Key == KanColleSettings.DisplayMaterial1) ?? repair;
			this._SelectedItem2 = this.Values.FirstOrDefault(x => x.Key == KanColleSettings.DisplayMaterial2) ?? build;
            this._SelectedItem2 = this.Values.FirstOrDefault(x => x.Key == KanColleSettings.DisplayMaterial3) ?? develop;
            this._SelectedItem2 = this.Values.FirstOrDefault(x => x.Key == KanColleSettings.DisplayMaterial4) ?? improvement;
        }

        #region GetMaterialRegenStopTime
        public DateTime GetMaterialRegenStopTime(string type)
        {
            return DateTime.Now.Add(GetMaterialRegenTimeRemaining(type));
        }
        #endregion

        #region GetMaterialRegenTimeRemaining
        /// <summary>
        /// Gets the time remaining before material regeneration stops for a given basic material type.
        /// Only accepts Fuel, Ammunition, Steel, and Bauxite.
        /// </summary>
        /// <param name="type">Type of resource.</param>
        /// <returns>TimeSpan remaining before regeneration stops, if invalid type, returns TimeSpan of 0</returns>
        public TimeSpan GetMaterialRegenTimeRemaining(string type)
        {
            int MaterialCap = KanColleClient.Current.Homeport.Admiral.MaxMaterialCount;
            if (!this.GetType().HasProperty(type))
                return new TimeSpan(0, 0, 0);
            else if (!new string[] { nameof(Materials.Fuel), nameof(Materials.Ammunition), nameof(Materials.Steel), nameof(Materials.Bauxite) }.Contains(type))
                return new TimeSpan(0, 0, 0);
            else if ((int)Model.GetPropertyValue(type) >= MaterialCap)
                return new TimeSpan(0, 0, 0);
            else
                return nameof(Materials.Bauxite).Equals(type) ? new TimeSpan(0, (MaterialCap - (int)this.GetPropertyValue(type)) * 3, 0) : new TimeSpan(0, MaterialCap - (int)this.GetPropertyValue(type), 0);
        }
        #endregion

        public class MaterialViewModel : ViewModel
		{
			public string Key { get; }

			public string Display { get; }


            #region Tooltip

            private string _Tooltip;

            public string Tooltip
            {
                get { return this._Tooltip != "" ? this._Tooltip : null; }
                set
                {
                    if(this._Tooltip != value)
                    {
                        this._Tooltip = value;
                        this.RaisePropertyChanged();
                    }
                }
            }

            #endregion


            #region Value 変更通知プロパティ

            private int _Value;

			public int Value
			{
				get { return this._Value; }
				set
				{
					if (this._Value != value)
					{
						this._Value = value;
						this.RaisePropertyChanged();
					}
				}
			}

			#endregion

			public MaterialViewModel(string key, string display, string tooltip = null)
			{
				this.Key = key;
				this.Display = display;
                this.Tooltip = tooltip;
			}
		}
	}
}
