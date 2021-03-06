﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace ClassRoomAPI.Controls
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NotifyPopup : UserControl
    {
        private Popup m_Popup;

        private string m_TextBlockContent;
        private TimeSpan m_ShowTime;

        private NotifyPopup()
        {
            this.InitializeComponent();
            m_Popup = new Popup();
            MeasurePopupSize();
            m_Popup.Child = this;
            this.Loaded += NotifyPopup_Loaded;
            this.Unloaded += NotifyPopup_Unloaded;
        }

        public NotifyPopup(string content, TimeSpan showTime) : this()
        {
            this.m_TextBlockContent = content;
            this.m_ShowTime = showTime;
        }

        public NotifyPopup(string content) : this(content, TimeSpan.FromSeconds(2))
        {
            this.FontFamily = new FontFamily("黑体");
            this.FontSize = 24;
        }

        public void Show()
        {
            this.m_Popup.IsOpen = true;
        }

        public void Close()
        {
            this.m_Popup.IsOpen = false;
        }

        public void SetHorizonAlignment(int Alignment)
        {
            this.Margin = new Thickness(0, -Alignment , 0, 0);

        }
        private void MeasurePopupSize()
        {
            this.Width = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().VisibleBounds.Width;

            double marginTop = 0;
            this.Height = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().VisibleBounds.Height;
            this.Margin = new Thickness(0, marginTop, 0, 0);
        }

        private void NotifyPopup_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbNotify.Text = m_TextBlockContent;
            this.sbOut.BeginTime = this.m_ShowTime;
            this.sbOut.Begin();
            this.sbOut.Completed += SbOut_Completed;
            Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().VisibleBoundsChanged += NotifyPopup_VisibleBoundsChanged;
        }

        private void NotifyPopup_VisibleBoundsChanged(Windows.UI.ViewManagement.ApplicationView sender, object args)
        {
            MeasurePopupSize();
        }

        private void SbOut_Completed(object sender, object e)
        {
            this.m_Popup.IsOpen = false;
        }


        private void NotifyPopup_Unloaded(object sender, RoutedEventArgs e)
        {
            Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().VisibleBoundsChanged -= NotifyPopup_VisibleBoundsChanged;
        }
    }
}
