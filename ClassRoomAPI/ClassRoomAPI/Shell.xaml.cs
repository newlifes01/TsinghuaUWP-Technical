﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Profile;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace ClassRoomAPI
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Shell : Page
    {


        private static List<NavMenuItem> navMenuPrimaryItem = new List<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                    Icon = "\xE80F",
                    Label = "新闻",
                    Selected = Visibility.Visible,
                    DestPage = typeof(MainPage)
                },
                


            });

        private static List<NavMenuItem> navMenuSecondaryItem = new List<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                    Icon = "\xE13D",
                    Label = "登录",
                    Selected = Visibility.Collapsed,
                    DestPage = typeof(MainPage)
                }
            });

        public Shell()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            var bb = AnalyticsInfo.VersionInfo.DeviceFamily;
            if (bb == "Windows.Desktop" || bb == "Windows.Tablet")
            {
                var titleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
                titleBar.BackgroundColor = Colors.Purple;
                titleBar.ButtonHoverBackgroundColor = Colors.Wheat;
                titleBar.ButtonBackgroundColor = Colors.Purple;
            }
            else
            {
                Debug.WriteLine("[MainPage]This version do not support Windows Mobile Any Longer");

            }

            NavMenuPrimaryListView.ItemsSource = navMenuPrimaryItem;
            NavMenuSecondaryListView.ItemsSource = navMenuSecondaryItem;
            // SplitView 开关
            PaneOpenButton.Click += (sender, args) =>
            {
                RootSplitView.IsPaneOpen = !RootSplitView.IsPaneOpen;
            };
            // 导航事件
            NavMenuPrimaryListView.ItemClick += NavMenuListView_ItemClick;
            NavMenuSecondaryListView.ItemClick += NavMenuListView_ItemClick;
            // 默认页


            RootFrame.SourcePageType = typeof(MainPage);

        }





        //UI界面处理函数
        private void NavMenuListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 遍历，将选中Rectangle隐藏
            foreach (var np in navMenuPrimaryItem)
            {
                np.Selected = Visibility.Collapsed;
            }
            foreach (var ns in navMenuSecondaryItem)
            {
                ns.Selected = Visibility.Collapsed;
            }

            NavMenuItem item = e.ClickedItem as NavMenuItem;
            // Rectangle显示并导航
            item.Selected = Visibility.Visible;
            TitleTextBlock.Text = item.Label;
            if (item.DestPage != null)
            {
                RootFrame.Navigate(item.DestPage);
            }

            RootSplitView.IsPaneOpen = false;
        }
    }
}
