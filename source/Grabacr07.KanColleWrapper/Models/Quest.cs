﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper.Models.Raw;
using Grabacr07.KanColleWrapper.Models.Translations;
using MetroTrilithon.Mvvm;

namespace Grabacr07.KanColleWrapper.Models
{
	public class Quest : RawDataWrapper<kcsapi_quest>, IIdentifiable
	{
		public int Id => this.RawData.api_no;

		/// <summary>
		/// 任務のカテゴリ (編成、出撃、演習 など) を取得します。
		/// </summary>
		public QuestCategory Category => (QuestCategory)this.RawData.api_category;

		/// <summary>
		/// 任務の種類 (1 回のみ、デイリー、ウィークリー) を取得します。
		/// </summary>
		public QuestType Type => (QuestType)this.RawData.api_type;

		/// <summary>
		/// 任務の状態を取得します。
		/// </summary>
		public QuestState State => (QuestState)this.RawData.api_state;

		/// <summary>
		/// 任務の進捗状況を取得します。
		/// </summary>
		public QuestProgress Progress => (QuestProgress)this.RawData.api_progress_flag;

		/// <summary>
		/// 任務名を取得します。
		/// </summary>
		public string Title => KanColleClient.Current.Translations.Lookup(TranslationType.QuestTitle, this.RawData) ?? this.RawData.api_title;

		/// <summary>
		/// 任務の詳細を取得します。
		/// </summary>
		public string Detail => KanColleClient.Current.Translations.Lookup(TranslationType.QuestDetail, this.RawData) ?? this.RawData.api_detail;


		public Quest(kcsapi_quest rawData) : base(rawData)
		{
			KanColleClient.Current.Translations.Subscribe(x =>
			{
				Debug.WriteLine("Raising property changed event in Quest.");
				this.RaisePropertyChanged("Title");
				this.RaisePropertyChanged("Detail");
			});
		}


		public override string ToString()
		{
			return $"ID = {this.Id}, Category = {this.Category}, Title = \"{this.Title}\", Type = {this.Type}, State = {this.State}";
		}
	}
}
